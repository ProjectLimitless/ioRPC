using System;
using Limitless.ioRPC.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ChildClientSample
{
    public class Calculator : IRPCAsyncHandler
    {
        /// <summary>
        /// The callback function takes the original called function as 
        /// </summary>
        private Action<string, object> asyncCallback;

        public int Add(int a, int b)
        {
            return a + b;
        }

        /// <summary>
        /// Shows how to return async results
        /// </summary>
        public void AsyncAdd(int a, int b)
        {
            // Fake the delay to showcase the async result
            Task.Run(() => {
                Thread.Sleep(TimeSpan.FromSeconds(5));
                asyncCallback("AsyncAdd", (a + b));
            });
        }

        /// <summary>
        /// Sets the callback for async results.
        /// </summary>
        /// <param name="asyncCallback">The callback function</param>
        public void SetAsyncCallback(Action<string, object> asyncCallback)
        {
            this.asyncCallback = asyncCallback;
        }
    }
}
