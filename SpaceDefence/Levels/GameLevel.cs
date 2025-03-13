using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine.Managers;
using SpaceDefence.GameObjects.Enemies;
using SpaceDefence.GameObjects.Powerups;
using System;
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
            int scale = 1;
            var player = GameManager.GetGameManager().Player;
            var screenWidth = SpaceDefence.SCREENWIDTH;
            var screenHeight = SpaceDefence.SCREENHEIGHT;

            float clampedX = MathHelper.Clamp(player.GetPosition().X, SpaceDefence.MINX + screenWidth / 2, SpaceDefence.MAXX + player.Width - screenWidth / 2);
            float clampedY = MathHelper.Clamp(player.GetPosition().Y, SpaceDefence.MINY + screenHeight / 2, SpaceDefence.MAXY - screenHeight / 2);

            return Matrix.CreateTranslation(-clampedX, -clampedY, 0) * Matrix.CreateScale(1f) * Matrix.CreateTranslation(screenWidth / 2, screenHeight / 2, 0);
        }
    }
}
