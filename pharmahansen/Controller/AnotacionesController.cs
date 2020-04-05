using pharmahansen.util;
using pharmahansen.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pharmahansen.Controller
{
    class AnotacionesController
    {
        Logger logger = Logger.Instance;
        List<Anotaciones> LAnotaciones = new List<Anotaciones>();
        GenController genController = GenController.Instance;
        ChemicalsController chemicalsController = ChemicalsController.Instance;
        private readonly static AnotacionesController _instance = new AnotacionesController();
        public String filePath1 { get; set; }
        public String filePath2 { get; set; }
        public String filePath3 { get; set; }

        public Anotaciones getById(int AnotacionID)
        {
            return LAnotaciones.Where(s => s.AnnotationId == AnotacionID).FirstOrDefault();
        }
        public Boolean cargarArchivo()
        {
            logger.EscribirLog("Cargamos las anotaciones de Drugs de PharmaGKB", Logger.Tipo.Informativo, false);
            logger.EscribirLog("Chequeamos si existe el archivo " + filePath1, Logger.Tipo.Informativo, false);
            try
            {
                if (File.Exists(filePath1))
                {
                    logger.EscribirLog("No existe el archivo " + filePath1, Logger.Tipo.Informativo, false);
                    return false;
                }
                foreach (string Anotaciones in File.ReadAllLines(filePath1))
                {
                    string[] varsa = Anotaciones.Split('\t');

                    int AnotationId = 0;
                    if (int.TryParse(varsa[0], out AnotationId))
                    {
                        Anotaciones anotacion = new Anotaciones();
                        anotacion.AnnotationId = AnotationId;
                        anotacion.Variant = varsa[1];
                        string[] genes = varsa[2].Split(',');
                        foreach (string s in genes)
                        {
                            string genid = string.Empty;
                            if (s.Contains("\""))
                            {
                                string aux = s.Split('"')[1];
                                genid = aux.Split('(')[1].Split(')')[0];

                            }
                            else
                            {
                                if (s.Contains(")"))
                                    genid = s.Split('(')[1].Split(')')[0];
                            }
                            if (genid != string.Empty)
                            {
                                anotacion.GeneId.Add(genid);
                                anotacion.Genes.Add(genController.getByID(genid));
                            }
                            else
                                anotacion.Genes.Add(genController.getByID(s));
                        }
                        string[] chemicals = varsa[3].Split(',');
                        foreach (string s in chemicals)
                        {
                            string chemicalid = string.Empty;
                            if (s.Contains("\""))
                            {
                                string aux = s.Split('"')[1];
                                if (aux.Contains(")"))
                                    chemicalid = aux.Split('(')[1].Split(')')[0];

                            }
                            else
                            {
                                if (s.Contains(")"))
                                    chemicalid = s.Split('(')[1].Split(')')[0];

                            }
                            if (chemicalid != string.Empty)
                            {
                                anotacion.ChemicalIDS.Add(chemicalid);
                                anotacion.Chemicals.Add(chemicalsController.getById(chemicalid));
                            }
                            else
                            {
                                anotacion.Chemicals.Add(chemicalsController.getById(s));
                            }
                        }
                        anotacion.PMID = int.Parse(varsa[4]);
                        anotacion.PhenotypeCategory = varsa[5];
                        anotacion.Significance = (varsa[6] == "No") ? false : true;
                        anotacion.Notes = varsa[7];
                        anotacion.Sentence = varsa[8];
                        if (varsa[9] != "")
                        {
                            var stydies = varsa[9].Split(',');
                            foreach (string s in stydies)
                            {
                                if (s.Contains("\""))
                                {
                                    anotacion.StudyParameters.Add(int.Parse(s.Split('"')[1]));
                                }
                                else
                                    anotacion.StudyParameters.Add(int.Parse(s));
                            }
                        }
                        anotacion.Alleles = varsa[10];
                        anotacion.Chromosome = varsa[11];
                        anotacion.Tipo = "Drugs";
                        LAnotaciones.Add(anotacion);
                    }
                }
                logger.EscribirLog("Cargamos " + LAnotaciones.Count + " anotaciones de Drugs", Logger.Tipo.Informativo, true);
            }
            catch(Exception ex)
            {
                logger.EscribirLog("Tuvimos problemas al cargar las Drogas: " + ex.Message, Logger.Tipo.Informativo, false);
                return false;
            }
            try { 
            logger.EscribirLog("Cargamos las anotaciones de Functional Analysis de PharmaGKB", Logger.Tipo.Informativo, false);
            logger.EscribirLog("Chequeamos si existe el archivo " + filePath2, Logger.Tipo.Informativo, false);
            if (File.Exists(filePath1))
            {
                logger.EscribirLog("No existe el archivo " + filePath2, Logger.Tipo.Informativo, false);
                return false;
            }
            
            foreach (string Anotaciones in File.ReadAllLines(filePath2))
            {
                string[] varsa = Anotaciones.Split('\t');

                int AnotationId = 0;
                if (int.TryParse(varsa[0], out AnotationId))
                {
                    Anotaciones anotacion = new Anotaciones();
                    anotacion.AnnotationId = AnotationId;
                    anotacion.Variant = varsa[1];
                    string[] genes = varsa[2].Split(',');
                    foreach (string s in genes)
                    {
                        string genid = string.Empty;
                        if (s.Contains("\""))
                        {
                            string aux = s.Split('"')[1];
                            genid = aux.Split('(')[1].Split(')')[0];

                        }
                        else
                        {
                            if (s.Contains(")"))
                                genid = s.Split('(')[1].Split(')')[0];
                        }
                        if (genid != string.Empty)
                        {
                            anotacion.GeneId.Add(genid);
                            anotacion.Genes.Add(genController.getByID(genid));
                        }
                        else
                            anotacion.Genes.Add(genController.getByID(s));
                    }
                    string[] chemicals = varsa[3].Split(',');
                    foreach (string s in chemicals)
                    {
                        string chemicalid = string.Empty;
                        if (s.Contains("\""))
                        {
                            string aux = s.Split('"')[1];
                            if (aux.Contains(")"))
                                chemicalid = aux.Split('(')[1].Split(')')[0];

                        }
                        else
                        {
                            if (s.Contains(")"))
                                chemicalid = s.Split('(')[1].Split(')')[0];

                        }
                        if (chemicalid != string.Empty)
                        {
                            anotacion.ChemicalIDS.Add(chemicalid);
                            anotacion.Chemicals.Add(chemicalsController.getById(chemicalid));
                        }
                        else
                        {
                            anotacion.Chemicals.Add(chemicalsController.getById(s));
                        }
                    }
                    anotacion.PMID = int.Parse(varsa[4]);
                    anotacion.PhenotypeCategory = varsa[5];
                    anotacion.Significance = (varsa[6] == "No") ? false : true;
                    anotacion.Notes = varsa[7];
                    anotacion.Sentence = varsa[8];
                    if (varsa[9] != "")
                    {
                        var stydies = varsa[9].Split(',');
                        foreach (string s in stydies)
                        {
                            if (s.Contains("\""))
                            {
                                anotacion.StudyParameters.Add(int.Parse(s.Split('"')[1]));
                            }
                            else
                                anotacion.StudyParameters.Add(int.Parse(s));
                        }
                    }
                    anotacion.Alleles = varsa[10];
                    anotacion.Chromosome = varsa[11];
                    anotacion.Tipo = "Functional Analysis";
                    LAnotaciones.Add(anotacion);
                }
            }
            logger.EscribirLog("Cargamos " + LAnotaciones.Where(s => s.Tipo == "Functional Analisys").Count() + " anotaciones de Functional Analysis", Logger.Tipo.Informativo, true);
            }
            catch (Exception ex)
            {
                logger.EscribirLog("Tuvimos problemas al cargar las Function Analisys: " + ex.Message, Logger.Tipo.Informativo, false);
                return false;
            }
            try{ 
            logger.EscribirLog("Cargamos las anotaciones de Fenotipos de PharmaGKB", Logger.Tipo.Informativo, false);
            logger.EscribirLog("Chequeamos si existe el archivo " + filePath3, Logger.Tipo.Informativo, false);
            if (File.Exists(filePath1))
            {
                logger.EscribirLog("No existe el archivo " + filePath3, Logger.Tipo.Informativo, false);
                return false;
            }
            foreach (string Anotaciones in File.ReadAllLines(filePath3))
            {
                string[] varsa = Anotaciones.Split('\t');

                int AnotationId = 0;
                if (int.TryParse(varsa[0], out AnotationId))
                {
                    Anotaciones anotacion = new Anotaciones();
                    anotacion.AnnotationId = AnotationId;
                    anotacion.Variant = varsa[1];
                    string[] genes = varsa[2].Split(',');
                    foreach (string s in genes)
                    {
                        string genid = string.Empty;
                        if (s.Contains("\""))
                        {
                            string aux = s.Split('"')[1];
                            genid = aux.Split('(')[1].Split(')')[0];

                        }
                        else
                        {
                            if (s.Contains(")"))
                                genid = s.Split('(')[1].Split(')')[0];
                        }
                        if (genid != string.Empty)
                        {
                            anotacion.GeneId.Add(genid);
                            anotacion.Genes.Add(genController.getByID(genid));
                        }
                        else
                            anotacion.Genes.Add(genController.getByID(s));
                    }
                    string[] chemicals = varsa[3].Split(',');
                    foreach (string s in chemicals)
                    {
                        string chemicalid = string.Empty;
                        if (s.Contains("\""))
                        {
                            string aux = s.Split('"')[1];
                            if (aux.Contains(")"))
                                chemicalid = aux.Split('(')[1].Split(')')[0];

                        }
                        else
                        {
                            if (s.Contains(")"))
                                chemicalid = s.Split('(')[1].Split(')')[0];

                        }
                        if (chemicalid != string.Empty)
                        {
                            anotacion.ChemicalIDS.Add(chemicalid);
                            anotacion.Chemicals.Add(chemicalsController.getById(chemicalid));
                        }
                        else
                        {
                            anotacion.Chemicals.Add(chemicalsController.getById(s));
                        }
                    }
                    anotacion.PMID = int.Parse(varsa[4]);
                    anotacion.PhenotypeCategory = varsa[5];
                    anotacion.Significance = (varsa[6] == "No") ? false : true;
                    anotacion.Notes = varsa[7];
                    anotacion.Sentence = varsa[8];
                    if (varsa[9] != "")
                    {
                        var stydies = varsa[9].Split(',');
                        foreach (string s in stydies)
                        {
                            if (s.Contains("\""))
                            {
                                anotacion.StudyParameters.Add(int.Parse(s.Split('"')[1]));
                            }
                            else
                                anotacion.StudyParameters.Add(int.Parse(s));
                        }
                    }
                    anotacion.Alleles = varsa[10];
                    anotacion.Chromosome = varsa[11];
                    anotacion.Tipo = "Phenotype";
                    LAnotaciones.Add(anotacion);
                }
            }
            logger.EscribirLog("Cargamos " + LAnotaciones.Where(s => s.Tipo == "Phenotype").Count() + " anotaciones de Fenotipos", Logger.Tipo.Informativo, true);
            return true;
            }
            catch (Exception ex)
            {
                logger.EscribirLog("Tuvimos problemas al cargar los Fenotipos: " + ex.Message, Logger.Tipo.Informativo, false);
                return false;
            }
        }

        private AnotacionesController()
        {
            if (String.IsNullOrEmpty(filePath1))
                filePath1 = Environment.CurrentDirectory + @"\Data\var_drug_ann.tsv";
            if (String.IsNullOrEmpty(filePath2))
                filePath2 = Environment.CurrentDirectory + @"\Data\var_drug_ann.tsv";
            if (String.IsNullOrEmpty(filePath3))
                filePath3 = Environment.CurrentDirectory + @"\Data\var_drug_ann.tsv";
        }
        public static AnotacionesController Instance
        {
            get
            {
                return _instance;
            }
        }
    }

}
