﻿using Limitless.ioRPC;
using Limitless.ioRPC.Structs;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

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
            List<object> parameters = new List<object>();
            parameters.Add(1);
            parameters.Add(3);
            ioCommand command = new ioCommand("Add", parameters);
            server.Execute(command);
            // Not testing events raised
        }

        [Test]
        public void ShouldExit()
        {
            bool exited = server.ExitAndWait(0);
            server.Dispose();
            // Check if false since process is being killed
            Assert.IsFalse(exited);
        }
    }
}
