using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using PharmaHansen.Util;



namespace vcfAnotaciones
{
    public class Program
    {
        static Logger logger = Logger.Instance;
        static string vcfFile = String.Empty;
        static string dbSNP = String.Empty;
        static List<Indice> indices = null;
        static Cromozoma cromozoma1 = new Cromozoma();
        static Cromozoma cromozoma2 = new Cromozoma();
        static Cromozoma cromozoma3 = new Cromozoma();
        static Cromozoma cromozoma4 = new Cromozoma();
        static Cromozoma cromozoma5 = new Cromozoma();
        static Cromozoma cromozoma6 = new Cromozoma();
        static Cromozoma cromozoma7 = new Cromozoma();
        static Cromozoma cromozoma8 = new Cromozoma();
        static Cromozoma cromozoma9 = new Cromozoma();
        static Cromozoma cromozoma10 = new Cromozoma();
        static Cromozoma cromozoma11 = new Cromozoma();
        static Cromozoma cromozoma12 = new Cromozoma();
        static Cromozoma cromozoma13 = new Cromozoma();
        static Cromozoma cromozoma14 = new Cromozoma();
        static Cromozoma cromozoma15 = new Cromozoma();
        static Cromozoma cromozoma16 = new Cromozoma();
        static Cromozoma cromozoma17 = new Cromozoma();
        static Cromozoma cromozoma18 = new Cromozoma();
        static Cromozoma cromozoma19 = new Cromozoma();
        static Cromozoma cromozoma20 = new Cromozoma();
        static Cromozoma cromozoma21 = new Cromozoma();
        static Cromozoma cromozoma22 = new Cromozoma();
        static Cromozoma cromozomaX = new Cromozoma();
        static Cromozoma cromozomaY = new Cromozoma();
        static HashSet<Indice> CHRMIX = new HashSet<Indice>();
        static HashSet<Indice> CHROTHER = new HashSet<Indice>();
        static Stopwatch stopWatch = new Stopwatch();
        private static System.Timers.Timer aTimer;
        static int encontrados = 0;
        static String[] Data;
        static int LineaI = 0;
        static String[] aux;
        static int contador = 0;
        static HashSet<Posicion> Datos = new HashSet<Posicion>();
        static Boolean procesando = false;
        static int multi = 10000000;
        static int saturados = 0;
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            int con = 0;
            /*   foreach (String s in Data)
                    if (s != null)
                        con++;*/
            if(!procesando)
                Console.WriteLine("LLevamos {0} de {2} en {1} elementos saturados {3}", LineaI, stopWatch.Elapsed, encontrados, saturados);
            else
                Console.WriteLine("Procesados {0} de {2} en {1} elementos saturados {3}", LineaI, stopWatch.Elapsed, encontrados, saturados);

            //if (indices != null)
            //    logger.EscribirLog("Llevamos vamos por el chr " + indices.Last().chr + ":" + String.Format("{0:n0}", indices.Last().PosicionEnd) + " en " + stopWatch.Elapsed, Logger.Tipo.Informativo, true);
        }
        private static async Task TaskAsync(String start, String ID)
        {
            await Task.Delay(0).ConfigureAwait(false);
            foreach (string s in aux)
            {

            }
            //if (Array.Exists(aux, element => element.StartsWith(start)))
            //{
            //    Console.WriteLine("Encontramos uno");
            //    Data[contador] = ID;
            //    contador++;
            //}
        }

