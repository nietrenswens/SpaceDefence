using SpaceDefence.Engine.Managers;
using SpaceDefence.Levels;
using System;

namespace SpaceDefence.Objectives
{
    public class DeliverToAlienPlanetObjective : Objective
    {
        private GameLevel _level => LevelManager.GetLevelManager().CurrentLevel as GameLevel;
        public DeliverToAlienPlanetObjective() : base("Deliver to alien", "Deliver the goods to the alien planet", "deliver_to_alien")
        {
            var player = GameManager.GetGameManager().Player;
            player.DropoffDeliveryEvent += OnDelivery;
        }

        private void OnDelivery(object o, EventArgs args)
        {
            if (_level.CurrentObjective == this)
            {
                _level.CurrentObjective.OnComplete();
            }
        }

        public override void OnComplete()
        {
            (LevelManager.GetLevelManager().CurrentLevel as GameLevel).SetObjective(new PickUpFromEarthObjective());
        }


    }
}
