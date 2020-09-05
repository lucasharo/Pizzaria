using Xunit;
using API;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Domain.DTO;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;
using System.IO;
using System.Collections.Generic;
using Domain.Entities;
using API.Models;
using Xunit.Priority;

namespace XUnitTest
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationFactory<Startup> _factory;

        public UnitTest1(WebApplicationFactory<Startup> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, conf) =>
                {
                    var configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

                    conf.AddJsonFile(configPath);
                });
            });

            _httpClient = _factory.CreateClient();
        }

        [Fact, Priority(0)]
        public async Task CadastrarUsuarioTest()
        {
            var pedido = new UsuarioDTO
            {
                Email = "lucas7@hotmail.com",
                Password = "123456",
                Nome = "Lucas",
                Sobrenome = "Miranda",
                Endereco = new Endereco
                {
                    CEP = "08539110",
                    Logradouro = "Rua",
                    Numero = "1",
                    Bairro = "Vila",
                    Cidade = "São Paulo"
                }
            };

            var request = new HttpRequestMessage(new HttpMethod("POST"), "api/v1/Usuario")
            {
                Content = new StringContent(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            var result = JsonConvert.DeserializeObject<BaseResponse<UsuarioLoginDTO>>(await response.Content.ReadAsStringAsync());

            Assert.True((int)HttpStatusCode.OK == result.StatusCode, result.MessageError);
        }

        [Fact, Priority(1)]
        public async Task CadastrarPedidoSemUsuarioTest()
        {
            var pedido = new PedidoDTO
            {
                Nome = "Lucas Miranda",
                Pizzas = new List<Pizza>
                {
                    new Pizza 
                    {
                        Sabores = new List<Sabor>
                        {
                            new Sabor
                            {
                                Id = 1
                            },
                            new Sabor
                            {
                                Id = 2
                            }
                        }
                    }
                },
                Endereco = new Endereco
                {
                    CEP = "08539110",
                    Logradouro = "Rua",
                    Numero = "1",
                    Bairro = "Vila",
                    Cidade = "São Paulo"                    
                }
            };

            var request = new HttpRequestMessage(new HttpMethod("POST"), "api/v1/Pedido")
            {
                Content = new StringContent(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            var result = JsonConvert.DeserializeObject<BaseResponse<string>>(await response.Content.ReadAsStringAsync());

            Assert.True((int)HttpStatusCode.OK == result.StatusCode, result.MessageError);
        }

        [Fact, Priority(2)]
        public async Task CadastrarPedidoComUsuarioTest()
        {
            var usuario = await Login("lucas7@hotmail.com", "123456");

            var pedido = new PedidoDTO
            {
                IdUsuario = usuario.Id,
                Pizzas = new List<Pizza>
                {
                    new Pizza
                    {
                        Sabores = new List<Sabor>
                        {
                            new Sabor
                            {
                                Id = 1
                            },
                            new Sabor
                            {
                                Id = 2
                            }
                        }
                    }
                }
            };

            var request = new HttpRequestMessage(new HttpMethod("POST"), "api/v1/Pedido")
            {
                Content = new StringContent(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            var result = JsonConvert.DeserializeObject<BaseResponse<string>>(await response.Content.ReadAsStringAsync());

            Assert.True((int)HttpStatusCode.OK == result.StatusCode, result.MessageError);
        }

        private async Task<UsuarioDTO> Login(string email, string password)
        {
            var data = new Dictionary<string, string>
            {
                { "email", email },
                { "password", password }
            };

            var content = new FormUrlEncodedContent(data);

            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            var request = new HttpRequestMessage(new HttpMethod("POST"), "api/v1/Login")
            {
                Content = content
            };

            var response = await _httpClient.SendAsync(request);

            var result = JsonConvert.DeserializeObject<BaseResponse<UsuarioDTO>>(await response.Content.ReadAsStringAsync());

            return result.Result;
        }
    }
}
