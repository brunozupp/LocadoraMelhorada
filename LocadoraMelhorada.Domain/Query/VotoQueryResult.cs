namespace LocadoraMelhorada.Domain.Query
{
    public class VotoQueryResult
    {
        public string VotoId { get; set; }

        public FilmeQueryResult Filme { get; set; }

        public UsuarioQueryResult Usuario { get; set; }
    }
}
