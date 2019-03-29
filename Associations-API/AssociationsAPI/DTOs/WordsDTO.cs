using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssociationsAPI.DTOs
{
    public class WordsDTO
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public WordsRelDTO[] Rels { get; set; }

    }
}
