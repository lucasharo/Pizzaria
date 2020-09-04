using API.Models;
using Microsoft.Extensions.Logging;
using Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.DTO;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiVersion("1")]
    public class PedidoController : BaseController
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(ILogger<BaseController> logger, IPedidoService pedidoService) : base(logger)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public ActionResult<BaseResponse<string>> CadastrarPedido([FromBody] PedidoDTO pedidoDTO) => Execute(() => _pedidoService.CadastrarPedido(pedidoDTO));
    }
}