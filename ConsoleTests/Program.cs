using Mongo;
using Mongo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTests
{
    //application made to test something quickly with debugger etc.
    class Program
    {
        public static void Main(string[] args)
        {
            var db = MongoDatabaseProvider.MongoDatabase;
            var repo = new BannerRepository(db);

            repo.Insert(new Infrastructure.DataModels.Banner()
            {

                Created = DateTime.Now,
                Id = 0,
                Html = "<html>test</html>",
                Modified = DateTime.Now
            });
        }
    }
}
