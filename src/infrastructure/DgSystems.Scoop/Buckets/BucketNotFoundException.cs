using System.Runtime.Serialization;

namespace DgSystems.Scoop
{
    [Serializable]
    internal class BucketNotFoundException : Exception
    {
        public BucketNotFoundException()
        {
        }

        public BucketNotFoundException(string? message) : base(message)
        {
        }

        public BucketNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BucketNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}