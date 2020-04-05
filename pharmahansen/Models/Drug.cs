using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pharmahansen.Models
{
    class Drug
    {
        public String PharmGKBAccessionId { get; set; }
        public String Name { get; set; }
        public List<String> GenericName { get; set; }
        public List<String> TradeNames { get; set; }
        public List<String> BrandMixtures { get; set; }
        public String Type { get; set; }
        public String Crossreferences { get; set; }
        public String SMILES { get; set; }
        public String InChI { get; set; }
        public Boolean DosingGuideline { get; set; }
        public List<String> ExternalVocabulary { get; set; }
        public int ClinicalAnnotationCount { get; set; }
        public int VariantAnnotationCount { get; set; }
        public int PathwayCount { get; set; }
        public int VIPCount { get; set; }
        public List<String> DosingGuidelineSources { get; set; }
        public String TopClinicalAnnotationLevel { get; set; }
        public String TopFDALabelTestingLevel { get; set; }
        public String TopAnyDrugLabelTestingLevel { get; set; }
        public String LabelHasDosingInfo { get; set; }
        public String HasRxAnnotation { get; set; }
        public String RxNormIdentifiers { get; set; }
        public List<String> ATCIdentifiers { get; set; }
        public List<int> PubChemCompoundIdentifiers { get; set; }
        public Drug() {
            GenericName = new List<string>();
            TradeNames = new List<string>();
            BrandMixtures = new List<string>();
            ExternalVocabulary = new List<string>();
            DosingGuidelineSources = new List<string>();
            ATCIdentifiers = new List<string>();
            PubChemCompoundIdentifiers = new List<int>();
        }
    }
}
