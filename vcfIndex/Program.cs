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
using PharmaHansen.Util;
using Newtonsoft.Json;
using ProtoBuf;

namespace vcfIndex
{
    class Program
    {
       
        static string vcfFile = String.Empty;
        static Stopwatch stopWatch = new Stopwatch();
        static HashSet<Indice> indices = new HashSet<Indice>();
        private static System.Timers.Timer aTimer;
        static long anterior = 0;
        static Int64 PosicionS = 0;
        static int procesadas = 0;
        static int totales = 0;
        static Logger logger = Logger.Instance;
      

       
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (indices.Count > 0)
                logger.EscribirLog("Llevamos vamos por el chr " + indices.Last().chrEnd + ":" + String.Format("{0:n0}", indices.Last().PosicionEnd) + " en " + stopWatch.Elapsed, Logger.Tipo.Informativo, true);
        }
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
       
        static int chequeoArray(string[] array)
        {
            int indice = 0;
            for (int x = 0; x < array.Length; x++)
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
                System.Environment.Exit(PharmaHansen.Util.CodigoSalida.ERROR_INVALID_COMMAND_LINE);
            }

            if (Argumentos.chequeoArgunto("-log", false, args))
                logger.NombreLog = Argumentos.traerArgumento("-log", args);

            logger.EscribirLog("Vamos a utilizrar el archivo " + logger.NombreLog + " como log.", Logger.Tipo.Informativo, false);


            if (!PharmaHansen.Util.Files.Exists(args, "vcf", out vcfFile))
                if (vcfFile == String.Empty)
                    System.Environment.Exit(PharmaHansen.Util.CodigoSalida.ERROR_INVALID_COMMAND_LINE);
                else
                    System.Environment.Exit(PharmaHansen.Util.CodigoSalida.ERROR_FILE_NOT_FOUND);
            logger.Verbose = Argumentos.chequeoArgunto("-v", true, args);
                if (logger.Verbose)
                    logger.EscribirLog("Verbose activado se escribira mas detalle", Logger.Tipo.Informativo, false);
                
              

        
            aTimer = new System.Timers.Timer(6000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            int contador = 0;


            string auxs = string.Empty;
            int cantB = 0;
            int paginador = 0;
            byte flag = 10;
            String chrAnterior = string.Empty;
            logger.EscribirLog("Comenzamos a leer el archivo " + vcfFile, Logger.Tipo.Informativo, false);
            using (FileStream fsb = new FileStream(vcfFile, FileMode.Open, FileAccess.Read))
            {
                long length = 0;
                int bufint = 1073741824;
                long tama = fsb.Length;
                Boolean ultino = false;
                long dif = 0;
                while (length < fsb.Length)
                {
                    byte[] buffer = new byte[bufint];
                    int b = fsb.Read(buffer, paginador * bufint, bufint);
                    length += buffer.Length;
                    if (tama - length < bufint)
                    {
                        dif = tama - length;
                        Console.WriteLine(tama - length);
                        ultino = true;
                    }
                    if (buffer.Contains(flag))
                    {
                        for (int x = 0; x < bufint; x++)
                            if (buffer[x] == flag)
                            {
                                contador++;
                                cantB++;
                                
                                if (contador == 1000)
                                {
                                    string[] aux = auxs.Split('\t');
                                    if (chrAnterior == string.Empty)
                                    {
                                        chrAnterior = aux[0];
                                    }
                                    Indice ind = new Indice();
                                    ind.chrStart = chrAnterior;
                                    ind.chrEnd = aux[0];
                                    ind.PosicionStart = PosicionS;
                                    Int64 Pose = 0;
                                    Boolean PoseB = Int64.TryParse(aux[1], out Pose);
                                    if (PoseB)
                                        ind.PosicionEnd = Pose;
                                    ind.ByteStart = anterior;
                                    ind.ByteEnd = anterior + cantB;
                                    anterior += cantB;
                                    indices.Add(ind);
                                    PosicionS = Pose;
                                    contador = 0;
                                    cantB = 0;
                                    auxs = string.Empty;
                                    chrAnterior = aux[0];
                                }
                            }
                            else
                            {
                                if (contador == 999)
                                {
                                    if (buffer[x] == flag)
                                        auxs = String.Empty;
                                    else
                                        auxs += Convert.ToChar(buffer[x]);
                                }
                                if (contador == 998 && (bufint - 1000) < x)
                                {
                                    Boolean pasol = true;
                                    for (int x1 = x; x1 < bufint; x1++)
                                    {
                                        if (buffer[x1] == flag)
                                            pasol = false;

                                    }
                                    if (pasol)
                                    {
                                        auxs += Convert.ToChar(buffer[x]);
                                    }
                                }
                                if(ultino && dif + x < 200)
                                {
                                    for(int x1 = 0; x1<200;x1++)
                                    {
                                        if (buffer[x1] == flag)
                                            auxs = string.Empty;
                                        else
                                            auxs+= Convert.ToChar(buffer[x1]);
                                    }
                                    string[] aux = auxs.Split('\t');
                                    Indice ind = new Indice();
                                    if (chrAnterior == String.Empty)
                                        chrAnterior = aux[0];
                                    ind.chrStart = chrAnterior;
                                    ind.chrEnd = aux[0];
                                    ind.PosicionStart = PosicionS;
                                    Int64 Pose = 0;
                                    Boolean PoseB = Int64.TryParse(aux[1], out Pose);
                                    if (PoseB)
                                        ind.PosicionEnd = Pose;
                                    ind.ByteStart = anterior;
                                    ind.ByteEnd = anterior + cantB;
                                    anterior += cantB;
                                    indices.Add(ind);
                                    PosicionS = Pose;
                                    contador = 0;
                                    cantB = 0;
                                    auxs = string.Empty;
                                    chrAnterior = aux[0];
                                    break;
                                }

                                cantB++;
                            }
                    }

                    
                }
            }
            aTimer.Enabled = false;
            logger.EscribirLog("Terminamos la lectura del Archivo " + vcfFile + " en " + stopWatch.Elapsed, Logger.Tipo.Informativo, false);
            logger.EscribirLog("Comenzamos a grabar el archivo " + vcfFile + ".bin", Logger.Tipo.Informativo, false);


            //File.WriteAllText(vcfFile + ".json", JsonConvert.SerializeObject(indices, Formatting.Indented));
            //using (var file = File.Create(vcfFile + ".bin")) { Serializer.Serialize(file, indices); }
            using (Stream stream = File.Open(vcfFile + ".bin", FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, indices);
               
            }
            logger.EscribirLog("Terminamos de grabar el archivo " + vcfFile + ".bin", Logger.Tipo.Informativo, false);
            logger.EscribirLog("Finalizamos el proceso", Logger.Tipo.Informativo, false);
            logger.EscribirLog("Demoramos " + stopWatch.Elapsed, Logger.Tipo.Informativo, true);
            stopWatch.Stop();
        }
    }
}
 