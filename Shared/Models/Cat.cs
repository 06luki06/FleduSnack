using System;
using System.ComponentModel.DataAnnotations;

namespace At.luki0606.FleduSnack.Shared.Models
{
    public class Cat
    {
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
