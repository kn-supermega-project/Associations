using Associations.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Associations.DataAccess.Entity
{
    public abstract class Entity<TKey> : IEntity<TKey> where TKey : IComparable
    {
        [Key]
        public abstract TKey Id { get; set; }
    }
}
