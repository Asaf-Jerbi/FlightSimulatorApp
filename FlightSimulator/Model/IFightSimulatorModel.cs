using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator
{
    interface IFightSimulatorModel : INotifyPropertyChanged
    {
        void connect(string ip, int port);
        void disconnect();
        void start();
        void set(string varName,double value);

        double Rudder { get; set; }
        double Elevator { get; set; }
        double Aileron { get; set; }
        double Throttle { get; set; }
        double IndicatedHeadingDeg { get; set; }
        double IndicatedVerticalSpeed { get; set; }
        double IndicatedGroundSpeedKt { get; set; }
        double IndicatedSpeedKt { get; set; }
        double IndicatedAltitudeFt { get; set; }
        double InternalRollDeg { get; set; }
        double InternalPitchDeg { get; set; }
        double AltimeterIndicatedAltitudeFt { get; set; }
        string PlaneOutOfMap { get; set; }


        Location Location { get; set; }

    }
}
