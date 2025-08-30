using System;
using System.ComponentModel.DataAnnotations;
using At.luki0606.FleduSnack.Shared.DTOs.Responses;
using At.luki0606.FleduSnack.Shared.Interfaces;

namespace At.luki0606.FleduSnack.Shared.Models
{
    public class Cat : IDto<CatResponseDto>
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public CatResponseDto ToResponseDto()
        {
            return new(Id, Name);
        }
    }
}
