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
    /// Interaction logic for WheelsControl.xaml
    /// </summary>
    public partial class WheelsControl : UserControl
    {
        private double aileron;
        private double throttle;
        public WheelsControl()
        {
            InitializeComponent();
            //this.DataContext = new ViewModel.WheelsControlVM();
        }
        private void aileronSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double x = aileronSlider.Value;
            //Console.WriteLine(x);
            aileronValText.Text = Math.Round(x, 3).ToString();
        }
        /*
         * throttle slider change event handeler
         */
        private void throttleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double x = throttleSlider.Value;
            throttleValText.Text = Math.Round(x,3).ToString();
            //Console.WriteLine(x);
        }
    }
}
