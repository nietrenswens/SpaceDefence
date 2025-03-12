﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Collision;
using SpaceDefence.Engine;

namespace SpaceDefence
{
    internal class Alien : GameObject
    {
        private CircleCollider _circleCollider;
        private Texture2D _texture;
        private const float PlayerClearance = 200;

        public Alien()
        {
            CollisionGroup = CollisionGroup.Enemy;
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
                    RandomMove();
                    break;
                case CollisionGroup.Player:
                    GameManager.GetGameManager().SetGameState(GameState.GameOver);
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
            
            var direction = (playerLocation - _circleCollider.Center);
            direction.Normalize();
            
            _circleCollider.Center += direction * 0.1f * gameTime.ElapsedGameTime.Milliseconds;
        }


    }
}
