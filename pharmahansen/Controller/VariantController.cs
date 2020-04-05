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
    class VariantController
    {
        static Logger logger = Logger.Instance;

        List<Variant> LVariantes = new List<Variant>();
        private readonly static VariantController _instance = new VariantController();
        public String filePath { get; set; }

        public String getPosicionByRS(String Rsid)
        {
            string posiciones = String.Empty;
            Variant variant = LVariantes.Where(s => s.VariantName == Rsid).FirstOrDefault();
            if (variant != null)
                if (String.IsNullOrEmpty(variant.GH38Chr))
                    logger.EscribirLog("Tenemos problemas con el siguiente rsid " + Rsid, Logger.Tipo.Error, false);
                else
                    posiciones = variant.GH38Chr + "\t" + variant.GH38Pos;
            else
                logger.EscribirLog("Tenemos problemas con el siguiente rsid " + Rsid,Logger.Tipo.Error,false );
           
            return posiciones;
        }

        public Boolean cargarArchivo()
        {
            logger.EscribirLog("Cargamos las variantes de PharmaGKB", Logger.Tipo.Informativo, false);
            try
            {
                logger.EscribirLog("Chequeamos si existe el archivo " + filePath, Logger.Tipo.Informativo, false);
                if(!File.Exists(filePath))
                {
                    logger.EscribirLog("No existe el archivo " + filePath, Logger.Tipo.Informativo, false);
                    return false;
                }
                foreach (string vars in File.ReadAllLines(filePath))
                {
                    GenController genController = GenController.Instance;
                    string[] varsa = vars.Split('\t');
                    int VariantAnnotationcount = 0;
                    if (int.TryParse(varsa[5], out VariantAnnotationcount))
                    {

                        Variant variante = new Variant();
                        variante.VariantID = varsa[0];
                        variante.VariantName = varsa[1];
                        string[] genes = varsa[2].Split(',');
                        foreach(string s in genes)
                        {
                            variante.GeneGeneIDs.Add(s);
                            variante.Genes.Add(genController.getByID(s));
                        }
                        
                       // variante.GeneSymbols = varsa[3];
                        variante.Location = varsa[4];
                        variante.VariantAnnotationcount = VariantAnnotationcount;
                        variante.ClinicalAnnotationcount = int.Parse(varsa[6]);
                        variante.Level12ClinicalAnnotationcount = int.Parse(varsa[7]);
                        variante.GuidelineAnnotationcount = int.Parse(varsa[8]);
                        variante.LabelAnnotationcount = int.Parse(varsa[9]);
                        String[] Synonyms = varsa[10].Split(',');
                        foreach (String s in Synonyms)
                        {
                            variante.Synonyms.Add(s);
                            if(s.Contains("GRCh38"))
                            {
                                variante.GH38Pos = s.Split(':')[1];
                                variante.GH38Chr = s.Split(':')[0].Split(']')[1];
                            }
                            if (s.Contains("GRCh37"))
                            {
                                variante.GH37Pos = s.Split(':')[1];
                                variante.GH37Chr = s.Split(':')[0].Split(']')[1];
                            }
                        }
                        LVariantes.Add(variante);
                    }

                }
                logger.EscribirLog("Cargamos " + LVariantes.Count + " variantes", Logger.Tipo.Informativo, true);
                return true;
            }
            catch(Exception ex)
            {
                logger.EscribirLog("Tuvimos un problema al cargar las variantes: " + ex.Message, Logger.Tipo.Error, false);
                return false;
            }
        }



        private VariantController()
        {
            if(String.IsNullOrEmpty(filePath))
                filePath = Environment.CurrentDirectory + @"\Data\variants.tsv";
            
        }
        public static VariantController Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
