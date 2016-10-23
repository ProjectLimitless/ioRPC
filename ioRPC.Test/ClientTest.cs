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
using NUnit.Framework;
using Limitless.ioRPC;
using Limitless.ioRPC.Interfaces;

namespace ioRPC.Test
{
    class MockHandler
    {
        public void TestHandler()
        {

        }
    }

    class AsyncMockHandler : IRPCAsyncHandler
    {
        public Action<string, object> asyncCallback;

        public void AsyncTestHandler()
        {

        }

        public void SetAsyncCallback(Action<string, object> asyncCallback)
        {
            this.asyncCallback = asyncCallback;
        }
    }

    [TestFixture]
    public class ClientTest
    {
        Client client;
        Client asyncClient;

        [Test]
        public void ShouldCreateClient()
        {
            MockHandler handler = new MockHandler();
            client = new Client(handler);
            Assert.IsNotNull(client);
        }

        [Test]
        public void ShouldCreateAsyncClient()
        {
            IRPCAsyncHandler asyncHandler = new AsyncMockHandler();
            asyncClient = new Client(asyncHandler);
            Assert.IsNotNull(asyncClient);
        }
        
        [Test]
        public void ShouldWrite()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (TextWriter textWriter = new StreamWriter(memoryStream))
                {
                    Console.SetOut(textWriter);
                    string testMessage = "test message";
                    client.Write("test message");

                    // Get the output
                    textWriter.Flush();
                    memoryStream.Position = 0;
                    using (StreamReader streamReader = new StreamReader(memoryStream))
                    {
                        string result = streamReader.ReadToEnd();
                        StringAssert.AreEqualIgnoringCase(testMessage, result.Trim());
                    }
                }
            }
        }

        [Test]
        public void ShouldWriteError()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (TextWriter textWriter = new StreamWriter(memoryStream))
                {
                    Console.SetError(textWriter);
                    string testMessage = "test error message";
                    client.WriteError("test error message");

                    // Get the output
                    textWriter.Flush();
                    memoryStream.Position = 0;
                    using (StreamReader streamReader = new StreamReader(memoryStream))
                    {
                        string result = streamReader.ReadToEnd();
                        StringAssert.AreEqualIgnoringCase(testMessage, result.Trim());
                    }
                }
            }
        }

        [Test]
        public void ShouldRaiseExit()
        {
            bool exitRaised = false;
            client.Exiting += (sender, e) => { exitRaised = true; };
            client.Exit();
            Assert.IsTrue(exitRaised);
        }
        
    }
}
