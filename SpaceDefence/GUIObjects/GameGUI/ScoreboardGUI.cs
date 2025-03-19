using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine;
using SpaceDefence.Engine.Managers;

namespace SpaceDefence.GUIObjects.GameGUI
{
    public class ScoreboardGUI : GUIObject
    {
        private GameStats _gameStats => GameManager.GetGameManager().GameStats;
        private Texture2D _texture;
        private SpriteFont _font;

        private float _backgroundScale = 0.8f;
        private float _width;
        private float _height;
        private Point _location;

        public ScoreboardGUI()
        {
            _location = new Point(0, 0);
        }

        public override void Load(ContentManager content)
        {
            _texture = content.Load<Texture2D>("gui\\scoreboard");
            _font = content.Load<SpriteFont>("scorefont");
            _width = _texture.Width;
            _height = _texture.Height;
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_texture, new Rectangle(_location.X, _location.Y, (int)(_width * _backgroundScale), (int)(_height * _backgroundScale)), new Rectangle(_location.X, _location.Y, (int)_width, (int)_height), Color.White);
            spriteBatch.DrawString(_font, _gameStats.Kills.ToString(), new Vector2(_location.X + _width - 110, _location.Y + 45), Color.White);
        }
    }
}
