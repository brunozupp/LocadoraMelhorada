namespace LocadoraMelhorada.Domain.Query
{
    public class VotoDoUsuarioQueryResult
    {
        public string VotoId { get; set; }

        public FilmeQueryResult Filme { get; set; }
    }
}
