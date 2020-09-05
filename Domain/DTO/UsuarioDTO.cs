using Dapper.Contrib.Extensions;
using Domain.Entities;

namespace Domain.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public Endereco Endereco { get; set; }
    }
}
