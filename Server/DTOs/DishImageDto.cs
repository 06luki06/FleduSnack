using System.ComponentModel.DataAnnotations;
using At.luki0606.FleduSnack.Shared.Enums;
using Microsoft.AspNetCore.Http;

namespace At.Luki0606.FleduSnack.Server.DTOs
{
    public class DishImageDto
    {
        [Required, MaxLength(100)]
        public string Brand { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Flavor { get; set; } = string.Empty;

        [Required]
        public Tasting Tasting { get; set; } = Tasting.NotReviewed;

        public IFormFile? Image { get; set; }
    }
}
