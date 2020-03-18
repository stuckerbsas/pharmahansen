using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Timers;

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

        static bool verbose = false;
        static string NombreLog = String.Empty;
        static string vcfFile = String.Empty;
        static Stopwatch stopWatch = new Stopwatch();
        enum Tipo{
            Informativo,
            Alerta,
            Error,
            Problema,
            Nada
        }
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
        static void EscribirLog(String Mensaje,Tipo tipo,bool Detalle)
        {
            if (!Detalle)
            {
                if (tipo != Tipo.Nada)
                {
                    Console.WriteLine("{0}",  Mensaje);
                    using (StreamWriter sw = new StreamWriter(NombreLog, true))
                    {
                        sw.WriteLine("{0}\t{1}\t{2}", DateTime.Now, tipo, Mensaje);
                    }
                }
                else
                {
                    Console.WriteLine("", DateTime.Now, tipo, Mensaje);
                    using (StreamWriter sw = new StreamWriter(NombreLog,true))
                    {
                        sw.WriteLine("", DateTime.Now, tipo, Mensaje);
                    }
                }
            }
            else
                if(verbose)
            {
                if (tipo != Tipo.Nada)
                {
                    Console.WriteLine("{0}",  Mensaje);
                    using (StreamWriter sw = new StreamWriter(NombreLog,true))
                    {
                        sw.WriteLine("{0}\t{1}\t{2}", DateTime.Now, tipo, Mensaje);
                    }
                }
                else
                {
                    Console.WriteLine("", DateTime.Now, tipo, Mensaje);
                    using (StreamWriter sw = new StreamWriter(NombreLog,true))
                    {
                        sw.WriteLine("", DateTime.Now, tipo, Mensaje);
                    }
                }
            }
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
        static void Main(string[] args)
        {
            stopWatch.Start();
            if (args.Length == 0)
            {
                NombreLog = "pharmaHansen" + GetTimestamp(DateTime.Now) + ".txt";
                EscribirLog("Por favor indique los parametros a utilizar", Tipo.Error, false);
                EscribirLog("", Tipo.Nada, false);
                EscribirLog("-vf\t(Requerido)\tparametro que indica el archivo que necesitamos usar", Tipo.Error, false);
                EscribirLog("-v\t(Opcional)\tparametro que indica si se desean mas datos", Tipo.Error, false);
                System.Environment.Exit(ERROR_INVALID_COMMAND_LINE);
            }
            else
            {
                if (chequeoArgunto("-log", false, args))
                    NombreLog = traerArgumento("-log", args);
                else
                    NombreLog = "pharmaHansen" + GetTimestamp(DateTime.Now) + ".txt";
                EscribirLog("Vamos a utilizrar el archivo " + NombreLog + " como log.", Tipo.Informativo, false);


                if (chequeoArgunto("-vcf", false, args))
                {
                    vcfFile = traerArgumento("-vcf", args);
                    EscribirLog("Vamos a empezar a trabajar en el archivo " + vcfFile, Tipo.Informativo,false);
                }
                else
                {
                    Console.WriteLine("Falta ingresar el -vcf");
                    EscribirLog("Falta ingresar el parametro de vcf", Tipo.Error,false);
                    System.Environment.Exit(ERROR_INVALID_COMMAND_LINE);
                }
                if (!File.Exists(vcfFile))
                {
                    EscribirLog("No exizte el archivo " + vcfFile, Tipo.Alerta,false);
                    System.Environment.Exit(ERROR_FILE_NOT_FOUND);
                }
                verbose = chequeoArgunto("-v", true, args);
                if (verbose)
                    EscribirLog("Verbose activado se escribira mas detalle", Tipo.Informativo, false);
                String[] lineas = File.ReadAllLines(vcfFile);
                EscribirLog("Vamos a Procesamos " + lineas.Length + " Lineas", Tipo.Informativo, true);
                
            }
            EscribirLog("Finalizamos el proceso", Tipo.Informativo, false);
            EscribirLog("Demoramos " + stopWatch.Elapsed, Tipo.Informativo, true);
            stopWatch.Stop();

        }
    }
}
