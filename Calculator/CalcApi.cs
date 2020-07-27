using DummyClient;
using Library;

namespace Calculator
{
    class CalcApi
    {
        Api api = new Api();
        public void DivNumbers(int a, int b)
        {
            var message = $"div {a} {b}";
            var request = new CalculatorRequest()
            {               
                Message = message
            };

            api.SendMessage(request);
        }
    }
}
