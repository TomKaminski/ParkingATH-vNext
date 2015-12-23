using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using ParkingATHWeb.Business.Services.Base;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.GateUsage;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.Business.Services
{
    public class GateUsageService : EntityService<GateUsageBaseDto, GateUsage, Guid>, IGateUsageService
    {
        private readonly IGateUsageRepository _repository;

        public GateUsageService(IUnitOfWork unitOfWork, IGateUsageRepository repository)
            : base(repository, unitOfWork)
        {
            _repository = repository;
        }

        public ServiceResult<IEnumerable<GateUsageAdminDto>> GetAllAdmin()
        {
            return ServiceResult<IEnumerable<GateUsageAdminDto>>
                .Success(_repository.Include(x=>x.User)
                .Select(Mapper.Map<GateUsageAdminDto>));
        }

        public ServiceResult<IEnumerable<GateUsageAdminDto>> GetAllAdmin(Expression<Func<GateUsageBaseDto, bool>> predicate)
        {
            return ServiceResult<IEnumerable<GateUsageAdminDto>>
                .Success(_repository.Include(x=>x.User).Where(MapExpressionToEntity(predicate))
                .Select(Mapper.Map<GateUsageAdminDto>));
        }
    }
}
