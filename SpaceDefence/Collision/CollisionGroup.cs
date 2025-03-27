using System;

namespace SpaceDefence.Collision;

[Flags]
public enum CollisionGroup
{
    None = 0,
    Player = 1,
    Enemy = 2,
    Bullet = 3,
    DeliverPlanet = 4,
    PickupPlanet = 5
}