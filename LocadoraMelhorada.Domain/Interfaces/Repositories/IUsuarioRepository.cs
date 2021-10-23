using LocadoraMelhorada.Domain.Entidades;
using LocadoraMelhorada.Domain.Query;
using System.Collections.Generic;

namespace LocadoraMelhorada.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository<IdType> where IdType : struct
    {
        IdType Inserir(Usuario usuario);
        void Atualizar(Usuario usuario);
        void Excluir(IdType id);
        List<UsuarioQueryResult> Listar();
        UsuarioQueryResult Obter(IdType id);
        bool CheckId(IdType id);

        bool Autenticar(string login, string senha);
    }
}
