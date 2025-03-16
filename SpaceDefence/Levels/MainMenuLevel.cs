using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine.Managers;
using SpaceDefence.GameObjects.GUI;
using SpaceDefence.GameObjects.GUI.StartMenu;
using SpaceDefence.Utilities;

namespace SpaceDefence.Levels
{
    public class MainMenuLevel : Level
    {
        private SpriteFont _font;

        public MainMenuLevel()
        {
            var quitButton = new QuitButton(340);
            var startButton = new StartButton(280);


            AddGameObject(quitButton);
            AddGameObject(startButton);

            quitButton.ButtonPressed += OnQuitButtonPressed;
            startButton.ButtonPressed += OnStartButtonPressed;
        }

        public override void Load(ContentManager content)
        {
            _font = content.Load<SpriteFont>("font");
            base.Load(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            TextUtilities.DrawTextXCentered("Space Defence", _font, 200, Color.White, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime, spriteBatch);
        }

        public void OnQuitButtonPressed(object o, ButtonEventArgs e)
        {
            GameManager.GetGameManager().Exit();
        }

        public void OnStartButtonPressed(object o, ButtonEventArgs e)
        {
            LevelManager.GetLevelManager().ChangeLevel(new GameLevel());
    }
    }
}
