﻿using System.ComponentModel.DataAnnotations;

namespace Maxus.Application.DTOs.Client
{
    public class DeleteClientReqest
    {
        [Required]
        public int Id { get; set; }
    }
}
