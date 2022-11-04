namespace InstantAssistant.Api.Services
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException() : base() { }
        public NotAuthorizedException(string message) : base(message) { }
        public NotAuthorizedException(string message, Exception inner) : base(message, inner) { }
        protected NotAuthorizedException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
