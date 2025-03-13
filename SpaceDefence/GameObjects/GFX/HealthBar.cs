using Microsoft.Xna.Framework.Graphics;
using System.Numerics;

namespace SpaceDefence.GameObjects.GFX
{
    public class HealthBar : GameObject
    {
        private Vector2 _centerTopOfObject;
        private int _width;
        private int _height;
        private int _maxHealth;
        private int _currentHealth;

        public HealthBar(Vector2 centerTopOfObject, int width, int height, int maxHealth, int currentHealth)
        {
            _centerTopOfObject = centerTopOfObject;
            _width = width;
            _height = height;
            _maxHealth = maxHealth;
            _currentHealth = currentHealth;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }
    }
}
