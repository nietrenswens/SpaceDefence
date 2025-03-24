using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine;
using SpaceDefence.Engine.Managers;

namespace SpaceDefence.Animations
{
    public class AlienPlanetAnimation : Animation
    {
        public AlienPlanetAnimation(Point position) : base(20, 96, 96, position, isLooping: true)
        {
            _texture = GameManager.GetGameManager().ContentManager.Load<Texture2D>("alien_planet");
        }
    }
}
