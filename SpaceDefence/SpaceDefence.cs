using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceDefence.Engine.Managers;
using SpaceDefence.GameObjects.Playable;
namespace SpaceDefence
{
    public class SpaceDefence : Game
    {
        public static int SCREENWIDTH = 1920;
        public static int SCREENHEIGHT = 1080;

        public static int MINX = 0;
        public static int MAXX = 4000;
        public static int MINY = 0;
        public static int MAXY = 4000;

        private SpriteBatch _spriteBatch;
        public GraphicsDeviceManager _graphics;
        private GameManager _gameManager;

        public SpaceDefence()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = false;

            // Set the size of the screen
            _graphics.PreferredBackBufferWidth = SCREENWIDTH;
            _graphics.PreferredBackBufferHeight = SCREENHEIGHT;
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //Initialize the GameManager
            _gameManager = GameManager.GetGameManager();

            // Place the player at the center of the screen
            var spawnX = (MINX + MAXX) / 2;
            var spawnY = (MINY + MAXY) / 2;
            Ship player = new Ship(new Point(spawnX, spawnY));

            // Add the starting objects to the GameManager
            _gameManager.Initialize(Content, GraphicsDevice, this, player);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            LevelManager.GetLevelManager().CurrentLevel.Load(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            LevelManager.GetLevelManager().CurrentLevel.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            LevelManager.GetLevelManager().CurrentLevel.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }

    }
}
