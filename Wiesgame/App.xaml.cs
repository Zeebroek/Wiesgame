using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WiesgameCore;
using Hik.Communication.ScsServices.Client;

namespace Wiesgame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IScsServiceClient<IWiesGameService> Client;

        public static void Disconnect()
        {
            if(Client != null)
            {
                Client.Disconnect();
                Client.Dispose();
            }
        }
    }
}
