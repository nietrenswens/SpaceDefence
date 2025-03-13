using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefence.Engine
{
    public class LivingGameObject : GameObject
    {
        public float Health { get; protected set; }
        public float MaxHealth { get; protected set; }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var boundingBox = collider.GetBoundingBox();
            var healthBarTopLeft = new Vector2(boundingBox.Left, boundingBox.Top - 10);
            base.Draw(gameTime, spriteBatch);
        }
    }
}
