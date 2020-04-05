using pharmahansen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pharmahansen.util;
using System.IO;

namespace pharmahansen.Controller
{
    class GenController
    {
        static Logger logger = Logger.Instance;
        private readonly static GenController _instance = new GenController();
        List<Gen> Gens = new List<Gen>();

        public String filePath { get; set; }

        public Gen getByID(String GenId)
        {
            return Gens.Where(s => s.PharmGKBAccessionID == GenId).FirstOrDefault();
        }

        public Boolean cargarArchivos()
        {
            try
            {
                logger.EscribirLog("Cargamos los Genes de PharmaGKB", Logger.Tipo.Informativo, false);
                logger.EscribirLog("Chequeando si existe el archivo "+ filePath, Logger.Tipo.Informativo, false);
                if (!File.Exists(filePath))
                {
                    logger.EscribirLog("No existe el archivo " + filePath, Logger.Tipo.Error, false);
                    return false;
                }

                    foreach (string gens in File.ReadAllLines(filePath))
                    {
                        string[] varsa = gens.Split('\t');
                        int NCBIGeneID = 0;
                        if (int.TryParse(varsa[1], out NCBIGeneID))
                        {

                            Gen gen = new Gen();
                            gen.PharmGKBAccessionID = varsa[0];
                            gen.NCBIGeneID = NCBIGeneID;
                            // foreach (String s in varsa[2].Split(','))
                            //   gen.HGNCID = ( != "") ? int.Parse(varsa[2]):0;
                            gen.EnsemblId = varsa[3];
                            gen.Name = varsa[4];
                            gen.Symbol = varsa[5];
                            string[] altes = varsa[6].Split(',');
                            foreach (string s in altes)
                            {
                                if (s.Contains("\""))
                                    gen.AlternateNames.Add(s.Split('"')[1]);
                                else
                                    gen.AlternateNames.Add(s);
                            }
                            string[] altesy = varsa[6].Split(',');
                            foreach (string s in altesy)
                            {
                                if (s.Contains("\""))
                                    gen.AlternateSymbols.Add(s.Split('"')[1]);
                                else
                                    gen.AlternateSymbols.Add(s);
                            }
                            gen.IsVIP = (varsa[8] == "No") ? false : true;
                            gen.HasVariantAnnotation = (varsa[9] == "No") ? false : true;
                            string[] cross = varsa[6].Split(',');
                            foreach (string s in cross)
                            {
                                if (s.Contains("\""))
                                    gen.Crossreferences.Add(s.Split('"')[1]);
                                else
                                    gen.Crossreferences.Add(s);
                            }
                            gen.HasCPICDosingGuideline = (varsa[11] == "No") ? false : true;
                            gen.Chromosome = varsa[12];
                            gen.ChromosomalStart = (varsa[15] != "") ? int.Parse(varsa[15]) : 0;
                            gen.ChromosomalEnd = (varsa[16] != "") ? int.Parse(varsa[16]) : 0;
                            Gens.Add(gen);
                        }
                    }
                logger.EscribirLog("Cargamos " + Gens.Count + " Genes", Logger.Tipo.Informativo, true);
                return true;
            }
            catch(Exception ex)
            {
                logger.EscribirLog("Tuvimos problemas al cargar los genes: " + ex.Message, Logger.Tipo.Error, false);
                return false;
            }
        }
        private GenController() {
            
            if(String.IsNullOrEmpty(filePath))
                filePath = Environment.CurrentDirectory + @"\Data\Genes.tsv";
        }

        public static GenController Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
