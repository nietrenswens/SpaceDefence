using SpaceDefence.Levels;
using System.Collections.Generic;

namespace SpaceDefence.Engine.Managers
{
    public class LevelManager
    {
        private static LevelManager _levelManager;

        public Level CurrentLevel { get; private set; }

        private LevelManager()
        {
            CurrentLevel = new MainMenuLevel();
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
