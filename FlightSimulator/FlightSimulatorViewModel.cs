using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator
{
    class FlightSimulatorViewModel : INotifyPropertyChanged
    {
        private IFightSimulatorModel model;
        private string _ip;
        private int _port;
        public event PropertyChangedEventHandler PropertyChanged;

        public string _Ip
        {
            get { return this._ip; }
            set
            {
                if (_ip != value)
                {
                    _ip = value;
                    OnPropertyChanged("_Ip");
                }
            }
        }

        public int _Port
        {
            get { return this._port; }
            set
            {
                if (_port != value)
                {
                    _port = value;
                    OnPropertyChanged("_Port");
                }
            }
        }


        public double Throttle
        {
            get
            {
                return model.Throttle;
            }            
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        
        public FlightSimulatorViewModel(IFightSimulatorModel model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    OnPropertyChanged("VM_" + e.PropertyName);
                };
        }


        public void connect()
        {            
            this.model.connect(_Ip, _Port);
            this.model.start();
        }       

    }
}
