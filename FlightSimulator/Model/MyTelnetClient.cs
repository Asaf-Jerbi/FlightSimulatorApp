using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace FlightSimulator
{
    class MyTelnetClient : ITelnetClient
    {
        //private NetworkStream networkStream;
        private TcpClient client;
        private static Mutex mutex = new Mutex();
        
        public void connect(string ip, int port)
        {
            //socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);           
            Console.WriteLine("Establishing connection to ip: {0} by port: {1}", ip, port);
            //socket.Connect(ip, port);            
            client = new TcpClient();
            try
            {
                client.Connect(ip, port);
                Console.WriteLine("Connection established");
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //networkStream = new NetworkStream(socket);
        }
        public void disconnect()
        {
            client.Client.Close();
            Console.WriteLine("Disconnecting server");
        }
        /// <summary>
        /// Reads from server 
        /// </summary>
        /// <param name="command"> command to the server </param>
        /// <returns></returns>
        public string read(string command)
        {
            //initializing data string
            string data = "";
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

                data = Encoding.ASCII.GetString(read, 0, read.Length);

                mutex.ReleaseMutex();
                //Console.WriteLine("server response of reading data is: {0}\n",Double.Parse(data));
                return data;
            }
            catch (Exception exception)
            {
                mutex.ReleaseMutex();
                disconnect();
                return data;
            }
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
                Console.WriteLine("server response after writing: {0}", data);
                mutex.ReleaseMutex();
            }
            catch (Exception exception)
            {
                mutex.ReleaseMutex();
                disconnect();
            }
        }
    }
}