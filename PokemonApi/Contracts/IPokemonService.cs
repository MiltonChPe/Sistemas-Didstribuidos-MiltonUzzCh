using System.ServiceModel;
using PokemonApi.Dtos;
namespace PokemonApi.Services;

[ServiceContract(Name = "PokemonService", Namespace = "http://pokemon-api/pokemon-service")]
public interface IPokemonService
{
    //metodos que se van a implementar en la clase PokemonService
    [OperationContract]
    Task<PokemonResponseDto> CreatePokemon(CreatePokemonDto pokemon, CancellationToken cancellationToken);
}