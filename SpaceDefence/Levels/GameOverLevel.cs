using Microsoft.Xna.Framework.Content;
using SpaceDefence.GameObjects.GFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefence.Levels
{
    public class GameOverLevel : Level
    {
        public override void Load(ContentManager content)
        {
            AddGameObject(new CenteredTitle("Game Over"));
            base.Load(content);
        }
    }
}
