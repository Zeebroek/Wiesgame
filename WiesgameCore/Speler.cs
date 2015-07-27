﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hik.Communication.ScsServices.Service;

namespace WiesgameCore
{
    [Serializable]
    public class Speler
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IScsServiceClient Client { get; set; }
        

        public Speler(int id, string name, IScsServiceClient client)
        {
            this.ID = id;
            this.Name = name;
            this.Client = client;
        }


    }
}
