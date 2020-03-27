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
        public event PropertyChangedEventHandler PropertyChanged;

        public FlightSimulatorViewModel(IFightSimulatorModel model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    //Implementation missing here
                };
        }


        //Properties in the view model (for binding):\
 

        
    }
}
