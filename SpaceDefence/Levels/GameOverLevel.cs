using Microsoft.Xna.Framework.Content;
using SpaceDefence.Engine.Managers;
using SpaceDefence.GameObjects.GFX;
using SpaceDefence.GameObjects.GUI;
using SpaceDefence.GameObjects.GUI.StartMenu;

namespace SpaceDefence.Levels
{
    public class GameOverLevel : Level
    {
        public override void Load(ContentManager content)
        {
            var middleY = SpaceDefence.SCREENHEIGHT / 2;
            var startButton = new StartButton(middleY + 80);
            AddGameObject(new CenteredTitle("Game Over"));
            AddGameObject(startButton);

            startButton.ButtonPressed += RestartGame;
            base.Load(content);
        }

        private void RestartGame(object o, ButtonEventArgs e)
        {
            GameManager.GetGameManager().ResetGame();
            LevelManager.GetLevelManager().ChangeLevel(new GameLevel());
        }
    }
}
