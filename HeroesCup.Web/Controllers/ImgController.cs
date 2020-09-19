using ClubsModule.Services.Contracts;
using HeroesCup.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HeroesCup.Web.Controllers
{

    public class ImgController : Controller
    {
        private readonly IImagesService imagesService;

        public ImgController(IImagesService imagesService)
        {
            this.imagesService = imagesService;
        }

        [Route("img/{filenameOrId}")]
        [HttpGet]
        public async Task<IActionResult> GetImageByFileName(string filenameOrId)
        {
            if (filenameOrId == null)
            {
                return NotFound();
            }

            Guid imageId;
            bool isValidId = Guid.TryParse(filenameOrId, out imageId);
            Image image = null;

            if (!isValidId)
            {
                image = await this.imagesService.GetImageByFileName(filenameOrId);
            }
            else
            {
                image = await this.imagesService.GetImage(imageId);                
            }

            if (image == null)
            {
                return NotFound();
            }

            Response.Headers.Add("Cache-Control", "max-age=31536000");
            return File(image.Bytes, image.ContentType);
        }
    }
}
