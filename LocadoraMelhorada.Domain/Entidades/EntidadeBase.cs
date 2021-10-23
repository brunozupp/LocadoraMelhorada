using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LocadoraMelhorada.Domain.Entidades
{
    public abstract class EntidadeBase<T> where T : notnull
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
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
