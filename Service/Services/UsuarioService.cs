using Services.Interfaces;
using Domain.DTO;
using Domain.Settings;
using Infra.Interfaces;
using CrossCutting.Security;
using Domain.Entities;
using Domain.Exceptions;

namespace Services.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UsuarioLoginDTO CadastrarUsuario(UsuarioDTO usuarioDTO)
        {
            var userDB = _unitOfWork.UsuarioRepository.GetByEmail(usuarioDTO.Email);

            if (userDB != null)
            {
                throw new AppException("Usuário já cadastrado");
            }
            else if (string.IsNullOrEmpty(usuarioDTO.Password))
            {
                throw new AppException("Favor informar uma senha válida");
            }

            var usuario = new Usuario
            {
                Nome = usuarioDTO.Nome,
                Sobrenome = usuarioDTO.Sobrenome,
                Email = usuarioDTO.Email,
                Password = Criptografia.SenhaCriptografada(usuarioDTO.Password),
                Role = Role.User
            };

            _unitOfWork.UsuarioRepository.Inserir(usuario);

            usuarioDTO.Endereco.IdUsuario = usuario.Id;

            _unitOfWork.UsuarioRepository.InserirEndereco(usuarioDTO.Endereco);

            _unitOfWork.Commit();

            return new UsuarioLoginDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
                Role = usuario.Role
            };
        }

        public UsuarioLoginDTO GetById(int id)
        {
            var usuario = _unitOfWork.UsuarioRepository.GetUsuarioById(id);

            return new UsuarioLoginDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
                Role = usuario.Role
            };
        }
    }
}