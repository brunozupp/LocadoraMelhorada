using LocadoraMelhorada.Domain.Entidades;
using LocadoraMelhorada.Domain.Interfaces.Repositories;
using LocadoraMelhorada.Domain.Query;
using LocadoraMelhorada.Infra.Data.DataContexts;
using MongoDB.Driver;
using System.Collections.Generic;

namespace LocadoraMelhorada.Infra.Data.Repositories.MongoDb
{
    public class FilmeRepositoryMongoDb<IdType> : IFilmeRepository<IdType> where IdType : notnull
    {
        private readonly IMongoCollection<Filme> _collection;

        public FilmeRepositoryMongoDb(MongoDbDataContext context)
        {
            _collection = context.MongoConexao.GetCollection<Filme>("livros");
        }

        public void Atualizar(Filme filme)
        {
            _collection.ReplaceOne(f => f.Id.Equals(filme.Id), filme);
        }

        public bool CheckId(IdType id)
        {
            return _collection.Find(f => f.Id.Equals(id)).Any();
        }

        public void Excluir(IdType id)
        {
            _collection.DeleteOne(f => f.Id.Equals(id));
        }

        public Filme Inserir(Filme filme)
        {
            _collection.InsertOne(filme);
            return filme;
        }

        public List<FilmeQueryResult> Listar()
        {
            var filmes = _collection.Find(e => true).ToList();

            var filmesQueryResult = new List<FilmeQueryResult>();

            filmes.ForEach(filme =>
            {
                filmesQueryResult.Add(new FilmeQueryResult()
                {
                    Diretor = filme.Diretor,
                    FilmeId = filme.Id,
                    Titulo = filme.Titulo
                });
            });

            return filmesQueryResult;
        }

        public FilmeQueryResult Obter(IdType id)
        {
            var filme = _collection.Find(e => e.Id.Equals(id)).FirstOrDefault();

            var filmeQueryResult = new FilmeQueryResult()
            {
                Diretor = filme.Diretor,
                FilmeId = filme.Id,
                Titulo = filme.Titulo
            };


            return filmeQueryResult;
        }
    }
}
