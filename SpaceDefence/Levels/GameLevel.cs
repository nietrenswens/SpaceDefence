using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceDefence.Engine.Managers;
using SpaceDefence.Engine.States.Levels;
using SpaceDefence.GameObjects.Enemies;
using SpaceDefence.GameObjects.GUI.PauseMenu;
using SpaceDefence.GameObjects.Powerups;
using System;
namespace SpaceDefence.Levels
{
    public class GameLevel : Level
    {
        private GameState _state;
        private PauseMenu _pauseMenu;


        public GameLevel()
        {
            _state = GameState.Playing;
            _pauseMenu = new PauseMenu();
        }

        public override void Load(ContentManager content)
        {
            AddGameObject(GameManager.GetGameManager().Player);
            AddGameObject(new Alien());
            AddGameObject(new Supply());
            _pauseMenu.Load(content);
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
            spriteBatch.Begin();
            if (_state == GameState.Paused)
                _pauseMenu.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (_state == GameState.Playing)
            {
                base.Update(gameTime);
            } 
            else if (_state == GameState.Paused)
            {
                InputManager.GetInputManager().Update();
                HandleInput();
            }

        }

        public override void HandleInput()
        {
            if (InputManager.GetInputManager().IsKeyPress(Keys.P))
            {
                TogglePause();
            }
            if (_state == GameState.Playing)
                base.HandleInput();
            else if (_state == GameState.Paused)
                _pauseMenu.HandleInput();
        }

        public Matrix GetWorldTransformationMatrix()
        {
            var player = GameManager.GetGameManager().Player;
            var screenWidth = SpaceDefence.SCREENWIDTH;
            var screenHeight = SpaceDefence.SCREENHEIGHT;

            float clampedX = MathHelper.Clamp(player.GetPosition().X, SpaceDefence.MINX + screenWidth / 2, SpaceDefence.MAXX + player.Width - screenWidth / 2);
            float clampedY = MathHelper.Clamp(player.GetPosition().Y, SpaceDefence.MINY + screenHeight / 2, SpaceDefence.MAXY - screenHeight / 2);

            return Matrix.CreateTranslation(-clampedX, -clampedY, 0) * Matrix.CreateScale(1f) * Matrix.CreateTranslation(screenWidth / 2, screenHeight / 2, 0);
        }

        public void TogglePause()
        {
            if (_state == GameState.Paused)
                _state = GameState.Playing;
            else
                _state = GameState.Paused;
        }
    }
}
