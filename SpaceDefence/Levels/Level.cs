﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine;
using System.Collections.Generic;

namespace SpaceDefence.Levels
{
    public abstract class Level
    {
        private List<GameObject> _gameObjects;
        private List<GameObject> _toBeRemoved;
        private List<GameObject> _toBeAdded;
        private InputManager _inputManager;

        public Level(InputManager inputManager)
        {
            _gameObjects = new List<GameObject>();
            _toBeRemoved = new List<GameObject>();
            _toBeAdded = new List<GameObject>();
            _inputManager = inputManager;
        }

        public void Unload()
        {
            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.Destroy();
            }
        }

        public virtual void Load(ContentManager content)
        {
            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.Load(content);
            }
        }

        public void HandleInput(InputManager inputManager)
        {
            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.HandleInput(inputManager);
            }
        }

        public void CheckCollision()
        {
            // Checks once for every pair of 2 GameObjects if the collide.
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                for (int j = i + 1; j < _gameObjects.Count; j++)
                {
                    if (_gameObjects[i].CheckCollision(_gameObjects[j]))
                    {
                        _gameObjects[i].OnCollision(_gameObjects[j]);
                        _gameObjects[j].OnCollision(_gameObjects[i]);
                    }
                }
            }

        }

        public void Update(GameTime gameTime)
        {
            _inputManager.Update();

            // Handle input
            HandleInput(_inputManager);


            // Update
            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.Update(gameTime);
            }

            // Check Collission
            CheckCollision();

            foreach (GameObject gameObject in _toBeAdded)
            {
                gameObject.Load(GameManager.GetGameManager().ContentManager);
                _gameObjects.Add(gameObject);
            }
            _toBeAdded.Clear();

            foreach (GameObject gameObject in _toBeRemoved)
            {
                gameObject.Destroy();
                _gameObjects.Remove(gameObject);
            }
            _toBeRemoved.Clear();

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }

        /// <summary>
        /// Add a new GameObject to the GameManager. 
        /// The GameObject will be added at the start of the next Update step. 
        /// Once it is added, the GameManager will ensure all steps of the game loop will be called on the object automatically. 
        /// </summary>
        /// <param name="gameObject"> The GameObject to add. </param>
        public void AddGameObject(GameObject gameObject)
        {
            _toBeAdded.Add(gameObject);
        }

        /// <summary>
        /// Remove GameObject from the GameManager. 
        /// The GameObject will be removed at the start of the next Update step and its Destroy() mehtod will be called.
        /// After that the object will no longer receive any updates.
        /// </summary>
        /// <param name="gameObject"> The GameObject to Remove. </param>
        public void RemoveGameObject(GameObject gameObject)
        {
            _toBeRemoved.Add(gameObject);
        }
    }
}
