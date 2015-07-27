using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing.Imaging;
using WiesgameCore;
using System.Drawing;
using System.IO;

namespace Wiesgame
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        Client client;
        public GameWindow(Client client)
        {
            InitializeComponent();
            this.client = client;
        }


        public void LoadModes(List<Spelmode> modes)
        {
            foreach(Spelmode sm in modes)
            {
                Button b = new Button();
                b.Width = 60;
                b.Height = 20;
                b.Content = sm.Name;
                b.Click += b_Click;
                b.Tag = sm;
                StackPanelModes.Children.Add(b);
            }
        }

        public void LoadHand(List<Kaart> kaarten)
        {
            StackPanelHand.Children.Clear();


            foreach(Kaart k in kaarten)
            {
                Bitmap bmp = (Bitmap)Properties.Resources.ResourceManager.GetObject(k.Soort.Naam.ToUpper() + "_" + k.Nummer);
                System.Windows.Controls.Image img = new System.Windows.Controls.Image();
                img.Source = Bitmap2BitmapImage(bmp);
                StackPanelHand.Children.Add(img);
            }
        }

        public void LoadNewMode(Spelmode sm, List<Speler> team)
        {
            StackPanelModes.Children.Clear();
            StackPanelModes.Children.Add(new TextBlock() { Text = sm.Name });
            string tm = "Team:";
            foreach(Speler s in team)
                tm += " " + s.Name;
            StackPanelModes.Children.Add(new TextBlock() { Text = tm });
        }

        public void LoadSpelers(List<Speler> spelers, Speler you)
        {
            int index = 0;
            foreach (Speler s in spelers)
                if (s.ID == you.ID)
                    index = spelers.IndexOf(s);

            switch(index)
            {
                case 0:
                    TextBlock1.Text = spelers[1].Name;
                    TextBlock2.Text = spelers[2].Name;
                    TextBlock3.Text = spelers[3].Name;
                    break;
                case 1:
                    TextBlock1.Text = spelers[2].Name;
                    TextBlock2.Text = spelers[3].Name;
                    TextBlock3.Text = spelers[0].Name;
                    break;
                case 2:
                    TextBlock1.Text = spelers[3].Name;
                    TextBlock2.Text = spelers[0].Name;
                    TextBlock3.Text = spelers[1].Name;
                    break;
                case 3:
                    TextBlock1.Text = spelers[0].Name;
                    TextBlock2.Text = spelers[1].Name;
                    TextBlock3.Text = spelers[2].Name;
                    break;
            }
        }
















        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        private BitmapSource Bitmap2BitmapImage(Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();
            BitmapSource retval;

            try
            {
                retval = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                             hBitmap,
                             IntPtr.Zero,
                             Int32Rect.Empty,
                             BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(hBitmap);
            }

            return retval;
        }


        #region Serverinteractions

        void b_Click(object sender, RoutedEventArgs e)
        {
            App.Client.ServiceProxy.KiesSpelMode((Spelmode)((Button)sender).Tag);
            StackPanelModes.Children.Clear();
        }

        #endregion




        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Disconnect();
            App.Current.Shutdown();
        }

    }
}
