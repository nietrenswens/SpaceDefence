using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine;

namespace SpaceDefence.GameObjects.GUI.PauseMenu
{
    public class QuitButton : XCenteredButton
    {
        public QuitButton(int y) : base(y)
        {
            SetButtonType(PauseMenuButtonTypes.QUIT);
        }

        public override void Load(ContentManager content)
        {
            _buttonTexture = content.Load<Texture2D>("gui\\quit_game");
            base.Load(content);
        }
    }
}
