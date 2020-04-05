using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pharmahansen.Models;

namespace pharmahansen.Models
{
    class CAMetaData
    {
        public int ClinicalAnnotationID { get; set; }
        public List<String> Location{ get; set; }
        public List<String> GeneId { get; set; }
        public List<Gen> Gen { get; set; }
        public String LevelofEvidence { get; set; }
        public String ClinicalAnnotationTypes { get; set; }
        virtual public List<int> GenotypePhenotypeIDs { get; set; }
        
        public List<Genotipo> Genotipos { get; set; }
        public List<Phenotype> Fenotipos { get; set; }
        public String AnnotationText { get; set; }
        public List<int> VariantAnnotationsIDs { get; set; }
        public List<Anotaciones> Anotaciones { get; set; }
        public String VariantAnnotations { get; set; }
        public List<int> PMIDs { get; set; }
        public int EvidenceCount { get; set; }
        public List<String> RelatedChemicalsId { get; set; }
        public List<Chemical> chemicals { get; set; }
        public List<String> RelatedDiseasesId { get; set; }
        public List<Phenotype> Phenotypes { get; set; }
        public String BiogeographicalGroups  { get; set; }
        public String Chromosome { get; set; }

        public CAMetaData()
        {
            GeneId = new List<string>();
            Gen = new List<Gen>();
            GenotypePhenotypeIDs = new List<int>();
            Genotipos = new List<Genotipo>();
            Fenotipos = new List<Phenotype>();
            VariantAnnotationsIDs = new List<int>();
            Anotaciones = new List<Anotaciones>();
            PMIDs = new List<int>();
            RelatedChemicalsId = new List<string>();
            chemicals = new List<Chemical>();
            RelatedDiseasesId = new List<string>();
            Phenotypes = new List<Phenotype>();
            Location = new List<string>();
        }
    }
}
