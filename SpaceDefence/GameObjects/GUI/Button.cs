using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Collision;
using SpaceDefence.Engine.Managers;
using System;

namespace SpaceDefence.GameObjects.GUI
{
    public abstract class Button : GameObject
    {
        protected Collider _buttonHitbox;
        protected Texture2D? _buttonTexture;
        protected Texture2D _hoverTexture;
        private string? _buttonType;
        protected Point _location;

        protected bool _hovering;

        public event EventHandler<ButtonEventArgs> ButtonPressed;

        public Button(Point location)
        {
            _location = location;
            _hovering = false;
        }

        public void SetButtonType(string type)
        {
            _buttonType = type;
        }

        public override void Load(ContentManager content)
        {
            _buttonHitbox = new RectangleCollider(new Rectangle(_location, _buttonTexture.Bounds.Size));
            base.Load(content);
        }

        public override void HandleInput()
        {
            if (_buttonType == null)
            {
                throw new Exception("Button type not set.");
            }
            var inputManager = InputManager.GetInputManager();
            if (_buttonHitbox.Contains(inputManager.CurrentMouseState.Position.ToVector2()))
            {
                _hovering = true;
                if (inputManager.LeftMousePress())
                {
                    ButtonPressed?.Invoke(this, new ButtonEventArgs { Type = _buttonType });
                }
            }
            else
            {
                _hovering = false;
            }
                base.HandleInput();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_hovering ? (_hoverTexture ?? _buttonTexture) : _buttonTexture, _buttonHitbox.GetBoundingBox(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }

    }

    public record ButtonEventArgs
    {
        public string Type { get; init; }
    }
}
