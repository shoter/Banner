using MongoDB.Driver;
using Ninject.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo
{
    public class MongoDatabaseProvider : Provider<IMongoDatabase>
    {
        //It's advisable to create MongoClient as singleton
        //http://mongodb.github.io/mongo-csharp-driver/2.0/reference/driver/connecting/#re-use
        public static MongoClient MongoClient { get; private set; }
        public static IMongoDatabase MongoDatabase { get; private set; }
        static MongoDatabaseProvider()
        {
            MongoClient = new MongoClient(System.Configuration.ConfigurationManager.ConnectionStrings["mongoDatabase"].ConnectionString);
            MongoDatabase = MongoClient.GetDatabase("banner");
        }
        protected override IMongoDatabase CreateInstance(IContext context)
        {
            return MongoDatabase;
        }
    }
}
