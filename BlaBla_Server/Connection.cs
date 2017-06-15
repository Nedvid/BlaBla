using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Collections.Generic;


namespace BlaBla_Server
{
    public class Connection
    {
        private TcpListener _server;
        private Boolean _isRunning;
        private IPAddress ipAd = IPAddress.Parse(GetLocalIPAddress());
        private int port = 8001;
        
        public Connection()
        {
            Console.WriteLine("Server is running...");
            _server = new TcpListener(ipAd, port);
            _server.Start();
            _isRunning = true;
            Console.WriteLine("ip server: " + ipAd);
            Console.WriteLine("-----------------------------------------------------------------\n");
            LoopClients();
        }

        public void LoopClients()
        {
            while (_isRunning)
            {
                // wait for client connection
                TcpClient newClient = _server.AcceptTcpClient();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(newClient.Client.RemoteEndPoint + ": " + "connected");
                Console.ResetColor();


                // client found.
                // create a thread to handle communication
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(newClient);
            }
        }

        public void HandleClient(object obj)
        {
            // retrieve client from parameter passed to thread
            TcpClient client = (TcpClient)obj;

            // sets two streams
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.ASCII);
            // you could use the NetworkStream to read and write, 
            // but there is no forcing flush, even when requested

            Boolean bClientConnected = true;
            String sData = null;

            while (bClientConnected)
            {
                // reads from stream
                sData = sReader.ReadLine();

                if (sData == "bye")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(client.Client.RemoteEndPoint + ": ");
                    Console.Write(sData + "\n");
                    Console.ResetColor();

                    string answer = "bye";
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(client.Client.RemoteEndPoint + ": ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(answer + "\n");
                    Console.ResetColor();
                    sWriter.WriteLine(answer);
                    sWriter.Flush();

                    bClientConnected = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    // shows content on the console.
                    Console.Write(client.Client.RemoteEndPoint + ": ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(sData + "\n");
                    Console.ResetColor();

                    string answer = db_Functions.checker(sData);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(client.Client.RemoteEndPoint + ": ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(answer + "\n");
                    Console.ResetColor();
                    sWriter.WriteLine(answer);
                    sWriter.Flush();
                }



            }
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        
    }
}
