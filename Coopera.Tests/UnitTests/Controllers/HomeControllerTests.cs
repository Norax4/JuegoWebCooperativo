using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;
using Coopera.Models;
using Coopera.Services;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;

namespace Coopera.Tests.Controllers
{
    public class HomeControllerTests : IClassFixture<WebApplicationFactory<Coopera.Program>>
    {
        private readonly WebApplicationFactory<Coopera.Program> _factory;

        public HomeControllerTests(WebApplicationFactory<Coopera.Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ComenzarPartidaExitoso()
        {
            //Arrange
            var mockService = new Mock<IPartidaService>();
            mockService.Setup(mock =>
            mock.NuevaPartida(
                It.IsAny<Dificultad>(),
                It.IsAny<string>(),
                It.IsAny<ISession>()
                ))
            .Returns(Task.CompletedTask);

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IPartidaService>(_ => mockService.Object);
                });
            }).CreateClient();

            Dictionary<string, string> formData = new Dictionary<string, string>
                {
                    { "dificultad", "Facil" },
                    { "nombreJugador", "John" }
                };

            var formDataContent = new FormUrlEncodedContent(formData);


            //Act
            var response = await client.PostAsync("/Home/ComenzarPartida", formDataContent);


            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            Assert.Contains($"<span id=\"playerNameDisplay\" class=\"muted\">Jugador: <em>{formData["nombreJugador"]}</em></span>", content);
        }
    }
}