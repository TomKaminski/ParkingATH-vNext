using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.Order;
using ParkingATHWeb.Contracts.DTO.Payments;
using ParkingATHWeb.Contracts.Services.Base;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IOrderService : IEntityService<OrderBaseDto,long>, IDependencyService
    {
        Task<ServiceResult<Guid>> GenerateExternalOrderIdAsync();
        Task<ServiceResult<OrderBaseDto>> GetAsync(Guid externalorderId);

        ServiceResult<IEnumerable<OrderAdminDto>> GetAllAdmin();
        ServiceResult<IEnumerable<OrderAdminDto>> GetAllAdmin(Expression<Func<OrderAdminDto, bool>> predicate);

        Task<ServiceResult<IEnumerable<OrderAdminDto>>> GetAllAdminAsync();
        Task<ServiceResult<IEnumerable<OrderAdminDto>>> GetAllAdminAsync(Expression<Func<OrderAdminDto, bool>> predicate);

        Task<ServiceResult<OrderStatus>> UpdateOrderState(PaymentNotification model);
    }
}
