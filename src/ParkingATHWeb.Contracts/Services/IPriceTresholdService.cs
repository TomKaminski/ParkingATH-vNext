using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.PriceTreshold;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IPriceTresholdService:IEntityService<PriceTresholdBaseDto,int>, IDependencyService
    {
        ServiceResult<IEnumerable<PriceTresholdAdminDto>> GetAllAdmin();
        ServiceResult<IEnumerable<PriceTresholdAdminDto>> GetAllAdmin(Expression<Func<PriceTresholdAdminDto, bool>> predicate);

        Task<ServiceResult<IEnumerable<PriceTresholdAdminDto>>> GetAllAdminAsync();
        Task<ServiceResult<IEnumerable<PriceTresholdAdminDto>>> GetAllAdminAsync(Expression<Func<PriceTresholdAdminDto, bool>> predicate);

        Task<ServiceResult> RecoverPriceTresholdAsync(int id);

        new Task<ServiceResult<PriceTresholdBaseDto, PrcAdminCreateInfo>> CreateAsync(PriceTresholdBaseDto entity);
        new ServiceResult<PriceTresholdBaseDto, PrcAdminCreateInfo> Create(PriceTresholdBaseDto entity);

    }
}
