using Dapper.Contrib.Extensions;

namespace Domain.Entities
{
    [Table("Endereco")]
    public class Endereco
    {
        [Key]
        public int Id { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public int IdUsuario { get; set; }
    }
}