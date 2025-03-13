using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine.Managers;

namespace SpaceDefence.GameObjects.Bullets
{
    internal class Laser : GameObject
    {
        private LinePieceCollider linePiece;
        private Texture2D sprite;
        private double lifespan = .25f;

        public Laser(LinePieceCollider linePiece)
        {
            this.linePiece = linePiece;
            SetCollider(linePiece);
            CollisionGroup = Collision.CollisionGroup.Bullet;
        }

        public Laser(LinePieceCollider linePiece, float length) : this(linePiece)
        {
            // Sets the length of the laser to be equal to the width of the screen, so it will always cover the full screen.
            this.linePiece.Length = length;
        }

        public override void Load(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Bullet");
            base.Load(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (lifespan < 0)
                LevelManager.GetLevelManager().CurrentLevel.RemoveGameObject(this);
            lifespan -= gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var playerLocation = GameManager.GetGameManager().Player.GetPosition().Center.ToVector2();
            Rectangle target = new Rectangle((int)linePiece.Start.X, (int)linePiece.Start.Y, 8, (int)linePiece.Length);
            spriteBatch.Draw(sprite, target, null, Color.White, linePiece.GetAngle(), new Vector2(sprite.Width / 2f, sprite.Height), SpriteEffects.None, 1);
            base.Draw(gameTime, spriteBatch);
        }
    }
}
