using Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class CalculatorRequest : IBaseMessage
    {
        public string TypeName { get; } = nameof(CalculatorRequest);
        public string Message { get; set; }
    }
}
