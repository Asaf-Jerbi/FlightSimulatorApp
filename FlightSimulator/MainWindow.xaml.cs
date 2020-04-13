using FlightSimulator.ViewModel;
using System;
using System.Configuration;
using System.Windows;


namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        FlightSimulatorViewModel fs_ViewModel;
        private IFightSimulatorModel model;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.model = new FlightSimulatorModel(new MyTelnetClient());
            fs_ViewModel = new FlightSimulatorViewModel(this.model);
            DataContext = fs_ViewModel; //here for binding the ip, port, etc            
        }
       
        /// <summary>
        /// Connecting to server after "Connect" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectButton_Click(object sender, RoutedEventArgs e)
        {       
            try
            {
                //connect to server   
                this.fs_ViewModel.connect();
                //open simulator window
                SimulatorWindow objSimulator = new SimulatorWindow(model);
                objSimulator.wheelsControl.DataContext = new WheelsControlVM(this.model);
                this.Visibility = Visibility.Hidden;
                objSimulator.DataContext = fs_ViewModel;
                objSimulator.Show();
                //this.Close();
            } catch (Exception exception)
            {
                this.errorLabel.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Terminates the program when the "Exit" burron is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            //terminate the whole program 
            System.Windows.Application.Current.Shutdown();
        }
    }
}
