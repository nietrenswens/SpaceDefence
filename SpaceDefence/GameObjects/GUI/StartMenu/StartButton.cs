using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine;

namespace SpaceDefence.GameObjects.GUI.StartMenu
{
    public class StartButton : XCenteredButton
    {
        public StartButton(int y) : base(y)
        {
            SetButtonType(StartMenuButtonTypes.START_BUTTON);
        }

        public override void Load(ContentManager content)
        {
            _buttonTexture = content.Load<Texture2D>("gui\\start_game");
            base.Load(content);
        }
    }
    
}
