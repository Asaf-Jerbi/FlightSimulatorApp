using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator
{
    public interface IFightSimulatorModel : INotifyPropertyChanged
    {
        // Methods.
        void connect(string ip, int port);
        void disconnect();
        void start();
        void set(string varName,double value);

        // Properties:
        string Rudder { get; set; }
        string Elevator { get; set; }
        string Aileron { get; set; }
        string Throttle { get; set; }
        string IndicatedHeadingDeg { get; set; }
        string IndicatedVerticalSpeed { get; set; }
        string IndicatedGroundSpeedKt { get; set; }
        string IndicatedSpeedKt { get; set; }
        string IndicatedAltitudeFt { get; set; }
        string InternalRollDeg { get; set; }
        string InternalPitchDeg { get; set; }
        string AltimeterIndicatedAltitudeFt { get; set; }       
        Location Location { get; set; }
        string Slowness { get; set; }
        Thread StartThread { get; }
    }
}
