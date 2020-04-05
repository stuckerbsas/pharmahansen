using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PharmaHansen.Util
{
    public class Files
    {
        static Logger logger = Logger.Instance;
        public static bool Exists(String[] args,String Argumento, out String variable)
        {
            if (Argumentos.chequeoArgunto("-" + Argumento, false, args))
            {
                variable = Argumentos.traerArgumento("-" + Argumento, args);
                logger.EscribirLog("Se utilizara la  " + Argumento +  variable, Logger.Tipo.Informativo, false);
            }
            else
            {
                logger.EscribirLog("Falta ingresar el parametro de -" + Argumento, Logger.Tipo.Error, false);
                variable = String.Empty;
                return false;
            }
            if (variable != null)
            {
                if (!File.Exists(variable))
                {
                    logger.EscribirLog("No existe el archivo " + variable, Logger.Tipo.Alerta, false);
                    return false;
                }
            }
            return true;
        }
    }
}
