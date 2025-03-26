using SpaceDefence.Engine.Managers;
using SpaceDefence.Levels;

namespace SpaceDefence.Objectives
{
    public class DeliverToAlienPlanetObjective : Objective
    {
        public DeliverToAlienPlanetObjective() : base("Deliver to alien", "Deliver the goods to the alien planet", "objective_icon")
        {
        }

        public override void OnComplete()
        {
            (LevelManager.GetLevelManager().CurrentLevel as GameLevel).SetObjective(new PickUpFromEarth());
        }


    }
}
