using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaHansen.Util
{
    public class CodigoSalida
    {
        #region Codigos de salida
        public const int ERROR_SUCCESS = 0;
        public const int ERROR_FILE_NOT_FOUND = 0x2;
        public const int ERROR_PATH_NOT_FOUND = 0x3;
        public const int ERROR_ACCESS_DENIED = 0x5;
        public const int ERROR_BAD_FORMAT = 0xB;
        public const int ERROR_NOT_ENOUGH_MEMORY = 0x8;
        public const int ERROR_INVALID_COMMAND_LINE = 0x667;
        #endregion

    }
}
