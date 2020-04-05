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
    class DrugController
    {
        Logger logger = Logger.Instance;
        List<Drug> LDrugs = new List<Drug>();
        private readonly static DrugController _instance = new DrugController();
        public String pathFile { get; set; }

        public Boolean cargarArchivo()
        {

            logger.EscribirLog("Cargamos las Drogas de PharmaGKB", Logger.Tipo.Informativo, false);
            logger.EscribirLog("Chequeamos si existe el archivo " + pathFile, Logger.Tipo.Informativo, false);
            try
            {
                if (!File.Exists(pathFile))
                {
                    logger.EscribirLog("No existe el archivo " + pathFile, Logger.Tipo.Informativo, false);
                    return false;
                }
                foreach (string drugs in File.ReadAllLines(pathFile))
                {
                    string[] varsa = drugs.Split('\t');
                    int ClinicalAnnotationCount = 0;
                    if (int.TryParse(varsa[11], out ClinicalAnnotationCount))
                    {

                        Drug drug = new Drug();
                        drug.PharmGKBAccessionId = varsa[0];
                        drug.Name = varsa[1];
                        String[] Generics = varsa[2].Split(',');
                        foreach (String s in Generics)
                        {
                            if (s.Contains("\""))
                                drug.GenericName.Add(s.Split('"')[1]);
                            else
                                drug.GenericName.Add(s);
                        }
                        String[] Traders = varsa[3].Split(',');
                        foreach (String s in Traders)
                        {
                            if (s.Contains("\""))
                                drug.TradeNames.Add(s.Split('"')[1]);
                            else
                                drug.TradeNames.Add(s);
                        }
                        String[] BrandMixtures = varsa[4].Split(',');
                        foreach (String s in BrandMixtures)
                        {
                            if (s.Contains("\""))
                                drug.BrandMixtures.Add(s.Split('"')[1]);
                            else
                                drug.BrandMixtures.Add(s);
                        }
                        drug.Type = varsa[5];
                        String[] Crossreferences = varsa[6].Split(',');
                        foreach (String s in Crossreferences)
                        {
                            if (s.Contains("\""))
                                drug.BrandMixtures.Add(s.Split('"')[1]);
                            else
                                drug.BrandMixtures.Add(s);
                        }
                        drug.SMILES = varsa[7];
                        drug.InChI = varsa[8];
                        drug.DosingGuideline = (varsa[9] == "No") ? false : true;
                        String[] ExternalVocabulary = varsa[10].Split(',');
                        foreach (String s in Crossreferences)
                        {
                            if (s.Contains("\""))
                                drug.ExternalVocabulary.Add(s.Split('"')[1]);
                            else
                                drug.ExternalVocabulary.Add(s);
                        }
                        drug.ClinicalAnnotationCount = ClinicalAnnotationCount;
                        drug.VariantAnnotationCount = int.Parse(varsa[12]);
                        drug.PathwayCount = int.Parse(varsa[13]);
                        drug.VIPCount = int.Parse(varsa[14]);
                        String[] DosingGuidelineSources = varsa[15].Split(',');
                        foreach (String s in Crossreferences)
                        {
                            if (s.Contains("\""))
                                drug.DosingGuidelineSources.Add(s.Split('"')[1]);
                            else
                                drug.DosingGuidelineSources.Add(s);
                        }
                        drug.TopClinicalAnnotationLevel = varsa[16];
                        drug.TopFDALabelTestingLevel = varsa[17];
                        drug.TopAnyDrugLabelTestingLevel = varsa[18];
                        drug.LabelHasDosingInfo = varsa[19];
                        drug.HasRxAnnotation = varsa[20];
                        drug.RxNormIdentifiers = varsa[21];
                        String[] ATCIdentifiers = varsa[22].Split(',');
                        foreach (String s in Crossreferences)
                        {
                            if (s.Contains("\""))
                                drug.ATCIdentifiers.Add(s.Split('"')[1]);
                            else
                                drug.ATCIdentifiers.Add(s);
                        }
                    String[] PubChemCompoundIdentifiers = varsa[23].Split(',');
                    if(varsa[23] != "")
                    foreach (String s in PubChemCompoundIdentifiers)
                    {
                        if (s.Contains("\""))
                            drug.PubChemCompoundIdentifiers.Add((s.Split('"')[1] != "") ? int.Parse(s.Split('"')[1]) : 0);
                        else
                            drug.PubChemCompoundIdentifiers.Add((s != "") ? int.Parse(s) : 0);
                    }
                    
                        LDrugs.Add(drug);
                    }
                }
                logger.EscribirLog("Cargamos " + LDrugs.Count + " Drogas", Logger.Tipo.Informativo, true);
                return true;
            }
            catch(Exception ex)
            {
                logger.EscribirLog("Tuvimos problemas para cargar las Drogas: " + ex.Message, Logger.Tipo.Error, true);
                return false;
            }
        }
        private DrugController()
        {
            if (string.IsNullOrEmpty(pathFile))
                pathFile = Environment.CurrentDirectory + @"\Data\drugs.tsv";

        }

        public static DrugController Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
