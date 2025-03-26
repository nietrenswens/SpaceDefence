using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Objectives;

namespace SpaceDefence.GUIObjects
{
    public class GUIObjective : GUIObject
    {
        private Objective _objective;
        private Texture2D _texture;
        private SpriteFont _titleFont;
        private SpriteFont _subTitleFont;

        protected float _iconScale = 1.0f;

        private const int ICON_SIZE = 64;

        public GUIObjective(Objective objective)
        {
            _objective = objective;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var drawPosition = new Vector2(40, SpaceDefence.SCREENHEIGHT - _texture.Height - 400);
            spriteBatch.Draw(_texture, new Rectangle(drawPosition.ToPoint(), new Point(ICON_SIZE, ICON_SIZE)), new Rectangle(new Point(0, 0), new Point(_texture.Width, _texture.Height)), Color.White);

            var titleHeight = _titleFont.MeasureString(_objective.Title).Y;
            var subTitleHeight = _titleFont.MeasureString(_objective.Subtitle).Y;
            spriteBatch.DrawString(_titleFont, _objective.Title, drawPosition + new Vector2(ICON_SIZE + 20, (ICON_SIZE - titleHeight / 2) / 6), Color.White);
            spriteBatch.DrawString(_subTitleFont, _objective.Subtitle, drawPosition + new Vector2(ICON_SIZE + 20, (ICON_SIZE - subTitleHeight / 2) / 6 * 5), Color.White);
        }

        public override void Load(ContentManager content)
        {
            _titleFont = content.Load<SpriteFont>("objective_fonts\\title");
            _subTitleFont = content.Load<SpriteFont>("objective_fonts\\subtitle");
            _texture = content.Load<Texture2D>("gui\\objectives\\" + _objective.IconSpriteName);
        }
    }
}
