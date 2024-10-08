using System.ComponentModel.DataAnnotations;

namespace Maxus.Application.DTOs.Client
{
    public class GetClientReqest
    {
        [Required]
        public int Id { get; set; }
    }
}
