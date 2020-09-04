using System;
using Xunit;
using API;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Domain.DTO;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;
using System.IO;

namespace XUnitTest
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationFactory<Startup> _factory;

        public UnitTest1(WebApplicationFactory<Startup> factory)
        {
            string projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile(configPath);
                });
            });

            _httpClient = _factory.CreateClient();

            /*
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();

            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());

            .AddTransient<IConfiguration>(x => configuration);

            _httpClient = server.CreateClient();*/
        }

        [Theory]
        [InlineData("POST")]
        public async Task CadastrarPedidoTest(string method)
        {
            var pedido = new PedidoDTO();

            var request = new HttpRequestMessage(new HttpMethod(method), "api/v1/Pedido");

            request.Content = new StringContent(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
