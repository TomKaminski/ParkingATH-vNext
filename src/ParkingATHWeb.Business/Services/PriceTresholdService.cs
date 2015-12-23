using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class PriceTresholdService:EntityService<PriceTresholdBaseDto,PriceTreshold, int>, IPriceTresholdService
    {
        private readonly IPriceTresholdRepository _repository;

        public PriceTresholdService(IUnitOfWork unitOfWork, IPriceTresholdRepository repository) : base(repository, unitOfWork)
        {
            _repository = repository;
        }

        public ServiceResult<IEnumerable<PriceTresholdAdminDto>> GetAllAdmin()
        {
            return ServiceResult<IEnumerable<PriceTresholdAdminDto>>
                .Success(_repository.Include(x=>x.Orders)
                .Select(Mapper.Map<PriceTresholdAdminDto>));
        }

        public ServiceResult<IEnumerable<PriceTresholdAdminDto>> GetAllAdmin(Expression<Func<PriceTresholdAdminDto, bool>> predicate)
        {
            var param = Expression.Parameter(typeof(PriceTreshold));
            var result = new CustomExpressionVisitor<PriceTreshold>(param).Visit(predicate.Body);
            var lambda = Expression.Lambda<Func<PriceTreshold, bool>>(result, param);
            return ServiceResult<IEnumerable<PriceTresholdAdminDto>>
                .Success( _repository.Include(x=>x.Orders).Where(lambda)
                .Select(Mapper.Map<PriceTresholdAdminDto>));
        }
    }
}
