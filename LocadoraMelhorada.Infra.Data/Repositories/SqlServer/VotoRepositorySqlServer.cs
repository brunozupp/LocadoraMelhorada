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
    public class VotoRepositorySqlServer<IdType> : IVotoRepository<IdType> where IdType : struct
    {
        private readonly DynamicParameters _parameters = new DynamicParameters();
        private readonly SqlServerDataContext _dataContext;

        public VotoRepositorySqlServer(SqlServerDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CheckId(IdType id)
        {
            _parameters.Add("Id", id);

            return _dataContext.SqlServerConexao.Query<bool>(VotoQueries.CheckId, _parameters).FirstOrDefault();
        }

        public void Excluir(IdType id)
        {
            _parameters.Add("Id", id);

            _dataContext.SqlServerConexao.Execute(VotoQueries.Excluir, _parameters);
        }

        public Voto Inserir(Voto voto)
        {
            _parameters.Add("FilmeId", voto.FilmeId, DbType.Int64);
            _parameters.Add("UsuarioId", voto.UsuarioId, DbType.Int64);

            _dataContext.SqlServerConexao.ExecuteScalar<IdType>(VotoQueries.Inserir, _parameters);

            return voto;
        }

        public bool JaFoiVotado(IdType usuarioId, IdType filmeId)
        {
            _parameters.Add("UsuarioId", usuarioId);
            _parameters.Add("FilmeId", filmeId);

            return _dataContext.SqlServerConexao.Query<bool>(VotoQueries.JaFoiVotado, _parameters).FirstOrDefault();
        }

        public List<VotoQueryResult> Listar()
        {
            return _dataContext.SqlServerConexao.Query<VotoQueryResult, FilmeQueryResult, UsuarioQueryResult, VotoQueryResult>(
                    VotoQueries.Listar,
                    map: ((voto, filme, usuario) =>
                    {
                        voto.Filme = filme;
                        voto.Usuario = usuario;

                        return voto;
                    }),
                        splitOn: "FilmeId,UsuarioId"
                    ).ToList();
        }

        public List<VotoDoUsuarioQueryResult> ListarPorUsuario(IdType usuarioId)
        {
            _parameters.Add("UsuarioId", usuarioId);

            return _dataContext.SqlServerConexao.Query<VotoDoUsuarioQueryResult, FilmeQueryResult, VotoDoUsuarioQueryResult>(
                VotoQueries.ListarPorUsuario,
                map: ((voto, filme) =>
                {
                    voto.Filme = filme;

                    return voto;
                }),
                    splitOn: "FilmeId",
                    param: _parameters
                ).ToList();
        }
    }
}
