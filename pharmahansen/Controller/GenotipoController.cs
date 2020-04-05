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
    class GenotipoController
    {
        Logger logger = Logger.Instance;
        List<Genotipo> Genotipos = new List<Genotipo>();
        public String filePath { get; set; }
        private readonly static GenotipoController _instance = new GenotipoController();

        public Genotipo getById(int GenotipoId)
        {
            return Genotipos.Where(s => s.GenotypePhenotypeID == GenotipoId).FirstOrDefault();
        }

        public Boolean cargarArchivo()
        {

            logger.EscribirLog("Cargamos los Genotipos de PharmaGKB", Logger.Tipo.Informativo, false);
            logger.EscribirLog("Chequeamos si existe el archivo " + filePath, Logger.Tipo.Informativo, false);
            if(!File.Exists(filePath))
            {
                logger.EscribirLog("No existe el archivo " + filePath, Logger.Tipo.Informativo, false);
                return false;
            }

            foreach (string Genotips in File.ReadAllLines(filePath))
            {
                string[] varsa = Genotips.Split('\t');
                int GenotypePhenotypeID = 0;
                if (int.TryParse(varsa[0], out GenotypePhenotypeID))
                {
                    Genotipo clinical_Ann = new Genotipo();
                    clinical_Ann.GenotypePhenotypeID = GenotypePhenotypeID;
                    clinical_Ann.Genotype = varsa[1];
                    clinical_Ann.ClinicalPhenotype = varsa[2];
                    Genotipos.Add(clinical_Ann);
                }
            }
            logger.EscribirLog("Cargamos " + Genotipos.Count + " Genotipos", Logger.Tipo.Informativo, true);
            return true;
        }
        private GenotipoController()
        {
            if (String.IsNullOrEmpty(filePath))
                filePath = Environment.CurrentDirectory + @"\Data\clinical_ann.tsv";
        }
        public static GenotipoController Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
