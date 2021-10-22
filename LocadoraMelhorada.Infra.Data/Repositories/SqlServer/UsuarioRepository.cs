using Dapper;
using LocadoraMelhorada.Domain.Entidades;
using LocadoraMelhorada.Domain.Interfaces.Repositories;
using LocadoraMelhorada.Domain.Query;
using LocadoraMelhorada.Infra.Data.DataContexts;
using LocadoraMelhorada.Infra.Data.Repositories.SqlServer.Queries;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LocadoraMelhorada.Infra.Data.Repositories.SqlServer
{
    public class UsuarioRepository : IUsuarioRepository<long>
    {
        private readonly DynamicParameters _parameters = new DynamicParameters();
        private readonly SqlServerDataContext _dataContext;

        public UsuarioRepository(SqlServerDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Atualizar(Usuario usuario)
        {
            _parameters.Add("Id", usuario.Id, DbType.Int64);
            _parameters.Add("Nome", usuario.Nome, DbType.String);
            _parameters.Add("Login", usuario.Login, DbType.String);
            _parameters.Add("Senha", usuario.Senha, DbType.String);

            _dataContext.SqlServerConexao.Execute(UsuarioQueries.Atualizar, _parameters);
        }

        public bool Autenticar(string login, string senha)
        {
            _parameters.Add("Login", login, DbType.String);
            _parameters.Add("Senha", senha, DbType.String);

            return _dataContext.SqlServerConexao.Query<bool>(UsuarioQueries.Autenticar, _parameters).FirstOrDefault();
        }

        public bool CheckId(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            return _dataContext.SqlServerConexao.Query<bool>(UsuarioQueries.CheckId, _parameters).FirstOrDefault();
        }

        public void Excluir(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            _dataContext.SqlServerConexao.Execute(UsuarioQueries.Excluir, _parameters);
        }

        public long Inserir(Usuario usuario)
        {
            _parameters.Add("Nome", usuario.Nome, DbType.String);
            _parameters.Add("Login", usuario.Login, DbType.String);
            _parameters.Add("Senha", usuario.Senha, DbType.String);

            return _dataContext.SqlServerConexao.ExecuteScalar<long>(UsuarioQueries.Inserir, _parameters);
        }

        public List<UsuarioQueryResult> Listar()
        {
            var usuarios = _dataContext.SqlServerConexao.Query<UsuarioQueryResult>(UsuarioQueries.Listar).ToList();
            return usuarios;
        }

        public UsuarioQueryResult Obter(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            var usuario = _dataContext.SqlServerConexao.Query<UsuarioQueryResult>(UsuarioQueries.Obter, _parameters).FirstOrDefault();
            return usuario;
        }
    }
}
