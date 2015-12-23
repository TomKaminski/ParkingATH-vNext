using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.PriceTreshold;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IPriceTresholdService:IEntityService<PriceTresholdBaseDto,int>, IDependencyService
    {
        ServiceResult<IEnumerable<PriceTresholdAdminDto>> GetAllAdmin();
        ServiceResult<IEnumerable<PriceTresholdAdminDto>> GetAllAdmin(Expression<Func<PriceTresholdAdminDto, bool>> predicate);
    }
}
