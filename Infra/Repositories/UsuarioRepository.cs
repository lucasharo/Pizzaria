using Infra.Interfaces;
using Dapper;
using Domain.Entities;
using System.Data.SqlClient;

namespace Infra.Repositories
{
    public class UsuarioRepository : Repository, IUsuarioRepository
    {
        public UsuarioRepository(SqlConnection sqlConnection, SqlTransaction transaction) : base(sqlConnection, transaction)
        {
        }

        public Usuario GetUsuarioById(int id)
        {
            string query = @"SELECT * FROM USUARIO WHERE ID = @ID";

            var result = _sqlConnection.QueryFirstOrDefault<Usuario>(query, new { ID = id }, transaction: _sqlTransaction);

            return result;
        }

        public Endereco GetEnderecoUsuarioById(int id)
        {
            string query = @"SELECT * FROM ENDERECO WHERE IDUSUARIO = @ID";

            var result = _sqlConnection.QueryFirstOrDefault<Endereco>(query, new { ID = id }, transaction: _sqlTransaction);

            return result;
        }
    }
}