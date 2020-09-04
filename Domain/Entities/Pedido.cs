using Dapper.Contrib.Extensions;

namespace Domain.Entities
{
    [Table("Pedido")]
    public class Pedido
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public int? IdUsuario { get; set; }
    }
}
