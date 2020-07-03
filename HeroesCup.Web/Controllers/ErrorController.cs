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
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier ,
                Message = "Sorry, an error occurred while executing your request."
            };
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
            {
               errorModel.Reason = "File error thrown";
            }
            else
            {
                errorModel.Reason = "Internal server error";
            }

            return View("Error", errorModel);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("404")]
        public IActionResult PageNotFound()
        {
            var errorModel = new ErrorViewModel()
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Reason = "404 - Page not found",
                Message = "Oops, better check that URL."
            };

            return View("Error", errorModel);
        }
    }
}
