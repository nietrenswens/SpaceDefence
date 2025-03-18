using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefence.Engine.Managers;
using SpaceDefence.Levels;
using SpaceDefence.Utilities;

namespace SpaceDefence.GameObjects.GUI.PauseMenu
{
    public class PauseMenu : GameObject
    {
        private Texture2D _overlay;
        private SpriteFont _font;

        private ResumeButton _resumeButton;
        private QuitButton _quitButton;

        public PauseMenu()
        {
            _resumeButton = new ResumeButton(SpaceDefence.SCREENHEIGHT / 2);
            _quitButton = new QuitButton(SpaceDefence.SCREENHEIGHT / 2 + 80);

            _resumeButton.ButtonPressed += OnResumeButtonPressed;
            _quitButton.ButtonPressed += OnQuitButtonPressed;
        }

        public override void Load(ContentManager content)
        {
            _overlay = content.Load<Texture2D>("dark_overlay");
            _font = content.Load<SpriteFont>("font");
            _resumeButton.Load(content);
            _quitButton.Load(content);
            base.Load(content);
        }

        public override void HandleInput()
        {
            _resumeButton.HandleInput();
            _quitButton.HandleInput();
            base.HandleInput();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_overlay, new Rectangle(0, 0, SpaceDefence.SCREENWIDTH, SpaceDefence.SCREENHEIGHT), Color.White);
            TextUtilities.DrawTextXCentered("Paused", _font, SpaceDefence.SCREENHEIGHT / 2 - 400, Color.White, spriteBatch);
            _resumeButton.Draw(spriteBatch, gameTime);
            _quitButton.Draw(spriteBatch, gameTime);
            base.Draw(spriteBatch, gameTime);
        }

        public void OnResumeButtonPressed(object o, ButtonEventArgs args)
        {
            (LevelManager.GetLevelManager().CurrentLevel as GameLevel).TogglePause();
        }

        public void OnQuitButtonPressed(object o, ButtonEventArgs args)
        {
            GameManager.GetGameManager().Exit();
        }
    }
}
