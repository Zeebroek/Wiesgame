using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiesgameCore
{
    public class Spelmode
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public int Prioriteit { get; private set; }
        public bool hasTroef { get; private set; }

        public static Spelmode DUO = new Spelmode(0, "Duo", 2);
        public static Spelmode SOLO5 = new Spelmode(1, "Solo 5", 1);
        public static Spelmode SOLO9 = new Spelmode(2, "Solo 9", 3);
        public static Spelmode MISERIE = new Spelmode(3, "Miserie", 4, false);
        public static Spelmode TROEL = new Spelmode(4, "Troel", 5);
        public static Spelmode OPEMISERIE = new Spelmode(5, "Ope miserie", 6, false);
        public static Spelmode TROELA = new Spelmode(6, "Troela", 7);
        public static Spelmode SOLOLOEMP = new Spelmode(7, "Sololoemp", 8);
        public static Spelmode SOLOSLUM = new Spelmode(8, "Soloslum", 9);

        private Spelmode(int id, string name, int prioriteit, bool hasTroef = true)
        {
            this.ID = id;
            this.Name = name;
            this.Prioriteit = prioriteit;
            this.hasTroef = hasTroef;
        }
    }
}
