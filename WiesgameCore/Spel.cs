using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiesgameCore
{
    [Serializable]
    public class Spel
    {
        List<Speler> spelers;
        List<Speler> team;
        Spelmode spelmode;
        Slag currentSlag;

        public List<Speler> Spelers { get { return spelers; } set { spelers = value; } }
        public List<Speler> Team { get { return team; } set { team = value; } }
        public Dictionary<Speler, List<Kaart>> Kaarten { get; set; }
        public Dictionary<Speler, Slag> Slagen { get; set; }
        public Spelmode Spelmode { get { return spelmode; } set { spelmode = value; } }
        public Slag CurrentSlag { get { return currentSlag; } set { currentSlag = value; } }
        public KaartSoort Troef { get; set;}
        public Game Game { get; set; }
        public Dictionary<Spelmode, Speler> GekozenModes { get; set; }

        bool isFirstTurn { get; set; }

        public Spel(Game g, List<Speler> spelers, int afgepakt)
        {
            this.Game = g;
            this.spelers = spelers;
            this.GekozenModes = new Dictionary<Spelmode, Speler>();
            this.Kaarten = new Dictionary<Speler, List<Kaart>>();

            Dictionary<int, List<Kaart>> result = Boek.Deling(afgepakt);
            foreach (KeyValuePair<int, List<Kaart>> kvp in result)
                Kaarten.Add(Spelers[kvp.Key], kvp.Value);

            foreach (Speler s in spelers)
                s.Client.GetClientProxy<IWiesGameClient>().ReceiveHand(Kaarten[s]);
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
            if ((Kaarten[s].Where(o => o.Nummer == 1).Count() == 3 && GekozenModes.Where(o => o.Key.Prioriteit > Spelmode.TROEL.Prioriteit).Count() == 0) || (GekozenModes.Where(o => o.Key == Spelmode.TROELA).Count() == 1 && Kaarten[s].Where(o => o.Nummer == 1).Count() == 1))
                result.Add(Spelmode.TROEL);

            if (GekozenModes.Where(o => o.Key.Prioriteit >= Spelmode.MISERIE.Prioriteit).Count() == 0)
                result.Add(Spelmode.MISERIE);

            if ((Kaarten[s].Where(o => o.Nummer == 1).Count() == 4 && GekozenModes.Where(o => o.Key.Prioriteit > Spelmode.TROELA.Prioriteit).Count() == 0) || (GekozenModes.Where(o => o.Key == Spelmode.TROELA).Count() == 1 && SpelerTroelaHarte().Key == s))
                result.Add(Spelmode.TROELA);

            if (GekozenModes.Where(o => o.Key.Prioriteit >= Spelmode.OPEMISERIE.Prioriteit).Count() == 0)
                result.Add(Spelmode.OPEMISERIE);
            if (GekozenModes.Where(o => o.Key.Prioriteit >= Spelmode.SOLOLOEMP.Prioriteit).Count() == 0)
                result.Add(Spelmode.SOLOLOEMP);
            result.Add(Spelmode.SOLOSLUM);
            
            return result;
        }

        public void KiesMode(Spelmode s)
        {
            Speler sp = Spelers[GekozenModes.Count];
            GekozenModes.Add(s, sp);
            SendVolgendeKies();        
        }

        public void SendVolgendeKies()
        {
            try
            {
                Speler s;
                if (GekozenModes.Count != 4)
                {
                    s = Spelers[GekozenModes.Count];
                    s.Client.GetClientProxy<IWiesGameClient>().ReceiveModes(BeschikbareModes(s));
                }
                else
                {
                    //Begin spel!
                    var result = Comparator.WinnerMode(this);
                    spelmode = result.Key;
                    team = result.Value;
                    foreach (Speler sp in spelers)
                        sp.Client.GetClientProxy<IWiesGameClient>().ReceiveWinnerMode(spelmode, team);
                    isFirstTurn = true;
                    SendTurn();
                }
            } catch(Exception ex)
            {
                int i = 8;
                i++;
            }
        }

        private KeyValuePair<Speler, Kaart> SpelerTroelaHarte()
        {
            Speler troela = GekozenModes[Spelmode.TROELA];
            for(int i = 13;i>0;i--)
                foreach(Speler s in Kaarten.Keys)
                    if (Kaarten[s].Where(o => o.Soort == KaartSoort.HARTES && o.Nummer == i).Count() == 1)
                        if (s != troela)
                            return new KeyValuePair<Speler,Kaart>(s, Kaarten[s].Where(o => o.Soort == KaartSoort.HARTES && o.Nummer == i).ElementAt(0));
            return new KeyValuePair<Speler,Kaart>();
        }

        public void SendTurn()
        {
            if(isFirstTurn)
            {
                if(Spelmode == Spelmode.TROEL)
                {
                    Speler s = Kaarten.Keys.ElementAt(0);
                    foreach(KeyValuePair<Speler, List<Kaart>> kvp in Kaarten)
                        if(kvp.Value.Where(o => o.Nummer == 1).Count() == 1)
                        {
                            s = kvp.Key;
                            break;
                        }
                    ReArrangePlayers(s);
                } 
                else if(Spelmode == Spelmode.TROELA)
                {
                    Speler s = SpelerTroelaHarte().Key;
                    ReArrangePlayers(s);
                }
            }
            CurrentSlag = new Slag(this);
            //spelers[currentSlag.Kaarten.Count].sendturn();
        }

        public bool PlayKaart(Speler s, Kaart k)
        {
            if (!CanPlayKaart(k, s))
                return false;
             
            currentSlag.Kaarten.Add(s, k);
            if(currentSlag.Kaarten.Count == 4)
            {
                Slagen.Add(Comparator.Winner(currentSlag, this), currentSlag);
                if(Kaarten[s].Count != 0)
                {
                    currentSlag = new Slag(this);
                    SendTurn();
                }
                else
                {
                    //NIEUW SPEL STARTEN + winnar berekenen
                }

            }
            else
            {
                SendTurn();
            }
            return true;
                
        
        }

        private bool CanPlayKaart(Kaart k, Speler s)
        {
            if(Spelmode == Spelmode.TROELA && currentSlag.Kaarten.Count == 0 && Slagen.Count == 0)
                if (k != SpelerTroelaHarte().Value)
                    return false;
            if (Spelmode == Spelmode.TROEL && currentSlag.Kaarten.Count == 0 && Slagen.Count == 0)
                if (k.Nummer != 1)
                    return false;
            if(currentSlag.Kaarten.Count != 0)
            {
                KaartSoort first = currentSlag.Kaarten.Values.ElementAt(0).Soort;

                if (Kaarten[s].Where(o => o.Soort == first).Count() > 0 && k.Soort != first)
                    return false;
                else if (Spelmode == Spelmode.DAMME && Kaarten[s].Where(o => o.Nummer == 12).Count() > 0 && k.Nummer != 12)
                    return false;
            }
            return true;
        }

        private void ReArrangePlayers(Speler first)
        {
            List<Speler> nieuw = new List<Speler>();
            nieuw.Add(first);

            int index = spelers.IndexOf(first);

            if (index == 0)
                return;
            else if(index == 1)
            {
                nieuw.Add(spelers[2]);
                nieuw.Add(spelers[3]);
                nieuw.Add(spelers[0]);
            } 
            else if(index == 2)
            {
                nieuw.Add(spelers[3]);
                nieuw.Add(spelers[0]);
                nieuw.Add(spelers[1]);
            }
            else if(index == 3)
            {
                nieuw.Add(spelers[0]);
                nieuw.Add(spelers[1]);
                nieuw.Add(spelers[2]);
            }

            this.spelers = nieuw;
        }

        
        




    }
}
