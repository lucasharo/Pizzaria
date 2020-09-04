using Infra.Interfaces;
using System.Collections.Generic;
using Dapper;
using Dapper.Contrib.Extensions;
using Domain.Entities;
using System.Data.SqlClient;

namespace Infra.Repositories
{
    public class PedidoRepository : Repository, IPedidoRepository
    {
        public PedidoRepository(SqlConnection sqlConnection, SqlTransaction transaction) : base(sqlConnection, transaction)
        {
        }

        public IEnumerable<Sabor> ListarSabores()
        {
            string query = @"SELECT * FROM SABOR";

            var result = _sqlConnection.Query<Sabor>(query, transaction: _sqlTransaction);

            return result;
        }

        public int InserirPedido(Pedido pedido)
        {
            var result = _sqlConnection.Insert(pedido, _sqlTransaction);

            return (int)result;
        }

        public int InserirPizza(Pizza pizza)
        {
            var result = _sqlConnection.Insert(pizza, _sqlTransaction);

            return (int)result;
        }

        public int InserirPizzaSabor(Pizza_Sabor pizzaSabor)
        {
            var result = _sqlConnection.Insert(pizzaSabor, _sqlTransaction);

            return (int)result;
        }
    }
}