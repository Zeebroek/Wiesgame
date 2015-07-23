using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiesgameCore
{
    public class KaartSoort
    {
        string naam;

        public static KaartSoort SCHORPES = new KaartSoort("Schorpes");
        public static KaartSoort KLAVERS = new KaartSoort("Klavers");
        public static KaartSoort HARTES = new KaartSoort("Hartes");
        public static KaartSoort KOEKES = new KaartSoort("Koekes");

        private KaartSoort(string naam)
        {
            this.naam = naam;
        }
    }
}
