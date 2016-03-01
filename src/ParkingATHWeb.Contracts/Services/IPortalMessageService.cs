using System;
using ParkingATHWeb.Contracts.DTO.SupportMessage;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IPortalMessageService: IEntityService<PortalMessageDto, Guid>,IDependencyService
    {
    }
}
