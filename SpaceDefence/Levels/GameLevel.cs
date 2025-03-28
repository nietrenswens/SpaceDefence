using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceDefence.Engine;
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
        private Texture2D _background;


        private ObjectManager<GUIObject> _guiObjectManager;
        public Objective CurrentObjective { get; private set; }
        public GameObjectCenteredCamera Camera { get; private set; }


        public GameLevel()
        {
            _state = GameState.Playing;
            _pauseMenu = new PauseMenu();
            _enemyManager = new EnemyManager();
            _guiObjectManager = new();
        }

        public override void Load(ContentManager content)
        {
            _background = content.Load<Texture2D>("background");
            SetObjective(new PickUpFromEarthObjective());
            _guiObjectManager.AddObject(new ScoreboardGUI());
            AddGameObject(GameManager.GetGameManager().Player);
            AddGameObject(new Planet("alien_planet", true));
            AddGameObject(new Planet("alien_planet", true));
            AddGameObject(new Planet("earth_planet", false));
            AddGameObject(new Planet("earth_planet", false));
            AddGameObject(new Supply());

            Camera = new GameObjectCenteredCamera(GameManager.GetGameManager().Player, 1f);

            _enemyManager.SpawnEnemies();

            _pauseMenu.Load(content);
            base.Load(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(transformMatrix: Camera.GetWorldTransformationMatrix(), samplerState: SamplerState.PointClamp);
            DrawBackground(spriteBatch);
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

        private void DrawBackground(SpriteBatch spriteBatch)
        {
            var width = _background.Width;
            var height = _background.Height;
            var levelWidth = SpaceDefence.MAXX - SpaceDefence.MINX;
            var levelHeight = SpaceDefence.MAXY - SpaceDefence.MINY;

            for (int i = 0; i < levelWidth / width + 1; i++)
            {
                for (int j = 0; j < levelHeight / height + 1; j++)
                {
                    spriteBatch.Draw(_background, new Vector2(SpaceDefence.MINX + i * width, SpaceDefence.MINY + j * height), Color.White);
                }
            }
        }
    }
}
