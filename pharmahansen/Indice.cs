using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pharmahansen
{
    [Serializable]
    class Indice
    {
        public int PosicionStart { get; set; }
        public int PosicionEnd { get; set; }
        public String chr { get; set; }
        public long start { get; set; }
        public long End { get; set; }
    }
}
