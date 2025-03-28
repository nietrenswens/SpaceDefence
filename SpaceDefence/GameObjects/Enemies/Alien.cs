using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Animations;
using SpaceDefence.Collision;
using SpaceDefence.Engine;
using SpaceDefence.Engine.Managers;
using SpaceDefence.GameObjects.Bullets;
using SpaceDefence.GameObjects.Player;

namespace SpaceDefence.GameObjects.Enemies
{
    internal class Alien : LivingGameObject
    {
        private CircleCollider _circleCollider;
        private Texture2D _texture;
        private float _speed;
        private int _playerDamageTimer = 0;

        private const int _playerDamageTimeIntervals = 400;
        private const float PlayerClearance = 960;

        public Alien()
        {
            CollisionGroup = CollisionGroup.Enemy;
            _speed = 0.1f;
            MaxHealth = 40;
            Health = MaxHealth;
            ShowHealthBar = true;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _texture = content.Load<Texture2D>("Alien");
            _circleCollider = new CircleCollider(Vector2.Zero, _texture.Width / 2);
            SetCollider(_circleCollider);
            RandomMove();
        }

        public override void OnCollision(GameObject other)
        {
            switch (other.CollisionGroup)
            {
                case CollisionGroup.Bullet:
                    TakeDamage(other);
                    break;
                case CollisionGroup.Player:
                    if (other is not Ship player)
                        return;
                    if (_playerDamageTimer < 0)
                    {
                        player.TakeDamage(10);
                        _playerDamageTimer = _playerDamageTimeIntervals;
                    }
                    break;
            }
            base.OnCollision(other);
        }

        private void RandomMove()
        {
            GameManager gm = GameManager.GetGameManager();
            _circleCollider.Center = gm.RandomScreenLocation();

            Vector2 centerOfPlayer = gm.Player.Center.ToVector2();
            while ((_circleCollider.Center - centerOfPlayer).Length() < PlayerClearance)
                _circleCollider.Center = gm.RandomScreenLocation();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_texture, _circleCollider.GetBoundingBox(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            _playerDamageTimer -= gameTime.ElapsedGameTime.Milliseconds;
            Move(gameTime);
            base.Update(gameTime);
        }

        public bool CheckCollision(Collider collider)
        {
            return _circleCollider.CheckIntersection(collider);
        }

        private void Move(GameTime gameTime)
        {
            var playerLocation = GameManager.GetGameManager().Player.Center.ToVector2();

            var direction = playerLocation - _circleCollider.Center;
            direction.Normalize();

            _circleCollider.Center += direction * _speed * gameTime.ElapsedGameTime.Milliseconds;
        }

        private void TakeDamage(GameObject projectile)
        {
            switch (projectile)
            {
                case Bullet bullet:
                    Health -= 10;
                    break;
                case Laser laser:
                    Health -= 50;
                    break;
            }

            if (Health <= 0)
                Die(projectile);
        }

        public override void Die(GameObject killer = null)
        {
            LevelManager.GetLevelManager().CurrentLevel.RemoveGameObject(this);

            if (killer != null && killer.CollisionGroup == CollisionGroup.Bullet)
            {
                var gameManager = GameManager.GetGameManager();
                gameManager.GameStats.AddKill();
            }

            LevelManager.GetLevelManager().CurrentLevel.AddAnimation(new ExplosionAnimation(_circleCollider.Center.ToPoint()));
        }

    }
}
