using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiesgameCore
{
    [Serializable]
    public class Kaart
    {
        public int Nummer { get; private set; }
        public KaartSoort Soort { get; private set; }

        public Kaart(int nummer, KaartSoort soort)
        {
            this.Nummer = nummer;
            this.Soort = soort;
        }
    }
}
