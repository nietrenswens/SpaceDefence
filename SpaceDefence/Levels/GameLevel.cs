using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefence.Levels
{
    public class GameLevel : Level
    {
        public GameLevel(InputManager inputManager) : base(inputManager)
        {
        }

        public override void Load(ContentManager content)
        {
            AddGameObject(GameManager.GetGameManager().Player);
            AddGameObject(new Alien());
            AddGameObject(new Supply());
            base.Load(content);
        }
    }
}
