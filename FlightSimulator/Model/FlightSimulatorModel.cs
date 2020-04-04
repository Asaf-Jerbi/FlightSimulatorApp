using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator
{
    class FlightSimulatorModel : IFightSimulatorModel
    {
        public event PropertyChangedEventHandler PropertyChanged; //must have as INtifyPropertyChanged
        ITelnetClient telnetClient;
        volatile Boolean stop;

        public FlightSimulatorModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            this.stop = false;
        }

        public double Rudder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Elevator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Aileron { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Throttle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void connect(string ip, int port)
        {
            this.telnetClient.connect(ip, port);
        }

        public void disconnect()
        {
            stop = true;
            this.telnetClient.disconnect();
        }

        public void start()
        {
            new Thread(delegate ()
                {
                    
                    telnetClient.write("get rudder"); //TODO: here can be sent string to server 
                    Rudder = Double.Parse(telnetClient.read());
                    Elevator = Double.Parse(telnetClient.read());
                    Aileron = Double.Parse(telnetClient.read());
                    Throttle = Double.Parse(telnetClient.read());
                    Thread.Sleep(250); //sleeping for 1/4 second.
                }).Start();
        }
    }
}
