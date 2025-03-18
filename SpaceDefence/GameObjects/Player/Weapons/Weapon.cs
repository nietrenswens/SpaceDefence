using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine.Interfaces;

namespace SpaceDefence.GameObjects.Player.Weapons
{
    public abstract class Weapon : ILoadable, IUpdatable, IInputable, Engine.Interfaces.IDrawable
    {
        protected Texture2D _texture;

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        public virtual void HandleInput()
        {
        }

        public virtual void Load(ContentManager content)
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}
