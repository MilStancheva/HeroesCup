using HeroesCup.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;

namespace HeroesCup.Web.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("500")]
        public IActionResult AppError()
        {
            var errorModel = new ErrorViewModel()
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            };
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
            {
               errorModel.Message = "File error thrown";
            }
            if (exceptionHandlerPathFeature?.Path == "/home")
            {
                errorModel.Message += " from home page";
            }

            return View("Error", errorModel);
        }
    }
}
