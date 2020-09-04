using Dapper.Contrib.Extensions;

namespace Domain.Entities
{
    [Table("Sabor")]
    public class Sabor
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}
