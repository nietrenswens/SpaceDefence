using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine.Managers;
using SpaceDefence.GameObjects.Enemies;
using SpaceDefence.GameObjects.Powerups;
namespace SpaceDefence.Levels
{
    public class GameLevel : Level
    {
        public override void Load(ContentManager content)
        {
            AddGameObject(GameManager.GetGameManager().Player);
            AddGameObject(new Alien());
            AddGameObject(new Supply());
            base.Load(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            spriteBatch.Begin(transformMatrix: GetWorldTransformationMatrix());
            foreach(var gameObject in _gameObjects)
            {
                gameObject.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }

        public Matrix GetWorldTransformationMatrix()
        {
            var player = GameManager.GetGameManager().Player;
            var screenWidth = SpaceDefence.SCREENWIDTH;
            var screenHeight = SpaceDefence.SCREENHEIGHT;
            return Matrix.CreateTranslation(-player.GetPosition().X, -player.GetPosition().Y, 0) * Matrix.CreateScale(1.2f) * Matrix.CreateTranslation(screenWidth / 2, screenHeight / 2, 0);
        }
    }
}
