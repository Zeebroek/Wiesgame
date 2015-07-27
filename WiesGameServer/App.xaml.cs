using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Hik.Communication.ScsServices.Service;

namespace WiesGameServer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IScsServiceApplication Server;

        public static void Disconnect()
        {
            if(Server != null)
            {
                Server.Stop();
            }
        }
    }
}
