using pharmahansen.Models;
using pharmahansen.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pharmahansen.Controller
{
    class ChemicalsController
    {
        Logger logger = Logger.Instance;
        List<Chemical> LChemicals = new List<Chemical>();
        public String filePath { get; set; }
        private readonly static ChemicalsController _instance = new ChemicalsController();
        public Chemical getById(String ChemicalId)
        {
            return LChemicals.Where(s => s.PharmGKBAccessionId == ChemicalId).FirstOrDefault();
        }
        public Boolean cargarArchivo()
        {
            logger.EscribirLog("Cargamos los Quimicos de PharmaGKB", Logger.Tipo.Informativo, false);
            logger.EscribirLog("Cheaquemos si existe el archivo " + filePath, Logger.Tipo.Informativo, false);
            if(!File.Exists(filePath))
            {
                logger.EscribirLog("No existe el archivo " + filePath, Logger.Tipo.Informativo, false);
                return false;
            }
            try
            {
                foreach (string chemicals in File.ReadAllLines(filePath))
                {
                    string[] varsa = chemicals.Split('\t');
                    int ClinicalAnnotationCount = 0;
                    if (int.TryParse(varsa[11], out ClinicalAnnotationCount))
                    {

                        Chemical chemical = new Chemical();
                        chemical.PharmGKBAccessionId = varsa[0];
                        chemical.Name = varsa[1];
                        string[] GenericNames = varsa[2].Split(',');
                        foreach (String s in GenericNames)
                        {
                            if (s.Contains("\""))
                                chemical.GenericNames.Add(s.Split('"')[1]);
                            else
                                chemical.GenericNames.Add(s);

                        }
                        string[] TradeNames = varsa[3].Split(',');
                        foreach (String s in TradeNames)
                        {
                            if (s.Contains("\""))
                                chemical.TradeNames.Add(s.Split('"')[1]);
                            else
                                chemical.TradeNames.Add(s);

                        }

                        chemical.BranMixtures = varsa[4];
                        chemical.Type = varsa[5];
                        string[] CrossReferences = varsa[6].Split(',');
                        foreach (String s in CrossReferences)
                        {
                            if (s.Contains("\""))
                                chemical.CrossReferences.Add(s.Split('"')[1]);
                            else
                                chemical.CrossReferences.Add(s);

                        }
                        chemical.Smiles = varsa[7];
                        chemical.InChl = varsa[8];
                        chemical.DosingGuideline = (varsa[9] == "No") ? false : true;
                        string[] ExternalVocabulary = varsa[10].Split(',');
                        foreach (String s in ExternalVocabulary)
                        {
                            if (s.Contains("\""))
                                chemical.ExternalVocabulary.Add(s.Split('"')[1]);
                            else
                                chemical.ExternalVocabulary.Add(s);

                        }

                        chemical.ClinicalAnnotationCount = ClinicalAnnotationCount;
                        chemical.VariantAnnotationCount = int.Parse(varsa[12]);
                        chemical.PathwayCount = int.Parse(varsa[13]);
                        chemical.VIPCount = int.Parse(varsa[14]);
                        chemical.DosingGuidelineSources = varsa[15];
                        chemical.TopClinicalAnnotationLevel = varsa[16];
                        chemical.TopFDALabelTestingLevel = varsa[17];
                        chemical.TopAnyDrugLabelTestingLevel = varsa[18];
                        chemical.LabelHasDosingInfo = varsa[19];
                        chemical.HasRxAnnotation = varsa[20];
                        chemical.RxNormIdentifiers = varsa[21];
                        chemical.ATCIdentifiers = varsa[22];
                        string[] PubChemCompoundIdentifiers = varsa[23].Split(',');
                        if (varsa[23] != "")
                            foreach (String s in PubChemCompoundIdentifiers)
                            {
                                if (s.Contains("\""))
                                    chemical.PubChemCompoundIdentifiers.Add(int.Parse(s.Split('"')[1]));
                                else
                                    chemical.PubChemCompoundIdentifiers.Add(int.Parse(s));

                            }
                        LChemicals.Add(chemical);
                    }
                }
                logger.EscribirLog("Cargamos " + LChemicals.Count + " Drogas", Logger.Tipo.Informativo, true);
                return true;
            }
            catch(Exception ex)
            {
                logger.EscribirLog("Tuvimos un problema al cargar los Quimicos: " + ex.Message, Logger.Tipo.Error, false);
                return false;
            }
        }

        private ChemicalsController()
        { 
            if(String.IsNullOrEmpty(filePath))
                filePath = Environment.CurrentDirectory + @"\Data\chemicals.tsv";
        }
        public static ChemicalsController Instance
        {
            get
            {
                return _instance;
            }
        }

    }
}
