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


    }
}
