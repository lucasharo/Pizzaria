using Domain.Entities;
using System.Collections.Generic;

namespace Infra.Interfaces
{
    public interface IPedidoRepository
    {
        IEnumerable<Sabor> ListarSabores();
        int InserirPedido(Pedido pedido);
        int InserirPizza(Pizza pizza);
        int InserirPizzaSabor(Pizza_Sabor pizzaSabor);
    }
}