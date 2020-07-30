using DummyClient;
using Library;

namespace Calculator
{
    class CalcApi
    {
        PandaBaseApi api = new PandaBaseApi();
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
