using LocadoraMelhorada.Domain.Entidades;
using LocadoraMelhorada.Domain.Interfaces.Repositories;
using LocadoraMelhorada.Domain.Query;
using LocadoraMelhorada.Infra.Data.DataContexts;
using MongoDB.Driver;
using System.Collections.Generic;

namespace LocadoraMelhorada.Infra.Data.Repositories.MongoDb
{
    public class UsuarioRepositoryMongoDb<IdType> : IUsuarioRepository<IdType> where IdType : notnull
    {
        private readonly IMongoCollection<Usuario> _collection;

        public UsuarioRepositoryMongoDb(MongoDbDataContext context)
        {
            _collection = context.MongoConexao.GetCollection<Usuario>("usuarios");
        }

        public void Atualizar(Usuario usuario)
        {
            _collection.ReplaceOne(u => u.Id.Equals(usuario.Id), usuario);
        }

        public bool Autenticar(string login, string senha)
        {
            return _collection.Find(u => u.Login.Equals(login) && u.Senha.Equals(senha)).Any();
        }

        public bool CheckId(IdType id)
        {
            return _collection.Find(u => u.Id.Equals(id)).Any();
        }

        public void Excluir(IdType id)
        {
            _collection.DeleteOne(f => f.Id.Equals(id));
        }

        public Usuario Inserir(Usuario usuario)
        {
            _collection.InsertOne(usuario);
            return usuario;
        }

        public List<UsuarioQueryResult> Listar()
        {
            var usuarios = _collection.Find(e => true).ToList();

            var usuariosQueryResult = new List<UsuarioQueryResult>();

            usuarios.ForEach(usuario =>
            {
                usuariosQueryResult.Add(new UsuarioQueryResult()
                {
                    Login = usuario.Login,
                    Nome = usuario.Nome,
                    UsuarioId = usuario.Id
                });
            });

            return usuariosQueryResult;
        }

        public UsuarioQueryResult Obter(IdType id)
        {
            var usuario = _collection.Find(e => e.Id.Equals(id)).FirstOrDefault();

            var usuarioQueryResult = new UsuarioQueryResult()
            {
                Login = usuario.Login,
                Nome = usuario.Nome,
                UsuarioId = usuario.Id
            };

            return usuarioQueryResult;
        }
    }
}
