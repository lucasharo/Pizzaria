using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Domain.DTO;
using Microsoft.Extensions.Logging;
using API.Models;

namespace API.Controllers
{
    [ApiVersion("1")]
    public class LoginController : BaseController
    {
        private readonly ILoginService _service;

        public LoginController(ILogger<BaseController> logger, ILoginService service): base(logger)
        {
            _service = service;
        }

        [Consumes("application/x-www-form-urlencoded")]
        [HttpPost]
        public ActionResult<BaseResponse<UsuarioLoginDTO>> Login([FromForm]LoginDTO login) => Execute(() => _service.Login(login.Email, login.Password));
    }
}