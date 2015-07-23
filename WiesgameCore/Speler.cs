using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiesgameCore
{
    public class Speler
    {
        public int ID { get; set; }
        public string Name { get; set; }
        

        public Speler(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }


    }
}
