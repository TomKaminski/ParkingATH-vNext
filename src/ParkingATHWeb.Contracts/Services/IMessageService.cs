using System;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IMessageService: IEntityService<MessageDto, Guid>,IDependencyService
    {
        void SendMessage(MessageDto message);
    }
}
