using System;
using System.Threading.Tasks;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.PortalMessage;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IPortalMessageService: IEntityService<PortalMessageDto, Guid>,IDependencyService
    {
        Task<ServiceResult<PortalMessageClustersDto>> GetPortalMessageClusterForCurrentUserAsync(int userId);

        Task<ServiceResult> FakeDelete(Guid messageId, int userId);

        Task<ServiceResult> DeleteSingleByAdmin(int userId, Guid messageId);
        Task<ServiceResult> DeleteClusterByAdmin(int userId, Guid messageId);
    }
}
