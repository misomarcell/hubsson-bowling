using System;
using System.Runtime.Serialization;

namespace Bowling
{
    [Serializable]
    internal class FrameIsAlreadyNukedWithYourBalls : Exception
    {
        public FrameIsAlreadyNukedWithYourBalls()
        {
        }

        public FrameIsAlreadyNukedWithYourBalls(string message) : base(message)
        {
        }

        public FrameIsAlreadyNukedWithYourBalls(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FrameIsAlreadyNukedWithYourBalls(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}