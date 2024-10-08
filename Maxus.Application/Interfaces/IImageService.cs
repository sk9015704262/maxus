using Maxus.Application.DTOs.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(ImageUploadRequest request);
    }
}
