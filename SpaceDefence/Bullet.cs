using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Collision;
using SpaceDefence.Engine.Managers;
using SpaceDefence.GameObjects.Enemies;
using SpaceDefence.GameObjects.Powerups;
using System;

namespace SpaceDefence.GameObjects.Bullets
{
    public class Bullet : GameObject
    {
        private Texture2D _texture;
        private CircleCollider _circleCollider;
        private Vector2 _velocity;
        public float bulletSize = 4;

        public Bullet(Vector2 location, Vector2 direction, float speed)
        {
            _circleCollider = new CircleCollider(location, bulletSize);
            SetCollider(_circleCollider);
            CollisionGroup = CollisionGroup.Bullet;
            _velocity = direction * speed;
        }

        public override void Load(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Bullet");
            base.Load(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _circleCollider.Center += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            var player = GameManager.GetGameManager().Player;
            var bulletToPlayer = player.Center.ToVector2() - _circleCollider.Center;
            if (Math.Abs(bulletToPlayer.Length()) > 1000f)
            {
                LevelManager.GetLevelManager().CurrentLevel.RemoveGameObject(this);
            }

        }

        public override void OnCollision(GameObject other)
        {
            if (other is Alien || other is Supply)
            {
                LevelManager.GetLevelManager().CurrentLevel.RemoveGameObject(this);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_texture, _circleCollider.GetBoundingBox(), Color.Red);
            base.Draw(spriteBatch, gameTime);
        }
    }
}
