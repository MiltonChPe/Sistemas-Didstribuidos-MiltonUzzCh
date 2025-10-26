namespace FortniteCosmeticsApi.Exceptions;

public class CosmeticNotFoundException : Exception
{
    public CosmeticNotFoundException(Guid id) : base($"cosmetic with ID '{id}' not found")
    {
    }
}