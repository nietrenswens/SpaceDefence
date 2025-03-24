using Microsoft.Xna.Framework;
using SpaceDefence.Animations;
using SpaceDefence.Engine.Managers;

namespace SpaceDefence.GameObjects.Planets
{
    public class AlienPlanet : Planet
    {

        public AlienPlanet(Point? position = null)
        {
            if (position == null)
            {
                Position = new Point(GameManager.GetGameManager().RNG.Next(SpaceDefence.MINX, SpaceDefence.MAXX), GameManager.GetGameManager().RNG.Next(SpaceDefence.MINX, SpaceDefence.MAXX));
            }
            else
            {
                this.Position = (Point)position;
            }
            _animation = new AlienPlanetAnimation(Position);
        }
    }
}
