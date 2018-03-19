using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using MongoDB.Driver;
using Mongo.Repositories;

namespace Mongo
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBannerRepository BannerRepository { get; private set; }

        public UnitOfWork(IMongoDatabase mongoDatabase)
        {
            BannerRepository = new BannerRepository(mongoDatabase);
        }

    }
}
