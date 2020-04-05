using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vcfformat
{
    class Posicion
    {
        public String CHR { get; set; }
        public int Position { get; set; }
        public String ID { get; set; }
        public List<String> DataA { get; set; }
        public Posicion()
        {
            DataA = new List<string>();
        }
    }
}
