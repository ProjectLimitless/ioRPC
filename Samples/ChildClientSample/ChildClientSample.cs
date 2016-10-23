using System;
using Limitless.ioRPC;
using Limitless.ioRPC.Interfaces;

namespace ChildClientSample
{
    class ChildClientSample
    {
        static void Main(string[] args)
        {
            IRPCAsyncHandler calc = new Calculator();
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
