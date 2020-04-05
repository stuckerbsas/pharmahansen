using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Timers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using pharmahansen.Models;
using pharmahansen.util;
using pharmahansen.Controller;

namespace pharmahansen
{
    class Program
    {
        #region Codigos de salida
        private const int ERROR_SUCCESS = 0;
        private const int ERROR_FILE_NOT_FOUND = 0x2;
        private const int ERROR_PATH_NOT_FOUND = 0x3;
        private const int ERROR_ACCESS_DENIED = 0x5;
        private const int ERROR_BAD_FORMAT = 0xB;
        private const int ERROR_NOT_ENOUGH_MEMORY = 0x8;
        private const int ERROR_INVALID_COMMAND_LINE = 0x667;
        #endregion

        static Logger logger = Logger.Instance;

        static bool verbose = false;
        static string NombreLog = String.Empty;
        static string vcfFile = String.Empty;
        static Stopwatch stopWatch = new Stopwatch();
        static List<Variant> LVariantes = new List<Variant>();
        static List<Gen> LGenes = new List<Gen>();
        static List<Drug> LDrgus = new List<Drug>();
        static List<Chemical> LChemicals = new List<Chemical>();
        static List<Phenotype> LPhenotypes = new List<Phenotype>();
        static List<Genotipo> LClinical_ann = new List<Genotipo>();
        static List<Anotaciones> LAnotaciones = new List<Anotaciones>();
        private static System.Timers.Timer aTimer;
        static long anterior = 0;
        static Int64 PosicionS = 0;
        
        enum Tipo{
            Informativo,
            Alerta,
            Error,
            Problema,
            Nada
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            
        }
       
        static Boolean chequeoArgunto(string Argumento,bool unico, string[] args)
        {
            
            if (unico)
                return Array.IndexOf(args, Argumento) > 0;
            else
            {
                try
                {
                    int inde = Array.IndexOf(args, Argumento);
                    if (args[inde + 1].StartsWith("-"))
                    {
                        return false;
                    }
                    else
                        return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        static string traerArgumento(String Argumento,string[] args)
        {
            return args[Array.IndexOf(args, Argumento) + 1];
        }
        static int chequeoArray(string[] array)
        {
            int indice = 0;
            for(int x= 0; x<array.Length; x++)
            {
                if (array[x] != null)
                    indice = x;
            }
            return indice;
        }

        static void Main(string[] args)
        {
            stopWatch.Start();

            if (args.Length == 0)
            {

                logger.EscribirLog("Por favor indique los parametros a utilizar", Logger.Tipo.Error, false);
                logger.EscribirLog("", Logger.Tipo.Nada, false);
                logger.EscribirLog("-vf\t(Requerido)\tparametro que indica el archivo que necesitamos usar", Logger.Tipo.Error, false);
                logger.EscribirLog("-v\t(Opcional)\tparametro que indica si se desean mas datos", Logger.Tipo.Error, false);
                System.Environment.Exit(ERROR_INVALID_COMMAND_LINE);
            }

            if (chequeoArgunto("-log", false, args))
                logger.NombreLog = traerArgumento("-log", args);
            
            logger.EscribirLog("Vamos a utilizrar el archivo " + logger.NombreLog + " como log.", Logger.Tipo.Informativo, false);


            if (chequeoArgunto("-vcf", false, args))
            {
                vcfFile = traerArgumento("-vcf", args);
                logger.EscribirLog("Vamos a empezar a trabajar en el archivo " + vcfFile, Logger.Tipo.Informativo, false);
            }
            else
            {
                Console.WriteLine("Falta ingresar el -vcf");
                logger.EscribirLog("Falta ingresar el parametro de vcf", Logger.Tipo.Error, false);
                System.Environment.Exit(ERROR_INVALID_COMMAND_LINE);
            }
            if (vcfFile != null)
            {
                if (!File.Exists(vcfFile))
                {
                    logger.EscribirLog("No exizte el archivo " + vcfFile, Logger.Tipo.Alerta, false);
                    System.Environment.Exit(ERROR_FILE_NOT_FOUND);
                }
            }
            logger.Verbose = chequeoArgunto("-v", true, args);
            if (verbose)
                logger.EscribirLog("Verbose activado se escribira mas detalle", Logger.Tipo.Informativo, false);

            GenController genController = GenController.Instance;
            if (!genController.cargarArchivos())
                System.Environment.Exit(ERROR_FILE_NOT_FOUND);

            VariantController variantController = VariantController.Instance;
            if (!variantController.cargarArchivo())
                System.Environment.Exit(ERROR_FILE_NOT_FOUND);
            DrugController drugController = DrugController.Instance;
            if (!drugController.cargarArchivo())
                System.Environment.Exit(ERROR_FILE_NOT_FOUND);

            ChemicalsController chemicalsController = ChemicalsController.Instance;
            if (!chemicalsController.cargarArchivo())
                System.Environment.Exit(ERROR_FILE_NOT_FOUND);
            PhenotypesController phenotypesController = PhenotypesController.Instance;
            if (!phenotypesController.cargarArchivo())
                System.Environment.Exit(ERROR_FILE_NOT_FOUND);
            GenotipoController genotipoController = GenotipoController.Instance;
            if (!genotipoController.cargarArchivo())
                System.Environment.Exit(ERROR_FILE_NOT_FOUND);
            CAMetadaDataController cAMetadaDataController = CAMetadaDataController.Instance;
            if (!cAMetadaDataController.cargarArchivo())
                System.Environment.Exit(ERROR_FILE_NOT_FOUND);

            


            IEnumerable<Gen> Guideline = LGenes.Where(s => s.HasCPICDosingGuideline == true);
            int cantidadHAV = Guideline.Count();
            


            aTimer = new System.Timers.Timer(6000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            logger.EscribirLog("Finalizamos el proceso", Logger.Tipo.Informativo, false);
            logger.EscribirLog("Demoramos " + stopWatch.Elapsed, Logger.Tipo.Informativo, true);
            stopWatch.Stop();
            Console.Read();
        }

        
    }
}
