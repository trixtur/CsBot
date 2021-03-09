using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsBot.Command
{
    interface iCommand
    {
        void handle(string command, int endCommand, string verb);
    }
}
