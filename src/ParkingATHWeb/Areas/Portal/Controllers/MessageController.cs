using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Areas.Portal.Controllers.Base;
using ParkingATHWeb.Areas.Portal.ViewModels.Message;
using ParkingATHWeb.Contracts.Services;

namespace ParkingATHWeb.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("[area]")]
    public class MessageController : BaseController
    {
        private readonly IMessageService _messageService;
        private readonly ITokenService _tokenService;

        public MessageController(IMessageService messageService, ITokenService tokenService)
        {
            _messageService = messageService;
            _tokenService = tokenService;
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> Display(string id)
        {
            var decodedToken = _tokenService.GetDecryptedData(id);
            var tokenData = await _tokenService.GetTokenBySecureTokenAndTypeAsync(decodedToken.Result.SecureToken, decodedToken.Result.TokenType);
            var message = await _messageService.GetMessageByTokenId(tokenData.Result.Id);
            var emailBody = _messageService.GetMessageBody(message).Result;

            return View(new DisplayMessageViewModel
            {
                EmailHtml = emailBody,
                Title = message.Title
            });
        }
    }
}
