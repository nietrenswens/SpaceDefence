using System;

namespace SpaceDefence.Collision;

[Flags]
public enum CollisionGroup
{
    Player = 0,
    Enemy = 1,
    Bullet = 2
}