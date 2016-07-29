using System;
using Limitless.ioRPC;

namespace ChildClientSample
{
    class ChildClientSample
    {
        static void Main(string[] args)
        {
            Calculator calc = new Calculator();
            Client client = new Client(calc);
            client.Exiting += Client_Exiting;
            client.Listen();
        }

        private static void Client_Exiting(object sender, EventArgs e)
        {
            Console.WriteLine("[Client] Exiting Event");
        }
    }
}
