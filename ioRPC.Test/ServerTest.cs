using Limitless.ioRPC;
using Limitless.ioRPC.Structs;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;

namespace ioRPC.Test
{
    [TestFixture]
    public class ServerTest
    {
        Server server;

        [Test]
        public void ShouldCreateAndStartServer()
        {
            ProcessStartInfo testInfo = new ProcessStartInfo();
#if DEBUG
            testInfo.FileName = "Samples\\bin\\Debug\\ChildClientSample.exe";
#else
            testInfo.FileName = "Samples\\bin\\Release\\ChildClientSample.exe";
#endif
            server = new Server(testInfo);
            Assert.IsNotNull(server);
            server.Start();
        }
        
        [Test]
        public void ShouldExecuteCommand()
        {
            ioCommand command = new ioCommand("Test");
            server.Execute(command);
        }

        [Test]
        public void ShouldExit()
        {
            bool exited = server.ExitAndWait(0);
            // Check if false since process is being killed
            Assert.IsFalse(exited);
        }
    }
}
