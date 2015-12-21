using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.Order;
using ParkingATHWeb.Contracts.Services.Base;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IOrderService : IEntityService<OrderBaseDto>, IDependencyService
    {
        Task<ServiceResult<string>> GenerateExternalOrderIdAsync();
        Task<ServiceResult<OrderBaseDto>> GetAsync(string externalorderId);

        ServiceResult<IEnumerable<OrderAdminDto>> GetAllAdmin();
        ServiceResult<IEnumerable<OrderAdminDto>> GetAllAdmin(Expression<Func<OrderAdminDto, bool>> predicate);

        Task<ServiceResult<OrderStatus>> UpdateOrderState(string status, string extOrderId);
    }
}
