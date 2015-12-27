using System;
using System.Threading.Tasks;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IMessageService: IEntityService<MessageDto, Guid>,IDependencyService
    {
        Task SendMessageAsync(MessageDto message, UserBaseDto userData);
    }
}
