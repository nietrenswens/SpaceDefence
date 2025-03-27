using SpaceDefence.Engine.Managers;

namespace SpaceDefence.Engine
{
    public class GameStats
    {
        public GameStats()
        {
            var player = GameManager.GetGameManager().Player;

            player.DropoffDeliveryEvent += (o, args) => AddScore();
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
