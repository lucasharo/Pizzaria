namespace Infra.Interfaces
{
    public interface IUnitOfWork
    {
        IPedidoRepository PedidoRepository { get; }
        IUsuarioRepository UsuarioRepository { get; }

        void Commit();
    }
}