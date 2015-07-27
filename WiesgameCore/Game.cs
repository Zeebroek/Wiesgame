using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiesgameCore
{
    [Serializable]
    public class Game
    {
        List<Speler> spelers;

        public int ID { get; set; }
        public List<Speler> Spelers { get { return spelers; } set { spelers = value; } }
        public List<int> Scores { get; set; }
        public Spel CurrentSpel { get; set; }

        public Game(List<Speler> spelers)
        {
            Scores = new List<int>();
            Spelers = spelers;
        }

        public void StartSpel()
        {
            CurrentSpel = new Spel(this, spelers, 0);
        }
    }
}
