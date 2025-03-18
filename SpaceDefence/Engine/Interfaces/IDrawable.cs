using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefence.Engine.Interfaces
{
    public interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
