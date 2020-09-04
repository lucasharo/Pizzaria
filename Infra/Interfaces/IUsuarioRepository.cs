using Domain.Entities;

namespace Infra.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario GetUsuarioById(int id);
        Endereco GetEnderecoUsuarioById(int id);
    }
}