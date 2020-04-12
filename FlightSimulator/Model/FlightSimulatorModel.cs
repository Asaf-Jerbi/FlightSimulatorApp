using Microsoft.Maps.MapControl.WPF;
using System;
using System.ComponentModel;
using System.Text;
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
                    IndicatedHeadingDeg = Math.Round(Double.Parse(telnetClient.read("get /instrumentation/heading-indicator/indicated-heading-deg\n")), 3);
                    IndicatedVerticalSpeed = Math.Round(Double.Parse(telnetClient.read("get /instrumentation/gps/indicated-vertical-speed\n")), 3);
                    IndicatedGroundSpeedKt = Math.Round(Double.Parse(telnetClient.read("get /instrumentation/gps/indicated-ground-speed-kt\n")), 3);
                    IndicatedSpeedKt = Math.Round(Double.Parse(telnetClient.read("get /instrumentation/airspeed-indicator/indicated-speed-kt\n")), 3);
                    IndicatedAltitudeFt = Math.Round(Double.Parse(telnetClient.read("get /instrumentation/gps/indicated-altitude-ft\n")), 3);
                    InternalRollDeg = Math.Round(Double.Parse(telnetClient.read("get /instrumentation/attitude-indicator/internal-roll-deg\n")), 3);
                    InternalPitchDeg = Math.Round(Double.Parse(telnetClient.read("get /instrumentation/attitude-indicator/internal-pitch-deg\n")), 3);
                    AltimeterIndicatedAltitudeFt = Math.Round(Double.Parse(telnetClient.read("get /instrumentation/altimeter/indicated-altitude-ft\n")), 3);
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
                //keeping in the right range.
                if(value.Latitude > 90)
                {
                    location.Latitude = 90;
                    PlaneOutOfMap = true;
                } else if (value.Latitude < -90)
                {
                    location.Latitude = -90;
                    PlaneOutOfMap = true;
                }
                else
                {
                    PlaneOutOfMap = false;
                }
                if (value.Longitude > 180)
                {
                    location.Longitude = 180;
                    PlaneOutOfMap = true;
                }
                else if (value.Longitude < -180)
                {
                    location.Longitude = -180;
                    PlaneOutOfMap = true;
                } else
                {
                    PlaneOutOfMap = false;
                }
                NotifyPropertyChanged("Location");
            }
        }
        public bool PlaneOutOfMap {
            get { return this.PlaneOutOfMap; }
            set { NotifyPropertyChanged("PlaneOutOfMap"); } }

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
        /// <summary>
        /// this method make a set command to sent to the server
        /// </summary>
        /// <param name="paramName"> the parameter name</param>
        /// <param name="value">the parameter value</param>
        public void set(string paramName, double value)
        {
            //its null but will be initialized.
            string command = null;
            if(paramName.Equals("throttle"))
            {
                command = "set /controls/engines/current-engine/throttle " + value + "\n";
            } else if (paramName.Equals("aileron"))
            {
                command = "set /controls/flight/aileron " + value + "\n";
            }
            else if (paramName.Equals("elevator"))
            {
                command = "set /controls/flight/elevator " + value + "\n";
            }
            else if (paramName.Equals("rudder"))
            {
                command = "set /controls/flight/rudder " + value + "\n";
            }
            telnetClient.write(command);

        }
    }
}
