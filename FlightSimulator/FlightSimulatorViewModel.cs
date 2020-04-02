using Microsoft.Maps.MapControl.WPF;
using System;
using System.ComponentModel;


namespace FlightSimulator
{
    class FlightSimulatorViewModel : INotifyPropertyChanged
    {
        //Fields
        private IFightSimulatorModel model;
        private string ip;
        private int port;
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="model">The model in the MVVM architecture</param>
        public FlightSimulatorViewModel(IFightSimulatorModel model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }


        /// <summary>
        /// Connecting to server using ip, port which got from UI (using binding)
        /// </summary>
        public void connect()
        {
            this.model.connect(Ip, Port);
            this.model.start();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //===========All properties===========
        public string Ip
        {
            get { return this.ip; }
            set
            {
                if (ip != value)
                {
                    ip = value;
                    NotifyPropertyChanged("Ip");
                }
            }
        }
        public int Port
        {
            get { return this.port; }
            set
            {
                if (port != value)
                {
                    port = value;
                    NotifyPropertyChanged("Port");
                }
            }
        }

        public double VM_IndicatedHeadingDeg
        {
            get { return this.model.IndicatedHeadingDeg; }
            set { }
        }

        public Location VM_Location
        {
            get { return this.model.Location; }
            set { }
        }

        public double Throttle
        {
            get { return model.Throttle; }
        }
        public double VM_IndicatedVerticalSpeed
        {
            get { return this.model.IndicatedVerticalSpeed; }
            set { }
        }
        public double VM_IndicatedGroundSpeedKt
        {
            get { return this.model.IndicatedGroundSpeedKt; }
            set { }
        }
        public double VM_IndicatedSpeedKt
        {
            get { return this.model.IndicatedSpeedKt; }
            set { }
        }
        public double VM_IndicatedAltitudeFt
        {
            get { return this.model.IndicatedAltitudeFt; }
            set { }
        }

        public double VM_InternalRollDeg
        {
            get { return this.model.InternalRollDeg; }
            set { }
        }
        public double VM_InternalPitchDeg
        {
            get { return this.model.InternalPitchDeg; }
            set { }
        }
        public double VM_AltimeterIndicatedAltitudeFt
        {
            get { return this.model.AltimeterIndicatedAltitudeFt; }
            set { }
        }
    }
}
