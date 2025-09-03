using System.ServiceModel;
using FortniteApi.Dtos;

namespace FortniteApi.Validators;

public static class CosmeticValidator
{
    public static CreateCosmeticDto ValidateName(this CreateCosmeticDto cosmetic) =>
        string.IsNullOrEmpty(cosmetic.Name) ? throw new FaultException("Cosmetic name is required") : cosmetic;

    public static CreateCosmeticDto ValidateType(this CreateCosmeticDto cosmetic) =>
        string.IsNullOrEmpty(cosmetic.Type) ? throw new FaultException("Cosmetic type is required") : cosmetic;

    public static CreateCosmeticDto ValidateRarity(this CreateCosmeticDto cosmetic) =>
        string.IsNullOrEmpty(cosmetic.Rarity) ? throw new FaultException("Cosmetic rarity is required") : cosmetic;

    public static CreateCosmeticDto ValidatePrice(this CreateCosmeticDto cosmetic) =>
        cosmetic.Price < 0 ? throw new FaultException("Cosmetic price must be greater than or equal to zero") : cosmetic;

    public static CreateCosmeticDto ValidateSeason(this CreateCosmeticDto cosmetic) =>
        string.IsNullOrEmpty(cosmetic.Season) ? throw new FaultException("Cosmetic season is required") : cosmetic;

    public static CreateCosmeticDto ValidateSource(this CreateCosmeticDto cosmetic) =>
        string.IsNullOrEmpty(cosmetic.Source) ? throw new FaultException("Cosmetic source is required") : cosmetic;
        
    
}