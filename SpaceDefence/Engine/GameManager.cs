﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SpaceDefence.Engine;
using SpaceDefence.Levels;

namespace SpaceDefence
{
    public class GameManager
    {
        private static GameManager gameManager;

        public Random RNG { get; private set; }
        public Ship Player { get; private set; }
        public InputManager InputManager { get; private set; }
        public Game Game { get; private set; }
        public GameState GameState { get; private set; }
        public ContentManager ContentManager {get; private set; }
        public Level CurrentLevel { get; private set; }

        public static GameManager GetGameManager()
        {
            if(gameManager == null)
                gameManager = new GameManager();
            return gameManager;
        }
        public GameManager()
        {
            InputManager = new InputManager();
            RNG = new Random();
            GameState = GameState.Playing;
            CurrentLevel = new GameLevel(InputManager);
        }

        public void Initialize(ContentManager content, Game game, Ship player)
        {
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

        public void SetGameState(GameState gameState)
        {
            GameState = gameState;
        }

    }
}
