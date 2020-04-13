using FlightSimulator.ViewModel;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        
        private IFightSimulatorModel model;

        /// <summary>
        /// Constructor: gets the model as needed in MVVM architecutre.
        /// </summary>
        /// <param name="m">The model of this current view (as demanded in MVVM architecture)</param>
        public SimulatorWindow(IFightSimulatorModel m)
        {
            this.model = m;
            InitializeComponent();
            //WheelsControlVM wcVM = new WheelsControlVM();
            //wheelsControl.DataContext = wcVM;
        }


        /// <summary>
        /// Disconnect from server when the Disconnect button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            this.model.StartThread.Abort();
            this.model.disconnect();
            main.Show();
        }
    }
}
