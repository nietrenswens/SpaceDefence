using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefence.Engine
{
    public class GameStats
    {
        public GameStats()
        {
        }

        public int Kills { get; private set; }

        public void AddKill()
        {
            Kills++;
        }
    }
}
