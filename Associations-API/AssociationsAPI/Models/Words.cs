using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssociationsAPI.Models
{
    public class Words
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public IEnumerable<WordsRel> Rels { get; set; }
    }
}
