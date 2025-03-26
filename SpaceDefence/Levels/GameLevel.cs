﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceDefence.Engine.Managers;
using SpaceDefence.Engine.States.Levels;
using SpaceDefence.GameObjects.GUI.PauseMenu;
using SpaceDefence.GameObjects.Planets;
using SpaceDefence.GameObjects.Powerups;
using SpaceDefence.GUIObjects;
using SpaceDefence.GUIObjects.GameGUI;
using SpaceDefence.Objectives;
namespace SpaceDefence.Levels
{
    public class GameLevel : Level
    {
        private GameState _state;
        private PauseMenu _pauseMenu;
        private EnemyManager _enemyManager;


        private ObjectManager<GUIObject> _guiObjectManager;
        public Objective CurrentObjective { get; private set; }


        public GameLevel()
        {
            _state = GameState.Playing;
            _pauseMenu = new PauseMenu();
            _enemyManager = new EnemyManager();
            _guiObjectManager = new();
        }

        public override void Load(ContentManager content)
        {
            SetObjective(new PickUpFromEarth());
            _guiObjectManager.AddObject(new ScoreboardGUI());
            AddGameObject(GameManager.GetGameManager().Player);
            AddGameObject(new Planet("alien_planet", true));
            AddGameObject(new Planet("earth_planet", false));
            AddGameObject(new Supply());

            _enemyManager.SpawnEnemies();

            _pauseMenu.Load(content);
            base.Load(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(transformMatrix: GetWorldTransformationMatrix());
            foreach (var gameObject in _gameObjectManager.Objects)
            {
                gameObject.Draw(spriteBatch, gameTime);
            }
            foreach (var animation in _animations)
            {
                animation.Draw(spriteBatch, gameTime);
            }
            spriteBatch.End();
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            if (_state == GameState.Paused)
                _pauseMenu.Draw(spriteBatch, gameTime);
            else if (_state == GameState.Playing)
            {
                _guiObjectManager.Draw(spriteBatch, gameTime);
            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (_state == GameState.Playing)
            {
                _enemyManager.Update(gameTime);
                _guiObjectManager.Update(gameTime);
                base.Update(gameTime);
            } else if (_state == GameState.Paused)
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

        public void SetObjective(Objective objective)
        {
            var currentGuiObjective = _guiObjectManager.Objects.Find(obj => obj is GUIObjective);
            _guiObjectManager.RemoveObject(currentGuiObjective);
            var newGuiObjective = new GUIObjective(objective);
            _guiObjectManager.AddObject(new GUIObjective(objective));
            CurrentObjective = objective;
        }
    }
}
