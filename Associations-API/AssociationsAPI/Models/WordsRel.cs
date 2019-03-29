using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssociationsAPI.Models
{
    public class WordsRel
    {
        public int Id { get; set; }
        public int WordId { get; set; }
        public int WordRelId { get; set; }
        public int MalePoints { get; set; }
        public int FemalePoints { get; set; }
        public int TotalPoints { get; set; }
        public float MalePercents { get; set; }
        public float FemalePercents { get; set; }
        public float TotalPercents { get; set; }
        public Words word { get; set; }
    }
}
