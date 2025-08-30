using System;

namespace At.luki0606.FleduSnack.Shared.DTOs.Responses
{
    public record DishResponseDto(Guid Id, string Brand, string Flavor, Enums.Tasting Tasting);
}
