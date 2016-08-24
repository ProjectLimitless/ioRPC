using Limitless.ioRPC.Events;
using Limitless.ioRPC.Structs;
using NUnit.Framework;

namespace ioRPC.Test
{
    [TestFixture]
    public class ioEventArgsTest
    {
        [Test]
        public void ShouldSetCommandNameConstructor()
        {
            string commandNameTest = "commandNameTest";
            ioEventArgs args = new ioEventArgs(commandNameTest);
            string resultName = args.CommandName;
            Assert.That(resultName, Is.SameAs(commandNameTest));
        }

        [Test]
        public void ShouldGetNullData()
        {
            ioEventArgs args = new ioEventArgs("");
            Assert.That(args.Data, Is.Null);
        }

        [Test]
        public void ShouldGetBlankExceptionMessage()
        {
            ioEventArgs args = new ioEventArgs("");
            Assert.That(args.ExceptionMessage, Is.EqualTo(""));
        }

    }
}
