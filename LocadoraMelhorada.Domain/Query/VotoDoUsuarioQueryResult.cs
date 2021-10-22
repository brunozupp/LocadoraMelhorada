namespace LocadoraMelhorada.Domain.Query
{
    public class VotoDoUsuarioQueryResult
    {
        public long VotoId { get; set; }

        public FilmeQueryResult Filme { get; set; }
    }
}
