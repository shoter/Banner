using Infrastructure.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Infrastructure.Db.Paging;
using Infrastructure.DataModels;

namespace Mongo.Repositories
{
    public abstract class RepositoryBase<TModel, TKey> : IRepository<TModel, TKey>
        where TModel : IKeyedEntity<TKey>
    {
        private IMongoCollection<TModel> collection;
        protected readonly string counterName;
        protected IMongoCollection<IdCounter<TKey>> keyCollection;
        public RepositoryBase(IMongoCollection<TModel> collection)
        {
            this.collection = collection;
            this.keyCollection = collection.Database.GetCollection<IdCounter<TKey>>("counter");
            counterName = this.collection.CollectionNamespace + "counter";
            createCounterIfNotExist();
        }

        public TModel FindById(TKey id)
        {
            FilterDefinition<TModel> filter = createIdFilter(id);
            return collection.Find(filter).FirstOrDefault();
        }


        public void Insert(TModel model)
        {
            model.Id = GetNewId();
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

        public TKey GetNewId()
        {
            return keyCollection.FindOneAndUpdate(counter => counter.CounterName == counterName,
                GetCounterIncrement()).Id;
        }

        private void createCounterIfNotExist()
        {
            if (keyCollection.Count(counter => counter.CounterName == counterName) == 0)
                keyCollection.InsertOne(new IdCounter<TKey>()
                {
                    CounterName = counterName,
                    Id = default(TKey)
                });
        }

        protected abstract UpdateDefinition<IdCounter<TKey>> GetCounterIncrement();
    }
}
