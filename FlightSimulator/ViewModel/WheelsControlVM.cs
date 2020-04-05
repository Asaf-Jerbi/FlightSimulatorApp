using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModel
{
    class WheelsControlVM
    {
        IFightSimulatorModel model;
        public WheelsControlVM(IFightSimulatorModel model)
        {
            this.model = model;
        }
        private double rudder;
        private double elevator;
        private double throttle;
        private double aileron;

       

        public double VM_rudder
        {
            get { return this.rudder; }
            set
            {
                if (this.rudder != value)
                {
                    this.rudder = Math.Round(value, 3);
                    Console.WriteLine("rudder = "+value);
                    model.set("rudder", value);
                    
                }
            }
        }

        public double VM_elevator
        {
            get { return this.elevator; }
            set
            {
                if (this.elevator != value)
                {
                    this.elevator = Math.Round(value,3);
                    model.set("elevator", value);
                }
            }
        }

        public double VM_aileron
        {
            get { return this.aileron; }
            set { this.aileron = value;
                model.set("aileron", value);
            }
        }
        public double VM_throttle
        {
            get { return this.throttle; }
            set { this.throttle = value;
                model.set("throttle", value);
            }
        }
    }
}
