using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaHansen.Util
{
    [Serializable]
    public class Indice
    {
        public Int64 PosicionStart { get; set; }
        public Int64 PosicionEnd { get; set; }
        public String chrStart { get; set; }
        public String chrEnd { get; set; }
        public long ByteStart { get; set; }
        public long ByteEnd { get; set; }
        public ConcurrentStack<Posicion> posicions { get; set; }
        public Indice()
        {
            posicions = new ConcurrentStack<Posicion>();
        }
    }
}
