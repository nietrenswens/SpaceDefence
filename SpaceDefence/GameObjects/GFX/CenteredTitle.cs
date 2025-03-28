using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefence.GameObjects.GFX
{
    public class CenteredTitle : GameObject
    {
        private SpriteFont _font;
        private string _text;

        public float Y { get; private set; }

        public CenteredTitle(string text)
        {
            _text = text;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Vector2 size = _font.MeasureString(_text);
            Vector2 position = new Vector2((SpaceDefence.SCREENWIDTH - size.X) / 2, (SpaceDefence.SCREENHEIGHT - size.Y) / 2);
            if (Y == 0f)
                Y = position.Y;
            spriteBatch.DrawString(_font, _text, position, Color.White);
            base.Draw(spriteBatch, gameTime);
        }

        public override void Load(ContentManager content)
        {
            _font = content.Load<SpriteFont>("font");
            base.Load(content);
        }
    }
}
