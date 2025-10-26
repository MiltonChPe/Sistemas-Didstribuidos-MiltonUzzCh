using System.ServiceModel;
using FortniteCosmeticsApi.Infrastructure.Soap.Contracts;
using FortniteCosmeticsApi.Mappers;
using FortniteCosmeticsApi.Models;
using FortniteCosmeticsApi.Exceptions;

namespace FortniteCosmeticsApi.Gateways;

public class CosmeticGateway : ICosmeticGateway
{
    private readonly ICosmeticContract _cosmeticContract;
    private readonly ILogger<CosmeticGateway> _logger;

    public CosmeticGateway(IConfiguration configuration, ILogger<CosmeticGateway> logger)
    {
        var binding = new BasicHttpBinding();
        var endpoint = new EndpointAddress(configuration.GetValue<string>("FortniteService:Url"));
        _cosmeticContract = new ChannelFactory<ICosmeticContract>(binding, endpoint).CreateChannel();
        _logger = logger;
    }

    public async Task<Cosmetic> GetCosmeticByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var cosmetic = await _cosmeticContract.GetCosmeticById(id, cancellationToken);
            return cosmetic.ToModel();
        }
        catch (FaultException ex) when (ex.Message == "Cosmetic Not Found")
        {
            return null; // Retorna null si no se encuentra el cosmético
        }

    }

    public async Task<IList<Cosmetic>> GetCosmeticsByNameAsync(string name, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Datos pal soap");
        var cosmetics = await _cosmeticContract.GetCosmeticsByName(name, cancellationToken);
        return cosmetics.ToModel();
    }

    public async Task<PagedResult<Cosmetic>> GetCosmeticsAsync(string name, string type, int pageSize, int pageNumber, string orderBy, string orderDirection, CancellationToken cancellationToken)
    {
        var queryParameters = new Infrastructure.Soap.Dtos.QueryParameters
        {
            Name = name,
            Type = type,
            PageSize = pageSize,
            PageNumber = pageNumber,
            OrderBy = orderBy,
            OrderDirection = orderDirection
        };

        var paginated = await _cosmeticContract.GetCosmetics(queryParameters, cancellationToken);
        return paginated.ToPagedResult();
    }
    public async Task<Cosmetic> CreateCosmeticAsync(Cosmetic cosmetic, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("sending request to SOAP API, with name: {Name}", cosmetic.Name);
            var createdCosmetic = await _cosmeticContract.CreateCosmetic(cosmetic.ToRequest(), cancellationToken);
            return createdCosmetic.ToModel();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Algo trono en el create cosmetic a soap");
            throw;
        }
    }

    public async Task DeleteCosmeticAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _cosmeticContract.DeleteCosmetic(id, cancellationToken);
        }
        catch (FaultException ex) when (ex.Message == "Cosmetic Not Found")
        {
            throw new Exceptions.CosmeticNotFoundException(id);
        }
    }

    public async Task UpdateCosmeticAsync(Cosmetic cosmetic, CancellationToken cancellationToken)
    {
        try
        {
            await _cosmeticContract.UpdateCosmetic(cosmetic.ToUpdateRequest(), cancellationToken);
        }
        catch (FaultException ex) when (ex.Message == "Cosmetic Not Found")
        {
            throw new CosmeticNotFoundException(cosmetic.Id);
        }
        catch (FaultException ex) when (ex.Message == "Cosmetic with the same Name Already Exist")
        {
            throw new CosmeticAlreadyExistsException(cosmetic.Name);
        }
    }
    

}