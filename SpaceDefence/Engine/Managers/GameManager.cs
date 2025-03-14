using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine;
using SpaceDefence.GameObjects.Playable;
using SpaceDefence.Levels;

namespace SpaceDefence.Engine.Managers
{
    public class GameManager
    {
        private static GameManager gameManager;

        public Random RNG { get; private set; }
        public Ship Player { get; private set; }
        public InputManager InputManager { get; private set; }
        public GameStats GameStats { get; private set; }
        public Game Game { get; private set; }
        public ContentManager ContentManager {get; private set; }
        public GraphicsDevice GraphicsDevice { get; private set; }

        public static GameManager GetGameManager()
        {
            if(gameManager == null)
                gameManager = new GameManager();
            return gameManager;
        }
        public GameManager()
        {
            RNG = new Random();
            GameStats = new GameStats();
        }

        public void Initialize(ContentManager content, GraphicsDevice graphicsDevice, Game game, Ship player)
        {
            GraphicsDevice = graphicsDevice;
            Game = game;
            ContentManager = content;
            Player = player;
        }


        /// <summary>
        /// Get a random location on the screen.
        /// </summary>
        public Vector2 RandomScreenLocation()
        {
            return new Vector2(
                RNG.Next(0, Game.GraphicsDevice.Viewport.Width),
                RNG.Next(0, Game.GraphicsDevice.Viewport.Height));
        }

    }
}
