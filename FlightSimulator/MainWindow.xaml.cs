using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        FlightSimulatorViewModel fs_vm;

        public MainWindow()
        {
            InitializeComponent();
            //note: the view (main wondow) knows only the view model and doesn't know the model).
            fs_vm = new FlightSimulatorViewModel(new FlightSimulatorModel(new MyTelnetClient()));
            DataContext = fs_vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {           
            //todo: change in window two fields of
        }
    }
}
