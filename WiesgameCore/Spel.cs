using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiesgameCore
{
    public class Spel
    {
        List<Speler> spelers;
        public Spelmode spelmode;
        public Slag currentSlag;

        public List<Speler> Spelers { get { return spelers; } set { spelers = value; } }
        public Spelmode Spelmode { get { return spelmode; } set { spelmode = value; } }
        public Slag CurrentSlag { get { return currentSlag; } set { currentSlag = value; } }
        




    }
}
