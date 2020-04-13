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

namespace FlightSimulator.views
{
    /// <summary>
    /// Interaction logic for joystick.xaml
    /// </summary>
    public partial class joystick : UserControl
    {
        private Point firstPoint = new Point();

        public joystick()
        {
            InitializeComponent();
        }
        
        /*
         * Dependency property to be able to bind joystick properties
         */
        public static readonly DependencyProperty rudderProperty =
         DependencyProperty.Register("rudder", typeof(double),typeof(joystick));
        public double rudder
        {
            get { return (double)GetValue(rudderProperty); }
            set { SetValue(rudderProperty, value);
            }
        }
        public static readonly DependencyProperty elevatorProperty =
         DependencyProperty.Register("elevator", typeof(double), typeof(joystick));
        public double elevator
        {
            get { return (double)GetValue(elevatorProperty); }
            set
            {
                SetValue(elevatorProperty, value);
            }
        }   
        /// <summary>
        /// Event hadeler when mouse is presed on know
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
           if(e.ChangedButton == MouseButton.Left)
            {
                firstPoint = e.GetPosition(this);
                (Knob).CaptureMouse();
            }
        }

        /// <summary>
        ///  This function take care of know event when mouse clicked and moved at the same time
        ///  we take care that the know wont be able to get out of the joystick , but keep move as long
        ///  as the mouse still pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            double slope ,absX, absY;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //calculating lineare quation to find points at radius distance from base if mouse go out.
                double x = e.GetPosition(this).X - firstPoint.X;
                double y = e.GetPosition(this).Y - firstPoint.Y;
                //if mouse position is out of joystick range
                if (Math.Sqrt(x * x + y * y) > Base.Width / 2)
                {
                    //handling all cases, because the y axis is opposite
                    if (x == 0)
                    {
                        if (y > 0)
                        {
                            knobPosition.Y = 170;
                        } else if ( y < 0)
                        {
                            knobPosition.Y = -170;
                        }
                    }
                    else
                    {
                        //linear equation to calculate point at radious on same line.
                        slope = y / x;
                        absX = Math.Sqrt(Math.Pow(Base.Width / 2, 2) / (Math.Pow(slope, 2) + 1));
                        absY = absX * slope;
                        if (x > 0)
                        {
                            knobPosition.X = absX;
                        }
                        else if (x < 0)
                        {
                            knobPosition.X = -absX;
                        }
                        else
                        {
                            knobPosition.X = 0;
                        }
                        if (y > 0)
                        {
                            if (x > 0)
                            {
                                knobPosition.Y = absY;
                            }
                            else
                            {
                                knobPosition.Y = -absY;
                            }
                        }
                        else if (y < 0)
                        {
                            if (x < 0)
                            {
                                knobPosition.Y = -absY;
                            }
                            else
                            {
                                knobPosition.Y = absY;
                            }
                        }

                        else
                        {
                            knobPosition.Y = 0;
                        }
                    }
                }
                //else know position in joystick range
                else
                {
                    knobPosition.X = x;
                    knobPosition.Y = y;
                }
            }
            //normalize our values to [-1,1] range
            rudder = knobPosition.X / 170;
            elevator = knobPosition.Y / -170;
        }

        /// <summary>
        /// Mouse click release event handeler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //initial values to start values
            knobPosition.X = 0;
            knobPosition.Y = 0;
            rudder = 0;
            elevator = 0;
            UIElement element = (UIElement)Knob;
            element.ReleaseMouseCapture();
        }
    }
}
