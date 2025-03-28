using Microsoft.Xna.Framework;
using SpaceDefence.Engine.Interfaces;
using System;

namespace SpaceDefence.Utilities
{
    public class Timer : IUpdatable
    {
        private float _durationInMS;
        private float _progress;

        public bool IsFinished { get; private set; }

        private Action _callback;

        public Timer(float durationInMS, Action callback)
        {
            _durationInMS = durationInMS;
            _progress = 0;
            _callback = callback;
        }

        public void Update(GameTime gameTime)
        {
            _progress += gameTime.ElapsedGameTime.Milliseconds;
            if (_progress > _durationInMS && !IsFinished)
            {
                IsFinished = true;
                _callback();
            }
        }
    }
}
