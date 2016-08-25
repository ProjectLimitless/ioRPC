/** 
 * This file is part of ioRPC.
 * Copyright © 2016 Donovan Solms.
 * Project Limitless
 * https://www.projectlimitless.io
 * 
 * ioRPC and Project Limitless is free software: you can redistribute it and/or modify
 * it under the terms of the Apache License Version 2.0.
 * 
 * You should have received a copy of the Apache License Version 2.0 with
 * ioRPC. If not, see http://www.apache.org/licenses/LICENSE-2.0.
 */
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using NUnit.Framework;
using Limitless.ioRPC;
using Limitless.ioRPC.Structs;

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
