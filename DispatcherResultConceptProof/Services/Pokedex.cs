using DispatcherResultConceptProof.Dispatchers;
using DispatcherResultConceptProof.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DispatcherResultConceptProof.Services
{
    public interface IPokedexService
    {
        Task<DispatcherResult<Pokemon>> GetPokemonAsync(string nameOrId);
    }

    public class PokedexService : IPokedexService
    {
        private readonly string _baseUrl = "https://pokeapi.co/api/v2/pokemon/";
        private readonly HttpClient _client;

        public PokedexService(HttpClient client)
        {
            _client = client;
        }

        public async Task<DispatcherResult<Pokemon>> GetPokemonAsync(string nameOrId)
        {
            var response = await _client.GetAsync($"{_baseUrl}{nameOrId}");
            var dispatcherResult = new DispatcherResult<Pokemon>();

            if (!response.IsSuccessStatusCode)
            {
                dispatcherResult.ErrorMessages = new List<string> { response.ReasonPhrase };
                return dispatcherResult;
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            var pokemon = JsonConvert.DeserializeObject<Pokemon>(responseContent);

            dispatcherResult.Data = pokemon;
            dispatcherResult.SuccessfulMessages = new List<string> { "Successfully retrieved Pokemon data." };

            return dispatcherResult;
        }
    }
}