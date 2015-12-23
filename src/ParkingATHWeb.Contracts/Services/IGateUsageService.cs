using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.GateUsage;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IGateUsageService:IEntityService<GateUsageBaseDto,Guid>, IDependencyService
    {
        ServiceResult<IEnumerable<GateUsageAdminDto>> GetAllAdmin();
        ServiceResult<IEnumerable<GateUsageAdminDto>> GetAllAdmin(Expression<Func<GateUsageBaseDto, bool>> predicate);
    }
}
