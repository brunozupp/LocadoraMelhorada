namespace LocadoraMelhorada.Domain.Entidades
{
    public abstract class EntidadeBase<T>
    {
        public T Id { get; private set; }

        public EntidadeBase() { }

        public EntidadeBase(T id)
        {
            Id = id;
        }

        public void AtualizarId(T id)
        {
            Id = id;
        }
    }
}
