using LocadoraMelhorada.Infra.Settings;
using MongoDB.Driver;

namespace LocadoraMelhorada.Infra.Data.DataContexts
{
    public class MongoDbDataContext
    {
        public IMongoDatabase MongoConexao { get; set; }

        public MongoDbDataContext(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            MongoConexao = client.GetDatabase(settings.DatabaseName);
        }
    }
}
