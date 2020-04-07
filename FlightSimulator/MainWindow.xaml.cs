﻿using FlightSimulator.ViewModel;
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
        private IFightSimulatorModel model;

        public MainWindow()
        {
            InitializeComponent();
            this.model = new FlightSimulatorModel(new MyTelnetClient());
            fs_ViewModel = new FlightSimulatorViewModel(this.model);
            DataContext = fs_ViewModel; //here for binding the ip, port, etc

            
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                //connect to server   
                this.fs_ViewModel.connect(); 
                //open simulator window
                SimulatorWindow objSimulator = new SimulatorWindow();
                objSimulator.wheelsControl.DataContext = new WheelsControlVM(this.model);
                this.Visibility = Visibility.Hidden;
                objSimulator.DataContext = fs_ViewModel;
                objSimulator.Show();
                this.Close();
            }
            catch (Exception exception)
            {
                this.errorLabel.Visibility = Visibility.Visible;
            }
        }
    }
}
