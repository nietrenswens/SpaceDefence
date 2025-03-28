using Microsoft.Xna.Framework;

namespace SpaceDefence.Engine
{
    public class GameObjectCenteredCamera
    {
        public GameObject Target;
        public float Zoom;

        public GameObjectCenteredCamera(GameObject target, float zoom)
        {
            Target = target;
            Zoom = zoom;
        }

        public Matrix GetWorldTransformationMatrix()
        {
            var screenWidth = SpaceDefence.SCREENWIDTH;
            var screenHeight = SpaceDefence.SCREENHEIGHT;

            float clampedX = MathHelper.Clamp(Target.BoundingBox.X, SpaceDefence.MINX + screenWidth / 2, SpaceDefence.MAXX + Target.Width - screenWidth / 2);
            float clampedY = MathHelper.Clamp(Target.BoundingBox.Y, SpaceDefence.MINY + screenHeight / 2, SpaceDefence.MAXY - screenHeight / 2);

            return Matrix.CreateTranslation(-clampedX, -clampedY, 0) * Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(screenWidth / 2, screenHeight / 2, 0);
        }
    }
}
