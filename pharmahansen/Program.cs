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
        static List<Indice> indices = new List<Indice>();
        private static System.Timers.Timer aTimer;
        static long anterior = 0;
        static Int64 PosicionS = 0;
        static int procesadas = 0;
        static int totales = 0;
        enum Tipo{
            Informativo,
            Alerta,
            Error,
            Problema,
            Nada
        }

        static void ingresarListado(string[] array)
        {
           
            var charArray = array.SelectMany(x => x.ToCharArray());
            
            string linea = array[chequeoArray(array)];

            string[] aux = linea.Split('\t');
            Indice ind = new Indice();
            ind.chr = aux[0];
            ind.PosicionStart = PosicionS;
            ind.PosicionEnd = int.Parse(aux[1]);
            ind.start = anterior;
            ind.End = anterior + Buffer.ByteLength(charArray.ToArray());
            anterior += Buffer.ByteLength(charArray.ToArray());
            indices.Add(ind);
            PosicionS = int.Parse(aux[1]);
            
        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if(indices.Count > 0)
            Console.Write("\rLlevamos vamos por el chr {0}:{1} en {2}", indices.Last().chr, String.Format("{0:n0}", indices.Last().PosicionEnd) , stopWatch.Elapsed);
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
            
            Dictionary<long, String> test = new Dictionary<long, string>();
            /*List<Indice> salida = new List<Indice>();
            using (Stream stream = File.Open("data.bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                salida = (List<Indice>)bin.Deserialize(stream);
            }
            
           
            Console.WriteLine(salida.Where(s => s.chr == "1").Count());
            Console.WriteLine(salida.Where(s => s.chr == "1" && s.End > 100000 && s.start < 99500).Count());
            IEnumerable<Indice> in1d = salida.Where(s => s.chr == "1" && s.PosicionEnd >= 267356 && s.PosicionStart < 267356);
            string[] arradd = new string[10];

            arradd[0] = "a";
            arradd[1] = "a";
            arradd[2] = "a";
            arradd[3] = "a";
            arradd[4] = "a";
            arradd[5] = "a";
            Console.WriteLine(arradd[arradd.Length - 1]);


            Console.WriteLine(salida.Where(s => s.chr == "1" && s.PosicionEnd > 267357 && s.PosicionStart < 267355).Count());

            
            int indie = Convert.ToInt32( in1d.First().End - in1d.First().start);
           // byte[] byteps = File.ReadAllBytes(@"C:\Datos\All_20180418.vcf");
         //   Console.WriteLine(byteps.Length);
           FileStream fs = new FileStream(@"C:\Datos\All_20180418.vcf", FileMode.Open);
            fs.Seek(in1d.First().start, SeekOrigin.Current);
            
            byte[] buffer1 = new byte[1901077];
            fs.Read(buffer1, 0, 1901077);
            //fs.Read(buffer1, in1d.First().start, indie);
            // fs.Seek(in1d.First().start, SeekOrigin.Begin);
            // fs.SetLength(indie);
            int contado = 0;
            string[] ar = new string[20000];
            int con = 0;
            string aux1 = string.Empty;
            int cona = 0;
            for (int x = 0; x < buffer1.Length; x++)
            {
                if (buffer1[x] != '\n')
                {
                    aux1 += Convert.ToChar( buffer1[x]);
                }
                else
                {
                    ar[cona] = aux1;
                    aux1 = string.Empty;
                    cona++;
                }
            }
            fs.Close();
            /* for(long xi1 = in1d.First().start; xi1< in1d.First().End; xi1++)
             {
                 buffer1[contado] = byteps[xi1];
                 contado++;
             }*/

            // int aux23 = fs.Read(buffer1,0, Convert.ToInt32(indie));

            /*

            using (StreamReader sr = new StreamReader(@"C:\Datos\All_20180418.vcf"))
            {
                long indie = in1d.First().End - in1d.First().start;
                char[] buffer = new char[indie];
                
                char[] chara = new char[indie];
                sr.Read(chara, Convert.ToInt32( in1d.First().start), Convert.ToInt32(indie));
                string[] ar = new string[1000];
                int con = 0;
                string aux = string.Empty;
                int cona = 0;
                for(int x = 0; x< chara.Length;x++)
                {
                    if(chara[x] != '\n')
                    {
                        aux += chara[x];
                    }
                    else
                    {
                        ar[cona] = aux;
                        aux = string.Empty;
                        cona++;
                    }
                }
            }*/

            using (FileStream fsr = new FileStream(@"C:\Datos\All_20180418.vcf", FileMode.Open, FileAccess.Read))
            {
                byte[] buffer1 = new byte[192126];
                fsr.Read(buffer1, 0, 192126);
                int contado = 0;
                string[] ar = new string[20000];
                int con = 0;
                string aux1 = string.Empty;
                int cona = 0;
                for (int x = 0; x < buffer1.Length; x++)
                {
                    if (buffer1[x] != '\n')
                    {
                        aux1 += Convert.ToChar(buffer1[x]);
                    }
                    else
                    {
                        ar[cona] = aux1;
                        aux1 = string.Empty;
                        cona++;
                    }
                }
             }


            aTimer = new System.Timers.Timer(6000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            string[] array = new string[10000];
            int contador = 0;
            int estimador = 0;
            Console.WriteLine("Metodo File");
            Stopwatch filee = new Stopwatch();
            filee.Start();

            string auxs = string.Empty;
            int cantB = 0;
            int paginador = 0;
            byte flag = 10;

            using (FileStream fsb = new FileStream(@"C:\Datos\All_20180418.vcf",FileMode.Open, FileAccess.Read))
            {
                long length = 0;
                int bufint = 100000;
                while (length < fsb.Length)
                {
                    byte[] buffer = new byte[bufint];
                    int b = fsb.Read(buffer, paginador * bufint, bufint);
                    length += buffer.Length;
                    if(buffer.Contains(flag))
                    {
                        for (int x = 0; x < bufint; x++)
                            if (buffer[x] == flag)
                            {
                                contador++;
                                cantB++;
                                if (contador == 1000)
                                {

                                    string[] aux = auxs.Split('\t');
                                    Indice ind = new Indice();
                                    ind.chr = aux[0];
                                    ind.PosicionStart = PosicionS;
                                    Int64 Pose = 0;
                                    Boolean PoseB=Int64.TryParse(aux[1], out Pose);
                                    if(PoseB)
                                        ind.PosicionEnd = Pose;
                                    ind.start = anterior;
                                    ind.End = anterior + cantB;
                                    anterior += cantB;
                                    indices.Add(ind);
                                    PosicionS = Pose;
                                    contador = 0;
                                    cantB = 0;
                                    auxs = string.Empty;
                                }
                            }
                            else
                            {
                                if (contador == 999)
                                    auxs += Convert.ToChar(buffer[x]);
                                if (contador == 998 && (bufint - 1000) < x)
                                {
                                    Boolean pasol = true;
                                    for (int x1 = x; x1 < bufint; x1++)
                                    {
                                        if (buffer[x1] == flag)
                                            pasol = false;

                                    }
                                    if(pasol)
                                    {
                                        auxs += Convert.ToChar(buffer[x]);
                                    }
                                }
                                cantB++;
                            }
                    }
                    
                    //Console.Write(Convert.ToChar(b));
                    /*  if (b != 10)
                      {
                          if(contador == 999)
                              auxs += Convert.ToChar(b);
                          cantB++;
                      }
                      else
                      {
                          contador++;
                          cantB++;
                          if (contador == 1000)
                          {

                              string[] aux = auxs.Split('\t');
                              Indice ind = new Indice();
                              ind.chr = aux[0];
                              ind.PosicionStart = PosicionS;
                              ind.PosicionEnd = int.Parse(aux[1]);
                              ind.start = anterior;
                              ind.End = anterior + cantB;
                              anterior += cantB;
                              indices.Add(ind);
                              PosicionS = int.Parse(aux[1]);
                              contador = 0;
                              cantB = 0;
                              auxs = string.Empty;
                          }


                      }*/
                }
            }

            Console.WriteLine("Terminamos el Sistema File sen {0}", filee.Elapsed);
            using (Stream stream = File.Open("data1.bin", FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();

                bin.Serialize(stream, indices);
            }
            Console.WriteLine("Finalizamos en {0}", filee.Elapsed);

            foreach (string linea in File.ReadLines(@"C:\Datos\All_20180418.vcf"))
            {
                
                if(contador == 1000)
                {
                    // string[] parametros = array;
                    // ingresarListado(array);
                    //var thread = new Thread(
                    //() => ingresarListado(parametros));
                    //thread.Start();
                    //Thread.Sleep(100);
                    // ingresarListado(array);

                    
                   char[] charArray = string.Join(string.Empty, array).ToCharArray();

                    //string linea1 = array[chequeoArray(array)];

                    string[] aux = linea.Split('\t');
                    Indice ind = new Indice();
                    ind.chr = aux[0];
                    ind.PosicionStart = PosicionS;
                    ind.PosicionEnd = int.Parse(aux[1]);
                    ind.start = anterior;
                    ind.End = anterior + Buffer.ByteLength(charArray.ToArray());
                    anterior += Buffer.ByteLength(charArray.ToArray());
                    indices.Add(ind);
                    PosicionS = int.Parse(aux[1]);
                    array = new string[10000];
                    array[0] = linea;
                    contador = 1;
                }
                else
                {
                    estimador += Encoding.ASCII.GetBytes(linea).Length;
                    array[contador] = linea;
                    contador++;
                }
           
            }
            Console.WriteLine("Finalizamos el loop en {0}", filee.Elapsed);
            using (Stream stream = File.Open("data.bin", FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();

                bin.Serialize(stream, indices);
            }
            Console.WriteLine("Finalizamos en {0}", filee.Elapsed);
            filee.Stop();
            array = new string[1000];
             contador = 0;
             anterior = 0;
             PosicionS = 0;
            indices = new List<Indice>();
            Console.WriteLine("Comazamos con el metodo Stream");
            filee.Reset();
            filee.Start();
            /* using (StreamReader sr = new StreamReader(@"C:\Datos\All_20180418.vcf"))
             {
                 while(!sr.EndOfStream)
                 {
                     string linea = sr.ReadLine();
                     if (contador < 1000)
                     {
                         array[contador] = linea;
                         contador++;
                     }
                     else
                     {
                         int buffer = 0;

                         array = new string[1000];
                         contador = 0;
                     }
                 }
             }

             Console.WriteLine("Finalizamos el loop en {0}", filee.Elapsed);
             using (Stream stream = File.Open("data.bin", FileMode.Create))
             {
                 BinaryFormatter bin = new BinaryFormatter();

                 bin.Serialize(stream, indices);
             }
             Console.WriteLine("Finalizamos en {0}", filee.Elapsed);
             */
            Console.WriteLine("nos llevo {0}", stopWatch.Elapsed);
            stopWatch.Stop();
            Console.Read();
        }
        public static void DoWork(object data)
        {
            Console.WriteLine("Static thread procedure. Data='{0}'",
                data);
        }
        
    }
}
