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
using Limitless.ioRPC.Events;
using Limitless.ioRPC.Structs;

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
