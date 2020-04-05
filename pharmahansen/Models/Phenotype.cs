using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pharmahansen.Models
{
    class Phenotype
    {
        public String PharmGKBAccessionId { get; set; }
        public String Name { get; set; }
        public List<String> AlternateNames { get; set; }
        public List<String> CrossReferences { get; set; }
        public List<String> ExtenralVocabulary { get; set; }

        public Phenotype()
        {
            AlternateNames = new List<string>();
            CrossReferences = new List<string>();
            ExtenralVocabulary = new List<string>();
        }

    }
}
