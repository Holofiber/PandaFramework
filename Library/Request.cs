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

   
}
