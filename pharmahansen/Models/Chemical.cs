using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pharmahansen.Models
{
    class Chemical
    {
        public String PharmGKBAccessionId { get; set; }
        public String Name { get; set; }
        public List<String> GenericNames { get; set; }
        public List<String> TradeNames { get; set; }
        public String BranMixtures { get; set; }
        public String Type { get; set; }
        public List<String> CrossReferences { get; set; }
        public String Smiles { get; set; }
        public String InChl { get; set; }
        public Boolean DosingGuideline { get; set; }
        public List<String> ExternalVocabulary { get; set; }
        public int ClinicalAnnotationCount { get; set; }
        public int VariantAnnotationCount { get; set; }
        public int PathwayCount { get; set; }
        public int VIPCount { get; set; }
        public String DosingGuidelineSources { get; set; }
        public String TopClinicalAnnotationLevel { get; set; }
        public String TopFDALabelTestingLevel { get; set; }
        public String TopAnyDrugLabelTestingLevel { get; set; }
        public String LabelHasDosingInfo { get; set; }
        public String HasRxAnnotation { get; set; }
        public String RxNormIdentifiers { get; set; }
        public String ATCIdentifiers { get; set; }
        public List<int> PubChemCompoundIdentifiers { get; set; }

        public Chemical()
        {
            GenericNames = new List<string>();
            TradeNames = new List<string>();
            CrossReferences = new List<string>();
            ExternalVocabulary = new List<string>();
            PubChemCompoundIdentifiers = new List<int>();
        }
    }
}
