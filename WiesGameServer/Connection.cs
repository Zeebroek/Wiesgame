using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using WiesgameCore;

namespace WiesGameServer
{
    public class Connection
    {
        string user;
        string pass;

        public Connection(string username, string password)
        {
            this.user = username;
            this.pass = password;
        }

    }
}
