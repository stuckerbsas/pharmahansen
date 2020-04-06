using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace vcfformat
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
        static string output = String.Empty;
        static List<String> salida = new List<string>();
        //enumerador de los tipos de mensajes.
        enum Tipo
        {
            Informativo,
            Alerta,
            Error,
            Problema,
            Nada
        }
        //función para generar el timestamp
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
        //función para mostrar en pantalla y escribir en el archivo log
        static void EscribirLog(String Mensaje, Tipo tipo, bool Detalle)
        {
            if (!Detalle)
            {
                if (tipo != Tipo.Nada)
                {
                    Console.WriteLine("{0}", Mensaje);
                    using (StreamWriter sw = new StreamWriter(NombreLog, true))
                    {
                        sw.WriteLine("{0}\t{1}\t{2}", DateTime.Now, tipo, Mensaje);
                    }
                }
                else
                {
                    Console.WriteLine("", DateTime.Now, tipo, Mensaje);
                    using (StreamWriter sw = new StreamWriter(NombreLog, true))
                    {
                        sw.WriteLine("", DateTime.Now, tipo, Mensaje);
                    }
                }
            }
            else
                if (verbose)
            {
                if (tipo != Tipo.Nada)
                {
                    Console.WriteLine("{0}", Mensaje);
                    using (StreamWriter sw = new StreamWriter(NombreLog, true))
                    {
                        sw.WriteLine("{0}\t{1}\t{2}", DateTime.Now, tipo, Mensaje);
                    }
                }
                else
                {
                    Console.WriteLine("", DateTime.Now, tipo, Mensaje);
                    using (StreamWriter sw = new StreamWriter(NombreLog, true))
                    {
                        sw.WriteLine("", DateTime.Now, tipo, Mensaje);
                    }
                }
            }
        }
        //función para chequear si existe el parametro
        static Boolean chequeoArgunto(string Argumento, bool unico, string[] args)
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
        //función para extrare un argumento de los parametros
        static string traerArgumento(String Argumento, string[] args)
        {
            return args[Array.IndexOf(args, Argumento) + 1];
        }

        //función para escribir en el output
        static void EscribirArchivo(String Linea)
        {
            try
            {
             //   File.AppendText(output).WriteLineAsync(Linea);
                using (StreamWriter sw = new StreamWriter(output,true))
                {
                    sw.WriteLine(Linea);
                }
            }
            catch (Exception ex)
            {
                EscribirLog(ex.Message, Tipo.Error, false);
            }
        }

        static void escribirLinea(String Linea,Boolean reemplazo,String ID, String Ref, String Alt )
        {

            if (reemplazo)
            {
                String[] laux = Linea.Split('\t');
                Linea = laux[0] + "\t" + laux[1] + "\t" + ID +"\t" + Ref +"\t" + Alt + "\t" + laux[5] + "\t" + laux[6] + "\t" + laux[7] + "\t" + laux[8] + "\t" + laux[9];
               // EscribirArchivo(Linea);
                
                
            }
            salida.Add(Linea);
           // else
           //   EscribirArchivo(Linea);

        }

        static void Main(string[] args)
        {

          

            if (args.Length>0)
            {
                if (chequeoArgunto("-log", false, args))
                    NombreLog = traerArgumento("-log", args);
                else
                    NombreLog = "pharmaHansen" + GetTimestamp(DateTime.Now) + ".txt";
                EscribirLog("Vamos a utilizrar el archivo " + NombreLog + " como log.", Tipo.Informativo, false);

                if (chequeoArgunto("-vcf", false, args))
                {
                    vcfFile = traerArgumento("-vcf", args);
                    EscribirLog("Vamos a empezar a trabajar en el archivo " + vcfFile, Tipo.Informativo, false);
                }
                else
                {
                    Console.WriteLine("Falta ingresar el -vcf");
                    EscribirLog("Falta ingresar el parametro de vcf", Tipo.Error, false);
                    System.Environment.Exit(ERROR_INVALID_COMMAND_LINE);
                }
                if (!File.Exists(vcfFile))
                {
                    EscribirLog("No existe el archivo " + vcfFile, Tipo.Alerta, false);
                    System.Environment.Exit(ERROR_FILE_NOT_FOUND);
                }

                if(chequeoArgunto("-out",false,args))
                {
                    output = traerArgumento("-out", args);
                    EscribirLog("La salida se va a escribir en " + output, Tipo.Informativo, false);
                }
                else
                {
                    Console.WriteLine("Falta ingresar el -out");
                    EscribirLog("Falta ingresar el parametro de out", Tipo.Error, false);
                    System.Environment.Exit(ERROR_INVALID_COMMAND_LINE);
                }
                if (File.Exists(output))
                {
                    EscribirLog("Existe el archivo " + output + " se procedera a reemplazarlo.", Tipo.Alerta, false);
                    File.Delete(output);
                }
               

                String[] Lines = File.ReadAllLines(vcfFile);
                foreach(string line in Lines)
                {
                    Boolean flag = true;
                    if (line.StartsWith("#"))
                    {
                        escribirLinea(line,false,"","","");
                    }
                    else
                    {
                        String[] lineaaux = line.Split('\t');
                        int posicion;

                        string formateada = string.Empty;
                        if (lineaaux[5] == "inf")
                        {
                            lineaaux[5] = "100";
                            formateada = line.Replace("inf", "100");
                        }
                        else
                            formateada = line;
                        
                        

                        bool OK = int.TryParse(lineaaux[1],out posicion);
                        if (!OK)
                        {
                            EscribirLog("Tuvimos problemas al procesar la linea " + line, Tipo.Error, false);
                            System.Environment.Exit(ERROR_BAD_FORMAT);
                        }
                        else
                        {
                            switch (lineaaux[0])
                            {
                                case "chr1":
                                    if (lineaaux[9].Split(':')[0] == "0/0")
                                    {
                                        if (posicion == 97450065)
                                        {
                                            escribirLinea(formateada, true, "rs72549303", "TG", "T");
                                            flag = false;
                                        }
                                        if (posicion == 97740414)
                                        {
                                            escribirLinea(formateada, true, "rs72549309", "AAGTA", "A");
                                            flag = false;
                                        }
                                    }
                                    if (flag)
                                        escribirLinea(formateada, false, "", "", "");
                                    break;
                                case "chr2":
                                    if (posicion == 233760233)
                                    {
                                        string[] genotipado = lineaaux[9].Split(':');
                                        if (genotipado[0] == "0/0")
                                        {
                                            escribirLinea(formateada, true, ".", "CAT", "CATAT,C,CATATAT");
                                            flag = false;
                                        }

                                        if (genotipado[0] == "1/1")
                                        {

                                            if (lineaaux[3] == "C" && lineaaux[4] == "CAT")
                                            {
                                                escribirLinea(formateada, true, ".", "CAT", "CATAT,C,CATATAT");
                                                flag = false;
                                            }
                                            if (lineaaux[3] == "CAT" && lineaaux[4] == "C")
                                            {
                                                formateada.Replace("1/1", "2/2");
                                                escribirLinea(formateada, true, ".", "CAT", "CATAT,C,CATATAT");
                                                flag = false;
                                            }
                                            if (lineaaux[3] == "C" && lineaaux[4] == "CATAT")
                                            {
                                                formateada.Replace("1/1", "3/3");
                                                escribirLinea(formateada, true, ".", "CAT", "CATAT,C,CATATAT");
                                                flag = false;
                                            }
                                        }
                                        if (genotipado[0] == "0/1")
                                        {
                                            if (lineaaux[3] == "C" && lineaaux[4] == "CAT")
                                            {
                                                escribirLinea(formateada, true, ".", "CAT", "CATAT,C,CATATAT");
                                                flag = false;
                                            }
                                            if (lineaaux[3] == "CAT" && lineaaux[4] == "C")
                                            {
                                                formateada.Replace("0/1", "0/2");
                                                escribirLinea(formateada, true, ".", "CAT", "CATAT,C,CATATAT");
                                                flag = false;
                                            }
                                            if (lineaaux[3] == "C" && lineaaux[4] == "CATAT")
                                            {
                                                formateada.Replace("0/1", "0/3");
                                                escribirLinea(formateada, true, ".", "CAT", "CATAT,C,CATATAT");
                                                flag = false;
                                            }
                                        }
                                        if (genotipado[0] == "1/2")
                                        {
                                            if (lineaaux[3] == "C" && (lineaaux[4] == "CAT,CATAT" || lineaaux[4] == "CATAT,CAT"))
                                            {
                                                formateada.Replace("1/2", "1/3");
                                                escribirLinea(formateada, true, ".", "CAT", "CATAT,C,CATATAT");
                                                flag = false;
                                            }
                                            if (lineaaux[3] == "CAT" && (lineaaux[4] == "C,CATATAT" || lineaaux[4] == "CATATAT,C"))
                                            {
                                                formateada.Replace("1/2", "2/3");
                                                escribirLinea(formateada, true, ".", "CAT", "CATAT,C,CATATAT");
                                                flag = false;
                                            }
                                            if (lineaaux[3] == "CAT" && (lineaaux[4] == "C,CATAT"|| lineaaux[4] == "CATAT,C"))
                                            {
                                                escribirLinea(formateada, true, ".", "CAT", "CATAT,C,CATATAT");
                                                flag = false;
                                            }
                                        }
                                        
                                        
                                    }
                                    
                                    if (flag)
                                        escribirLinea(formateada, false, "", "", "");
                                    break;
                                case "chr7":
                                    if (lineaaux[9].Split(':')[0] == "0/0")
                                    {
                                        if (posicion == 99652770)
                                        {
                                            escribirLinea(formateada, true, "rs41303343", "T", "TA");
                                            flag = false;
                                        }
                                        if (posicion == 117559589)
                                        {
                                            escribirLinea(formateada, true, "rs121908745", "CATC", "C");
                                            flag = false;
                                        }
                                        if (posicion == 117559590)
                                        {
                                            escribirLinea(formateada, true, "rs199826652", "ATCT", "A");
                                            flag = false;
                                        }
                                        if (posicion == 117559591)
                                        {
                                            escribirLinea(formateada, true, "rs113993960", "TCTT", "T");
                                            flag = false;
                                        }
                                        if (posicion == 117592218)
                                        {
                                            escribirLinea(formateada, true, "rs121908746", "AA", "A");
                                            flag = false;
                                        }
                                        if (posicion == 117627580)
                                        {
                                            escribirLinea(formateada, true, "rs121908747", "CC", "C");
                                            flag = false;
                                        }
                                    }
                                    if (flag)
                                        escribirLinea(formateada, false, "", "", "");
                                    break;
                                case "chr10":
                                    if (lineaaux[9].Split(':')[0] == "0/0")
                                    {
                                        if (posicion == 94942212)
                                        {
                                            escribirLinea(formateada, true, ".", "AAGAAATGGAA", "A");
                                            flag = false;
                                        }
                                        if (posicion == 94949281)
                                        {
                                            escribirLinea(formateada, true, "rs9332131", "GA", "G");
                                            flag = false;
                                        }
                                    }
                                    if (flag)
                                        escribirLinea(formateada, false, "", "", "");
                                    break;
                                case "chr13":
                                    if (lineaaux[9].Split(':')[0] == "0/0")
                                    {
                                        if (posicion == 48037782)
                                        {
                                            escribirLinea(formateada, true, "rs746071566", "AGGAGTC", "A");
                                            flag = false;
                                        }
                                        if (posicion == 48037801)
                                        {
                                            escribirLinea(formateada, true, "rs869320766", "G", "GGAGTCG");
                                            flag = false;
                                        }
                                        if (posicion == 48037826)
                                        {
                                            escribirLinea(formateada, true, "rs777311140", "G", "GCGGG");
                                            flag = false;
                                        }
                                        if (posicion == 48040981)
                                        {
                                            escribirLinea(formateada, true, "rs1457579126", "AA", "A");
                                            flag = false;
                                        }
                                        if (posicion == 48041103)
                                        {
                                            escribirLinea(formateada, true, "rs761191455", "T", "TG");
                                            flag = false;
                                        }
                                    }
                                    if (flag)
                                        escribirLinea(formateada, false, "", "", "");
                                    break;
                                default:
                                    escribirLinea(formateada, false, "", "", "");
                                    break;
                            }
                        }
                    }
                }
                File.AppendAllLines(output, salida);
                byte[] dataAsBytes = salida
                                      .SelectMany(s => System.Text.Encoding.ASCII.GetBytes(s))
                                      .ToArray();
                using (FileStream fs = new FileStream(output + ".gz", FileMode.OpenOrCreate, FileAccess.Write))
                using (GZipStream compress = new GZipStream(fs, CompressionLevel.Optimal))
                    compress.Write(dataAsBytes, 0, dataAsBytes.Length);

                EscribirLog("Finalizamos el formateo. Esta listo para usar en el Pharmcat.", Tipo.Informativo, false);
                System.Environment.Exit(ERROR_INVALID_COMMAND_LINE);
            }
            else
            {
                EscribirLog("Por favor indique el vcf a formatear", Tipo.Alerta, false);
                System.Environment.Exit(ERROR_INVALID_COMMAND_LINE);
            }
            
        }
    }
}
