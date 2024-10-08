using Maxus.Application.DTOs.Image;
using Maxus.Application.Interfaces;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace Maxus.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _configuration;
        private List<string> allowedImageExtensions;
        private string ImagePath;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ImageService(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this._configuration = configuration;
            allowedImageExtensions = _configuration["AllowedImageExtensions"].ToString().Split(',').ToList();
            ImagePath = _configuration["ImageBasePath"];
            this.webHostEnvironment = webHostEnvironment;
           
        }

        public async Task<string> UploadImageAsync(ImageUploadRequest request)
        {
            if (request.File == null)
            {
                throw new ArgumentNullException(nameof(request.File), "No file was uploaded.");
            }

            var fileExtension = Path.GetExtension(request.File.FileName).ToLower();
         
            if (!allowedImageExtensions.Contains(fileExtension))
            {
                throw new InvalidOperationException("Invalid file type. Only image files are allowed.");
            }

            if (!request.File.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Invalid MIME type. Only image files are allowed.");
            }


            var dynamicFolderPath = request.Path; 
            var uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "Image", dynamicFolderPath);

            Directory.CreateDirectory(uploadPath);

            var newFileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(uploadPath, newFileName);

            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await request.File.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while uploading the file.", ex);
            }

            var relativePath = Path.Combine(dynamicFolderPath, newFileName).Replace("\\", "/");
            var imageUrl = new Uri(new Uri(ImagePath), relativePath).ToString();

            return relativePath;
        }
    }


}

