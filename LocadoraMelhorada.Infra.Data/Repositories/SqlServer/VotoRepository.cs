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
    public class VotoRepository : IVotoRepository<long>
    {
        private readonly DynamicParameters _parameters = new DynamicParameters();
        private readonly SqlServerDataContext _dataContext;

        public VotoRepository(SqlServerDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CheckId(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            return _dataContext.SqlServerConexao.Query<bool>(VotoQueries.CheckId, _parameters).FirstOrDefault();
        }

        public void Excluir(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            _dataContext.SqlServerConexao.Execute(VotoQueries.Excluir, _parameters);
        }

        public long Inserir(Voto voto)
        {
            _parameters.Add("FilmeId", voto.FilmeId, DbType.Int64);
            _parameters.Add("UsuarioId", voto.UsuarioId, DbType.Int64);

            return _dataContext.SqlServerConexao.ExecuteScalar<long>(VotoQueries.Inserir, _parameters);
        }

        public bool JaFoiVotado(long usuarioId, long filmeId)
        {
            _parameters.Add("UsuarioId", usuarioId, DbType.Int64);
            _parameters.Add("FilmeId", filmeId, DbType.Int64);

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

        public List<VotoDoUsuarioQueryResult> ListarPorUsuario(long usuarioId)
        {
            _parameters.Add("UsuarioId", usuarioId, DbType.Int64);

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
