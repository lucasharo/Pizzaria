using Services.Interfaces;
using Domain.DTO;
using Domain.Exceptions;
using Infra.Interfaces;
using CrossCutting.Security;

namespace Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public LoginService(ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public UsuarioLoginDTO Login(string email, string password)
        {
            var usuario = _unitOfWork.UsuarioRepository.GetByEmail(email);

            if (usuario == null)
            {
                throw new AppException("Usuário não encontrado");
            }
            else if (usuario.Password != Criptografia.SenhaCriptografada(password))
            {
                throw new AppException("Senha inválida");
            }

            usuario.Token = _tokenService.GenerateToken(usuario);

            _unitOfWork.UsuarioRepository.Atualizar(usuario);

            _unitOfWork.Commit();

            return new UsuarioLoginDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
                Role = usuario.Role,
                Token = usuario.Token
            };
        }
    }
}