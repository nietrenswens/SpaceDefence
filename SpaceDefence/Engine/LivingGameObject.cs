#nullable enable
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.GameObjects.GFX;

namespace SpaceDefence.Engine
{
    public class LivingGameObject : GameObject
    {
        public float Health { get; protected set; }
        public float MaxHealth { get; protected set; }
        public bool ShowHealthBar { get; protected set; }

        private HealthBar? _healthBar;

        public override void Update(GameTime gameTime)
        {
            if (_healthBar == null)
                _healthBar = new HealthBar(new Vector2(GetCenterX(), GetTopY()), 200, 20, MaxHealth, Health);
            _healthBar.SetHealth(Health);
            _healthBar.SetLocation(new Vector2(GetCenterX(), GetTopY()));
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (_healthBar != null && ShowHealthBar)
                _healthBar.Draw(spriteBatch, gameTime);
            base.Draw(spriteBatch, gameTime);
        }

        public virtual void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0)
                Destroy();
        }

        public virtual void Die(GameObject? killer = null) { }

        private float GetCenterX()
        {
            return collider.GetBoundingBox().Center.ToVector2().X;
        }

        private float GetTopY()
        {
            return collider.GetBoundingBox().Top;
        }

        public override void Destroy()
        {
            _healthBar = null;
            base.Destroy();
        }
    }
}
