
namespace FlightSimulator
{
    interface ITelnetClient
    {
        void connect(string ip, int port);
        void write(string command);
        string read(string command); //blocking call 
        void disconnect(); 
    }
}
