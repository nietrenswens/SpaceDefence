using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine.Exceptions;

namespace SpaceDefence.Engine
{
    public abstract class Animation : Interfaces.IUpdatable, Interfaces.IDrawable
    {
        public bool IsFinished { get; set; }
        public Point Position { get; set; }

        private int _frameDuration;
        private int _currentFrameIndex;
        private int _frameWidth;
        private int _frameHeight;
        private int _time;
        private bool _isLooping;

        private int _offsetLeft;
        private int _offsetRight;

        protected Texture2D _texture;

        public Animation(int frameDuration, int frameWidth, int frameHeight, Point position, int offsetLeft = 0, int offsetRight = 0, bool isLooping = false)
        {
            _frameDuration = frameDuration;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _currentFrameIndex = 0;
            _time = 0;
            Position = position;
            _offsetLeft = offsetLeft;
            _offsetRight = offsetRight;
            _isLooping = isLooping;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (IsFinished)
                return;

            _time += gameTime.ElapsedGameTime.Milliseconds;

            if (_isLooping)
                _currentFrameIndex = _time / _frameDuration % (_texture.Width / _frameWidth);
            else
                _currentFrameIndex = _time / _frameDuration;

            if (_currentFrameIndex >= _texture.Width / _frameWidth && !_isLooping)
                IsFinished = true;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (IsFinished) return;
            if (_texture == null) throw new IncorrectAnimationFrameException("Texture is null");

            Rectangle sourceRect = new Rectangle(_currentFrameIndex * _frameWidth, 0, _frameWidth, _frameHeight);
            Rectangle rect = new Rectangle(Position.X - _offsetLeft, Position.Y, _frameWidth, _frameHeight);
            spriteBatch.Draw(_texture, rect, sourceRect, Color.White);
        }
    }
}
