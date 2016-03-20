using System.Threading.Tasks;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.Payments;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services.Payments
{
    public interface IPaymentAuthorizeService : IDependencyService
    {
        Task<ServiceResult<PaymentAuthorizationResponse>> GetAuthorizeTokenAsync();
    }
}
