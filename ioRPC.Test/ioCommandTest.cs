using Limitless.ioRPC.Structs;
using NUnit.Framework;

namespace ioRPC.Test
{
    [TestFixture]
    public class ioCommandTest
    {
        [Test]
        public void ShouldSetCommandName()
        {
            string commandNameTest = "testCommand";
            ioCommand command = new ioCommand(commandNameTest);
            string commandName = command.Name;
            Assert.That(commandName, Is.SameAs(commandNameTest));
        }

        [Test]
        public void ShouldInitializeParametersList()
        {
            ioCommand command = new ioCommand();
            command.Parameters.Add("testParam");
            Assert.That(command.Parameters.Count, Is.EqualTo(1));
        }

        [Test]
        public void ShouldNotParse()
        {
            ioCommand command;
            bool parseResult = ioCommand.TryParse("this is invalid xml", out command);
            Assert.That(parseResult, Is.False);
        }

        [Test]
        public void ShouldParse()
        {
            string commandXml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><ioCommand xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><Name>testCommand</Name><Parameters><anyType xsi:type=\"xsd:string\">testParam</anyType></Parameters></ioCommand>";

            ioCommand command;
            bool parseResult = ioCommand.TryParse(commandXml, out command);
            Assert.That(parseResult, Is.True);
            Assert.That(command.Name, Does.Contain("testCommand"));
            Assert.That(command.Parameters.Count, Is.EqualTo(1));
        }
    }
}
