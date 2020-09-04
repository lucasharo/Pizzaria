using Dapper.Contrib.Extensions;

namespace Domain.Entities
{
    [Table("Pizza_Sabor")]
    public class Pizza_Sabor
    {
        [Key]
        public int Id { get; set; }
        public int IdPizza { get; set; }
        public int IdSabor { get; set; }
    }
}
