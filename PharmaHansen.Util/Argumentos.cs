using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaHansen.Util
{
    public class Argumentos
    {
        public static Boolean chequeoArgunto(string Argumento, bool unico, string[] args)
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
        public static string traerArgumento(String Argumento, string[] args)
        {
            return args[Array.IndexOf(args, Argumento) + 1];
        }
    }
}
