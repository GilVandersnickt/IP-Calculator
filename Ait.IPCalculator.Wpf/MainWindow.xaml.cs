﻿using Ait.IPCalculator.Core.Services;
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


namespace Ait.IPCalculator.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConverterService converterService = new ConverterService();
        ValidatorService validatorService = new ValidatorService();

        public MainWindow()
        {
            InitializeComponent();
            // your code here...
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            // your code here ...
            if (Validate())
                MessageBox.Show("Gelieve geldig ip adres in te geven.");
            else
                Calculate();
        }
        private void ClearControls()
        {
            txtIPBit.Text = "";
            txtSubnetBit.Text = "";
            txtNetworkAddressBit.Text = "";
            txtNetworkAddressDD.Text = "";
            txtFirstHostAddressBit.Text = "";
            txtFirstHostAddressDD.Text = "";
            txtLastHostAddressBit.Text = "";
            txtLastHostAddressDD.Text = "";
            txtBroadcastAddressBit.Text = "";
            txtBroadcastAddressDD.Text = "";
            txtMaxNumberOfHosts.Text = "";
            txtNetworkClass.Text = "";
            txtNetworkType.Text = "";
        }
        private bool Validate()
        {
            if (validatorService.ValidateIpAddress(txtIP.Text))
                return false;
            else
                return true;
        }

        private void Calculate()
        {
            txtIPBit.Text = converterService.ConvertDottedDecimalToBinary(txtIP.Text);
        }
        private void cmbSubnet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
        }


    }
}
