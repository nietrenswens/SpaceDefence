using Microsoft.Xna.Framework;
using SpaceDefence.GameObjects.Enemies;
using SpaceDefence.Levels;

namespace SpaceDefence.Engine.Managers
{
    public class EnemyManager : Interfaces.IUpdatable
    {
        private int _difficultyIncrementTimer = 0;
        private int _difficultyIncrementInterval = 15000;
        public void SpawnEnemies()
        {
            var level = LevelManager.GetLevelManager().CurrentLevel as GameLevel;

            Spawn<Asteroid>(8);
            Spawn<Alien>(6);
        }

        public void Update(GameTime gameTime)
        {
            _difficultyIncrementTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (_difficultyIncrementTimer >= 15000)
            {
                _difficultyIncrementTimer = 0;
                _difficultyIncrementInterval -= 400;
                if (_difficultyIncrementInterval < 4000)
                    _difficultyIncrementInterval = 4000;
                Spawn<Alien>();
            }
        }

        private void Spawn<T>(int amount = 1) where T : GameObject, new()
        {
            var level = LevelManager.GetLevelManager().CurrentLevel as GameLevel;
            for (int i = 0; i < amount; i++)
            {
                level.AddGameObject(new T());
            }
        }
    }
}
