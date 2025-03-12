using System;
using SpaceDefence.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefence
{
    public class Ship : GameObject
    {
        private Texture2D _shipBody;
        private Texture2D _baseTurret;
        private Texture2D _laserTurret;
        private float _buffTimer = 10;
        private float _buffDuration = 10f;
        private float _acceleration = 30f;
        private float _maxSpeed = 200f;
        private RectangleCollider _rectangleCollider;
        private Point _target;
        private Vector2 _velocity;

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
        }

        public override void Load(ContentManager content)
        {
            // Ship sprites from: https://zintoki.itch.io/space-breaker
            _shipBody = content.Load<Texture2D>("ship_body");
            _baseTurret = content.Load<Texture2D>("base_turret");
            _laserTurret = content.Load<Texture2D>("laser_turret");
            _rectangleCollider.shape.Size = _shipBody.Bounds.Size;
            _rectangleCollider.shape.Location -= new Point(_shipBody.Width/2, _shipBody.Height/2);
            base.Load(content);
        }



        public override void HandleInput(InputManager inputManager)
        {
            base.HandleInput(inputManager);
            _target = inputManager.CurrentMouseState.Position;
            if(inputManager.LeftMousePress())
            {

                Vector2 aimDirection = LinePieceCollider.GetDirection(GetPosition().Center, _target);
                Vector2 turretExit = _rectangleCollider.shape.Center.ToVector2() + aimDirection * _baseTurret.Height / 2f;
                if (_buffTimer <= 0)
                {
                    GameManager.GetGameManager().AddGameObject(new Bullet(turretExit, aimDirection, 150));
                }
                else
                {
                    GameManager.GetGameManager().AddGameObject(new Laser(new LinePieceCollider(turretExit, _target.ToVector2()),400));
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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
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
            }
            else
            {
                Rectangle turretLocation = _laserTurret.Bounds;
                turretLocation.Location = _rectangleCollider.shape.Center;
                spriteBatch.Draw(_laserTurret, turretLocation, null, Color.White, aimAngle, turretLocation.Size.ToVector2() / 2f, SpriteEffects.None, 0);
            }
            base.Draw(gameTime, spriteBatch);
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
            _rectangleCollider.shape.Location += new Point((int)x, (int)y);

            if (_rectangleCollider.shape.Right < 0 && velocity.X < 0)
            {
                _rectangleCollider.shape.X = SpaceDefence.SCREENWIDTH;
            }
            else if (_rectangleCollider.shape.Left > SpaceDefence.SCREENWIDTH && velocity.X > 0)
            {
                _rectangleCollider.shape.X = 0;
            }
            else if (_rectangleCollider.shape.Bottom < 0 && velocity.Y < 0)
            {
                _rectangleCollider.shape.Y = SpaceDefence.SCREENHEIGHT;
            }
            else if (_rectangleCollider.shape.Top > SpaceDefence.SCREENHEIGHT && velocity.Y > 0)
            {
                _rectangleCollider.shape.Y = 0;
            }
        }
    }
}