        public static void comparar(String start, String ID)
        {

            //string[] xxx = auxs.Split('\t');
            //string acomparar = "chr" + xxx[0] + "\t" + xxx[1];

            

            if (Array.Exists(aux, element => element.Contains(start)))
            {
                Console.WriteLine("Encontramos uno");
                Data[contador] = ID;
                contador++;
            }

        }
        private static void comparar1(Posicion pos)
        {

              
            Indice sutbotal = null;
            switch(pos.CHR)
            {
                case "chr1":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma1.BuscarItem(pos);
                    break;
                case "chr2":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma2.BuscarItem(pos);
                    break;
                case "chr3":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma3.BuscarItem(pos);
                    break;
                case "chr4":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma4.BuscarItem(pos);
                    break;
                case "chr5":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma5.BuscarItem(pos);
                    break;
                case "chr6":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma6.BuscarItem(pos);
                    break;
                case "chr7":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma7.BuscarItem(pos);
                    break;
                case "chr8":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma8.BuscarItem(pos);
                    break;
                case "chr9":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma9.BuscarItem(pos);
                    break;
                case "chr10":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma10.BuscarItem(pos);
                    break;
                case "chr11":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma11.BuscarItem(pos);
                    break;
                case "chr12":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma12.BuscarItem(pos);
                    break;
                case "chr13":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma13.BuscarItem(pos);
                    break;
                case "chr14":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma14.BuscarItem(pos);
                    break;
                case "chr15":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma15.BuscarItem(pos);
                    break;
                case "chr16":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma16.BuscarItem(pos);
                    break;
                case "chr17":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma17.BuscarItem(pos);
                    break;
                case "chr18":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma18.BuscarItem(pos);
                    break;
                case "chr19":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma19.BuscarItem(pos);
                    break;
                case "chr20":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma20.BuscarItem(pos);
                    break;
                case "chr21":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma21.BuscarItem(pos);
                    break;
                case "chr22":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozoma22.BuscarItem(pos);
                    break;
                case "chrY":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozomaY.BuscarItem(pos);
                    break;
                case "chrX":
                    // sutbotal = CHR1.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart);
                    sutbotal = cromozomaX.BuscarItem(pos);
                    break;
            }
                
            HashSet<string> data = new HashSet<String>();

            if (sutbotal != null)
                ponerla(pos, sutbotal, 0);
            else
                Console.WriteLine("Tenemos problema con la posicion {0} del cromozoma {1}", pos.Position, pos.CHR);

            
            
            
        }

        public static void ponerla(Posicion pos, Indice ind,int intentos)
        {
            if (intentos > 0)
                return;

            try
            {
                ind.posicions.Push(pos);
                LineaI++;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  
               // Console.WriteLine("Intento {0} de Pos {1}:{2} ", intentos, pos.CHR, pos.Position);
                int inte = intentos++;
                saturados++;
                ponerla(pos, ind, inte);
            }
        }

