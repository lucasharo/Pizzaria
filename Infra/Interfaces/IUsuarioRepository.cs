using Domain.Entities;
using System.Collections.Generic;

namespace Infra.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario GetUsuarioById(int id);
        Endereco GetEnderecoUsuarioById(int id);
        Usuario Inserir(Usuario usuario);
        bool Atualizar(Usuario usuario);
        Usuario GetByEmail(string email);
        Endereco InserirEndereco(Endereco endereco);
    }
}