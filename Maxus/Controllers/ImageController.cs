using AccountingAPI.Responses;
using Maxus.Application.DTOs.Image;
using Maxus.Application.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
  

    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> ImageUpload([FromForm] ImageUploadRequest requestObj)
        {
            try
            {
                var Image = await imageService.UploadImageAsync(requestObj);
                if (Image != null)
                {
                    return Ok(new ApiResponse<object>(Image, "Image Upload successfully."));
                }
                return BadRequest(new { Message = "Error creating." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

    }
}
