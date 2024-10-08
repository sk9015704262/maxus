using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.Image
{
    public class ImageUploadRequest
    {
        [Required]
        public string? Path { get; set; }

        [Required]
        public IFormFile? File { get; set; }

    }
}
