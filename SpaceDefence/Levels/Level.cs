using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine;
using SpaceDefence.Engine.Managers;
using System.Collections.Generic;

namespace SpaceDefence.Levels
{
    public abstract class Level
    {
        public List<GameObject> GameObjects { get { return _gameObjectManager.Objects; } }

        protected ObjectManager<GameObject> _gameObjectManager;

        protected List<Animation> _animationsToBeAdded;
        protected List<Animation> _animations;
        protected List<Animation> _animationsToBeRemoved;

        public Level()
        {
            _gameObjectManager = new();
            _animations = new();
            _animationsToBeAdded = new();
            _animationsToBeRemoved = new();
        }

        public void Unload()
        {
            foreach (GameObject gameObject in _gameObjectManager.Objects)
            {
                gameObject.Destroy();
            }
        }

        public virtual void Load(ContentManager content)
        {
            foreach (GameObject gameObject in _gameObjectManager.Objects)
            {
                gameObject.Load(content);
            }
        }

        public virtual void HandleInput()
        {
            foreach (GameObject gameObject in _gameObjectManager.Objects)
            {
                gameObject.HandleInput();
            }
        }

        public void CheckCollision()
        {
            // Checks once for every pair of 2 GameObjects if the collide.
            for (int i = 0; i < _gameObjectManager.Objects.Count; i++)
            {
                for (int j = i + 1; j < _gameObjectManager.Objects.Count; j++)
                {
                    if (_gameObjectManager.Objects[i].CheckCollision(_gameObjectManager.Objects[j]))
                    {
                        _gameObjectManager.Objects[i].OnCollision(_gameObjectManager.Objects[j]);
                        _gameObjectManager.Objects[j].OnCollision(_gameObjectManager.Objects[i]);
                    }
                }
            }

        }

        public virtual void Update(GameTime gameTime)
        {
            var inputManager = InputManager.GetInputManager();
            inputManager.Update();

            // Handle input
            HandleInput();


            // Update
            foreach (GameObject gameObject in _gameObjectManager.Objects)
            {
                gameObject.Update(gameTime);
            }

            // Check Collission
            CheckCollision();

            _gameObjectManager.Update(gameTime);

            foreach (Animation animation in _animationsToBeAdded)
            {
                _animations.Add(animation);
            }
            _animationsToBeAdded.Clear();

            foreach (Animation animation in _animations)
            {
                animation.Update(gameTime);
                if (animation.IsFinished)
                {
                    _animationsToBeRemoved.Add(animation);
                }
            }

            foreach (Animation animation in _animationsToBeRemoved)
            {
                _animations.Remove(animation);
            }
            _animationsToBeRemoved.Clear();

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _gameObjectManager.Draw(spriteBatch, gameTime);

            foreach (Animation animation in _animations)
            {
                animation.Draw(spriteBatch, gameTime);
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
            _gameObjectManager.AddObject(gameObject);
        }

        /// <summary>
        /// Remove GameObject from the GameManager. 
        /// The GameObject will be removed at the start of the next Update step and its Destroy() mehtod will be called.
        /// After that the object will no longer receive any updates.
        /// </summary>
        /// <param name="gameObject"> The GameObject to Remove. </param>
        public void RemoveGameObject(GameObject gameObject)
        {
            _gameObjectManager.RemoveObject(gameObject);
        }

        public void AddAnimation(Animation animation)
        {
            _animationsToBeAdded.Add(animation);
        }
    }
}
