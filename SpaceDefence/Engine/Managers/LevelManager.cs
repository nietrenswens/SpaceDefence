using SpaceDefence.Levels;

namespace SpaceDefence.Engine.Managers
{
    public class LevelManager
    {
        private static LevelManager _levelManager;

        public Level CurrentLevel { get; private set; }

        private LevelManager()
        {
            CurrentLevel = new GameLevel();
        }

        public static LevelManager GetLevelManager()
        {
            if (_levelManager == null)
                _levelManager = new LevelManager();
            return _levelManager;
        }

        public void ChangeLevel(Level level)
        {
            CurrentLevel.Unload();
            CurrentLevel = level;
            CurrentLevel.Load(GameManager.GetGameManager().ContentManager);
        }

    }
}
