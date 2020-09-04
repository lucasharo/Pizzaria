using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace Domain.Entities
{
    [Table("Pizza")]
    public class Pizza
    {
        [Key]
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public int IdPedido { get; set; }

        [Computed, Write(false)]
        public IEnumerable<Sabor> Sabores { get; set; }

    }
}
