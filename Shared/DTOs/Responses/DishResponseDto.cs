using System;
using At.luki0606.FleduSnack.Shared.Enums;

namespace At.luki0606.FleduSnack.Shared.DTOs.Responses
{
    public record DishResponseDto(Guid Id, string Brand, string Flavor, Tasting Tasting, string? ImageUrl);
}
