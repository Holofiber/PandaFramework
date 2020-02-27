using System;
using System.Collections.Generic;
using System.Text;
using DummyClient;

namespace DummyServer
{
    public class Request
    {
        public Guid ID { get; set; }
        public ValidCommand Command { get; set; }
        public string Message { get; set; }
        public List<string> Object { get; set; }
    }
}
