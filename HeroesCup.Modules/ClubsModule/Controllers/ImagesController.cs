using ClubsModule.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IImagesService imagesService;

        public ImagesController(IImagesService imagesService)
        {
            this.imagesService = imagesService;
        }

        [Route("images/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetImageById(Guid id)
        {
            var image = await this.imagesService.GetImage(id);
            if (image == null)
            {
                return NotFound();
            }

            Response.Headers.Add("Cache-Control", "max-age=31536000");
            return File(image.Bytes, image.ContentType);
        }
    }
}
