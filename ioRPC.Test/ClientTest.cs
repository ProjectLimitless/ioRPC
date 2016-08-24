using Limitless.ioRPC;
using NUnit.Framework;
using System;
using System.IO;

namespace ioRPC.Test
{
    class MockHandler
    {
        public void TestHandler()
        {

        }
    }

    [TestFixture]
    public class ClientTest
    {
        Client client;

        [Test]
        public void ShouldCreateClient()
        {
            MockHandler handler = new MockHandler();
            client = new Client(handler);
            Assert.IsNotNull(client);
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
