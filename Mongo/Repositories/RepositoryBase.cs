using Infrastructure.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Infrastructure.Db.Paging;

namespace Mongo.Repositories
{
    public class RepositoryBase<TModel, TKey> : IRepository<TModel, TKey>
    {
        private IMongoCollection<TModel> collection;
        public RepositoryBase(IMongoCollection<TModel> collection)
        {
            this.collection = collection;
        }

        public TModel FindById(TKey id)
        {
            FilterDefinition<TModel> filter = createIdFilter(id);
            return collection.Find(filter).FirstOrDefault();
        }

        public void Insert(TModel model)
        {
            collection.InsertOne(model);
        }

        public void Remove(TKey id)
        {
            FilterDefinition<TModel> filter = createIdFilter(id);
            collection.DeleteOne(filter);
        }

        public void Update(TKey id, TModel model)
        {
            FilterDefinition<TModel> filter = createIdFilter(id);
            collection.ReplaceOne(filter, model);
        }

        public Page<TModel> Page(int skipCount, int pageSize)
        {
            long count = collection.Count(x => true);
            return new Page<TModel>(
                items: collection.Find(x => true)
                .Skip(skipCount)
                .Limit(pageSize)
                .ToList(),
                total: count);
                
        }
        public IFindFluent<TModel, TModel> Where(System.Linq.Expressions.Expression<Func<TModel, bool>> predicate)
        {
            return collection.Find(predicate);
        }


        private static FilterDefinition<TModel> createIdFilter(TKey id)
        {
            return Builders<TModel>.Filter.Eq("Id", id);
        }

    }
}
