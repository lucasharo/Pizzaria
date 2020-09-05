using Microsoft.AspNetCore.Mvc;
using API.Helpers;
using Services.Interfaces;
using Domain.DTO;
using Microsoft.Extensions.Logging;
using API.Models;

namespace API.Controllers
{
    [ApiVersion("1")]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _userService;

        public UsuarioController(ILogger<BaseController> logger, IUsuarioService userService): base(logger)
        {
            _userService = userService;
        }

        [HttpPost]
        public ActionResult<BaseResponse<UsuarioLoginDTO>> CadastrarUsuario([FromBody]UsuarioDTO usuario) => Execute(() => _userService.CadastrarUsuario(usuario));

        [AuthorizeUser]
        [HttpGet("{id}")]
        public ActionResult<BaseResponse<UsuarioLoginDTO>> GetById(int id) => Execute(() => _userService.GetById(id));
    }
}