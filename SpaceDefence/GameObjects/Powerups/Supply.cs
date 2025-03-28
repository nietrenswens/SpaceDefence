using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Collision;
using SpaceDefence.Engine.Managers;
using SpaceDefence.GameObjects.Player.Weapons;

namespace SpaceDefence.GameObjects.Powerups
{
    internal class Supply : GameObject
    {
        private RectangleCollider _rectangleCollider;
        private Texture2D _texture;
        private float playerClearance = 100;

        public Supply()
        {

        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _texture = content.Load<Texture2D>("Crate");
            _rectangleCollider = new RectangleCollider(_texture.Bounds);

            SetCollider(_rectangleCollider);
            RandomMove();
        }

        public override void OnCollision(GameObject other)
        {
            RandomMove();
            int rnd = GameManager.GetGameManager().RNG.Next(0, 2);
            if (rnd == 0)
                GameManager.GetGameManager().Player.UpgradeWeapon(new MissleGun());
            else
                GameManager.GetGameManager().Player.UpgradeWeapon(new LaserGun());
            base.OnCollision(other);
        }

        public void RandomMove()
        {
            GameManager gm = GameManager.GetGameManager();
            _rectangleCollider.shape.Location = (gm.RandomScreenLocation() - _rectangleCollider.shape.Size.ToVector2() / 2).ToPoint();

            Vector2 centerOfPlayer = gm.Player.Center.ToVector2();
            while ((_rectangleCollider.shape.Center.ToVector2() - centerOfPlayer).Length() < playerClearance)
                _rectangleCollider.shape.Location = gm.RandomScreenLocation().ToPoint();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_texture, _rectangleCollider.shape, Color.White);
            base.Draw(spriteBatch, gameTime);
        }


    }
}
