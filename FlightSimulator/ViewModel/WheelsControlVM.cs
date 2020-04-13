using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModel
{
    /// <summary>
    /// View model for wheels control view.
    /// </summary>
    class WheelsControlVM
    {
        IFightSimulatorModel model;
        public WheelsControlVM(IFightSimulatorModel model)
        {
            this.model = model;
        }
        //Our fields.
        private double rudder;
        private double elevator;
        private double throttle;
        private double aileron;

       
        //The VM properties with the desired behavior so we will be able to send set command to the server when value changed.
        //the properties are binding to properties in the WheelsControl view.
        public double VM_rudder
        {
            get { return this.rudder; }
            set
            {
                if (this.rudder != value)
                {
                    this.rudder = Math.Round(value, 3);
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
