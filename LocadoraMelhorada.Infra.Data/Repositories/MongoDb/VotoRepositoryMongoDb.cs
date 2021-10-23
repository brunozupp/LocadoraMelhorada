using LocadoraMelhorada.Domain.Entidades;
using LocadoraMelhorada.Domain.Interfaces.Repositories;
using LocadoraMelhorada.Domain.Query;
using LocadoraMelhorada.Infra.Data.DataContexts;
using MongoDB.Driver;
using System.Collections.Generic;

namespace LocadoraMelhorada.Infra.Data.Repositories.MongoDb
{
    public class VotoRepositoryMongoDb<IdType> : IVotoRepository<IdType> where IdType : notnull
    {
        private readonly IMongoCollection<Voto> _collectionVotos;
        private readonly IFilmeRepository<string> _filmeRepository;
        private readonly IUsuarioRepository<string> _usuarioRepository;

        public VotoRepositoryMongoDb(MongoDbDataContext context, IFilmeRepository<string> filmeRepository, IUsuarioRepository<string> usuarioRepository)
        {
            _collectionVotos = context.MongoConexao.GetCollection<Voto>("votos");
            _filmeRepository = filmeRepository;
            _usuarioRepository = usuarioRepository;
        }

        public bool CheckId(IdType id)
        {
            return _collectionVotos.Find(u => u.Id.Equals(id)).Any();
        }

        public void Excluir(IdType id)
        {
            _collectionVotos.DeleteOne(f => f.Id.Equals(id));
        }

        public Voto Inserir(Voto voto)
        {
            _collectionVotos.InsertOne(voto);
            return voto;
        }

        public bool JaFoiVotado(IdType usuarioId, IdType filmeId)
        {
            return _collectionVotos.Find(u => u.UsuarioId.Equals(usuarioId) && u.FilmeId.Equals(filmeId)).Any();
        }

        public List<VotoQueryResult> Listar()
        {
            var votos = _collectionVotos.Find(e => true).ToList();

            var votosQueryResult = new List<VotoQueryResult>();

            votos.ForEach(voto =>
            {
                votosQueryResult.Add(new VotoQueryResult()
                {
                    VotoId = voto.Id,
                    Filme = _filmeRepository.Obter(voto.FilmeId),
                    Usuario = _usuarioRepository.Obter(voto.UsuarioId)
                });
            });

            return votosQueryResult;
        }

        public List<VotoDoUsuarioQueryResult> ListarPorUsuario(IdType usuarioId)
        {
            var votos = _collectionVotos.Find(e => e.UsuarioId.Equals(usuarioId)).ToList();

            var votosDoUsuarioQueryResult = new List<VotoDoUsuarioQueryResult>();

            votos.ForEach(voto =>
            {
                votosDoUsuarioQueryResult.Add(new VotoDoUsuarioQueryResult()
                {
                    VotoId = voto.Id,
                    Filme = _filmeRepository.Obter(voto.FilmeId),
                });
            });

            return votosDoUsuarioQueryResult;
        }
    }
}
