using System;

namespace Library
{
    public class Request
    {
        public Guid ID { get; set; }
        public ValidCommand Command { get; set; }
        public string Message { get; set; }
        public object Object { get; set; }
    }

    //public interface IBaseMessage { string TypeName { get; } }
    public interface IFromServiceToClientMessage : IBaseMessage { }
    public interface IFromClientToServiceMessage : IBaseMessage { }
    public interface IMessageWithId : IFromClientToServiceMessage, IFromServiceToClientMessage { int RequestId { get; set; } }

    [Serializable]
    public class SignInRequest : IMessageWithId
    {
        public string TypeName { get; } = nameof(SignInRequest);
        public int RequestId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Guid Token { get; set; }        
    }
}
