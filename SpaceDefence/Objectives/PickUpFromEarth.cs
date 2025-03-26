using SpaceDefence.Engine.Managers;
using SpaceDefence.Levels;

namespace SpaceDefence.Objectives
{
    public class PickUpFromEarth : Objective
    {
        public PickUpFromEarth() : base("Pickup from earth", "Pick up the goods from the earh planet", "deliver_to_earth")
        {
        }

        public override void OnComplete()
        {
            (LevelManager.GetLevelManager().CurrentLevel as GameLevel).SetObjective(new DeliverToAlienPlanetObjective());
        }
    }
}
