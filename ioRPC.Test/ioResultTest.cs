using Limitless.ioRPC.Structs;
using NUnit.Framework;

namespace ioRPC.Test
{
    [TestFixture]
    public class ioResultTest
    {
        [Test]
        public void ShouldSetCommandName()
        {
            string resultNameTest = "testCommand";
            ioResult result = new ioResult(resultNameTest);
            string resultName = result.CommandName;
            Assert.That(resultName, Is.SameAs(resultNameTest));
        }

        [Test]
        public void ShouldSetData()
        {
            string dataTest = "testData";
            ioResult result = new ioResult("testCommand", dataTest);
            object resultData = result.Data;
            Assert.That(resultData, Is.SameAs(dataTest));
        }

        [Test]
        public void ShouldNotParse()
        {
            ioResult result;
            bool parseResult = ioResult.TryParse("this is invalid xml", out result);
            Assert.That(parseResult, Is.False);
        }

        [Test]
        public void ShouldParse()
        {
            string resultXml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><ioResult xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><CommandName>testCommand</CommandName><ExceptionMessage /><Data xsi:type=\"xsd:string\">testData</Data></ioResult>";

            ioResult result;
            bool parseResult = ioResult.TryParse(resultXml, out result);
            Assert.That(parseResult, Is.True);
            Assert.That(result.CommandName, Does.Contain("testCommand"));
            Assert.That(result.Data, Does.Contain("testData"));
        }
    }
}
