using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine;
using SpaceDefence.Engine.Managers;

namespace SpaceDefence.Animations
{
    public class PlanetAnimation : Animation
    {
        public PlanetAnimation(Point position, string spriteSheetName) : base(20, 96, 96, position, isLooping: true)
        {
            _texture = GameManager.GetGameManager().ContentManager.Load<Texture2D>(spriteSheetName);
        }
    }
}