        public static void leerArchivo(Indice ind)
        {
            byte flag = 10;
            String auxs = String.Empty;
            using (FileStream fs = new FileStream(dbSNP, FileMode.Open, FileAccess.Read))
            {
                int cantidad = (int)(ind.ByteEnd - ind.ByteStart);
                byte[] buffer = new byte[cantidad];
                fs.Seek(ind.ByteStart, SeekOrigin.Begin);

                fs.Read(buffer, 0, cantidad);
                string au = string.Empty;
                int step = 0;
                string id = string.Empty;
                string gref = string.Empty;
                string galt = string.Empty;
                foreach (byte b in buffer)
                {
                    if (b == 10)
                    {
                        int posicion = int.Parse(au);
                        foreach (Posicion pos in ind.posicions)
                            if (pos.ID == null)
                            {
                                if (posicion == pos.Position)
                                {
                                    pos.ID = id;
                                    pos.galt = galt;
                                    pos.gref = gref;
                                }
                            }
                        au = string.Empty;
                        id = string.Empty;
                        gref = string.Empty;
                        galt = string.Empty;
                        step = 0;
                    }
                    else
                    {
                        if (step < 5)
                        {
                            char c = Convert.ToChar(b);
                            if (c == '\t')
                                step++;
                            if (step == 1 && c != '\t')
                                au += c;
                            if(step == 2 && c != '\t')
                                id += c;
                            if(step == 3 && c != '\t')
                                gref += c;
                            if (step == 4 && c != '\t')
                                galt += c;


                        }

                    }
                }
            }
            foreach(Posicion pos in ind.posicions)
            {
                Datos.Add(pos);
                LineaI++;
            }
        }
        static void Main(string[] args)
        {
            stopWatch.Start();
            disclaimer(args);

            logger.EscribirLog("Cargamos los indices de " + dbSNP, Logger.Tipo.Informativo, true);
            aTimer = new System.Timers.Timer(6000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            
            using (Stream stream = File.Open(dbSNP + ".bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();

                indices = (List<Indice>)bin.Deserialize(stream);
            }

            Console.WriteLine("");
            logger.EscribirLog("Cargamos " + indices.Count() + " de indices de " + dbSNP, Logger.Tipo.Informativo, true);
            logger.EscribirLog("Se cargaron con existo los indices de " + dbSNP, Logger.Tipo.Informativo, false);


          
            

            foreach (Indice i in indices)
            {
                i.posicions = new ConcurrentStack<Posicion>();
                switch (i.chrStart)
                {
                    case "1":

                        if (i.chrStart != i.chrEnd)
                        {
                            CHRMIX.Add(i);
                        }
                        else
                        {
                            cromozoma1.AgregarItem(i);
                        }
                        break;
                    case "2":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma2.AgregarItem(i);
                        break;
                    case "3":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma3.AgregarItem(i);
                        break;
                    case "4":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma4.AgregarItem(i);
                        break;
                    case "5":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma5.AgregarItem(i);
                        break;
                    case "6":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma6.AgregarItem(i);
                        break;
                    case "7":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma7.AgregarItem(i);
                        break;
                    case "8":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma8.AgregarItem(i);
                        break;
                    case "9":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma9.AgregarItem(i);
                        break;
                    case "10":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma10.AgregarItem(i);
                        break;
                    case "11":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma11.AgregarItem(i);
                        break;
                    case "12":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma12.AgregarItem(i);
                        break;
                    case "13":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma13.AgregarItem(i);
                        break;
                    case "14":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma14.AgregarItem(i);
                        break;
                    case "15":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma15.AgregarItem(i);
                        break;
                    case "16":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma16.AgregarItem(i);
                        break;
                    case "17":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma17.AgregarItem(i);
                        break;
                    case "18":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma18.AgregarItem(i);
                        break;
                    case "19":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma19.AgregarItem(i);
                        break;
                    case "20":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma20.AgregarItem(i);
                        break;
                    case "21":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma21.AgregarItem(i);
                        break;
                    case "22":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozoma22.AgregarItem(i);
                        break;
                    case "X":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozomaX.AgregarItem(i);
                        break;
                    case "Y":
                        if (i.chrStart != i.chrEnd)
                            CHRMIX.Add(i);
                        else
                            cromozomaY.AgregarItem(i);
                        break;
                    default:
                        CHROTHER.Add(i);
                        break;
                }
            }
            indices.Clear();
            aTimer.Enabled = true;
            aux = File.ReadAllLines(vcfFile);
            HashSet<String> Encabezados = new HashSet<string>();
            HashSet<Task> tareas = new HashSet<Task>();

            foreach (String s in aux)
            {
                if (s.StartsWith("#"))
                    Encabezados.Add(s);
                else
                {
                    string[] asa = s.Split('\t');
                    Posicion pos = new Posicion();
                    pos.CHR = asa[0];
                    pos.Position = int.Parse(asa[1]);
                    for (int x = 3; x < asa.Length; x++)
                        pos.DataA.Add(asa[x]);
                    Datos.Add(pos);
                    encontrados++;
                }
            }

            Console.WriteLine("Empiezar");
            foreach (Posicion pos in Datos)
            {
                  tareas.Add(Task.Factory.StartNew(() => comparar1(pos)));
                //tareas.Add(Task.Run(() => comparar1(pos)));

            }
            Task.WaitAll(tareas.ToArray());
            saturados = 0;
            LineaI = 0;
            HashSet<Indice> HSIndices = new HashSet<Indice>();

            foreach (Indice ind in cromozoma1.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma2.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma3.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma4.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma5.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma6.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma7.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma8.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma9.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma10.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma11.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma12.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma13.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma14.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma15.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma16.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma17.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma18.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma19.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma20.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma21.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozoma22.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozomaX.GetIndices())
                HSIndices.Add(ind);
            foreach (Indice ind in cromozomaY.GetIndices())
                HSIndices.Add(ind);
            tareas = new HashSet<Task>();
            Console.WriteLine(Datos.Count);
            Datos.Clear();
            int cotador = 0;
            foreach (Indice ind in HSIndices)
            {
                cotador += ind.posicions.Count();
                tareas.Add(Task.Factory.StartNew(() => leerArchivo(ind)));
            }
            Console.WriteLine(cotador);
            Task.WaitAll(tareas.ToArray());
            Console.WriteLine(Datos.Count);
            Console.WriteLine("Final validar horario");
            
            int contador = 0;
            Datos.TrimExcess();
            string[] Data = new string[Datos.Count];
            foreach(Posicion pos in Datos)
            {
                string pose = ".";
                string gref = (String.IsNullOrEmpty(pos.gref)) ? pos.DataA[0] : pos.gref;
                string galt = (String.IsNullOrEmpty(pos.galt)) ? pos.DataA[1] : pos.galt;
                if (pos.ID != null)
                    pose = pos.ID;
                Data[contador] = pos.CHR + "\t" + pos.Position + "\t" + pose + "\t" + gref + "\t" + galt;
                for(int x = 2; x < pos.DataA.Count(); x++)
                    Data[contador] += "\t" + pos.DataA[x];
                contador++;

            }

            String[] nombre = vcfFile.Split('.');
            string salida = string.Empty;
            for (int x = 0; x < nombre.Length - 1; x++)
                salida += nombre[x] + ".";
            salida += "PH.vcf";

            if (File.Exists(salida))
                File.Delete(salida);
            File.AppendAllLines(salida, Encabezados.ToArray());
            File.AppendAllLines(salida, Data);


            Task.WaitAll(tareas.ToArray());
            aTimer.Enabled = false;

            //using (FileStream fsb = new FileStream(dbSNP, FileMode.Open, FileAccess.Read))
            //{
            //    long length = 0;
            //    int bufint = 1073741824;
            //    long tama = fsb.Length;
            //    Boolean ultino = false;
            //    long dif = 0;
            //    while (length < fsb.Length)
            //    {
            //        byte[] buffer = new byte[bufint];
            //        int b = fsb.Read(buffer, paginador * bufint, bufint);
            //        length += buffer.Length;

            //        if (buffer.Contains(flag))
            //        {
            //            for (int x = 0; x < bufint; x++)
            //                if (buffer[x] == flag)
            //                {
            //                    if (!auxs.StartsWith("#"))
            //                    {



            //                        /*foreach (String s in aux)
            //                        {
            //                            if (s.StartsWith(acomparar))
            //                            {
            //                                b1 = true;
            //                                break;
            //                            }

            //                            if (b1)
            //                            {
            //                                Console.WriteLine("Encontramos uno");
            //                                auxs = string.Empty;
            //                            }
            //                            else
            //                                auxs = string.Empty;
            //                        }*/
            //                        Task task1 = Task.Factory.StartNew(() => comparar(auxs, id));
            //                        //var t = new Thread(() => comparar(auxs,id));
            //                        //t.IsBackground = true;
            //                        //t.Start();
            //                        //Task t1 = TaskAsync(auxs, id); 
            //                        auxs = string.Empty;
            //                            id = string.Empty;
            //                            step = 0;

            //                    }
            //                    else
            //                    {
            //                        auxs = string.Empty;
            //                        id = string.Empty;
            //                        step = 0;
            //                    }
            //                    LineaI++;
            //                }
            //                else
            //                {
            //                    if(step < 3)
            //                        if(step <2)
            //                            auxs += Convert.ToChar(buffer[x]);
            //                        else
            //                            id += Convert.ToChar(buffer[x]);
            //                    if (Convert.ToChar(buffer[x]) == '\t')
            //                        step++;
            //                }
            //        }


            //    }
            //}

            /* for (int x =0; x< aux.Length; x++)
             {
                 if(!aux[x].StartsWith("#"))
                 {
                     string[] linea = aux[x].Split('\t');
                     string cromo = (prefijo) ? linea[0] : linea[0].Substring(3);
                     IEnumerable <Indice> indices1 = ind.Where(s1 => (int.Parse(linea[1]) < s1.PosicionEnd  && int.Parse(linea[1]) > s1.PosicionStart) && (s1.chrStart == cromo || s1.chrEnd == cromo));
                     // Console.WriteLine(indices1.Count());
                     if (indices1.Count() > 1)
                     {
                         Console.WriteLine("Tenemos problemas");
                     }
                     else
                          if (indices1.Count() == 1)
                     {
                         using (FileStream fs = new FileStream(dbSNP, FileMode.Open, FileAccess.Read))
                         {
                             int cantidad = (int)(indices1.First().ByteEnd - indices1.First().ByteStart);
                             byte[] buffer = new byte[cantidad];
                             fs.Seek(indices1.First().ByteStart,SeekOrigin.Begin);

                             fs.Read(buffer, 0, cantidad);
                             string au = string.Empty;
                             foreach(byte b in buffer)
                             {
                                 if (b == 10)
                                 {
                                     if(au.Contains(linea[1]))
                                     {
                                         aux[x] = linea[0] + "\t" + linea[1] + "\t" + au.Split('\t')[2];
                                         for (int x1 = 3; x1 < linea.Length; x1++)
                                         {
                                             aux[x] += "\t" + linea[x1];
                                         }
                                         break;
                                     }
                                     au = string.Empty;
                                 }
                                 else
                                 {
                                     au += Convert.ToChar(b);
                                 }
                             }
                         }

                     }


               }
             }*/
            Console.WriteLine(stopWatch.Elapsed);
        }

        
        private static void disclaimer(string[] args)
        {
            if (args.Length == 0)
            {

                logger.EscribirLog("Por favor indique los parametros a utilizar", Logger.Tipo.Error, false);
                logger.EscribirLog("", Logger.Tipo.Nada, false);
                logger.EscribirLog("-vf\t(Requerido)\tparametro que indica el archivo que necesitamos usar", Logger.Tipo.Error, false);
                logger.EscribirLog("-v\t(Opcional)\tparametro que indica si se desean mas datos", Logger.Tipo.Error, false);
                System.Environment.Exit(PharmaHansen.Util.CodigoSalida.ERROR_BAD_FORMAT);
            }
            if (Argumentos.chequeoArgunto("-log", false, args))
                logger.NombreLog = Argumentos.traerArgumento("-log", args);

            logger.EscribirLog("Vamos a utilizrar el archivo " + logger.NombreLog + " como log.", Logger.Tipo.Informativo, false);


            if (!PharmaHansen.Util.Files.Exists(args, "vcf", out vcfFile))
                if (vcfFile == String.Empty)
                    System.Environment.Exit(PharmaHansen.Util.CodigoSalida.ERROR_INVALID_COMMAND_LINE);
                else
                    System.Environment.Exit(PharmaHansen.Util.CodigoSalida.ERROR_FILE_NOT_FOUND);
            if (!PharmaHansen.Util.Files.Exists(args, "dbSNP",out dbSNP))
                if(dbSNP == String.Empty)
                    System.Environment.Exit(PharmaHansen.Util.CodigoSalida.ERROR_INVALID_COMMAND_LINE);
                else
                    System.Environment.Exit(PharmaHansen.Util.CodigoSalida.ERROR_FILE_NOT_FOUND);
            logger.EscribirLog("Chequeamos los indices de " + dbSNP, Logger.Tipo.Informativo, false);
            if(!File.Exists(dbSNP + ".bin"))
            {
                logger.EscribirLog("No se encontro el indice para el archivo" + dbSNP + " por favor ejecutar el comando vcfindex.exe -vcf " + dbSNP, Logger.Tipo.Error, false);
                System.Environment.Exit(PharmaHansen.Util.CodigoSalida.ERROR_FILE_NOT_FOUND);
            }
            logger.Verbose = Argumentos.chequeoArgunto("-v", true, args);
            if (logger.Verbose)
                logger.EscribirLog("Verbose activado se escribira mas detalle", Logger.Tipo.Informativo, false);
        }
    }
}
