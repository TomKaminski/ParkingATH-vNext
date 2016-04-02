using System.Net;
using Microsoft.AspNet.Mvc;

namespace ParkingATHWeb.Controllers
{
    [Route("Error")]
    public class ErrorController : Controller
    {
        [Route("Status/{statusCode}/{pathBase}")]
        public IActionResult StatusCode(int statusCode, string pathBase)
        {
            switch (statusCode)
            {
                case (int)HttpStatusCode.NotFound:
                    return RedirectToAction("Index", "Home", new { area = "Portal" , pathBase});

                case (int)HttpStatusCode.InternalServerError:
                default:
                    return RedirectToAction("UnexpectedError");
            }
        }

        [Route("404NotFound")]
        public IActionResult NotFound()
        {
            return View();
        }

        [Route("UnexpectedError")]
        public IActionResult UnexpectedError()
        {
            return View();
        }
    }
}
