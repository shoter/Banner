using Infrastructure.DataModels;
using Infrastructure.Db.Paging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IRepository<TModel, TKey>
        where TModel: IEntity
    {
        void Insert(TModel model);
        void Update(TKey id, TModel model);
        void Remove(TKey key);
        TModel FindById(TKey id);
        IFindFluent<TModel, TModel> Where(Expression<Func<TModel, bool>> predicate);
        Page<TModel> Page(int skipCount, int pageSize);
        void RemoveAll();
    }
}
