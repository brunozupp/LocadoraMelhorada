using LocadoraMelhorada.Domain.Entidades;
using LocadoraMelhorada.Domain.Query;
using System.Collections.Generic;

namespace LocadoraMelhorada.Domain.Interfaces.Repositories
{
    public interface IFilmeRepository<IdType> where IdType : notnull
    {
        Filme Inserir(Filme filme);
        void Atualizar(Filme filme);
        void Excluir(IdType id);
        List<FilmeQueryResult> Listar();
        FilmeQueryResult Obter(IdType id);
        bool CheckId(IdType id);
    }
}
