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
 * ioRPC. If not, see <http://www.apache.org/licenses/LICENSE-2.0>.
 */

using System;

namespace Limitless.ioRPC.Events
{
    /// <summary>
    /// Holds information for events raised by ioRPC.
    /// </summary>
    public class ioEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the name of the command that the event was raised from.
        /// </summary>
        public string CommandName { get; internal set; }
        /// <summary>
        /// The full exception if an exception occurred.
        /// </summary>
        public string ExceptionMessage { get; internal set; }
        /// <summary>
        /// The function result data.
        /// </summary>
        public object Data { get; internal set; }

        public ioEventArgs(string commandName)
        {
            CommandName = commandName;
            ExceptionMessage = "";
        }
    }
}
