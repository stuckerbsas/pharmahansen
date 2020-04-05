using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pharmahansen.Models
{
    class Gen
    {
        public String PharmGKBAccessionID { get; set; }
        public int NCBIGeneID { get; set; }
        public int[] HGNCID { get; set; }
        public String EnsemblId { get; set; }
        public String Name { get; set; }
        public String Symbol { get; set; }
        public List<String> AlternateNames { get; set; }
        public List<String> AlternateSymbols { get; set; }
        public Boolean IsVIP { get; set; }
        public Boolean HasVariantAnnotation { get; set; }
        public List<String> Crossreferences { get; set; }
        public Boolean HasCPICDosingGuideline { get; set; }
        public String Chromosome { get; set; }
        public int ChromosomalStart { get; set; }
        public int ChromosomalEnd { get; set; }

        public Gen()
        {
            AlternateNames = new List<String>();
            AlternateSymbols = new List<String>();
            Crossreferences = new List<String>();
        }
    }
}
