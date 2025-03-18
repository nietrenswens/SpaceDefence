using System;
using SpaceDefence.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine.Managers;
using SpaceDefence.GameObjects.Bullets;
using SpaceDefence.Levels;
using SpaceDefence.Engine;

namespace SpaceDefence.GameObjects.Playable
{
    public class Ship : LivingGameObject
    {
        private Texture2D _shipBody;
        private Texture2D _baseTurret;
        private Texture2D _laserTurret;
        private float _buffTimer = 0;
        private float _buffDuration = 10f;
        private float _acceleration = 30f;
        private float _maxSpeed = 400f;
        private RectangleCollider _rectangleCollider;
        private Point _target;
        private Vector2 _velocity;

        public float Width => _rectangleCollider.shape.Width;
        public float Height => _rectangleCollider.shape.Height;

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
        }

        public override void Load(ContentManager content)
        {
            // Ship sprites from: https://zintoki.itch.io/space-breaker
            _shipBody = content.Load<Texture2D>("ship_body");
            _baseTurret = content.Load<Texture2D>("base_turret");
            _laserTurret = content.Load<Texture2D>("laser_turret");
            _rectangleCollider.shape.Size = _shipBody.Bounds.Size;
            _rectangleCollider.shape.Location -= new Point(_shipBody.Width / 2, _shipBody.Height / 2);
            base.Load(content);
        }

        public override void HandleInput()
        {
            var inputManager = InputManager.GetInputManager();
            _target = inputManager.GetRelativeMousePosition().ToPoint();
            if (inputManager.LeftMousePress())
            {

                Vector2 aimDirection = LinePieceCollider.GetDirection(GetPosition().Center, _target);
                Vector2 turretExit = _rectangleCollider.shape.Center.ToVector2() + aimDirection * _baseTurret.Height / 2f;
                if (_buffTimer <= 0)
                {
                    LevelManager.GetLevelManager().CurrentLevel.AddGameObject(new Bullet(turretExit, aimDirection, 150));
                } else
                {
                    LevelManager.GetLevelManager().CurrentLevel.AddGameObject(new Laser(new LinePieceCollider(turretExit, _target.ToVector2()), 400));
                }
            }

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

        public override void Update(GameTime gameTime)
        {
            // Update the Buff timer
            if (_buffTimer > 0)
                _buffTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            var direction = LinePieceCollider.GetAngle(_velocity);
            move(_velocity, gameTime);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var direction = LinePieceCollider.GetAngle(_velocity);
            Rectangle shipLocation = _rectangleCollider.shape;
            shipLocation.Location = _rectangleCollider.shape.Center;
            spriteBatch.Draw(_shipBody, shipLocation, null, Color.White, direction, _shipBody.Bounds.Size.ToVector2() / 2f, SpriteEffects.None, 0);
            float aimAngle = LinePieceCollider.GetAngle(LinePieceCollider.GetDirection(GetPosition().Center, _target));
            if (_buffTimer <= 0)
            {
                Rectangle turretLocation = _baseTurret.Bounds;
                turretLocation.Location = _rectangleCollider.shape.Center;
                spriteBatch.Draw(_baseTurret, turretLocation, null, Color.White, aimAngle, turretLocation.Size.ToVector2() / 2f, SpriteEffects.None, 0);
            } else
            {
                Rectangle turretLocation = _laserTurret.Bounds;
                turretLocation.Location = _rectangleCollider.shape.Center;
                spriteBatch.Draw(_laserTurret, turretLocation, null, Color.White, aimAngle, turretLocation.Size.ToVector2() / 2f, SpriteEffects.None, 0);
            }
            base.Draw(spriteBatch, gameTime);
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                LevelManager.GetLevelManager().ChangeLevel(new GameOverLevel());
            }
        }


        public void Buff()
        {
            _buffTimer = _buffDuration;
        }

        public Rectangle GetPosition()
        {
            return _rectangleCollider.shape;
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
