using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hik.Communication.ScsServices.Service;

namespace WiesgameCore
{
    [ScsService(Version = "1.0.0.0")]
    public interface IWiesGameService
    {
        /// <summary>
        /// Called when user tries to play a card
        /// </summary>
        /// <param name="k">Card played</param>
        bool PlayKaart(Kaart k);

        /// <summary>
        /// Called when user chooses a gamemode
        /// </summary>
        /// <param name="sm">Gamemode chosen</param>
        void KiesSpelMode(Spelmode sm);

        /// <summary>
        /// Called when user sends a message to other players
        /// </summary>
        /// <param name="s">Message to send</param>
        void SendMessage(string s);

        /// <summary>
        /// Called when user tries to login
        /// </summary>
        /// <param name="name">username</param>
        /// <param name="password">password</param>
        bool Login(string name, string password);



    }
}
