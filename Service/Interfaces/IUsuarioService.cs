using Domain.DTO;
using Domain.Entities;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IUsuarioService
    {
        UsuarioLoginDTO CadastrarUsuario(UsuarioDTO usuario);
        UsuarioLoginDTO GetById(int id);
    }
}
