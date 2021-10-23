﻿using Dapper;
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
    public class FilmeRepositorySqlServer<IdType> : IFilmeRepository<IdType> where IdType : struct
    {
        private readonly DynamicParameters _parameters = new DynamicParameters();
        private readonly SqlServerDataContext _dataContext;

        public FilmeRepositorySqlServer(SqlServerDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Atualizar(Filme filme)
        {
            //_parameters.Add("Id", filme.Id, DbType.Int64);
            _parameters.Add("Id", filme.Id);
            _parameters.Add("Titulo", filme.Titulo, DbType.String);
            _parameters.Add("Diretor", filme.Diretor, DbType.String);

            _dataContext.SqlServerConexao.Execute(FilmeQueries.Atualizar, _parameters);
        }

        public bool CheckId(IdType id)
        {
            _parameters.Add("Id", id);

            return _dataContext.SqlServerConexao.Query<bool>(FilmeQueries.CheckId, _parameters).FirstOrDefault();
        }

        public void Excluir(IdType id)
        {
            _parameters.Add("Id", id);

            _dataContext.SqlServerConexao.Execute(FilmeQueries.Excluir, _parameters);
        }

        public Filme Inserir(Filme filme)
        {
            _parameters.Add("Titulo", filme.Titulo, DbType.String);
            _parameters.Add("Diretor", filme.Diretor, DbType.String);

            var id = _dataContext.SqlServerConexao.ExecuteScalar<IdType>(FilmeQueries.Inserir, _parameters);

            return filme;
        }

        public List<FilmeQueryResult> Listar()
        {
            var filmes = _dataContext.SqlServerConexao.Query<FilmeQueryResult>(FilmeQueries.Listar).ToList();
            return filmes;
        }

        public FilmeQueryResult Obter(IdType id)
        {
            _parameters.Add("Id", id);

            var filme = _dataContext.SqlServerConexao.Query<FilmeQueryResult>(FilmeQueries.Obter, _parameters).FirstOrDefault();
            return filme;
        }
    }
}
