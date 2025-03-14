using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Collision;
using SpaceDefence.Engine;
using SpaceDefence.Engine.Managers;
using System;

namespace SpaceDefence.GameObjects.GUI
{
    public abstract class Button : GameObject
    {
        protected Collider _buttonHitbox;
        protected Texture2D _buttonTexture;
        private string? _buttonType;
        protected Point _location;

        public event EventHandler<ButtonEventArgs> ButtonPressed;

        public Button(Point location)
        {
            _location = location;
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
            if (inputManager.LeftMousePress() && _buttonHitbox.Contains(inputManager.CurrentMouseState.Position.ToVector2()))
            {
                ButtonPressed?.Invoke(this, new ButtonEventArgs { Type = _buttonType });
            }
            base.HandleInput();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_buttonTexture, _buttonHitbox.GetBoundingBox(), Color.White);
            base.Draw(gameTime, spriteBatch);
        }

    }

    public record ButtonEventArgs
    {
        public string Type { get; init; }
    }
}
