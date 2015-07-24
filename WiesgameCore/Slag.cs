using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiesgameCore
{
    public class Slag
    {
        public Spel Spel { get; private set; }
        public Dictionary<Speler, Kaart> Kaarten { get; set; }


        public Slag(Spel spel)
        {
            this.Spel = spel;
        }

    }
}
