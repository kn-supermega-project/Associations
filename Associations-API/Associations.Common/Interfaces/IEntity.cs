using System;
using System.Collections.Generic;
using System.Text;

namespace Associations.Common.Interfaces
{
    public interface IEntity<TKey> where TKey : IComparable
    {
        TKey Id { get; set; }
    }
}
