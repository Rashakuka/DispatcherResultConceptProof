using DispatcherResultConceptProof.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DispatcherResultConceptProof
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                 .AddSingleton<HttpClient>() 
                 .AddSingleton<IPokedexService, PokedexService>()
                 .BuildServiceProvider();

            var pokedexService = serviceProvider.GetRequiredService<IPokedexService>();

            Console.WriteLine("Enter the name or ID of a Pokemon:");
            var input = Console.ReadLine();

            var pokemon = await pokedexService.GetPokemonAsync(input);
            if (pokemon.HasErrors)
            {
                Console.WriteLine(pokemon.GetErrorMessagesAsString());
                return;
            }

            Console.WriteLine($"Name: {pokemon.Data.Name}");
            Console.WriteLine($"Height: {pokemon.Data.Height}");
            Console.WriteLine($"Weight: {pokemon.Data.Weight}");
        }
    }
}