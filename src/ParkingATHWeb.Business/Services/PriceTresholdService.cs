using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Data.Entity;
using ParkingATHWeb.Business.Services.Base;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.PriceTreshold;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Business.Services
{
    public class PriceTresholdService : EntityService<PriceTresholdBaseDto, PriceTreshold, int>, IPriceTresholdService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPriceTresholdRepository _repository;
        private readonly IMapper _mapper;

        public PriceTresholdService(IUnitOfWork unitOfWork, IPriceTresholdRepository repository, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<ServiceResult<IEnumerable<PriceTresholdBaseDto>>> GetAllAsync()
        {
            var count = Count(x => !x.IsDeleted);
            if (count.Result == 0)
            {
                _repository.Add(new PriceTreshold
                {
                    MinCharges = 0,
                    PricePerCharge = 3
                });
                await _unitOfWork.CommitAsync();
            }
            return ServiceResult<IEnumerable<PriceTresholdBaseDto>>.Success(_repository.GetAll(x => !x.IsDeleted).OrderBy(x => x.MinCharges).Select(_mapper.Map<PriceTresholdBaseDto>));
        }

        public ServiceResult<IEnumerable<PriceTresholdAdminDto>> GetAllAdmin()
        {
            return ServiceResult<IEnumerable<PriceTresholdAdminDto>>
                .Success(_repository.Include(x => x.Orders)
                .Select(_mapper.Map<PriceTresholdAdminDto>));
        }

        public ServiceResult<IEnumerable<PriceTresholdAdminDto>> GetAllAdmin(Expression<Func<PriceTresholdAdminDto, bool>> predicate)
        {
            var param = Expression.Parameter(typeof(PriceTreshold));
            var result = new CustomExpressionVisitor<PriceTreshold>(param).Visit(predicate.Body);
            var lambda = Expression.Lambda<Func<PriceTreshold, bool>>(result, param);
            return ServiceResult<IEnumerable<PriceTresholdAdminDto>>
                .Success(_repository.Include(x => x.Orders).Where(lambda)
                .Select(_mapper.Map<PriceTresholdAdminDto>));
        }

        public async Task<ServiceResult<IEnumerable<PriceTresholdAdminDto>>> GetAllAdminAsync()
        {
            return ServiceResult<IEnumerable<PriceTresholdAdminDto>>
                .Success(
                    (await _repository.Include(x => x.Orders).ToListAsync()).Select(_mapper.Map<PriceTresholdAdminDto>));
        }

        public async Task<ServiceResult<IEnumerable<PriceTresholdAdminDto>>> GetAllAdminAsync(Expression<Func<PriceTresholdAdminDto, bool>> predicate)
        {
            var param = Expression.Parameter(typeof(PriceTreshold));
            var result = new CustomExpressionVisitor<PriceTreshold>(param).Visit(predicate.Body);
            var lambda = Expression.Lambda<Func<PriceTreshold, bool>>(result, param);

            var count = Count(x => !x.IsDeleted);
            if (count.Result == 0)
            {
                _repository.Add(new PriceTreshold
                {
                    MinCharges = 0,
                    PricePerCharge = 3
                });
                await _unitOfWork.CommitAsync();
            }

            return ServiceResult<IEnumerable<PriceTresholdAdminDto>>
                .Success((await _repository.Include(x => x.Orders).Where(lambda).ToListAsync()).Select(_mapper.Map<PriceTresholdAdminDto>));
        }

        public override async Task<ServiceResult> DeleteAsync(int id)
        {
            var obj = await _repository.FindAsync(id);
            if (obj.MinCharges == 0)
            {
                return ServiceResult.Failure("Nie można usunąć bazowego przedziału!");
            }
            obj.IsDeleted = true;
            _repository.Edit(obj);
            await _unitOfWork.CommitAsync();
            return ServiceResult.Success();
        }

        public override ServiceResult Delete(int id)
        {
            var obj = _repository.Find(id);
            if (obj.MinCharges == 0)
            {
                return ServiceResult.Failure("Nie można usunąć bazowego przedziału!");
            }
            obj.IsDeleted = true;
            _repository.Edit(obj);
            _unitOfWork.Commit();
            return ServiceResult.Success();
        }

        public override async Task<ServiceResult<PriceTresholdBaseDto>> EditAsync(PriceTresholdBaseDto entity)
        {
            var conflictingItem =
                await _repository.FirstOrDefaultAsync(
                    x =>
                        x.IsDeleted != true && x.Id != entity.Id &&
                        (x.MinCharges == entity.MinCharges || x.PricePerCharge == entity.PricePerCharge));

            if (conflictingItem != null)
            {
                return
                    ServiceResult<PriceTresholdBaseDto>.Failure(
                        $"Podane wartości kolidują z już istniejącym przedziałem (min. wyjazdy: {conflictingItem.MinCharges}, cena za szt.: {conflictingItem.PricePerCharge.ToString("##.00")})");
            }

            var defaultPrc = await _repository.FirstOrDefaultAsync(x => x.MinCharges == 0);
            if (defaultPrc.Id == entity.Id && entity.MinCharges != 0)
            {
                return ServiceResult<PriceTresholdBaseDto>.Failure("Nie można edytować minimalnej ilości wyjazdów dla bazowego przedziału!");
            }
            return await base.EditAsync(entity);
        }

        public override ServiceResult Edit(PriceTresholdBaseDto entity)
        {
            var conflictingItem =
                 _repository.FirstOrDefault(
                    x =>
                        x.IsDeleted != true && x.Id != entity.Id &&
                        (x.MinCharges == entity.MinCharges || x.PricePerCharge == entity.PricePerCharge));

            if (conflictingItem != null)
            {
                return
                    ServiceResult<PriceTresholdBaseDto>.Failure(
                        $"Podane wartości kolidują z już istniejącym przedziałem (min. wyjazdy: {conflictingItem.MinCharges}, cena za szt.: {conflictingItem.PricePerCharge.ToString("##.00")})");
            }
            var defaultPrc = _repository.FirstOrDefault(x => x.MinCharges == 0);
            if (defaultPrc.Id == entity.Id && entity.MinCharges != 0)
            {
                return ServiceResult<PriceTresholdBaseDto>.Failure("Nie można edytować minimalnej ilości wyjazdów dla bazowego przedziału!");
            }
            return base.Edit(entity);
        }

   
    }
}
