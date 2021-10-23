namespace LocadoraMelhorada.Domain.Entidades
{
    public class Voto : EntidadeBase<string>
    {
        public string UsuarioId { get; private set; }

        public string FilmeId { get; private set; }

        public Voto(string usuarioId, string filmeId) : base()
        {
            UsuarioId = usuarioId;
            FilmeId = filmeId;
        }

        public Voto(string id, string usuarioId, string filmeId) : base(id)
        {
            UsuarioId = usuarioId;
            FilmeId = filmeId;
        }
    }
}
