using System;
using System.Runtime.Serialization;

namespace Bowling
{
    [Serializable]
    public class GameIsNotFinishedYetException : Exception
    {
        public GameIsNotFinishedYetException()
        {
        }

        public GameIsNotFinishedYetException(string message) : base(message)
        {
        }

        public GameIsNotFinishedYetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GameIsNotFinishedYetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}