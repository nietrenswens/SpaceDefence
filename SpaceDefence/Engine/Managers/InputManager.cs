using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceDefence.Levels;

namespace SpaceDefence.Engine.Managers
{
    public class InputManager
    {
        private static InputManager _inputManager;

        public KeyboardState LastKeyboardState { get; private set; }
        public KeyboardState CurrentKeyboardState { get; private set; }
        public MouseState LastMouseState { get; private set; }
        public MouseState CurrentMouseState { get; private set; }

        public static InputManager GetInputManager()
        {
            if (_inputManager == null)
                _inputManager = new InputManager();
            return _inputManager;
        }

        /// <summary>
        /// Keeps track of input states and contains methods to work with them.
        /// </summary>
        private InputManager()
        {
            LastKeyboardState = Keyboard.GetState();
            CurrentKeyboardState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();
            LastMouseState = Mouse.GetState();
        }

        /// <summary>
        /// Updates the current and previous keyboard and mouse states
        /// </summary>
        public void Update()
        {
            LastKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();
            LastMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }

        /// <summary>
        /// Gets whether the <paramref name="key"/> is currently down.
        /// </summary>
        /// <param name="key">The key for which you wish to know the _state</param>
        /// <returns>true if the key is currently down, otherwise false</returns>
        public bool IsKeyDown(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }


        /// <summary>
        /// Gets whether the <paramref name="key"/> is currently up.
        /// </summary>
        /// <param name="key">The key for which you wish to know the _state</param>
        /// <returns>true if the key is currently up, otherwise false</returns>
        public bool IsKeyUp(Keys key)
        {
            return CurrentKeyboardState.IsKeyUp(key);
        }



        /// <summary>
        /// Gets whether the <paramref name="key"/> was pressed in this frame.
        /// </summary>
        /// <param name="key">The key for which you wish to know the _state</param>
        /// <returns>true if the key is currently down and was up in the previous step, otherwise false</returns>
        public bool IsKeyPress(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key) && LastKeyboardState.IsKeyUp(key);
        }


        /// <summary>
        /// Gets whether the left mouse button was pressed in this frame.
        /// </summary>
        /// <returns>true if the button is currently down and was up in the previous step, otherwise false</returns>
        public bool LeftMousePress()
        {
            return CurrentMouseState.LeftButton == ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Released;
        }


        /// <summary>
        /// Gets whether the right mouse button was pressed in this frame.
        /// </summary>
        /// <returns>true if the button is currently down and was up in the previous step, otherwise false</returns>
        public bool RightMousePress()
        {
            return CurrentMouseState.RightButton == ButtonState.Pressed && LastMouseState.RightButton == ButtonState.Released;
        }

        public Vector2 GetRelativeMousePosition()
        {
            var screenMousePosition = CurrentMouseState.Position.ToVector2();
            if (LevelManager.GetLevelManager().CurrentLevel is not GameLevel)
                return screenMousePosition;

            var gameLevel = LevelManager.GetLevelManager().CurrentLevel as GameLevel;

            Matrix inverseTransform = Matrix.Invert(gameLevel.Camera.GetWorldTransformationMatrix());
            return Vector2.Transform(screenMousePosition, inverseTransform);
        }
    }
}