using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiesgameCore
{
    [Serializable]
    public class Boek
    {

        //Doe de deling
        public static Dictionary<int, List<Kaart>> Deling(int afgepakt)
        {
            Dictionary<int, List<Kaart>> result = new Dictionary<int, List<Kaart>>();

            result.Add(0, new List<Kaart>()); //Lijst veu elke speler
            result.Add(1, new List<Kaart>());
            result.Add(2, new List<Kaart>());
            result.Add(3, new List<Kaart>());

            List<Kaart> boek = new List<Kaart>();

            //Vullen boek kaarten
            for(int i = 1;i<=52;i++)
                for(int k = 0;k<4;k++)
                {
                    Kaart kaart = new Kaart(i, KaartSoort.ForID(k));
                    boek.Add(kaart);
                }

            
            //Shuffle
            List<Kaart> tmp = new List<Kaart>();
            Random r = new Random();
            for(int i = 0;i<boek.Count;i++)
                tmp.Add(boek.ElementAt(r.Next(0, boek.Count)));
            boek = tmp;


            //Afpakken
            List<Kaart> temp = new List<Kaart>();
            temp.AddRange(boek);
            boek.Clear();
            for (int i = temp.Count; i > temp.Count - afgepakt;i-- )
                boek.Add(temp.ElementAt(i));
            foreach (Kaart k in temp)
                if(!boek.Contains(k))
                    boek.Add(k);

            if (boek.Count != 52)
                Console.WriteLine("ERROR");


            //Verdelen
            for (int i = 0; i < 3;i++ )
                for (int k = 0; k < 4; k++)
                    for (int l = 0; l < (i == 2 ? 6 : 5); l++)
                        result[k].Add(boek.ElementAt(0));

                return result;
        }


    }
}
