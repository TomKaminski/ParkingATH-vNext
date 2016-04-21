using System.Threading.Tasks;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.Payments;
using ParkingATHWeb.Contracts.Services.Base;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Contracts.Services.Payments
{
    public interface IPayuService : IDependencyService
    {
        Task<ServiceResult<PaymentResponse>> ProcessPaymentAsync(PaymentRequest request, int userId, OrderPlace orderPlace);
        Task<ServiceResult<PaymentCardResponse>> ProcessCardPaymentAsync(PaymentCardRequest request, int userId, OrderPlace orderPlace);

    }
}
