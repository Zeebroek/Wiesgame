using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiesgameCore
{
    public interface IWiesGameClient
    {

        void ReceiveMessage(string message, string sender = "SYSTEM");

        void ReceiveHand(List<Kaart> hand);

        void ReceiveModes(List<Spelmode> modes);

        void ReceiveWinnerMode(Spelmode mode, List<Speler> team);

        void ReceiveSpelers(List<Speler> spelers, Speler you);

    }
}
