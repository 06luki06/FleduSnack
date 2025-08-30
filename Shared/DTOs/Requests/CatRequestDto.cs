using System.ComponentModel.DataAnnotations;

namespace At.luki0606.FleduSnack.Shared.DTOs.Requests
{
    public class CatRequestDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public CatRequestDto(string name)
        {
            Name = name;
        }
    }
}
