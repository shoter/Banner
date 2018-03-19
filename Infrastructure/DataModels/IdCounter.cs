using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataModels
{
    public class IdCounter<TKey>
    {
        public string CounterName { get; set; }
        public TKey Id { get; set; }
    }
}
