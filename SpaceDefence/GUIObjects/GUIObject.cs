using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine.Interfaces;

namespace SpaceDefence.GUIObjects
{
    public abstract class GUIObject : ILoadable, IUpdatable, Engine.Interfaces.IDrawable
    {
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        public abstract void Load(ContentManager content);

        public void Update(GameTime gameTime)
        {
        }

    }
}
