using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.Services.Base;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IMessageService: IEntityService<MessageDto, Guid>,IDependencyService
    {
        Task<ServiceResult> SendMessageAsync(EmailType type, UserBaseDto userData, string appBasePath, Dictionary<string, string> additionalParameters = null);
        ServiceResult<string> GetMessageBody(MessageDto message);
        Task<ServiceResult<MessageDto>> GetMessageByTokenId(long id);
    }
}
