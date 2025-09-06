using FortniteApi.Dtos;
using FortniteApi.Infrastructure.Entities;
using FortniteApi.Models;
using Humanizer;
namespace FortniteApi.Mappers;

public static class CosmeticMapper
{
    public static Cosmetic ToModel(this FortniteEntity fortniteEntity)
    {
        if (fortniteEntity == null) return null;

        return new Cosmetic
        {
            Id = fortniteEntity.Id,
            Name = fortniteEntity.Name,
            Type = fortniteEntity.Type,
            Rarity = fortniteEntity.Rarity,
            Price = fortniteEntity.Price,
            Season = fortniteEntity.Season,
            Source = fortniteEntity.Source
        };
    }

    public static FortniteEntity ToEntity(this Cosmetic cosmetic)
    {
        if (cosmetic == null) return null;

        return new FortniteEntity
        {
            Id = cosmetic.Id,
            Name = cosmetic.Name,
            Type = cosmetic.Type,
            Rarity = cosmetic.Rarity,
            Price = cosmetic.Price,
            Season = cosmetic.Season,
            Source = cosmetic.Source
        };
    }

    public static Cosmetic ToModel(this CreateCosmeticDto requestCosmeticDto)
    {
        return new Cosmetic
        {
            Name = requestCosmeticDto.Name,
            Type = requestCosmeticDto.Type,
            Rarity = requestCosmeticDto.Rarity,
            Price = requestCosmeticDto.Price,
            Season = requestCosmeticDto.Season,
            Source = requestCosmeticDto.Source
        };
    }

    public static CosmeticResponseDto ToResponseDto(this Cosmetic cosmetic)
    {
        return new CosmeticResponseDto
        {
            Id = cosmetic.Id,
            Name = cosmetic.Name,
            Type = cosmetic.Type,
            Rarity = cosmetic.Rarity,
            Price = cosmetic.Price,
            Season = cosmetic.Season,
            Source = cosmetic.Source
        };
    }

    public static IList<CosmeticResponseDto> ToResponseDto(this IReadOnlyList<Cosmetic> cosmetics)
    {
        return cosmetics.Select(c => c.ToResponseDto()).ToList();
    }
    
    public static IReadOnlyList<Cosmetic> ToModel(this IReadOnlyList<FortniteEntity> cosmetics)
    {
        return cosmetics.Select(c => c.ToModel()).ToList();
    }

}