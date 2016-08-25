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
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Limitless.ioRPC.Structs
{
    /// <summary>
    /// Defines an ioRPC command.
    /// </summary>
    public class ioCommand
    {
        /// <summary>
        /// The function to execute. 
        /// <seealso cref="Type.GetMethod(string)"/>
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The parameters to send to the method.
        /// <seealso cref="MethodInfo.Invoke(object, Parameters)"/>
        /// </summary>
        public List<object> Parameters { get; set; }

        /// <summary>
        /// Creates a new ioCommand instance.
        /// </summary>
        public ioCommand()
        {
            Parameters = new List<object>();
        }

        /// <summary>
        /// Creates a new ioCommand instance with the specified method name.
        /// </summary>
        /// <param name="name">The name of the method to execute</param>
        public ioCommand(string name)
        {
            Name = name;
            Parameters = new List<object>();
        }

        /// <summary>
        /// Creates a new ioCommand instance with the specified method name and parameters.
        /// </summary>
        /// <param name="name">The name of the method to execute</param>
        /// <param name="parameters">The parameters to pass to the method</param>
        public ioCommand(string name, List<object> parameters)
        {
            Name = name;
            Parameters = parameters;
        }

        /// <summary>
        /// Serialize this command to XML.
        /// </summary>
        /// <returns>The serialized string of this instance as XML</returns>
        public string Serialize()
        {
            var serializer = new XmlSerializer(typeof(ioCommand));
            StringWriter stringWriter = new StringWriter();
            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = false }))
            {
                serializer.Serialize(xmlWriter, this);
                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// Create a new ioCommand from XML.
        /// </summary>
        /// <param name="commandXml">The XML to parse</param>
        /// <param name="command">The created ioCommand</param>
        /// <returns>True on success, false otherwise</returns>
        public static bool TryParse(string commandXml, out ioCommand command)
        {
            command = null;
            XmlSerializer parser = new XmlSerializer(typeof(ioCommand));
            try
            {
                command = (ioCommand)parser.Deserialize(new StringReader(commandXml));
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            return true;
        }
    }
}
