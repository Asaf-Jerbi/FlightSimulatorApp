using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        FlightSimulatorViewModel fs_vm;

        public MainWindow()
        {
            InitializeComponent();
            //note: the view (main wondow) knows only the view model and doesn't know the model).
            fs_vm = new FlightSimulatorViewModel(new FlightSimulatorModel(new MyTelnetClient()));
            DataContext = fs_vm;
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {

            try
                {
                    //connect to server                     
                    this.fs_vm.connect();
                    //open new window with the simulator after making the connection
                    SimulatorWindow objSimulator = new SimulatorWindow();
                    this.Visibility = Visibility.Hidden;
                    objSimulator.Show();
                }
                catch (Exception exception)
                {
                    this.errorLabel.Visibility = Visibility.Visible;
                }     
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ipAddress.Text = "127.0.0.1";
            this.port.Text = "5402";
        }
    }
}
