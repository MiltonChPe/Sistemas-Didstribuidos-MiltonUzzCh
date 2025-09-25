using System.ServiceModel;
using PokedexApi.Infrastructure.Soap.Dtos;
namespace PokedexApi.Infrastructure.Soap.Contracts;

[ServiceContract(Name = "PokemonService", Namespace = "http://pokemon-api/pokemon-service")]
public interface IPokemonContract
{
    //metodos que se van a implementar en la clase PokemonService
    [OperationContract]
    Task<PokemonResponseDto> CreatePokemon(CreatePokemonDto pokemon, CancellationToken cancellationToken);

    [OperationContract]
    Task<PokemonResponseDto> GetPokemonById(Guid id, CancellationToken cancellationToken);

    [OperationContract]
    Task<IList<PokemonResponseDto>> GetPokemonsByName(string name, CancellationToken cancellationToken);

    [OperationContract]
    Task<DeletePokemonResponseDto> DeletePokemon(Guid id, CancellationToken cancellationToken);

    [OperationContract]
    Task<PokemonResponseDto> UpdatePokemon(UpdatePokemonDto pokemon, CancellationToken cancellationToken);

   
    [OperationContract]
    Task<PagedPokemonResponseDto> GetPokemons(QueryParameters queryParameters, CancellationToken cancellationToken);

}