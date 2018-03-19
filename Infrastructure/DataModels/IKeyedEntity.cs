using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataModels
{
    public interface IKeyedEntity<TKey> : IEntity
    {
        TKey Id { get; set; }
    }
}
