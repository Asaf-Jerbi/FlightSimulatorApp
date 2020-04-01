using Microsoft.Maps.MapControl.WPF;
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
using System.Windows.Shapes;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        private bool mousePressed;
        private double x, y, x1, y1;
        public SimulatorWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        /*
         * Mouse press down event handeler
         */
        //private void joystick_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (!mousePressed)
        //    {
        //        x = e.GetPosition(sender as Ellipse).X;
        //        y = e.GetPosition(sender as Ellipse).Y;
        //    }
        //    Console.WriteLine("Mouse Down");
        //    Console.WriteLine(x + "," + y);
        //    mousePressed = true;
        //}
        ///*
        // * Mouse movment event handeler
        // */
        //private void joystick_MouseMove(object sender, MouseEventArgs e)
        //{
        //    Console.WriteLine("Mouse Move");
        //    if (mousePressed)
        //    {
        //        Ellipse ellipse = (sender as Ellipse);
        //        x1 = e.GetPosition(ellipse).X;
        //        y1 = e.GetPosition(ellipse).Y;
        //    }

        //}
        ///*
        // * Mouse release event handeler
        // */
        //private void joystick_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    mousePressed = false;
        //    Console.WriteLine("Mouse up");
        //}

        /*
        * Helpin methos to calculate mouse movement length
        */
        private double length(double x, double y, double x1, double y1)
        {
            return Math.Sqrt((x1 - x) * (x1 - x) + (y1 - y) * (y1 - y));
        }
        /*
         * rudder slider change event handeler
         */
        
    }
}
