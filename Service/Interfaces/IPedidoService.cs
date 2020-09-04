using Domain.DTO;

namespace Service.Interfaces
{
    public interface IPedidoService
    {
        string CadastrarPedido(PedidoDTO pedidoDTO);
    }
}