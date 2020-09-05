using Domain.Entities;

namespace Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Usuario user);
        string GenerateTokenChangePassword(long id);
        string GetToken();
        int GetIdByToken();
    }
}