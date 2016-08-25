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
using System.Diagnostics;
using Limitless.ioRPC.Structs;
using Limitless.ioRPC.Events;

namespace Limitless.ioRPC
{
    /// <summary>
    /// Provides a wrapper around a process that acts as a middleman
    /// for sending and receiving commands via standard input 
    /// and output using Console.WriteLine, Console.ReadLine
    /// </summary>
    public class Server : IDisposable
    {
        /// <summary>
        /// Raised when the subprocess has exited.
        /// </summary>
        public event EventHandler Exited;
        /// <summary>
        /// Raised when a function call's result is received.
        /// </summary>
        public event CommandResultReceivedEventHandler CommandResultReceived;
        /// <summary>
        /// Raised when a function call threw an exception.
        /// </summary>
        public event CommandExceptionReceivedEventHandler CommandExceptionReceived;
        
        public delegate void CommandResultReceivedEventHandler(object sender, ioEventArgs e);
        public delegate void CommandExceptionReceivedEventHandler(object sender, ioEventArgs e);

        /// <summary>
        /// The information required to start the process.
        /// </summary>
        private ProcessStartInfo processStartInfo;
        /// <summary>
        /// The process that the server is wrapped around.
        /// </summary>
        private Process process;

        /// <summary>
        /// Creates a new instance of the ioRPC server.
        /// </summary>
        /// <param name="startInfo">The process information to wrap around</param>
        public Server(ProcessStartInfo startInfo)
        {
            processStartInfo = startInfo;
            // Set the required properties for this to work
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.UseShellExecute = false;
        }

        /// <summary>
        /// Start the process and read the outputs.
        /// </summary>
        public void Start()
        {
            // Set up the process
            process = new Process();
            process.StartInfo = processStartInfo;
            process.EnableRaisingEvents = true;
            // Set up events
            process.OutputDataReceived += Process_OutputDataReceived;
            process.ErrorDataReceived += Process_ErrorDataReceived;
            process.Exited += Process_Exited;
            // Run
            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
        }

        /// <summary>
        /// Write a string to the input of the child process.
        /// </summary>
        /// <param name="message">The message to write</param>
        public void Write(string message)
        {
            process.StandardInput.WriteLine(message);
        }

        /// <summary>
        /// Sends the command to subprocess to execute.
        /// </summary>
        /// <param name="command">The command to send</param>
        public void Execute(ioCommand command)
        {
            string commandXml = command.Serialize();
            Write(commandXml);
        }

        /// <summary>
        /// Sends a command to the process to exit. If the process 
        /// doesn't exit in the specified amount of seconds, it is killed.
        /// </summary>
        /// <param name="milliseconds">The amount of milliseconds to wait before killing the process</param>
        /// <returns>True if the process exited on its own, false if it was killed</returns>
        public bool ExitAndWait(int milliseconds)
        {
            if (process.WaitForExit(milliseconds) == false)
            {
                process.Kill();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Event handler for data received via standard out on the child process.
        /// </summary>
        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            ioResult rpcResult;
            if (ioResult.TryParse(e.Data, out rpcResult))
            {
                ioEventArgs eventArgs = new ioEventArgs(rpcResult.CommandName);
                eventArgs.Data = rpcResult.Data;
                OnCommandResultReceived(eventArgs);
            }
        }

        /// <summary>
        /// Event handler for data received via standard error on the child process.
        /// </summary>
        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            ioResult rpcResult;
            if (ioResult.TryParse(e.Data, out rpcResult))
            {
                ioEventArgs eventArgs = new ioEventArgs(rpcResult.CommandName);
                eventArgs.ExceptionMessage = rpcResult.ExceptionMessage;
                OnCommandExceptionReceived(eventArgs);
            }
        }

        /// <summary>
        /// Event handler for when the subprocess exited.
        /// </summary>
        private void Process_Exited(object sender, EventArgs e)
        {
            OnExited(e);
        }

        #region Events
        protected virtual void OnExited(EventArgs e)
        {
            if (Exited != null)
                Exited(this, e);
        }
        protected virtual void OnCommandResultReceived(ioEventArgs e)
        {
            if (CommandResultReceived != null)
                CommandResultReceived(this, e);
        }
        protected virtual void OnCommandExceptionReceived(ioEventArgs e)
        {
            if (CommandExceptionReceived != null)
                CommandExceptionReceived(this, e);
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ExitAndWait(1000);
                    process.Dispose();
                }
                disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose ensures the child process is always exited.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
