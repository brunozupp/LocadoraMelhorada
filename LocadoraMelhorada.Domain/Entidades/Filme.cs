namespace LocadoraMelhorada.Domain.Entidades
{
    public class Filme : EntidadeBase<string>
    {
        public string Titulo { get; private set; }

        public string Diretor { get; private set; }

        public Filme(string titulo, string diretor) : base()
        {
            Titulo = titulo;
            Diretor = diretor;
        }

        public Filme(string id, string titulo, string diretor) : base(id)
        {
            Titulo = titulo;
            Diretor = diretor;
        }
    }
}
