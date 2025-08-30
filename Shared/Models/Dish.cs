using System;
using System.ComponentModel.DataAnnotations;
using At.luki0606.FleduSnack.Shared.DTOs.Responses;
using At.luki0606.FleduSnack.Shared.Enums;
using At.luki0606.FleduSnack.Shared.Interfaces;

namespace At.luki0606.FleduSnack.Shared.Models
{
    public class Dish : IDto<DishResponseDto>
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string Brand { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Flavor { get; set; } = string.Empty;

        [Required]
        public Tasting Tasting { get; set; } = Tasting.NotReviewed;

        [MaxLength(500)]
        public string? PhotoPath { get; set; }

        public Guid CatId { get; set; }

        public DishResponseDto ToResponseDto()
        {
            return new(Id, Brand, Flavor, Tasting);
        }
    }
}
