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
    public class FilmeRepository : IFilmeRepository<long>
    {
        private readonly DynamicParameters _parameters = new DynamicParameters();
        private readonly SqlServerDataContext _dataContext;

        public FilmeRepository(SqlServerDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Atualizar(Filme filme)
        {
            _parameters.Add("Id", filme.Id, DbType.Int64);
            _parameters.Add("Titulo", filme.Titulo, DbType.String);
            _parameters.Add("Diretor", filme.Diretor, DbType.String);

            _dataContext.SqlServerConexao.Execute(FilmeQueries.Atualizar, _parameters);
        }

        public bool CheckId(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            return _dataContext.SqlServerConexao.Query<bool>(FilmeQueries.CheckId, _parameters).FirstOrDefault();
        }

        public void Excluir(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            _dataContext.SqlServerConexao.Execute(FilmeQueries.Excluir, _parameters);
        }

        public long Inserir(Filme filme)
        {
            _parameters.Add("Titulo", filme.Titulo, DbType.String);
            _parameters.Add("Diretor", filme.Diretor, DbType.String);

            return _dataContext.SqlServerConexao.ExecuteScalar<long>(FilmeQueries.Inserir, _parameters);
        }

        public List<FilmeQueryResult> Listar()
        {
            var filmes = _dataContext.SqlServerConexao.Query<FilmeQueryResult>(FilmeQueries.Listar).ToList();
            return filmes;
        }

        public FilmeQueryResult Obter(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            var filme = _dataContext.SqlServerConexao.Query<FilmeQueryResult>(FilmeQueries.Obter, _parameters).FirstOrDefault();
            return filme;
        }
    }
}
