using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Animations;
using SpaceDefence.Collision;
using SpaceDefence.Engine;
using SpaceDefence.Engine.Managers;
using SpaceDefence.GameObjects.Player.Weapons;
using SpaceDefence.Levels;
using System;

namespace SpaceDefence.GameObjects.Player
{
    public class Ship : LivingGameObject
    {
        private Weapon _weapon;

        private Texture2D _shipBody;
        private float _buffTimer = 0;
        private float _buffDuration = 15f; // in seconds
        private float _acceleration = 30f;
        private float _maxSpeed = 400f;
        private RectangleCollider _rectangleCollider;
        private Vector2 _velocity;

        public bool IsCarryingDelivery { get; set; }
        public bool IsDead { get; private set; }
        public Guid? LastVisitedPlanetID { get; set; }

        public event EventHandler PickupDeliveryEvent;
        public event EventHandler DropoffDeliveryEvent;
        public event EventHandler KillEvent;

        /// <summary>
        /// The player character
        /// </summary>
        /// <param name="Position">The ship's starting position</param>
        public Ship(Point Position)
        {
            _rectangleCollider = new RectangleCollider(new Rectangle(Position, Point.Zero));
            SetCollider(_rectangleCollider);
            CollisionGroup = CollisionGroup.Player;
            _velocity = Vector2.Zero;
            MaxHealth = 100;
            Health = MaxHealth;
            ShowHealthBar = true;
            IsCarryingDelivery = false;
            _weapon = new DefaultGun();
            IsDead = false;
        }

        public override void Load(ContentManager content)
        {
            // Ship sprites from: https://zintoki.itch.io/space-breaker
            _shipBody = content.Load<Texture2D>("ship_body");
            _rectangleCollider.shape.Size = _shipBody.Bounds.Size;
            _rectangleCollider.shape.Location -= new Point(_shipBody.Width / 2, _shipBody.Height / 2);
            _weapon.Load(content);
            base.Load(content);
        }

        public override void HandleInput()
        {
            if (IsDead)
                return;
            _weapon.HandleInput();
            var inputManager = InputManager.GetInputManager();

            if (inputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
            {
                _velocity.X -= _acceleration;
            }
            if (inputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D))
            {
                _velocity.X += _acceleration;
            }
            if (inputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W))
            {
                _velocity.Y -= _acceleration;
            }
            if (inputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S))
            {
                _velocity.Y += _acceleration;
            }

            if (_velocity.Length() > _maxSpeed)
            {
                _velocity.Normalize();
                _velocity *= _maxSpeed;
            }
            base.HandleInput();
        }

        public override void OnCollision(GameObject other)
        {
            if (IsDead)
                return;
            switch (other.CollisionGroup)
            {
                case CollisionGroup.PickupPlanet:
                    if (IsCarryingDelivery)
                        return;
                    IsCarryingDelivery = true;
                    PickupDeliveryEvent?.Invoke(this, EventArgs.Empty);
                    break;
                case CollisionGroup.DeliverPlanet:
                    if (!IsCarryingDelivery)
                        return;
                    IsCarryingDelivery = false;
                    DropoffDeliveryEvent?.Invoke(this, EventArgs.Empty);
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (IsDead)
                return;
            // Update the Buff timer
            _weapon.Update(gameTime);
            if (_buffTimer > 0)
                _buffTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_buffTimer < 0)
            {
                _buffTimer = 0;
                ResetWeapon();
            }

            var direction = LinePieceCollider.GetAngle(_velocity);
            move(_velocity, gameTime);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (IsDead)
                return;
            var direction = LinePieceCollider.GetAngle(_velocity);
            Rectangle shipLocation = _rectangleCollider.shape;
            shipLocation.Location = _rectangleCollider.shape.Center;
            spriteBatch.Draw(_shipBody, shipLocation, null, Color.White, direction, _shipBody.Bounds.Size.ToVector2() / 2f, SpriteEffects.None, 0);
            _weapon.Draw(spriteBatch, gameTime);
            base.Draw(spriteBatch, gameTime);
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Die();
            }
        }

        public void UpgradeWeapon(Weapon weapon)
        {
            _weapon = weapon;
            _weapon.Load(GameManager.GetGameManager().ContentManager);
            Buff();
        }

        public void ResetWeapon()
        {
            _weapon = new DefaultGun();
            _weapon.Load(GameManager.GetGameManager().ContentManager);
        }

        public override void Die(GameObject killer = null)
        {
            if (IsDead)
                return;
            var gameLevel = LevelManager.GetLevelManager().CurrentLevel as GameLevel;
            IsDead = true;
            gameLevel.AddAnimation(new ExplosionAnimation(_rectangleCollider.shape.Center));

            GameManager.GetGameManager().Timers.Add(new Utilities.Timer(1200, () => LevelManager.GetLevelManager().ChangeLevel(new GameOverLevel())));
        }


        public void Buff()
        {
            _buffTimer = _buffDuration;
        }

        private void move(Vector2 velocity, GameTime gameTime)
        {
            var x = velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            var y = velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            var minX = SpaceDefence.MINX;
            var minY = SpaceDefence.MINY;
            var maxX = SpaceDefence.MAXX;
            var maxY = SpaceDefence.MAXY;

            var clampedX = MathHelper.Clamp(_rectangleCollider.shape.Location.X + (int)x, minX, maxX - _rectangleCollider.shape.Width);
            var clampedY = MathHelper.Clamp(_rectangleCollider.shape.Location.Y + (int)y, minY, maxY - _rectangleCollider.shape.Height);
            _rectangleCollider.shape.Location = new Point((int)clampedX, (int)clampedY);
        }
    }
}
