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

        public WheelsControl()
        {
            InitializeComponent();
            //this.DataContext = new ViewModel.WheelsControlVM();
        }
        /// <summary>
        /// Slider moving event handeler
        /// </summary>
        private void aileronSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //getting new value and showing on screen
            double x = aileronSlider.Value;
            //Console.WriteLine(x);
            aileronValText.Text = Math.Round(x, 3).ToString();
        }
        /// <summary>
        /// Slider moving event handeler
        /// </summary>
        private void throttleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //getting new value and showing on screen
            double x = throttleSlider.Value;
            throttleValText.Text = Math.Round(x,3).ToString();
            //Console.WriteLine(x);
        }
    }
}
