using System;
using System.ComponentModel.DataAnnotations;
using At.luki0606.FleduSnack.Shared.Enums;

namespace At.luki0606.FleduSnack.Shared.Models
{
    public class Dish
    {
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string Brand { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Flavor { get; set; } = string.Empty;

        [Required]
        public Tasting Tasting { get; set; } = Tasting.NotReviewed;

        [MaxLength(500)]
        public string? PhotoPath { get; set; }

        public Guid CatId { get; set; }
    }
}
