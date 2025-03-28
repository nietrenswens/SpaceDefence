using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine.Managers;
using SpaceDefence.GameObjects.Bullets;


namespace SpaceDefence.GameObjects.Player.Weapons
{
    public class MissleGun : Weapon
    {
        private Point _target;
        private float _aimAngle => LinePieceCollider.GetAngle(LinePieceCollider.GetDirection(GameManager.GetGameManager().Player.Center, _target));
        private Ship _player => GameManager.GetGameManager().Player;
        public override void Load(ContentManager content)
        {
            _texture = content.Load<Texture2D>("missle_turret");
        }

        public override void HandleInput()
        {
            var inputManager = InputManager.GetInputManager();
            _target = inputManager.GetRelativeMousePosition().ToPoint();
            if (inputManager.LeftMousePress())
            {
                Vector2 aimDirection = LinePieceCollider.GetDirection(_player.Center, _target);
                Vector2 turretExit = _player.Center.ToVector2() + aimDirection * _texture.Height / 2f;
                //LevelManager.GetLevelManager().CurrentLevel.AddGameObject(new Missle(turretExit, aimDirection, 150));
                LevelManager.GetLevelManager().CurrentLevel.AddGameObject(new Missle(turretExit, aimDirection, 150));
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Rectangle turretLocation = _texture.Bounds;
            turretLocation.Location = _player.Center;
            spriteBatch.Draw(_texture, turretLocation, null, Color.White, _aimAngle, turretLocation.Size.ToVector2() / 2f, SpriteEffects.None, 0);
        }
    }
}
