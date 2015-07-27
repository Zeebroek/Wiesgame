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
        public static void Run(MainWindow mw, string username)
        {

            try
            {
                App.Client = ScsServiceClientBuilder.CreateClient<IWiesGameService>(
                new ScsTcpEndPoint("127.0.0.1", 8001), new Client(mw.ListBoxConsole, mw));

                App.Client.Connect();

                try
                {
                    if (App.Client.ServiceProxy.Login(username, "bullshit"))
                    {
                        mw.ListBoxConsole.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                mw.ListBoxConsole.Items.Add("logged in!");
                            }));

                    }
                }
                catch (Exception e)
                {
                    mw.ListBoxConsole.Items.Add(e.Message);
                }
            }

            catch (Exception e)
            {
                mw.ListBoxConsole.Items.Add(e.StackTrace);
            }
        }

        ListBox ListBox { get; set; }
        GameWindow GameWindow { get; set; }
        MainWindow MainWindow { get; set; }


        public Client(ListBox lb, MainWindow mainWindow)
        {
            this.ListBox = lb;
            this.MainWindow = mainWindow;
        }







        public void ReceiveMessage(string text, string sender)
        {
            Write("[" + sender + "]: " + text);
        }

        public void ReceiveHand(List<Kaart> hand)
        {
            try
            {
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    GameWindow.LoadHand(hand);
                }));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.StackTrace);
            }

        }

        public void ReceiveModes(List<Spelmode> modes)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    GameWindow.LoadModes(modes);
                }));
        }

        public void ReceiveWinnerMode(Spelmode sm, List<Speler> team)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                GameWindow.LoadNewMode(sm, team);
            }));
        }

        public void ReceiveSpelers(List<Speler> spelers, Speler you)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (GameWindow == null)
                {
                    GameWindow = new GameWindow(this);
                    MainWindow.Hide();
                    GameWindow.Show();
                }

                GameWindow.LoadSpelers(spelers, you);
            }));
        }







        private void Write(string message)
        {
            if (GameWindow == null)
                ListBox.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        ListBox.Items.Add(message);
                    }));
            else
                GameWindow.Chatbox.Items.Add(message);
        }







    }
}
