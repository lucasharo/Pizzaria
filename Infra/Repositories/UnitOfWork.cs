using Infra.Interfaces;
using System.Data.SqlClient;
using System;

namespace Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlTransaction _sqlTransaction;

        public IPedidoRepository PedidoRepository { get; }
        public IUsuarioRepository UsuarioRepository { get; }

        public UnitOfWork(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            _sqlConnection = sqlConnection;
            _sqlTransaction = sqlTransaction;

            PedidoRepository = new PedidoRepository(sqlConnection, sqlTransaction);
            UsuarioRepository = new UsuarioRepository(sqlConnection, sqlTransaction);
        }

        public void Commit()
        {
            _sqlTransaction.Commit();
        }

        public void Dispose()
        {
            _sqlTransaction.Dispose();
            _sqlConnection.Dispose();
        }
    }
}