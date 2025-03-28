using SpaceDefence.Engine.Managers;
using SpaceDefence.Levels;
using System;

namespace SpaceDefence.Objectives
{
    public class PickUpFromEarthObjective : Objective
    {
        private GameLevel _level => LevelManager.GetLevelManager().CurrentLevel as GameLevel;
        public PickUpFromEarthObjective() : base("Pickup from earth", "Pick up the goods from the earh planet", "deliver_to_earth")
        {
            var player = GameManager.GetGameManager().Player;
            player.PickupDeliveryEvent += OnPickup;
        }

        private void OnPickup(object o, EventArgs args)
        {
            if (_level.CurrentObjective == this)
            {
                _level.CurrentObjective.OnComplete();
            }
        }

        public override void OnComplete()
        {
            (LevelManager.GetLevelManager().CurrentLevel as GameLevel).SetObjective(new DeliverToAlienPlanetObjective());
        }
    }
}
