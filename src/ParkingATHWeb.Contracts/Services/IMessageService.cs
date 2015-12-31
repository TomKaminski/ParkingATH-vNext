using System;
using System.Threading.Tasks;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IMessageService: IEntityService<MessageDto, Guid>,IDependencyService
    {
        Task<ServiceResult>  SendMessageAsync(MessageDto message, UserBaseDto userData, string appBasePath);
        ServiceResult<string> GetMessageBody(MessageDto message);
        Task<ServiceResult<MessageDto>> GetMessageByTokenId(long id);
    }
}
