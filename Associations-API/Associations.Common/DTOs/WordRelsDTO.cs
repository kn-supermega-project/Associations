using AssociationsAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Associations.Common.DTOs
{
    public class WordRelsDTO
    {
        public int Id { get; set; }
        public int WordId { get; set; }
        public int WordRelId { get; set; }
        public int MalePoints { get; set; }
        public int FemalePoints { get; set; }
        public int TotalPoints { get; set; }

    }

}
