using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine;
using SpaceDefence.Engine.Managers;
using SpaceDefence.GameObjects.Player;

namespace SpaceDefence.GameObjects.Planets
{
    public abstract class Planet : GameObject
    {
        protected Animation _animation;
        public Point Position { get; set; }
        private Texture2D _pixel;

        public Planet()
        {
            _pixel = new Texture2D(GameManager.GetGameManager().GraphicsDevice, 1, 1);
            _pixel.SetData(new Color[] { Color.White });
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Ship player)
            {
                if (player.IsCarryingDelivery)
                {
                    player.IsCarryingDelivery = false;
                    // TODO: Add score
                }
                else
                {
                    player.IsCarryingDelivery = true;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            _animation.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_pixel, collider.GetBoundingBox(), Color.White);
            _animation.Draw(spriteBatch, gameTime);
            base.Draw(spriteBatch, gameTime);
        }
    }
}
