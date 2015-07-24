using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiesgameCore
{
    public class Comparator
    {
        public static Speler Winner(Slag slag, Spel spel)
        {
            KaartSoort first = slag.Kaarten.ElementAt(0).Soort;
            KaartSoort troef = spel.Troef;

            Kaart hoogste = slag.Kaarten.ElementAt(0);

            if (!spel.Spelmode.hasTroef) //Geen rekening me troef
            {
                foreach (Kaart k in slag.Kaarten.Where(o => o.Soort == first))
                    if (k.Nummer > hoogste.Nummer || k.Nummer == 1)
                        hoogste = k;
            }
            else
            { //Wel rekening me troef
                foreach (Kaart k in slag.Kaarten)
                {
                    if (k.Soort != first && k.Soort != troef)
                        continue;
                    if (((k.Soort == first && hoogste.Soort != troef) || (k.Soort == troef && hoogste.Soort == troef)) && (k.Nummer > hoogste.Nummer || k.Nummer == 1))
                        hoogste = k;
                    else if (k.Soort == troef && hoogste.Soort != troef)
                        hoogste = k;
                }
            }

            return spel.Spelers[slag.Kaarten.IndexOf(hoogste)];
        }

        public static KeyValuePair<Spelmode, List<Speler>> WinnerMode(Spel spel)
        {
            KeyValuePair<Spelmode, List<Speler>> result;

            var gekozenModes = spel.GekozenModes;

            Spelmode hoogste = gekozenModes.Keys.ElementAt(0);
            List<Speler> spelers = new List<Speler>();
            spelers.Add(gekozenModes.Values.ElementAt(0));

            foreach(KeyValuePair<Spelmode, Speler> kvp in gekozenModes)
                if(kvp.Key.Prioriteit > hoogste.Prioriteit)
                {
                    spelers.Clear();
                    spelers.Add(kvp.Value);
                    hoogste = kvp.Key;
                }
                else if(kvp.Key.Prioriteit == hoogste.Prioriteit && !spelers.Contains(kvp.Value))
                {
                    spelers.Add(kvp.Value);
                }

            result = new KeyValuePair<Spelmode, List<Speler>>(hoogste, spelers);

            return result;
        }

    }
}
