using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SpaceDefence.Engine.Managers;
using SpaceDefence.GameObjects.Enemies;
using SpaceDefence.GameObjects.Powerups;
using System;
using SpaceDefence.Engine;
using SpaceDefence.GameObjects.Player;
using System.Linq;

namespace SpaceDefence.GameObjects.Bullets
{
    class Missle : GameObject
    {
        private Texture2D _texture;
        private CircleCollider _circleCollider;
        private Vector2 _velocity;
        private Point _startLocation;
        public float bulletSize = 16;
        private float _angle;

        public Missle(Vector2 location, Vector2 direction, float speed)
        {
            _circleCollider = new CircleCollider(location, bulletSize);
            SetCollider(_circleCollider);
            CollisionGroup = Collision.CollisionGroup.Bullet;
            _velocity = direction * speed;
            _startLocation = location.ToPoint();

            _angle = new LinePieceCollider(location, location + direction).GetAngle();
        }

        public override void Load(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Missle");
            base.Load(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _circleCollider.Center += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            var player = GameManager.GetGameManager().Player;
            var bulletToPlayer = player.GetPosition().Center.ToVector2() - _circleCollider.Center;
            if (Math.Abs(bulletToPlayer.Length()) > 1000f)
            {
                LevelManager.GetLevelManager().CurrentLevel.RemoveGameObject(this);
            }

        }

        public override void OnCollision(GameObject other)
        {
            if (other is LivingGameObject && other is not Ship)
            {
                var go = (LivingGameObject)other;
                go.Die();
                var radiusCircleCollider = new CircleCollider(_circleCollider.GetBoundingBox().Center.ToVector2(), 80);
                CheckCollisionWithOtherEnemies(radiusCircleCollider);
                LevelManager.GetLevelManager().CurrentLevel.RemoveGameObject(this);
            } else if (other is Supply)
            {
                LevelManager.GetLevelManager().CurrentLevel.RemoveGameObject(this);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_texture, _circleCollider.GetBoundingBox(), null, Color.White, _angle, new Vector2(_texture.Width / 2, _texture.Height / 2), SpriteEffects.None, 0);
            base.Draw(spriteBatch, gameTime);
        }

        private void CheckCollisionWithOtherEnemies(CircleCollider radiusCircleCollider)
        {
            var enemies = LevelManager.GetLevelManager().CurrentLevel.GameObjects.OfType<Alien>();
            foreach (var enemy in enemies)
            {
                if (enemy.CheckCollision(radiusCircleCollider))
                {
                    enemy.Die();
                }
            }
        }
    }
}
