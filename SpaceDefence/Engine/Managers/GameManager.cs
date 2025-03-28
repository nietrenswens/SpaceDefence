using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.GameObjects.Player;
using SpaceDefence.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceDefence.Engine.Managers
{
    public class GameManager
    {
        private static GameManager gameManager;

        public Random RNG { get; private set; }
        public Ship Player { get; private set; }
        public GameStats GameStats { get; private set; }
        public Game Game { get; private set; }
        public ContentManager ContentManager { get; private set; }
        public GraphicsDevice GraphicsDevice { get; private set; }

        public List<Timer> Timers { get; private set; }

        public static GameManager GetGameManager()
        {
            if (gameManager == null)
                gameManager = new GameManager();
            return gameManager;
        }
        public GameManager()
        {
            RNG = new Random();
        }

        public void Initialize(ContentManager content, GraphicsDevice graphicsDevice, Game game)
        {
            GraphicsDevice = graphicsDevice;
            Game = game;
            ContentManager = content;
            ResetGame();
        }

        public void Update(GameTime gameTime)
        {
            UpdateTimers(gameTime);
        }

        public void ResetGame()
        {
            Player = new Ship(new Point(1000, 1000));

            GameStats = new GameStats();
            Timers = new List<Timer>();
        }


        public void Exit()
        {
            Game.Exit();
        }

        private void UpdateTimers(GameTime gameTime)
        {
            foreach (Timer timer in Timers)
                timer.Update(gameTime);

            Timers = Timers.Where(timer => !timer.IsFinished).ToList();
        }

    }
}
