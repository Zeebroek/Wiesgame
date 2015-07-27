using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.Threading;
using WiesgameCore;
using Hik.Communication.ScsServices.Service;
using Hik.Communication.Scs.Server;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;

namespace WiesGameServer
{
    public class Server : ScsService, IWiesGameService
    {
        const int PORT = 8001;


        ListBox ListBox { get; set; }
        static List<Speler> Spelers { get; set; }

        public static Game Game { get; set; }

        public Server(ListBox lb)
        {
            this.ListBox = lb;
            Spelers = new List<Speler>();
        }

        public static void StartServer(ListBox lb)
        {
            App.Server = ScsServiceBuilder.CreateService(new ScsTcpEndPoint(PORT));
            App.Server.AddService<IWiesGameService, Server>(new Server(lb));

            App.Server.Start();

            lb.Dispatcher.BeginInvoke(new Action(() =>
            {
                lb.Items.Add("Server successfully started! Listening on port " + PORT);
            }));
        }

        public void KiesSpelMode(Spelmode sm)
        {
            Game.CurrentSpel.KiesMode(sm);
        }

        public bool Login(string username, string password)
        {
            //ADD DATABASE CREDENTIALS & SHIT HERE
            if (Spelers.Where(o => o.Name.ToLower() == username.ToLower()).Count() != 0)
            {
                throw new Exception("Username already taken!");
            }
            else if (Spelers.Count >= 4)
            {
                throw new Exception("Server full!");
            }
            else
            {
                Spelers.Add(new Speler(Spelers.Count, username, CurrentClient));
                WriteLog("New speler logged in: " + username);

                if(Spelers.Count == 4)
                {
                    Game = new Game(Spelers);
                    WriteLog("4 players detected: New Game started!");
                    WriteToAll("Starting new game...");
                    foreach (Speler s in Spelers)
                        s.Client.GetClientProxy<IWiesGameClient>().ReceiveSpelers(Spelers, s);
                    Game.StartSpel();
                }
            }


            return true;

        }

        public bool PlayKaart(Kaart k)
        {
            if(Game.CurrentSpel.PlayKaart(ForClientID((int)CurrentClient.ClientId), k))
                return true;

            return false;
        }

        public void SendMessage(string message)
        {

        }

        private void WriteLog(string message)
        {
            ListBox.Dispatcher.BeginInvoke(new Action(() =>
            {
                ListBox.Items.Add(message);
            }));
        }

        private Speler ForName(string name)
        {
            foreach (Speler s in Spelers)
                if (s.Name.ToLower() == name.ToLower())
                    return s;
            return null;
        }

        private Speler ForClientID(int id)
        {
            foreach (Speler s in Spelers)
                if (s.Client.ClientId == id)
                    return s;
            return null;
        }

        public void WriteToAll(string message, string sender = "SYSTEM")
        {
            foreach (Speler s in Spelers)
                s.Client.GetClientProxy<IWiesGameClient>().ReceiveMessage(message, sender);
        }





    }
}
