using Microsoft.Maps.MapControl.WPF;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace FlightSimulator
{
    class FlightSimulatorModel : IFightSimulatorModel
    {
        // Fields.
        public event PropertyChangedEventHandler PropertyChanged;
        ITelnetClient telnetClient;
        private Location location;
        private Thread srartThread;

        // Dashboard values.       
        private string indicatedHeadingDeg;
        private string indicatedVerticalSpeed;
        private string indicatedGroundSpeedKt;
        private string indicatedSpeedKt;
        private string indicatedAltitudeFt;
        private string internalRollDeg;
        private string internalPitchDeg;
        private string altimeterIndicatedAltitudeFt;
        private string planeOutOfMap;

        // UI errors values.
        private string slowness;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="telnetClient">telnet client to connect with to server</param>        public FlightSimulatorModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
        }


        /// <summary>
        /// Connect method.
        /// </summary>
        /// <param name="ip"> Ip for connection </param>
        /// <param name="port"> Port for connection </param>
        public void connect(string ip, int port)
        {
            this.telnetClient.connect(ip, port);
        }

        /// <summary>
        /// Start Method, running a thread which gets the relevant data from server every 0.25 seconds.
        /// </summary>
        public void start()
        {
            this.srartThread = new Thread(delegate ()
            {
                Stopwatch stopwatch = new Stopwatch();

                while (true)
                {
                    try
                    {
                        // Get all dashboard values from server (only the first 5 characters).
                        IndicatedHeadingDeg = Truncate(telnetClient.read("get /instrumentation/heading-indicator/indicated-heading-deg\n"), 5);
                        IndicatedVerticalSpeed = Truncate(telnetClient.read("get /instrumentation/gps/indicated-vertical-speed\n"), 5);
                        IndicatedGroundSpeedKt = Truncate(telnetClient.read("get /instrumentation/gps/indicated-ground-speed-kt\n"), 5);
                        IndicatedSpeedKt = Truncate(telnetClient.read("get /instrumentation/airspeed-indicator/indicated-speed-kt\n"), 5);
                        IndicatedAltitudeFt = Truncate(telnetClient.read("get /instrumentation/gps/indicated-altitude-ft\n"), 5);
                        InternalRollDeg = Truncate(telnetClient.read("get /instrumentation/attitude-indicator/internal-roll-deg\n"), 5);
                        InternalPitchDeg = Truncate(telnetClient.read("get /instrumentation/attitude-indicator/internal-pitch-deg\n"), 5);
                        AltimeterIndicatedAltitudeFt = Truncate(telnetClient.read("get /instrumentation/altimeter/indicated-altitude-ft\n"), 5);
                        // Get the airplane location values (for replacing correctly the pushpin on the map).
                        double longtitude = double.Parse(this.telnetClient.read("get /position/longitude-deg\n"));
                        double latitude = double.Parse(this.telnetClient.read("get /position/latitude-deg\n"));
                        Location = new Location(latitude, longtitude);
                        // Sleeping - so the server won't be overloaded. 
                        Thread.Sleep(250);
                        // If Slowness error message apppears (stopwatch turned on)
                        // show the slowness message for 5 seconds and then remove it.
                        if (stopwatch.ElapsedMilliseconds > 5000)
                        {
                            Slowness = "";
                            stopwatch.Reset();
                        }
                    }
                    // If an IO exception is catched (means slowness / disconnection occured):
                    catch (IOException io)
                    {
                        // Display the relevant error message for each case.
                        if (this.telnetClient.isConnected() == true)
                        {
                            Slowness = "Slowness was detected";
                            stopwatch.Start();
                        }
                        else
                        {
                            Slowness = "Server is down";
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("\n\n\n\n\n*************Thread got an exeption:**************\n");
                        Console.WriteLine(e.Message + "\n\n\n\n\n");
                    }
                }
            });
            srartThread.Start();
        }

        /// <summary>
        /// Disconnection method.
        /// </summary>
        public void disconnect()
        {
            this.telnetClient.disconnect();
        }

        /// <summary>
        ///  Implemented as a demand of the derivd class.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Properties
        public string Slowness
        {
            get
            {
                return this.slowness;
            }
            set
            {
                if (this.slowness != value)
                {
                    this.slowness = value;
                    NotifyPropertyChanged("Slowness");
                }
            }
        }

        public string Rudder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Elevator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Aileron { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Throttle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Thread StartThread { get => srartThread; }
        public Location Location
        {
            get { return this.location; }
            set
            {
                this.location = value;
                //keeping in the right range.
                if(this.location.Latitude > 90)
                {
                    location.Latitude = 90;
                    this.PlaneOutOfMap = "Plane Out of map";
                    outOfMap = true;
                } else if (this.location.Latitude < -90)
                {
                    location.Latitude = -90;
                    PlaneOutOfMap = "Plane Out of map";
                    outOfMap = true;
                }
                else
                {
                    PlaneOutOfMap = "";
                    outOfMap = false;
                }
                if (this.location.Longitude > 180)
                {
                    location.Longitude = 180;
                    PlaneOutOfMap = "Plane Out of map";
                }
                else if (this.location.Longitude < -180)
                {
                    location.Longitude = -180;
                    PlaneOutOfMap = "Plane Out of map";
                } else
                {   if (!outOfMap)
                    {
                        PlaneOutOfMap = "";
                    }
                }
                NotifyPropertyChanged("Location");
            }
        }
        public string PlaneOutOfMap {
            get { return this.planeOutOfMap; }
            set {
                this.planeOutOfMap = value; 
                NotifyPropertyChanged("PlaneOutOfMap"); } }

        public string IndicatedHeadingDeg
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

        public string IndicatedVerticalSpeed
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
        public string IndicatedGroundSpeedKt
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
        public string IndicatedSpeedKt
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
        public string IndicatedAltitudeFt
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
        public string InternalRollDeg
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
        public string InternalPitchDeg
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
        public string AltimeterIndicatedAltitudeFt
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
            if (paramName.Equals("throttle"))
            {
                command = "set /controls/engines/current-engine/throttle " + value + "\n";
            }
            else if (paramName.Equals("aileron"))
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

        /// <summary>
        /// Helper method.
        /// Gets string and length and returns a substring of this string in the given length.
        /// </summary>
        /// <param name="value"> The string </param>
        /// <param name="maxLength"> Length of the returned string </param>
        /// <returns></returns>
        public string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}
