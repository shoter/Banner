using Infrastructure.DataModels;
using Infrastructure.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Repositories
{
    public class BannerRepository : RepositoryBase<Banner, int>, IBannerRepository
    {
        public BannerRepository(IMongoDatabase mongoDatabase)
            :base(mongoDatabase.GetCollection<Banner>("banner"))
        {
        }


        protected override UpdateDefinition<IdCounter<int>> GetCounterIncrement()
        {
            return Builders<IdCounter<int>>.Update
                .Inc(x => x.Id, 1);
        }
    }
}
