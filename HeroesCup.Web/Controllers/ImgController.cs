using ClubsModule.Services.Contracts;
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

        [Route("img/{filename}")]
        [HttpGet]
        public async Task<IActionResult> GetImageByFileName(string filename)
        {
            var image = await this.imagesService.GetImageByFileName(filename);
            if (image == null)
            {
                return NotFound();
            }
            
            Response.Headers.Add("Cache-Control", "max-age=31536000");
            return File(image.Bytes, image.ContentType);
        }
    }
}
