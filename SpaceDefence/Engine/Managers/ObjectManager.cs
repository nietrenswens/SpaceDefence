using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine.Interfaces;
using System.Collections.Generic;

namespace SpaceDefence.Engine.Managers
{
    public class ObjectManager<T> where T : Interfaces.IDrawable, IUpdatable
    {
        private List<T> _objects;

        public List<T> Objects { get => _objects; }
        private List<T> _toBeRemoved;
        private List<T> _toBeAdded;

        public ObjectManager()
        {
            _objects = new List<T>();
            _toBeRemoved = new List<T>();
            _toBeAdded = new List<T>();
        }

        public void AddObject(T obj)
        {
            if (obj is ILoadable loadableObj)
                loadableObj.Load(GameManager.GetGameManager().ContentManager);
            _toBeAdded.Add(obj);
        }

        public void RemoveObject(T obj)
        {
            _toBeRemoved.Add(obj);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var obj in Objects)
            {
                obj.Draw(spriteBatch, gameTime);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var obj in Objects)
            {
                obj.Update(gameTime);
            }
            foreach (var obj in _toBeAdded)
            {
                Objects.Add(obj);
            }
            _toBeAdded.Clear();
            foreach (var obj in _toBeRemoved)
            {
                Objects.Remove(obj);
            }
            _toBeRemoved.Clear();
        }

    }
}
