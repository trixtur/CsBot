using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsBot.Games
{
    interface iGame
    {
        void Play(string command, int endCommand, string verb);
    }
}
