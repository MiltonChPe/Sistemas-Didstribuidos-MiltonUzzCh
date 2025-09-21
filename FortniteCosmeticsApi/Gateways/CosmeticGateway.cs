using System.ServiceModel;
using FortniteCosmeticsApi.Infrastructure.Soap.Contracts;
using FortniteCosmeticsApi.Mappers;
using FortniteCosmeticsApi.Models;

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
            return null;
        }
       
    }
}