using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefence.Utilities
{
    public static class TextUtilities
    {
        public static void DrawTextXCentered(string text, SpriteFont font, float y, Color color, SpriteBatch spriteBatch)
        {
            var width = font.MeasureString(text).X;
            var x = (SpaceDefence.SCREENWIDTH - width) / 2;
            spriteBatch.DrawString(font, text, new Vector2(x, y), color);
        }
    }
}
