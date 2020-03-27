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

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //public String ipAddress
        //{
        //    get { return ipAddress; }
        //    set { ipAddress = value; }
        //}

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {


            connectButton.MaxWidth = 200;
            connectButton.Content = "Connecting...";


            //Make here connection to server with a thread and then go to next page

            //open new window with the simulator after making the connection
            SimulatorWindow objSimulator = new SimulatorWindow();
            this.Visibility = Visibility.Hidden;
            objSimulator.Show();
            
        }
    }
}
