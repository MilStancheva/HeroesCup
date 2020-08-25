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

            return File(image.Bytes, image.ContentType);
        }
    }
}
