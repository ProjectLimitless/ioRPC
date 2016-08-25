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
using System.Reflection;
using Limitless.ioRPC.Structs;

namespace Limitless.ioRPC
{
    /// <summary>
    /// Provides a wrapped application a layer that
    /// acts as a middleman for receiving commands from a parent process
    /// using standard input and output using Console.WriteLine, Console.ReadLine
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Raised when the process is asked to exit.
        /// </summary>
        public event EventHandler Exiting;

        /// <summary>
        /// The primary object that handles RPC calls.
        /// </summary>
        private object rpcHandler;
        /// <summary>
        /// Flag to keep client in an event loop.
        /// </summary>
        private bool isRunning;

        /// <summary>
        /// Creates a new instance of the ioRPC client.
        /// </summary>
        /// <param name="handler">The instance to handle RPC calls</param>
        public Client(object handler)
        {
            rpcHandler = handler;
        }

        /// <summary>
        /// Listen for new commands from the parent process.
        /// </summary>
        public void Listen()
        {
            isRunning = true;
            while (isRunning)
            {
                string parentInput = Console.ReadLine();
                if (parentInput != string.Empty)
                {
                    ioCommand command;
                    if (ioCommand.TryParse(parentInput, out command))
                    {
                        ioResult result = Execute(command);
                        if (result != null)
                        {
                            string resultXml = result.Serialize();
                            if (result.ExceptionMessage != string.Empty)
                            {
                                WriteError(resultXml);
                            }
                            else
                            {
                                Write(resultXml);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Write an object to standard out for the parent to read.
        /// </summary>
        /// <param name="message">The object to write</param>
        public void Write(object message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Write an object to the standard error for the parent to read.
        /// </summary>
        /// <param name="message">The object to write</param>
        public void WriteError(object message)
        {
            Console.Error.WriteLine(message);
        }

        /// <summary>
        /// Set's the current loop's check value to false
        /// which causes an exit.
        /// </summary>
        public void Exit()
        {
            OnExiting(EventArgs.Empty);
            isRunning = false;
        }

        /// <summary>
        /// Execute the given RPC in the client.
        /// </summary>
        /// <param name="command">The command received from the parent</param>
        /// <returns>The call result</returns>
        private ioResult Execute(ioCommand command)
        {
            Type type;
            ioResult result = new ioResult(command.Name);
            MethodInfo method;
            object methodResult;
            try
            {
                // First check if this function exists inside the client
                type = this.GetType();
                method = type.GetMethod(command.Name);
                methodResult = method.Invoke(this, command.Parameters.ToArray());
                result.Data = methodResult;
            }
            catch (Exception)
            {
                try
                {
                    // Then try the rpcHandler
                    // If the method doesn't exist on the handler, an exception is thrown.
                    type = rpcHandler.GetType();
                    method = type.GetMethod(command.Name);
                    if (method == null)
                    {
                        throw new NotImplementedException(string.Format("Method '{0}' does not exist on the RPC Handler '{1}'", command.Name, rpcHandler.GetType().ToString()));
                    }
                    methodResult = method.Invoke(rpcHandler, command.Parameters.ToArray());
                    result.Data = methodResult;
                }
                catch (Exception ex)
                {
                    result.ExceptionMessage = ex.Message;
                }
            }
            return result;
        }

        #region Events
        protected virtual void OnExiting(EventArgs e)
        {
            if (Exiting != null)
                Exiting(this, e);
        }
        #endregion
    }
}
