using Infra.Interfaces;
using Dapper;
using Domain.Entities;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;

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

        public Usuario Inserir(Usuario usuario)
        {
            var result = _sqlConnection.Insert(usuario, _sqlTransaction);

            return usuario;
        }

        public bool Atualizar(Usuario usuario)
        {
            var result = _sqlConnection.Update(usuario, _sqlTransaction);

            return result;
        }

        public Usuario GetByEmail(string email)
        {
            string query = @"SELECT * FROM USUARIO WHERE EMAIL = @EMAIL";

            var result = _sqlConnection.QueryFirstOrDefault<Usuario>(query, new { EMAIL = email }, transaction: _sqlTransaction);

            return result;
        }

        public Endereco InserirEndereco(Endereco endereco)
        {
            var result = _sqlConnection.Insert(endereco, _sqlTransaction);

            return endereco;
        }
    }
}