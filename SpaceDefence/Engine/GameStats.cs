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
            NumberOfEnemies = 1;
        }

        public int Kills { get; private set; }

        public int NumberOfEnemies { get; private set; }

        public void AddKill()
        {
            Kills++;
            switch (Kills)
            {
                case >2 and < 5:
                    NumberOfEnemies = 2;
                    break;
                case > 4 and < 20:
                    NumberOfEnemies = 4;
                    break;
                case > 19 and < 30:
                    NumberOfEnemies = 8;
                    break;
                case > 29 and < 50:
                    NumberOfEnemies = 12;
                    break;
                case > 49:
                    NumberOfEnemies = 15;
                    break;
            }
        }
    }
}
