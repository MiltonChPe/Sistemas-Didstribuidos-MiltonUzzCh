using FortniteCosmeticsApi.Dtos;
using FortniteCosmeticsApi.Infrastructure.Soap.Dtos;
using FortniteCosmeticsApi.Models;

namespace FortniteCosmeticsApi.Mappers;

public static class CosmeticMapper
{
    public static Cosmetic ToModel(this CosmeticResponseDto cosmeticResponseDto)
    {
        return new Cosmetic
        {
            Id = cosmeticResponseDto.Id,
            Name = cosmeticResponseDto.Name,
            Type = cosmeticResponseDto.Type,
            Rarity = cosmeticResponseDto.Rarity,
            Price = cosmeticResponseDto.Price,
            Season = cosmeticResponseDto.Season,
            Source = cosmeticResponseDto.Source
        };
    }

    public static CosmeticResponse ToResponse(this Cosmetic cosmetic)
    {
        return new CosmeticResponse
        {
            Id = cosmetic.Id,
            Name = cosmetic.Name,
            Type = cosmetic.Type,
            Rarity = cosmetic.Rarity
        };
    }
}