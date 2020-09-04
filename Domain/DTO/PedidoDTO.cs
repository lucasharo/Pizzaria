using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class PedidoDTO
    {
        public int? IdUsuario { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }

        public Endereco Endereco { get; set; }
        public IEnumerable<Pizza> Pizzas { get; set; }
    }
}
