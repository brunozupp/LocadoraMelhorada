namespace LocadoraMelhorada.Domain.Entidades
{
    public class Filme : EntidadeBase<long>
    {
        public string Titulo { get; private set; }

        public string Diretor { get; private set; }

        public Filme(string titulo, string diretor) : base()
        {
            Titulo = titulo;
            Diretor = diretor;
        }

        public Filme(long id, string titulo, string diretor) : base(id)
        {
            Titulo = titulo;
            Diretor = diretor;
        }
    }
}
