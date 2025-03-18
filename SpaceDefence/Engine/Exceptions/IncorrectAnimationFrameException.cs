using System;

namespace SpaceDefence.Engine.Exceptions
{
    public class IncorrectAnimationFrameException : Exception
    {
        public IncorrectAnimationFrameException(string message) : base(message)
        {
        }
    }
}
