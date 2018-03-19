using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Db.Paging
{
    public class Page<T>
    {
        public long Total { get; private set; }
        public List<T> Items { get; private set; }

        public Page(List<T> items, long total)
        {
            Total = total;
            Items = items;
        }
    }
}
