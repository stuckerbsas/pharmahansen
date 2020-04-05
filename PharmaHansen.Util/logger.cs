using System;
using System.IO;

namespace PharmaHansen.Util
{
    public class Logger
    {
        public enum codigoSalida
        {
            ERROR_SUCCESS = 0,
            ERROR_FILE_NOT_FOUND = 0x2,
            ERROR_PATH_NOT_FOUND = 0x3,
            ERROR_ACCESS_DENIED = 0x5,
            ERROR_BAD_FORMAT = 0xB,
            ERROR_NOT_ENOUGH_MEMORY = 0x8,
            ERROR_INVALID_COMMAND_LINE = 0x667
        }
        public enum Tipo
        {
            Informativo,
            Alerta,
            Error,
            Problema,
            Nada
        }
        private String _NombreLog;

        public  String NombreLog
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
        public  String GetTimestamp()
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
        public   void crearNombre()
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
