using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace FlightSimulator
{
    class MyTelnetClient : ITelnetClient
    {

        private Socket socket;
        private NetworkStream networkStream;

        public void connect(string ip, int port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("Establishing connection to {0}", ip);
            socket.Connect(ip, port);
            Console.WriteLine("Connection established");

        }

        public void disconnect()
        {
            socket.Disconnect(false);
            Console.WriteLine("Disconnecting server");
        }

        public string read()
        {
            networkStream = new NetworkStream(socket);
            networkStream.Read
            //networkStream.Write("aa", 0, 0);
            ////Console.WriteLine("Writed successfully");
            ///

            return "No String was returned";
        }

        public void write(string command)
        {
            throw new NotImplementedException();
        }
    }
}
