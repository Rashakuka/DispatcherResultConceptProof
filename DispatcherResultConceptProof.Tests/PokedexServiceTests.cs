using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using DispatcherResultConceptProof.Services;
using Moq;
using Moq.Protected;
using Xunit;

namespace DispatcherResultConceptProof.Tests
{
    public class PokedexServiceTests
    {
        [Fact]
        public async Task GetPokemonAsync_Success_ReturnsPokemon()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"name\":\"pikachu\",\"height\":4,\"weight\":60}"),
            };

            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                   "SendAsync",
                   ItExpr.IsAny<HttpRequestMessage>(),
                   ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/") // Ensure the BaseAddress is set.
            };

            var pokedexService = new PokedexService(httpClient);

            var result = await pokedexService.GetPokemonAsync("pikachu");

            Assert.False(result.HasErrors);
            Assert.Equal("pikachu", result.Data.Name);
            Assert.Equal(4, result.Data.Height);
            Assert.Equal(60, result.Data.Weight);
            handlerMock.Protected().Verify(
            "SendAsync",
                Times.Exactly(1),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GetPokemonAsync_Error_ReturnsErrorMessage()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                ReasonPhrase = "Bad Request",
            };

            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                   "SendAsync",
                   ItExpr.IsAny<HttpRequestMessage>(),
                   ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var pokedexService = new PokedexService(httpClient);

            var result = await pokedexService.GetPokemonAsync("invalid");

            Assert.True(result.HasErrors);
            Assert.Equal("Bad Request", result.GetErrorMessagesAsString());
            handlerMock.Protected().Verify(
            "SendAsync",
                Times.Exactly(1),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}
