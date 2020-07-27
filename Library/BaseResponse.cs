namespace Library
{
    class BaseResponse : IBaseMessage
    {
        public string TypeName { get; }
        public string Message { get; }
    }
}
