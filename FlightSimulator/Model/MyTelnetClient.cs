using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;
using System.IO;
using System.Diagnostics;

namespace FlightSimulator
{
    class MyTelnetClient : ITelnetClient
    {

        private TcpClient client;
        private static Mutex mutex = new Mutex();


        public void connect(string ip, int port)
        {
            // Message.
            Console.WriteLine("Establishing connection to ip: {0} by port: {1}\n", ip, port);

            // Create client.
            client = new TcpClient();

            //set time out
            client.ReceiveTimeout = 2000;

            // Try to connect.
            try
            {
                client.Connect(ip, port);
                Console.WriteLine("\n\n***Connection established :) ***\n\n");
            }
            catch (Exception ex)
            {
                // If connection failed, try to connect with default ip, port.
                int defaultPort = Int32.Parse(ConfigurationManager.AppSettings["port"].ToString());
                string defaultIP = ConfigurationManager.AppSettings["ip"].ToString();
                client.Connect(defaultIP, defaultPort);
            }
        }

        // Disconnection.
        public void disconnect()
        {
            client.Client.Close();
            Console.WriteLine("\nDisconnecting server\n");
        }


        /// <summary>
        /// Reads data from server (according to given "get" command).
        /// </summary>
        /// <param name="command">  The "get" command to the server </param>
        /// <returns></returns>
        public string read(string command)
        {
            //initializing data string
            string data = "";
            try
            {
                mutex.WaitOne();

                // Writing.
                byte[] write = Encoding.ASCII.GetBytes(command);
                //write to server 
                client.GetStream().Write(write, 0, write.Length);

                // Reading.
                byte[] read = new byte[1024];

                // Here we read the server response so the server (the buffer) won't be with unnecessary information
                // that another thread by mistake can read instead of necessary information.
                client.GetStream().Read(read, 0, 1024);
                data = Encoding.ASCII.GetString(read, 0, read.Length);

                return data;
            }
            catch (IOException io)
            {
                // Moving the treatment in this exception upwards.
                throw new IOException();
            }
            catch (Exception exception)
            {
                Console.WriteLine("\n\n\n\n\n****** Reading got exception ******\n");
                Console.WriteLine(exception.Message + "\n\n\n\n\n");
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            return null;
        }


        /// <summary>
        /// Writes to server and reading the response so the reading will be 'pure' 
        /// and the response won't affect other processes that reads from the server 
        /// </summary>
        /// <param name="command">command to the servver</param>
        public void write(string command)
        {
            try
            {
                mutex.WaitOne();
                byte[] write = Encoding.ASCII.GetBytes(command);
                //write to server 
                client.GetStream().Write(write, 0, write.Length);
                byte[] read = new byte[1024];
                //reads server response so server won't be with unnecessary information
                //that another thread by mistake can read instead of necessary information.
                client.GetStream().Read(read, 0, 1024);
                string data = Encoding.ASCII.GetString(read, 0, read.Length);
                //Console.WriteLine("server response after writing: {0}", Double.Parse(data));
                mutex.ReleaseMutex();
            }
            catch (Exception exception)
            {
                mutex.ReleaseMutex();
                disconnect();
            }
        }

        /// <summary>
        /// returns whether the client is still connected to a remote host or not.
        /// </summary>
        /// <returns></returns>
        public bool isConnected()
        {
            return this.client.Connected;
        }
    }
}