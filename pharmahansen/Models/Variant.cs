using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pharmahansen.Models
{
    class Variant
    {
        public String VariantID { get; set; }
        public String VariantName { get; set; }
        public List<String> GeneGeneIDs { get; set; }
        public List<Gen> Genes { get; set; }
        public String Location { get; set; }
        public int VariantAnnotationcount { get; set; }
        public int ClinicalAnnotationcount { get; set; }
        public int Level12ClinicalAnnotationcount { get; set; }
        public int GuidelineAnnotationcount { get; set; }
        public int LabelAnnotationcount { get; set; }
        public List<String> Synonyms { get; set; }
        public String GH38Chr { get; set; }
        public String GH38Pos { get; set; }
        public String GH37Chr { get; set; }
        public String GH37Pos { get; set; }
        public Variant()
        {
            GeneGeneIDs = new List<string>();
            Genes = new List<Gen>();
            Synonyms = new List<string>();
        }

    }
}
