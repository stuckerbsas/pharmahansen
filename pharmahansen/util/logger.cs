using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pharmahansen.util
{
    class Logger
    {
        public enum Tipo
        {
            Informativo,
            Alerta,
            Error,
            Problema,
            Nada
        }
        private String _NombreLog;

        public String NombreLog
        {
            get { return _NombreLog; }
            set { _NombreLog = value; }
        }

        private bool _verbose;

        public bool Verbose
        {
            get { return _verbose; }
            set { _verbose = value; }
        }
        private String _prefijo = "Log_";
        public String Prefijo {
            get { return _prefijo; }
            set { _prefijo = value; }
        }
        public static String GetTimestamp()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }

       
        private readonly static Logger _instance = new Logger();

        private Logger()
        {  
            if (String.IsNullOrEmpty(NombreLog))
                crearNombre();

        }

        public static Logger Instance
        {
            get
            {
                return _instance;
            }
        }
        public void crearNombre()
        {
            NombreLog = String.Concat(Prefijo, GetTimestamp(), ".txt");
        }

        public void EscribirLog(String Mensaje, Tipo tipo, bool Detalle)
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
                if (Verbose)
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
    }
}
