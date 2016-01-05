using System;
using ParkingATHWeb.Areas.Admin.Controllers.Base;
using ParkingATHWeb.Areas.Admin.ViewModels.Message;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Areas.Admin.Controllers
{
    public class AdminMessageController : AdminServiceBaseController<AdminMessageListItemViewModel, MessageDto, Guid>
    {
        public AdminMessageController(IEntityService<MessageDto, Guid> entityService) : base(entityService)
        {
        }
    }
}
