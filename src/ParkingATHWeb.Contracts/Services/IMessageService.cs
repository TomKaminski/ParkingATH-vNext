using System;
using System.Collections.Generic;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IMessageService: IEntityService<MessageDto, Guid>,IDependencyService
    {
        void SendMessage(MessageDto message, Dictionary<string, string> parameters);
    }
}
