using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine;
using SpaceDefence.Engine.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefence.Animations
{
    public class ExplosionAnimation : Animation
    {
        public ExplosionAnimation(Point position) : base(20, 64, 64, position)
        {
            _texture = GameManager.GetGameManager().ContentManager.Load<Texture2D>("explosion");
        }
    }
}
