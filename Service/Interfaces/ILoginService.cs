using Domain.DTO;

namespace Services.Interfaces
{
    public interface ILoginService
    {
        UsuarioLoginDTO Login(string email, string password);
    }
}