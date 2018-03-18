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
    public class BannerRepository : RepositoryBase<Banner>, IBannerRepository
    {
        public BannerRepository(IMongoDatabase mongoDatabase)
            :base(mongoDatabase.GetCollection<Banner>("banner"))
        {
        }
    }
}
