using System.ComponentModel.DataAnnotations;
using At.luki0606.FleduSnack.Shared.Enums;

namespace At.luki0606.FleduSnack.Shared.DTOs.Requests
{
    public class DishRequestDto
    {
        [Required, MaxLength(100)]
        public string Brand { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Flavor { get; set; } = string.Empty;

        [Required]
        public Tasting Tasting { get; set; }
    }
}
