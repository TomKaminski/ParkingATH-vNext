using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.Areas.Portal.Controllers.Base;
using ParkingATHWeb.Areas.Portal.ViewModels.Message;
using ParkingATHWeb.Contracts.DTO.SupportMessage;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.Infrastructure.Attributes;
using ParkingATHWeb.Infrastructure.Extensions;

namespace ParkingATHWeb.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("[area]/Wiadomosci")]
    [Authorize]
    public class MessageController : BaseController
    {
        private readonly IMessageService _messageService;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IPortalMessageService _portalMessageService;
        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService, ITokenService tokenService, IUserService userService, IPortalMessageService portalMessageService, IMapper mapper)
        {
            _messageService = messageService;
            _tokenService = tokenService;
            _userService = userService;
            _portalMessageService = portalMessageService;
            _mapper = mapper;
        }

        [Route("")]
        public IActionResult Index()
        {
            return PartialView();
        }

        [AllowAnonymous]
        [Route("Podglad")]
        public async Task<IActionResult> Display(string id)
        {
            var decodedToken = _tokenService.GetDecryptedData(id);
            var tokenData = await _tokenService.GetTokenBySecureTokenAndTypeAsync(decodedToken.Result.SecureToken, decodedToken.Result.TokenType);
            var message = await _messageService.GetMessageByTokenId(tokenData.Result.Id);
            var emailBody = _messageService.GetMessageBody(message.Result).Result;

            return View(new DisplayMessageViewModel
            {
                EmailHtml = emailBody,
                Title = message.Result.Title
            });
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryTokenFromHeader]
        public async Task<IActionResult> SendQuickMessage([FromBody] QuickMessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Cant be null coz user is logged in
                model.UserId = CurrentUser.UserId.Value;

                var adminAccountResult = await _userService.GetAdminAccountIdAsync();
                if (!adminAccountResult.IsValid)
                {
                    model.AppendErrors(adminAccountResult.ValidationErrors);
                }
                else
                {
                    model.ReceiverUserId = adminAccountResult.Result;
                    var serviceRequest = _mapper.Map<PortalMessageDto>(model);
                    serviceRequest.Starter = true;
                    var createResult = await _portalMessageService.CreateAsync(serviceRequest);
                    if (createResult.IsValid)
                    {
                        model.AppendNotifications("Wiadomość wysłana, dziękujemy za wsparcie :)");
                    }
                    else
                    {
                        model.AppendErrors(createResult.ValidationErrors);
                    }
                }
            }
            model.AppendErrors(GetModelStateErrors(ModelState));
            return Json(model);
        }
    }
}
