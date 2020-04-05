using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaHansen.Util
{
    public class Cromozoma
    {
        public String Chr { get; set; }
        public HashSet<Indice> Lista1 { get; set; }
        public HashSet<Indice> Lista2 { get; set; }
        public HashSet<Indice> Lista3 { get; set; }
        public HashSet<Indice> Lista4 { get; set; }
        public HashSet<Indice> Lista5 { get; set; }
        public HashSet<Indice> Lista6 { get; set; }
        public HashSet<Indice> Lista7 { get; set; }
        public HashSet<Indice> Lista8 { get; set; }
        public HashSet<Indice> Lista9 { get; set; }
        public HashSet<Indice> Lista10 { get; set; }
        public HashSet<Indice> Lista11 { get; set; }
        public HashSet<Indice> Lista12 { get; set; }
        public HashSet<Indice> Lista13 { get; set; }
        public HashSet<Indice> Lista14 { get; set; }
        public HashSet<Indice> Lista15 { get; set; }
        public HashSet<Indice> Lista16 { get; set; }
        public HashSet<Indice> Lista17 { get; set; }
        public HashSet<Indice> Lista18 { get; set; }
        public HashSet<Indice> Lista19 { get; set; }
        public HashSet<Indice> Lista20 { get; set; }
        public HashSet<Indice> Lista21 { get; set; }
        public HashSet<Indice> Lista22 { get; set; }
        public HashSet<Indice> Lista23 { get; set; }
        public HashSet<Indice> Lista24 { get; set; }
        public HashSet<Indice> Lista25 { get; set; }
        private int multi = 10000000;
        public Cromozoma() {
            Lista1 = new HashSet<Indice>();
            Lista2 = new HashSet<Indice>();
            Lista3 = new HashSet<Indice>();
            Lista4 = new HashSet<Indice>();
            Lista5 = new HashSet<Indice>();
            Lista6 = new HashSet<Indice>();
            Lista7 = new HashSet<Indice>();
            Lista8 = new HashSet<Indice>();
            Lista9 = new HashSet<Indice>();
            Lista10 = new HashSet<Indice>();
            Lista11 = new HashSet<Indice>();
            Lista12 = new HashSet<Indice>();
            Lista13 = new HashSet<Indice>();
            Lista14 = new HashSet<Indice>();
            Lista15 = new HashSet<Indice>();
            Lista16 = new HashSet<Indice>();
            Lista17 = new HashSet<Indice>();
            Lista18 = new HashSet<Indice>();
            Lista19 = new HashSet<Indice>();
            Lista20 = new HashSet<Indice>();
            Lista21 = new HashSet<Indice>();
            Lista22 = new HashSet<Indice>();
            Lista23 = new HashSet<Indice>();
            Lista24 = new HashSet<Indice>();
            Lista25 = new HashSet<Indice>();
        }

        public void AgregarItem(Indice ind)
        {
           
            if (ind.PosicionEnd < 1 * multi)
            {
                Lista1.Add(ind);
            }
            if (ind.PosicionEnd >= 1 * multi && ind.PosicionEnd < 2 * multi)
                Lista2.Add(ind);
            if (ind.PosicionEnd >= 2 * multi && ind.PosicionEnd < 3 * multi)
                Lista3.Add(ind);
            if (ind.PosicionEnd >= 3 * multi && ind.PosicionEnd < 4 * multi)
                Lista4.Add(ind);
            if (ind.PosicionEnd >= 4 * multi && ind.PosicionEnd < 5 * multi)
                Lista5.Add(ind);
            if (ind.PosicionEnd >= 5 * multi && ind.PosicionEnd < 6 * multi)
                Lista5.Add(ind);
            if (ind.PosicionEnd >= 6 * multi && ind.PosicionEnd < 7 * multi)
                Lista6.Add(ind);
            if (ind.PosicionEnd >= 7 * multi && ind.PosicionEnd < 8 * multi)
                Lista7.Add(ind);
            if (ind.PosicionEnd >= 8 * multi && ind.PosicionEnd < 9 * multi)
                Lista8.Add(ind);
            if (ind.PosicionEnd >= 9 * multi && ind.PosicionEnd < 10 * multi)
                Lista9.Add(ind);
            if (ind.PosicionEnd >= 10 * multi && ind.PosicionEnd < 11 * multi)
                Lista10.Add(ind);
            if (ind.PosicionEnd >= 11 * multi && ind.PosicionEnd < 12 * multi)
                Lista11.Add(ind);
            if (ind.PosicionEnd >= 12 * multi && ind.PosicionEnd < 13 * multi)
                Lista12.Add(ind);
            if (ind.PosicionEnd >= 13 * multi && ind.PosicionEnd < 14 * multi)
                Lista13.Add(ind);
            if (ind.PosicionEnd >= 14 * multi && ind.PosicionEnd < 15 * multi)
                Lista14.Add(ind);
            if (ind.PosicionEnd >= 15 * multi && ind.PosicionEnd < 16 * multi)
                Lista15.Add(ind);
            if (ind.PosicionEnd >= 16 * multi && ind.PosicionEnd < 17 * multi)
                Lista16.Add(ind);
            if (ind.PosicionEnd >= 17 * multi && ind.PosicionEnd < 18 * multi)
                Lista17.Add(ind);
            if (ind.PosicionEnd >= 18 * multi && ind.PosicionEnd < 19 * multi)
                Lista18.Add(ind);
            if (ind.PosicionEnd >= 19 * multi && ind.PosicionEnd < 20 * multi)
                Lista19.Add(ind);
            if (ind.PosicionEnd >= 20 * multi && ind.PosicionEnd < 21 * multi)
                Lista20.Add(ind);
            if (ind.PosicionEnd >= 21 * multi && ind.PosicionEnd < 22 * multi)
                Lista21.Add(ind);
            if (ind.PosicionEnd >= 22 * multi && ind.PosicionEnd < 23 * multi)
                Lista22.Add(ind);
            if (ind.PosicionEnd >= 23 * multi && ind.PosicionEnd < 24 * multi)
                Lista23.Add(ind);
            if (ind.PosicionEnd >= 24 * multi)
                Lista24.Add(ind);
        }

        public Indice BuscarItem(Posicion pos)
        {
            if (pos.Position >= 1 * multi && pos.Position < 2 * multi)
                return Lista2.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 2 * multi && pos.Position < 3 * multi)
                return Lista3.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 3 * multi && pos.Position < 4 * multi)
                return Lista4.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 4 * multi && pos.Position < 5 * multi)
                return Lista5.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 5 * multi && pos.Position < 6 * multi)
                return Lista5.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 6 * multi && pos.Position < 7 * multi)
                return Lista6.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 7 * multi && pos.Position < 8 * multi)
                return Lista7.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 8 * multi && pos.Position < 9 * multi)
                return Lista8.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 9 * multi && pos.Position < 10 * multi)
                return Lista9.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 10 * multi && pos.Position < 11 * multi)
                return Lista10.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 11 * multi && pos.Position < 12 * multi)
                return Lista11.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 12 * multi && pos.Position < 13 * multi)
                return Lista12.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 13 * multi && pos.Position < 14 * multi)
                return Lista13.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 14 * multi && pos.Position < 15 * multi)
                return Lista14.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 15 * multi && pos.Position < 16 * multi)
                return Lista15.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 16 * multi && pos.Position < 17 * multi)
                return Lista16.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 17 * multi && pos.Position < 18 * multi)
                return Lista17.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 18 * multi && pos.Position < 19 * multi)
                return Lista18.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 19 * multi && pos.Position < 20 * multi)
                return Lista19.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 20 * multi && pos.Position < 21 * multi)
                return Lista20.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 21 * multi && pos.Position < 22 * multi)
                return Lista21.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 22 * multi && pos.Position < 23 * multi)
                return Lista22.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 23 * multi && pos.Position < 24 * multi)
                return Lista23.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            if (pos.Position >= 24 * multi)
                return Lista24.Where(s => pos.Position < s.PosicionEnd && pos.Position >= s.PosicionStart).First();
            return null;
        }
        public HashSet<Indice> GetIndices()
        {
            HashSet<Indice> indices = new HashSet<Indice>();
            foreach (Indice ind in Lista2.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista3.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista4.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista5.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista6.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista7.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista8.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista9.Where(s => s.posicions.Count > 0))
                    indices.Add(ind);
            foreach (Indice ind in Lista10.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista11.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista12.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista13.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista14.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista15.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista16.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista17.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista18.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista19.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista20.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista21.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista22.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista23.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista24.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            foreach (Indice ind in Lista25.Where(s => s.posicions.Count > 0))
                indices.Add(ind);
            return indices;
        }
    }
}
