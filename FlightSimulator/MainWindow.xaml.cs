using System;
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
            //note: the view (main wondow) knows only the view model and doesn't know the model).
            fs_vm = new FlightSimulatorViewModel(new FlightSimulatorModel(new MyTelnetClient()));
            DataContext = fs_vm;
            
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                //connect to server   
                this.fs_ViewModel.connect(); 
                //open simulator window
                SimulatorWindow objSimulator = new SimulatorWindow();
                this.Visibility = Visibility.Hidden;
                objSimulator.DataContext = fs_ViewModel;
                objSimulator.Show();
            }
            catch (Exception exception)
            {
                this.errorLabel.Visibility = Visibility.Visible;
            }
        }
    }
}
