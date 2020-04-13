
namespace FlightSimulator
{
    /// <summary>
    /// An interface for clients
    /// </summary>
    interface ITelnetClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        void connect(string ip, int port);
        void write(string command);
        string read(string command); //blocking call 
        void disconnect();
        bool isConnected();
    }
}
