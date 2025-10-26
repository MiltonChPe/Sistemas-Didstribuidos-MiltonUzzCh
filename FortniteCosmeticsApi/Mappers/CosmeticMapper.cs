using FortniteCosmeticsApi.Dtos;
using FortniteCosmeticsApi.Infrastructure.Soap.Dtos;
using FortniteCosmeticsApi.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

    public static Cosmetic ToModel(this UpdateCosmeticRequest cosmetic, Guid id)
    {
        return new Cosmetic
        {
            Id = id,
            Name = cosmetic.Name,
            Type = cosmetic.Type,
            Rarity = cosmetic.Rarity,
        };
    }

    public static Cosmetic ToModel(this CreateCosmeticRequest createCosmeticRequest)
    {
        return new Cosmetic
        {
            Name = createCosmeticRequest.Name,
            Type = createCosmeticRequest.Type,
            Rarity = createCosmeticRequest.Rarity,
            Price = createCosmeticRequest.Price,
            Season = createCosmeticRequest.Season,
            Source = createCosmeticRequest.Source
        };
    }
    public static IList<Cosmetic> ToModel(this IList<CosmeticResponseDto> cosmeticResponseDtos)
    {
        return cosmeticResponseDtos.Select(s => s.ToModel()).ToList();
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

    public static IList<CosmeticResponse> ToResponse(this IList<Cosmetic> cosmetics)
    {
        return cosmetics.Select(s => s.ToResponse()).ToList();
    }

    public static PagedCosmeticResponse ToPagedResponse(this PagedResult<Cosmetic> pagedResult)
    {
        return new PagedCosmeticResponse
        {
            PageNumber = pagedResult.PageNumber,
            PageSize = pagedResult.PageSize,
            TotalRecords = pagedResult.TotalRecords,
            TotalPages = pagedResult.TotalPages,
            Data = pagedResult.Data.ToResponse()
        };
    }
    public static CreateCosmeticDto ToRequest(this Cosmetic cosmetic)
    {
        return new CreateCosmeticDto
        {
            Name = cosmetic.Name,
            Type = cosmetic.Type,
            Rarity = cosmetic.Rarity,
            Price = cosmetic.Price,
            Season = cosmetic.Season,
            Source = cosmetic.Source
        };
    }

    public static PagedResult<Cosmetic> ToPagedResult(this PagedCosmeticResponseDto pagedDto)
    {
        if (pagedDto == null)
        {
            return new PagedResult<Cosmetic>
            {   
                TotalRecords = 0,
                PageNumber = 1,
                PageSize = 0,
                Data = new List<Cosmetic>()
            };
        }
        return new PagedResult<Cosmetic>
        {
            PageNumber = pagedDto.PageNumber,
            PageSize = pagedDto.PageSize,
            TotalRecords = pagedDto.TotalRecords,
            Data = pagedDto.Data.ToModel()
        };
    }

    public static UpdateCosmeticDto ToUpdateRequest(this Cosmetic cosmetic)
    {
        return new UpdateCosmeticDto
        {
            Id = cosmetic.Id,
            Name = cosmetic.Name,
            Type = cosmetic.Type,
            Rarity = cosmetic.Rarity,
        };
    }
}

