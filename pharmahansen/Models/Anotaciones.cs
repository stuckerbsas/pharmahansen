using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pharmahansen.Models
{
    class Anotaciones
    {
        public int AnnotationId { get; set; }
        public String Variant { get; set; }
        public List<String> GeneId { get; set; }
        public List<Gen> Genes { get; set; }
        public List<String> ChemicalIDS { get; set; }
        public List<Chemical> Chemicals { get; set; }
        public int PMID { get; set; }
        public String PhenotypeCategory { get; set; }
        public Boolean Significance { get; set; }
        public String Notes { get; set; }
        public String Sentence { get; set; }
        public List<int> StudyParameters { get; set; }
        public String Alleles { get; set; }
        public String Chromosome { get; set; }
        public String Tipo { get; set; }
        //public Study Estudio { get; set; }

        public Anotaciones()
        {
            ChemicalIDS = new List<string>();
            GeneId = new List<string>();
            Genes = new List<Gen>();
            Chemicals = new List<Chemical>();
            StudyParameters = new List<int>();
        }
    }
}
