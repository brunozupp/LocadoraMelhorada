using LocadoraMelhorada.Domain.Entidades;
using LocadoraMelhorada.Domain.Query;
using System.Collections.Generic;

namespace LocadoraMelhorada.Domain.Interfaces.Repositories
{
    public interface IVotoRepository<IdType> where IdType : notnull
    {
        Voto Inserir(Voto voto);

        void Excluir(IdType id);

        List<VotoQueryResult> Listar();

        List<VotoDoUsuarioQueryResult> ListarPorUsuario(IdType usuarioId);

        bool CheckId(IdType id);

        bool JaFoiVotado(IdType usuarioId, IdType filmeId);
    }
}
