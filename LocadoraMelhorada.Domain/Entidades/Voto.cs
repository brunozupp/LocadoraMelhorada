namespace LocadoraMelhorada.Domain.Entidades
{
    public class Voto : EntidadeBase<long>
    {
        public long UsuarioId { get; private set; }

        public long FilmeId { get; private set; }

        public Voto(long usuarioId, long filmeId) : base()
        {
            UsuarioId = usuarioId;
            FilmeId = filmeId;
        }

        public Voto(long id, long usuarioId, long filmeId) : base(id)
        {
            UsuarioId = usuarioId;
            FilmeId = filmeId;
        }
    }
}
