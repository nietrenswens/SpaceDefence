using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine;
using SpaceDefence.Engine.Managers;
using SpaceDefence.GameObjects.Playable;

namespace SpaceDefence.GameObjects.Enemies
{
    public class Asteroid : GameObject
    {
        private CircleCollider _circleCollider;
        private Texture2D _texture;
        private Texture2D _pixel;
        public Asteroid()
        {
        }

        public override void Load(ContentManager content)
        {
            _texture = content.Load<Texture2D>("asteroid");
            _circleCollider = new CircleCollider(Vector2.Zero, 16);
            SetCollider(_circleCollider);
            Spawn();
            base.Load(content);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_texture, _circleCollider.GetBoundingBox().Location.ToVector2() - new Vector2(32, 32), Color.White);
            base.Draw(spriteBatch, gameTime);
        }

        private void Spawn()
        {
            var player = GameManager.GetGameManager().Player;
            var playerPos = player.GetPosition();
            var rng = GameManager.GetGameManager().RNG;

            _circleCollider.Center = new Vector2(rng.Next(SpaceDefence.MINX, SpaceDefence.MAXX), rng.Next(SpaceDefence.MINY, SpaceDefence.MAXY));
            while (Vector2.Distance(playerPos.Center.ToVector2(), _circleCollider.Center) < 300)
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
            } else if (other.CollisionGroup == Collision.CollisionGroup.Enemy)
            {
                var gameObj = other as LivingGameObject;
                gameObj.Die();
            }
            base.OnCollision(other);
        }
    }
}
