using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine;
using SpaceDefence.Engine.Managers;
using SpaceDefence.GameObjects.Player;

namespace SpaceDefence.GameObjects.Enemies
{
    public class Asteroid : GameObject
    {
        private CircleCollider _circleCollider;
        private Texture2D _texture;

        private float _scale;

        public Asteroid(float scale = 1f)
        {
            _scale = scale;
        }

        public override void Load(ContentManager content)
        {
            _texture = content.Load<Texture2D>("asteroid");
            _circleCollider = new CircleCollider(Vector2.Zero, 24 * _scale);
            SetCollider(_circleCollider);
            Spawn();
            base.Load(content);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
            var sourceRect = new Rectangle(0, 0, 64, 64);
            int width = (int)(_texture.Width * _scale);
            int height = (int)(_texture.Height * _scale);
            var destRect = new Rectangle((int)_circleCollider.GetBoundingBox().Location.X - (width / 2), (int)_circleCollider.GetBoundingBox().Location.Y - (height / 2), width, height);
            spriteBatch.Draw(_texture, destRect, sourceRect, Color.White);
        }

        private void Spawn()
        {
            var player = GameManager.GetGameManager().Player;
            var center = player.Center;
            var rng = GameManager.GetGameManager().RNG;

            _circleCollider.Center = new Vector2(rng.Next(SpaceDefence.MINX, SpaceDefence.MAXX), rng.Next(SpaceDefence.MINY, SpaceDefence.MAXY));
            while (Vector2.Distance(center.ToVector2(), _circleCollider.Center) < 300)
            {
                _circleCollider.Center = new Vector2(rng.Next(SpaceDefence.MINX, SpaceDefence.MAXX), rng.Next(SpaceDefence.MINY, SpaceDefence.MAXY));
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other.CollisionGroup == Collision.CollisionGroup.Player)
            {
                var player = other as Ship;
                player.Die();
                return;
            }
            else if (other.CollisionGroup == Collision.CollisionGroup.Enemy)
            {
                var gameObj = other as LivingGameObject;
                gameObj.Die();
            }
            base.OnCollision(other);
        }
    }
}
