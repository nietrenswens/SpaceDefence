using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Animations;
using SpaceDefence.Engine;
using SpaceDefence.Engine.Managers;

namespace SpaceDefence.GameObjects.Planets
{
    public class Planet : GameObject
    {
        protected Animation _animation;
        public Point Position { get; set; }

        public Planet(string spriteSheetName, bool isReceiver, Point? position = null)
        {
            if (position == null)
            {
                Position = new Point(GameManager.GetGameManager().RNG.Next(SpaceDefence.MINX, SpaceDefence.MAXX), GameManager.GetGameManager().RNG.Next(SpaceDefence.MINX, SpaceDefence.MAXX));
            }
            else
            {
                Position = (Point)position;
            }
            _animation = new PlanetAnimation(Position, spriteSheetName);
            CollisionGroup = isReceiver ? Collision.CollisionGroup.DeliverPlanet : Collision.CollisionGroup.PickupPlanet;
        }

        public override void Load(ContentManager content)
        {
            var width = _animation.Width;
            var height = _animation.Height;
            SetCollider(new CircleCollider(Position.X + width / 2, Position.Y + height / 2, width / 2));
            base.Load(content);
        }

        public override void Update(GameTime gameTime)
        {
            _animation.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _animation.Draw(spriteBatch, gameTime);
            base.Draw(spriteBatch, gameTime);
        }
    }
}
