using System;
using System.Collections.Generic;
using System.Text;

namespace Associations.DataAccess.Entity
{
    public class Words : Entity<int>
    {
        public override int Id { get; set; }
        public string Word { get; set; }
        public int Type { get; set; }
        public IList<WordRels> Rels { get; set; }
    }
}
