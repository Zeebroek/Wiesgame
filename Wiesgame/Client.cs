using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Client;
using WiesgameCore;
using WiesGameServer;

namespace Wiesgame
{
    public class Client : IWiesGameClient
    {
        public static void Run(ListBox lb)
        {

            try
            {
                var client = ScsServiceClientBuilder.CreateClient<IWiesGameService>(
                new ScsTcpEndPoint("127.0.0.1", 8001), new Client(lb));

                client.Connect();

                try
                {
                    if (client.ServiceProxy.Login("Senne", "bullshit"))
                    {
                        lb.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                lb.Items.Add("logged in!");
                            }));
                        
                    }
                } 
                catch(Exception e)
                {
                    lb.Items.Add(e.Message);
                }
            }

            catch (Exception e)
            {
                lb.Items.Add(e.StackTrace);
            }
        }

        ListBox ListBox { get; set; }


        public Client(ListBox lb)
        {
            this.ListBox = lb;
        }

        public void ReceiveMessage(string text, string sender)
        {
            Write("[" + sender + "]: " + text);
        }

        private void Write(string message)
        {
            ListBox.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ListBox.Items.Add(message);
                }));
        }







    }
}
