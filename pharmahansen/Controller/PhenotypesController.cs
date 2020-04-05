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
    class PhenotypesController
    {
        Logger logger = Logger.Instance;
        private readonly static PhenotypesController _instance = new PhenotypesController();
        List<Phenotype> LPhenotypes = new List<Phenotype>();
        public String filePath { get; set; }
        public Phenotype getById(String PhenotypeId)
        {
            return LPhenotypes.Where(s => s.PharmGKBAccessionId == PhenotypeId).FirstOrDefault();
        }
        public Phenotype getByName(String Name)
        {
            return LPhenotypes.Where(s => s.Name.Contains( Name)).FirstOrDefault();
        }
        public Boolean cargarArchivo()
        {
            logger.EscribirLog("Cargamos los Fenotipos de PharmaGKB", Logger.Tipo.Informativo, false);
            logger.EscribirLog("Chequeamos si existe el Archivo " + filePath, Logger.Tipo.Informativo, false);
            if(!File.Exists(filePath))
            {
                logger.EscribirLog("No existe el Archivo " + filePath, Logger.Tipo.Informativo, false);
                return false;
            }
            foreach (string Phenotypes in File.ReadAllLines(filePath))
            {
                string[] varsa = Phenotypes.Split('\t');

                if (varsa[0] != "PharmGKB Accession Id")
                {
                    Phenotype phenotype = new Phenotype();
                    phenotype.PharmGKBAccessionId = varsa[0];
                    phenotype.Name = varsa[1];
                    string[] AlternateNames = varsa[2].Split(',');
                    foreach (String s in AlternateNames)
                        if (s.Contains("\""))
                            phenotype.AlternateNames.Add(s.Split('"')[1]);
                        else
                            phenotype.AlternateNames.Add(s);
                    string[] CrossReferences = varsa[3].Split(',');
                    foreach (String s in CrossReferences)
                        if (s.Contains("\""))
                            phenotype.CrossReferences.Add(s.Split('"')[1]);
                        else
                            phenotype.CrossReferences.Add(s);
                    string[] ExtenralVocabulary = varsa[4].Split(',');
                    foreach (String s in ExtenralVocabulary)
                        if (s.Contains("\""))
                            phenotype.ExtenralVocabulary.Add(s.Split('"')[1]);
                        else
                            phenotype.ExtenralVocabulary.Add(s);
           
                    LPhenotypes.Add(phenotype);
                }
            }
            logger.EscribirLog("Cargamos " + LPhenotypes.Count + " Fenotipos", Logger.Tipo.Informativo, true);
            return true;
        }
        private PhenotypesController()
        {
            if (String.IsNullOrEmpty(filePath))
                filePath = Environment.CurrentDirectory + @"\Data\phenotypes.tsv";
        }

        public static PhenotypesController Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
