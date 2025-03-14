using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine;

namespace SpaceDefence.GameObjects.GUI.PauseMenu
{
    public class ResumeButton : XCenteredButton
    {
        public ResumeButton(int y) : base(y)
        {
            SetButtonType(PauseMenuButtonTypes.RESUME);
        }

        public override void Load(ContentManager content)
        {
            _buttonTexture = content.Load<Texture2D>("gui\\resume_button");
            base.Load(content);
        }
    }
}
