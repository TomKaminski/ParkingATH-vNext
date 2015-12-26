using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IMessageService: IDependencyService
    {
        void SendMessage(MessageDto message);
    }
}
