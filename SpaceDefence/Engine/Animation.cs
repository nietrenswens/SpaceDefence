using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine.Exceptions;

namespace SpaceDefence.Engine
{
    public abstract class Animation : Interfaces.IUpdatable, Interfaces.IDrawable
    {
        public bool IsFinished => _currentFrameIndex >= _texture.Width / _frameWidth;
        public Point Position { get; set; }

        private int _frameDuration;
        private int _currentFrameIndex;
        private int _frameWidth;
        private int _frameHeight;
        private int time;

        private int offsetLeft;
        private int offsetRight;

        protected Texture2D _texture;

        public Animation(int frameDuration, int frameWidth, int frameHeight,  Point position, int offsetLeft = 0, int offsetRight = 0)
        {
            this._frameDuration = frameDuration;
            this._frameWidth = frameWidth;
            this._frameHeight = frameHeight;
            _currentFrameIndex = 0;
            time = 0;
            Position = position;
            this.offsetLeft = offsetLeft;
            this.offsetRight = offsetRight;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (IsFinished)
                return;

            time += gameTime.ElapsedGameTime.Milliseconds;
            _currentFrameIndex = time / _frameDuration;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (IsFinished) return;
            if (_texture == null) throw new IncorrectAnimationFrameException("Texture is null");

            Rectangle sourceRect = new Rectangle(_currentFrameIndex * _frameWidth, 0, _frameWidth, _frameHeight);
            Rectangle rect = new Rectangle(Position.X - offsetLeft, Position.Y, _frameWidth, _frameHeight);
            spriteBatch.Draw(_texture, rect, sourceRect, Color.White);
        }
    }
}
