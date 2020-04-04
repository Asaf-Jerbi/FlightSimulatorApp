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

        private bool mousePressed;

        public SimulatorWindow()
        {
            InitializeComponent();
            WheelsControlVM wcVM = new WheelsControlVM();
            wheelsControl.DataContext = wcVM;
        }

        }
    }
}
