using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Collision;
using SpaceDefence.Engine;
using SpaceDefence.Engine.Managers;
using SpaceDefence.GameObjects.Bullets;
using SpaceDefence.Levels;

namespace SpaceDefence.GameObjects.Enemies
{
    internal class Alien : LivingGameObject
    {
        private CircleCollider _circleCollider;
        private Texture2D _texture;
        private const float PlayerClearance = 200;
        private float _speed;

        public Alien()
        {
            CollisionGroup = CollisionGroup.Enemy;
            _speed = 0.1f;
            MaxHealth = 40;
            Health = MaxHealth;
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
                    LevelManager.GetLevelManager().ChangeLevel(new GameOverLevel());
                    break;
            }
            base.OnCollision(other);
        }

        private void RandomMove()
        {
            GameManager gm = GameManager.GetGameManager();
            _circleCollider.Center = gm.RandomScreenLocation();

            Vector2 centerOfPlayer = gm.Player.GetPosition().Center.ToVector2();
            while ((_circleCollider.Center - centerOfPlayer).Length() < PlayerClearance)
                _circleCollider.Center = gm.RandomScreenLocation();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _circleCollider.GetBoundingBox(), Color.White);
            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
            base.Update(gameTime);
        }

        private void Move(GameTime gameTime)
        {
            var playerLocation = GameManager.GetGameManager().Player.GetPosition().Center.ToVector2();

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
                Die();
        }

        private void Die()
        {
            LevelManager.GetLevelManager().CurrentLevel.RemoveGameObject(this);
        }

    }
}
