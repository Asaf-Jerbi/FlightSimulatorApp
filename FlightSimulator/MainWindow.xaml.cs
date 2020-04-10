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

        public MainWindow()
        {
            InitializeComponent();
            IFightSimulatorModel model = new FlightSimulatorModel(new MyTelnetClient());
            fs_ViewModel = new FlightSimulatorViewModel(model);
            DataContext = fs_ViewModel; //here for binding the ip, port, etc
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {       
            try
            {
                //connect to server   
                this.fs_ViewModel.connect(); 
            } catch (Exception exception)
            {
                this.errorLabel.Visibility = Visibility.Visible;
            }
                //open simulator window
                SimulatorWindow objSimulator = new SimulatorWindow();
                this.Visibility = Visibility.Hidden;
                objSimulator.DataContext = fs_ViewModel;
                objSimulator.Show();            
        }
    }
}
