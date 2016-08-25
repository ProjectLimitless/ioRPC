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

namespace Limitless.ioRPC.Structs
{
    /// <summary>
    /// Defines an ioRPC command result.
    /// </summary>
    public class ioResult
    {
        /// <summary>
        /// The name of the command this is a result for.
        /// </summary>
        public string CommandName { get; set; }
        /// <summary>
        /// The full exception if an exception occurred.
        /// </summary>
        public string ExceptionMessage { get; set; }
        /// <summary>
        /// The function result data.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ioResult()
        {
            CommandName = "";
            ExceptionMessage = "";
        }

        /// <summary>
        /// Default constructor.
        /// <param name="commandName">The name of the command this is the result for</param>
        /// </summary>
        public ioResult(string commandName)
        {
            CommandName = commandName;
            ExceptionMessage = "";
        }

        /// <summary>
        /// Creates a new instance of the ioResult with result data.
        /// </summary>
        /// <param name="commandName">The name of the command this is the result for</param>
        /// <param name="data">The function result data</param>
        public ioResult(string commandName, object data)
        {
            CommandName = "";
            Data = data;
            ExceptionMessage = "";
        }
        
        /// <summary>
        /// Serialize this result to XML.
        /// </summary>
        /// <returns>The serialized string of this instance as XML</returns>
        public string Serialize()
        {
            var serializer = new XmlSerializer(typeof(ioResult));
            StringWriter stringWriter = new StringWriter();
            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = false }))
            {
                serializer.Serialize(xmlWriter, this);
                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// Create a new ioResult from XML.
        /// </summary>
        /// <param name="resultXml">The XML to parse</param>
        /// <param name="result">The created ioResult</param>
        /// <returns>True on success, false otherwise</returns>
        public static bool TryParse(string resultXml, out ioResult result)
        {
            result = null;
            if (resultXml == null || resultXml == string.Empty)
            {
                return false;
            }

            XmlSerializer parser = new XmlSerializer(typeof(ioResult));
            try
            {
                result = (ioResult)parser.Deserialize(new StringReader(resultXml));
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            return true;
        }
    }
}
