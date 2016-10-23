/**
 * This sample shows the usage of ioRPC.
 * 
 * It does the following:
 * 1. Starts a process names 'ChildClientSample', which implements a basic 'Add' method.
 * 2. It sets up event listeners that is raised from the ioRPC server.
 * 3. Enters a loop that sends a command to the child process every second.
 * 4. It prints the output of the command.
 * 5. Finally sends the exit command and shuts down.
 * 
 */
using System;
using System.Diagnostics;
using Limitless.ioRPC;
using Limitless.ioRPC.Structs;
using System.Threading;

namespace ServerSample
{
    /// <summary>
    /// A sample server application showing
    /// how to use ioRPC.
    /// </summary>
    class ServerSample
    {
        static void Main(string[] args)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "ChildClientSample.exe";

            using (Server ioServer = new Server(startInfo))
            {
                ioServer.Exited += Iorpc_Exited;
                ioServer.CommandResultReceived += Iorpc_CommandResultReceived;
                ioServer.CommandExceptionReceived += Iorpc_CommandExceptionReceived;
                ioServer.Start();

                Console.WriteLine("[SampleServer] Server Started...");
                Thread.Sleep(1000);

                Console.WriteLine("[SampleServer] Executing async Add...");
                ioCommand asyncAdd = new ioCommand("AsyncAdd");
                asyncAdd.Parameters.Add(10);
                asyncAdd.Parameters.Add(8);
                ioServer.Execute(asyncAdd);

                while (Console.KeyAvailable == false)
                {
                    Console.WriteLine("[SampleServer] Server Running...");
                    Thread.Sleep(1000);

                    ioCommand add = new ioCommand("Add");
                    add.Parameters.Add(7);
                    add.Parameters.Add(5);
                    ioServer.Execute(add);
                }
                ioCommand exit = new ioCommand("Exit");
                ioServer.Execute(exit);
            }
            Console.WriteLine("[SampleServer] Done. Press any key to exit.");
            Console.ReadLine();
        }

        private static void Iorpc_CommandExceptionReceived(object sender, Limitless.ioRPC.Events.ioEventArgs e)
        {
            Console.WriteLine("[SampleServer] Received RPC Call Exception: fn({0}) {1}", e.CommandName, e.ExceptionMessage);
        }

        private static void Iorpc_CommandResultReceived(object sender, Limitless.ioRPC.Events.ioEventArgs e)
        {
            if (e.Data == null)
            {
                Console.WriteLine("[SampleServer] Received RPC Call Result: fn({0}) {1}", e.CommandName, "(void)");
            }
            else
            {
                Console.WriteLine("[SampleServer] Received RPC Call Result: fn({0}) {1}", e.CommandName, e.Data);
            }
        }

        private static void Iorpc_Exited(object sender, EventArgs e)
        {
            Console.WriteLine("[SampleServer] Subprocess has exited");
        }
    }
}
