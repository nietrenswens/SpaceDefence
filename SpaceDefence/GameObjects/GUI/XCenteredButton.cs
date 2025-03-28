using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SpaceDefence.Collision;

namespace SpaceDefence.GameObjects.GUI
{
    public abstract class XCenteredButton : Button
    {
        private int _y;

        public XCenteredButton(int y) : base(new Point(0, 0))
        {
            _y = y;
        }

        public override void Load(ContentManager content)
        {
            var size = _buttonTexture.Bounds.Size;
            _location = new Point((int)(SpaceDefence.SCREENWIDTH / 2 - size.X / 2), _y);
            _buttonHitbox = new RectangleCollider(new Rectangle(_location, _buttonTexture.Bounds.Size));
        }
    }
}
