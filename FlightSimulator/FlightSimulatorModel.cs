using Microsoft.Maps.MapControl.WPF;
using System;
using System.ComponentModel;
using System.Threading;

namespace FlightSimulator
{
    class FlightSimulatorModel : IFightSimulatorModel
    {
        //Fields
        public event PropertyChangedEventHandler PropertyChanged; 
        ITelnetClient telnetClient;
        volatile Boolean stop;
        private Location location;
        private double indicatedHeadingDeg;
        private double indicatedVerticalSpeed;
        private double indicatedGroundSpeedKt;
        private double indicatedSpeedKt;
        private double indicatedAltitudeFt;
        private double internalRollDeg;
        private double internalPitchDeg;
        private double altimeterIndicatedAltitudeFt;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="telnetClient">telnet client to connect with to server</param>
        public FlightSimulatorModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            this.stop = false;
        }


        public void connect(string ip, int port)
        {
            this.telnetClient.connect(ip, port);
        }

        public void start()
        {
            new Thread(delegate ()
            {
                while (true)
                {
                    // get all dashboard data
                    IndicatedHeadingDeg = Double.Parse(telnetClient.read("get /instrumentation/heading-indicator/indicated-heading-deg \r\n"));
                    IndicatedVerticalSpeed = Double.Parse(telnetClient.read("get /instrumentation/gps/indicated-vertical-speed \r\n"));
                    IndicatedGroundSpeedKt = Double.Parse(telnetClient.read("get /instrumentation/gps/indicated-ground-speed-kt \r\n"));
                    IndicatedSpeedKt = Double.Parse(telnetClient.read("get /instrumentation/airspeed-indicator/indicated-speed-kt \r\n"));
                    IndicatedAltitudeFt = Double.Parse(telnetClient.read("get /instrumentation/gps/indicated-altitude-ft \r\n"));
                    InternalRollDeg = Double.Parse(telnetClient.read("get /instrumentation/attitude-indicator/internal-roll-deg \r\n"));
                    InternalPitchDeg = Double.Parse(telnetClient.read("get /instrumentation/attitude-indicator/internal-pitch-deg \r\n"));
                    AltimeterIndicatedAltitudeFt = Double.Parse(telnetClient.read("get /instrumentation/altimeter/indicated-altitude-ft \r\n"));
                    // get longtitude
                    double longtitude = double.Parse(this.telnetClient.read("get /position/longitude-deg\n"));
                    // get altitude
                    double latitude = double.Parse(this.telnetClient.read("get /position/latitude-deg\n"));
                    Location = new Location(latitude, longtitude);
                    //sleep for 1/4 second
                    Thread.Sleep(250);
                }
            }).Start();
        }

        public void disconnect()
        {
            this.stop = true;
            this.telnetClient.disconnect();
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public double Rudder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Elevator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Aileron { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Throttle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Location Location
        {
            get { return this.location; }
            set
            {
                this.location = value;
                NotifyPropertyChanged("Location");
            }
        }

        public double IndicatedHeadingDeg
        {
            get
            {
                return this.indicatedHeadingDeg;
            }

            set
            {
                if (this.indicatedHeadingDeg != value)
                {
                    this.indicatedHeadingDeg = value;
                    NotifyPropertyChanged("IndicatedHeadingDeg");
                }
            }
        }

        public double IndicatedVerticalSpeed
        {
            get { return this.indicatedVerticalSpeed; }
            set
            {
                if (this.indicatedVerticalSpeed != value)
                {
                    this.indicatedVerticalSpeed = value;
                    NotifyPropertyChanged("IndicatedVerticalSpeed");
                }
            }
        }
        public double IndicatedGroundSpeedKt
        {
            get { return this.indicatedGroundSpeedKt; }
            set
            {
                if (this.indicatedGroundSpeedKt != value)
                {
                    this.indicatedGroundSpeedKt = value;
                    NotifyPropertyChanged("IndicatedGroundSpeedKt");
                }
            }
        }
        public double IndicatedSpeedKt
        {
            get { return this.indicatedSpeedKt; }
            set
            {
                if (this.indicatedSpeedKt != value)
                {
                    this.indicatedSpeedKt = value;
                    NotifyPropertyChanged("IndicatedSpeedKt");
                }
            }
        }
        public double IndicatedAltitudeFt
        {
            get { return this.indicatedAltitudeFt; }
            set
            {
                if (this.indicatedAltitudeFt != value)
                {
                    this.indicatedAltitudeFt = value;
                    NotifyPropertyChanged("IndicatedAltitudeFt");
                }
            }
        }
        public double InternalRollDeg
        {
            get { return this.internalRollDeg; }
            set
            {
                if (this.internalRollDeg != value)
                {
                    this.internalRollDeg = value;
                    NotifyPropertyChanged("InternalRollDeg");
                }
            }
        }
        public double InternalPitchDeg
        {
            get { return this.internalPitchDeg; }
            set
            {
                if (this.internalPitchDeg != value)
                {
                    this.internalPitchDeg = value;
                    NotifyPropertyChanged("InternalPitchDeg");
                }
            }
        }
        public double AltimeterIndicatedAltitudeFt
        {
            get { return this.altimeterIndicatedAltitudeFt; }
            set
            {
                if (this.altimeterIndicatedAltitudeFt != value)
                {
                    this.altimeterIndicatedAltitudeFt = value;
                    NotifyPropertyChanged("AltimeterIndicatedAltitudeFt");
                }
            }
        }



    }
}
