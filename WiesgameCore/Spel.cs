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
        Spelmode spelmode;
        Slag currentSlag;

        public List<Speler> Spelers { get { return spelers; } set { spelers = value; } }
        public Dictionary<Speler, List<Kaart>> Kaarten { get; set; }
        public Spelmode Spelmode { get { return spelmode; } set { spelmode = value; } }
        public Slag CurrentSlag { get { return currentSlag; } set { currentSlag = value; } }
        public KaartSoort Troef { get; set;}
        public Game Game { get; set; }
        public Dictionary<Spelmode, Speler> GekozenModes { get; set; }

        public Spel(Game g, List<Speler> spelers, int afgepakt)
        {
            this.Game = g;
            this.spelers = spelers;
            this.GekozenModes = new Dictionary<Spelmode, Speler>();
            this.Kaarten = new Dictionary<Speler, List<Kaart>>();

            Dictionary<int, List<Kaart>> result = Boek.Deling(afgepakt);
            foreach (KeyValuePair<int, List<Kaart>> kvp in result)
                Kaarten.Add(Spelers[kvp.Key], kvp.Value);
        }


        public List<Spelmode> BeschikbareModes(Speler s)
        {
            List<Spelmode> result = new List<Spelmode>();

            result.Add(Spelmode.PASS);

            if (s.ID == 3 && GekozenModes.Where(o => o.Key.Prioriteit > Spelmode.SOLO5.Prioriteit).Count() == 0)
                result.Add(Spelmode.SOLO5);
            else if (GekozenModes.Where(o => o.Key.ID == Spelmode.DUO.ID).Count() < 2 && GekozenModes.Where(o => o.Key.Prioriteit > Spelmode.DUO.Prioriteit).Count() == 0)
                result.Add(Spelmode.DUO); //Als vraag & meegaan -- word in client vertaald

            if (GekozenModes.Where(o => o.Key.Prioriteit >= Spelmode.SOLO9.Prioriteit).Count() == 0)
                result.Add(Spelmode.SOLO9);
            if (Kaarten[s].Where(o => o.Nummer == 1).Count() == 3 && GekozenModes.Where(o => o.Key.Prioriteit > Spelmode.TROEL.Prioriteit).Count() == 0)
                result.Add(Spelmode.TROEL);

            if (GekozenModes.Where(o => o.Key.Prioriteit >= Spelmode.MISERIE.Prioriteit).Count() == 0)
                result.Add(Spelmode.MISERIE);

            if (Kaarten[s].Where(o => o.Nummer == 1).Count() == 4 && GekozenModes.Where(o => o.Key.Prioriteit > Spelmode.TROELA.Prioriteit).Count() == 0)
                result.Add(Spelmode.TROELA);

            if (GekozenModes.Where(o => o.Key.Prioriteit >= Spelmode.OPEMISERIE.Prioriteit).Count() == 0)
                result.Add(Spelmode.OPEMISERIE);
            if (GekozenModes.Where(o => o.Key.Prioriteit >= Spelmode.SOLOLOEMP.Prioriteit).Count() == 0)
                result.Add(Spelmode.SOLOLOEMP);
            result.Add(Spelmode.SOLOSLUM);
            
            return result;
        }

        public void SendVolgendeKies()
        {
            Speler s;
            if (GekozenModes.Count != 4)
                s = Spelers[GekozenModes.Count];
            else
            {

            }
        }

        private Speler SpelerTroelaHarte()
        {
            Speler troela = GekozenModes[Spelmode.TROELA];
            for(int i = 13;i>0;i--)
                foreach(Speler s in Kaarten.Keys)
                    if (Kaarten[s].Where(o => o.Soort == KaartSoort.HARTES && o.Nummer == i).Count() == 1)
                        if (s != troela)
                            return s;
            return troela;
        }

        
        




    }
}
