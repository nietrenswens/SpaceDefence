using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Utilities;

namespace SpaceDefence.GameObjects.GUI
{
    public class PauseMenu : GameObject
    {
        private Texture2D _overlay;
        private SpriteFont _font;

        public override void Load(ContentManager content)
        {
            _overlay = content.Load<Texture2D>("dark_overlay");
            _font = content.Load<SpriteFont>("font");
            base.Load(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_overlay, new Rectangle(0, 0, SpaceDefence.SCREENWIDTH, SpaceDefence.SCREENHEIGHT), Color.White);
            TextUtilities.DrawTextXCentered("Paused", _font, SpaceDefence.SCREENHEIGHT / 2, Color.White, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }
    }
}
