using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine.Managers;

namespace SpaceDefence.GameObjects.GFX
{
    public class HealthBar : GameObject
    {
        private Vector2 _centerTopOfObject;
        private int _width;
        private int _height;
        private float _maxHealth;
        private float _currentHealth;
        private const float _offsetY = 20f;
        private Texture2D _greyPixel;
        private Texture2D _greenPixel;

        public HealthBar(Vector2 centerTopOfObject, int width, int height, float maxHealth, float currentHealth)
        {
            _centerTopOfObject = centerTopOfObject;
            _width = width;
            _height = height;
            _maxHealth = maxHealth;
            _currentHealth = currentHealth;

            _greyPixel = new Texture2D(GameManager.GetGameManager().GraphicsDevice, 1, 1);
            _greyPixel.SetData(new[] { Color.Gray });
            _greenPixel = new Texture2D(GameManager.GetGameManager().GraphicsDevice, 1, 1);
            _greenPixel.SetData(new[] { Color.Green });
        }

        public void SetHealth(float health)
        {
            _currentHealth = health;
        }

        public void SetLocation(Vector2 centerTopOfObject)
        {
            _centerTopOfObject = centerTopOfObject;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var x = _centerTopOfObject.X - _width / 2;
            var y = _centerTopOfObject.Y - _offsetY - _height / 2;
            var healthPercentage = (float)_currentHealth / _maxHealth;
            var healthWidth = (int)(_width * healthPercentage);

            spriteBatch.Draw(_greyPixel, new Rectangle((int)x, (int)y, _width, _height), Color.Gray);
            spriteBatch.Draw(_greenPixel, new Rectangle((int)x, (int)y, healthWidth, _height), Color.Green);
        }
    }
}
