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
    class CAMetadaDataController
    {
        Logger logger = Logger.Instance;
        List<CAMetaData> CAMetaDatas = new List<CAMetaData>();
        public String filePath { get; set; }
        private readonly static CAMetadaDataController _instance = new CAMetadaDataController();

        public IEnumerable<CAMetaData> getByLevelofEvidence(String level)
        {
            return CAMetaDatas.Where(s => s.LevelofEvidence == "1A" || s.LevelofEvidence == "1B" || s.LevelofEvidence == "2A" || s.LevelofEvidence == "2B"  );
        }
        public Boolean cargarArchivo()
        {

            logger.EscribirLog("Cargamos la información de Metada de PharmaGKB", Logger.Tipo.Informativo, false);
            logger.EscribirLog("Chequeamos si existe el archivo " + filePath, Logger.Tipo.Informativo, false);
            if (!File.Exists(filePath))
            {
                logger.EscribirLog("No existe el archivo " + filePath, Logger.Tipo.Informativo, false);
                return false;
            }

            foreach (string CMAMetadata in File.ReadAllLines(filePath))
            {
                string[] varsa = CMAMetadata.Split('\t');
                int ClinicalAnnotationID = 0;
                if (int.TryParse(varsa[0], out ClinicalAnnotationID))
                {
                    CAMetaData cAMeta = new CAMetaData();
                    cAMeta.ClinicalAnnotationID = ClinicalAnnotationID;
                    String[] location = varsa[1].Split(',');
                    foreach(String s in location)
                    {
                        if (s.Contains("\""))
                            cAMeta.Location.Add(s.Split('"')[1]);
                        else
                            cAMeta.Location.Add(s);
                    }
                    String[] GenId = varsa[2].Split(',');
                    foreach (String s in GenId)
                    {
                        
                        if (s.Contains("\""))
                        {
                            string genid = s;
                            if (s.Contains("("))
                                genid = s.Split('"')[1].Split('(')[1].Split(')')[0];
                            cAMeta.GeneId.Add(genid);
                            cAMeta.Gen.Add(GenController.Instance.getByID(genid));
                        }
                        else
                        {
                            string genid =s ;
                            if (s.Contains("("))
                                genid = s.Split('(')[1].Split(')')[0];
                            cAMeta.GeneId.Add(genid);
                            cAMeta.Gen.Add(GenController.Instance.getByID(genid));
                        }
                    }
                    cAMeta.LevelofEvidence = varsa[3];
                    cAMeta.ClinicalAnnotationTypes = varsa[4];

                    String[] GenotipoID = varsa[5].Split(',');
                    foreach (String s in GenotipoID)
                    {
                        if (s.Contains("\""))
                        {
                            cAMeta.GenotypePhenotypeIDs.Add(int.Parse( s.Split('"')[1]));
                            Genotipo genotipo = GenotipoController.Instance.getById(int.Parse(s.Split('"')[1]));
                            if(genotipo != null)
                                cAMeta.Genotipos.Add(genotipo);
                            Phenotype phenotype = PhenotypesController.Instance.getById(s.Split('"')[1]);
                            if(phenotype != null)
                                cAMeta.Phenotypes.Add(phenotype);
                        }
                        else
                        {
                            cAMeta.GenotypePhenotypeIDs.Add(int.Parse(s));
                            Genotipo genotipo = GenotipoController.Instance.getById(int.Parse(s));
                            if (genotipo != null)
                                cAMeta.Genotipos.Add(genotipo);
                            Phenotype phenotype = PhenotypesController.Instance.getById(s);
                            if (phenotype != null)
                                cAMeta.Phenotypes.Add(phenotype);
                        }
                    }
                    cAMeta.AnnotationText = varsa[6];
                    String[] Anotaciones = varsa[7].Split(',');
                    foreach (String s in Anotaciones)
                    {
                        if (s.Contains("\""))
                        {
                            cAMeta.VariantAnnotationsIDs.Add(int.Parse( s.Split('"')[1]));
                            cAMeta.Anotaciones.Add(AnotacionesController.Instance.getById(int.Parse( s.Split('"')[1])));
                        }
                        else
                        {
                            cAMeta.GeneId.Add(s);
                            cAMeta.Gen.Add(GenController.Instance.getByID(s));
                        }
                    }
                    cAMeta.VariantAnnotations = varsa[8];
                    String[] pmids = varsa[9].Split(';');
                    foreach (String s in pmids)
                        if (s.Contains("\""))
                            cAMeta.PMIDs.Add(int.Parse(s.Split(',')[1]));
                        else
                            cAMeta.PMIDs.Add(int.Parse(s));
                    cAMeta.EvidenceCount = int.Parse(varsa[10]);
                    string[] relatedchemicals = varsa[11].Split(',');
                    
                    foreach (string s in relatedchemicals)
                        if(s != "")
                            if (s.Contains("\""))
                            {
                                cAMeta.RelatedChemicalsId.Add(s.Split('"')[1]);
                                cAMeta.chemicals.Add(ChemicalsController.Instance.getById(s.Split('"')[1]));
                            }
                            else
                            {
                                cAMeta.RelatedChemicalsId.Add(s);
                                cAMeta.chemicals.Add(ChemicalsController.Instance.getById(s));
                            }
                    string[] relatedDiseases = varsa[12].Split('"');

                    foreach (string s in relatedDiseases)
                        if (s != "" && s != ",")
                            if (s.Contains("\""))
                            {
                                if (s.Contains("("))
                                {
                                    string PhenotypeID = s.Split('"')[0].Split('(')[1].Split(')')[0];
                                    cAMeta.RelatedDiseasesId.Add(PhenotypeID);
                                    cAMeta.Phenotypes.Add(PhenotypesController.Instance.getById(PhenotypeID));
                                }
                                else
                                    cAMeta.Phenotypes.Add(PhenotypesController.Instance.getById(s.Split('"')[1]));
                            }
                            else
                            {
                                string PhenotypeID = string.Empty;
                                string[] lista = s.Split('(');
                                foreach (string auxs in lista)
                                    if (auxs.StartsWith("PA"))
                                        PhenotypeID = auxs.Split(')')[0];
                                cAMeta.RelatedDiseasesId.Add(PhenotypeID);
                                Phenotype ph = PhenotypesController.Instance.getById(PhenotypeID);
                                if (ph != null)
                                    cAMeta.Phenotypes.Add(ph);
                                else
                                    Console.WriteLine("Tuvimos problemas con {0}" + s);
                            }
                    cAMeta.BiogeographicalGroups = varsa[13];
                    cAMeta.Chromosome = varsa[14];
                    CAMetaDatas.Add(cAMeta);
                }
            }
            logger.EscribirLog("Cargamos " + CAMetaDatas.Count + " Metadatas", Logger.Tipo.Informativo, true);
            return true;
        }
        private CAMetadaDataController()
        {
            if (String.IsNullOrEmpty(filePath))
                filePath = Environment.CurrentDirectory + @"\Data\clinical_ann_metadata.tsv";
        }
        public static CAMetadaDataController Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
