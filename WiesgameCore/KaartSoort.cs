using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiesgameCore
{
    [Serializable]
    public class KaartSoort
    {
        string naam;

        public static KaartSoort SCHORPES = new KaartSoort("Schorpes");
        public static KaartSoort KLAVERS = new KaartSoort("Klavers");
        public static KaartSoort HARTES = new KaartSoort("Hartes");
        public static KaartSoort KOEKES = new KaartSoort("Koeke");

        public string Naam { get { return naam; } }

        private KaartSoort(string naam)
        {
            this.naam = naam;
        }

        public static KaartSoort ForID(int id)
        {
            KaartSoort soort = null;
            switch (id)
            {
                case 0:
                    soort = KaartSoort.HARTES;
                    break;
                case 1:
                    soort = KaartSoort.KLAVERS;
                    break;
                case 2:
                    soort = KaartSoort.KOEKES;
                    break;
                case 3:
                    soort = KaartSoort.SCHORPES;
                    break;
            }
            return soort;
        }
    }
}
