using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
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
            var allAsync = await base.GetAllAsync(x=>!x.IsDeleted);
            if (!allAsync.Result.Any())
            {
                _repository.Add(new PriceTreshold
                {
                    MinCharges = 0,
                    PricePerCharge = 3
                });
                await _unitOfWork.CommitAsync();
            }
            return ServiceResult<IEnumerable<PriceTresholdBaseDto>>.Success(_repository.GetAll(x=>!x.IsDeleted).OrderBy(x => x.MinCharges).Select(_mapper.Map<PriceTresholdBaseDto>));
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

        public override async Task<ServiceResult> DeleteAsync(int id)
        {
            var obj = await _repository.FindAsync(id);
            obj.IsDeleted = true;
            _repository.Edit(obj);
            await _unitOfWork.CommitAsync();
            return ServiceResult.Success();
        }

        public override ServiceResult Delete(int id)
        {
            var obj = _repository.Find(id);
            obj.IsDeleted = true;
            _repository.Edit(obj);
            _unitOfWork.Commit();
            return ServiceResult.Success();
        }
    }
}
