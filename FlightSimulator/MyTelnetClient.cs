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
            Console.WriteLine("Establishing connection to ip: {0} by port: {1}", ip, port);
            socket.Connect(ip, port);            
            Console.WriteLine("Connection established");
            networkStream = new NetworkStream(socket);
        }

        public void disconnect()
        {
            socket.Disconnect(false);
            Console.WriteLine("Disconnecting server");
        }

        public string read()
        {
            byte[] buffer = new byte[1024];
            if(networkStream == null)
            {
                throw new System.ArgumentException("Error: client try to read from servr before a connection was established");
            }
            networkStream.Read(buffer, 0, buffer.Length);
            StringBuilder myCompleteMessage = new StringBuilder();
            int numberOfBytesRead = 0;

            // Incoming message may be larger than the buffer size.
            do
            {
                numberOfBytesRead = networkStream.Read(buffer, 0, buffer.Length);

                myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(buffer, 0, numberOfBytesRead));
            }
            while (networkStream.DataAvailable);

            return myCompleteMessage.ToString();

        }

        public void write(string command)
        {
            if (networkStream.CanWrite)
            {
                byte[] myWriteBuffer = Encoding.ASCII.GetBytes(command);
                networkStream.Write(myWriteBuffer, 0, myWriteBuffer.Length);
            }
            else
            {
                Console.WriteLine("Sorry.  You cannot write to this NetworkStream.");
            }
        }
    }
}
