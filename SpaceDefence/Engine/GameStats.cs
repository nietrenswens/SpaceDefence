namespace SpaceDefence.Engine
{
    public class GameStats
    {
        public GameStats()
        {
        }

        public int Kills { get; private set; }
        public int Score { get; private set; }

        public void AddKill()
        {
            Kills++;
        }

        public void AddScore()
        {
            Score++;
        }
    }
}
