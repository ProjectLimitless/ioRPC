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
using NUnit.Framework;
using Limitless.ioRPC.Structs;

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
