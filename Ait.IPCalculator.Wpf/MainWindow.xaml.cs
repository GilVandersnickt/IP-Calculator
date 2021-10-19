using Ait.IPCalculator.Core.Services;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Ait.IPCalculator.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AddressService addressService = new AddressService();
        ConverterService converterService = new ConverterService();
        SubnetService subnetService = new SubnetService();
        ValidatorService validatorService = new ValidatorService();

        public MainWindow()
        {
            InitializeComponent();
            // your code here...
            FillComboBox();
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            // your code here ...
            if (Validate())
                Calculate();
            else
                MessageBox.Show("Gelieve geldig ip adres in te geven.");
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
                return true;
            else
                return false;
        }
        private void Calculate()
        {
            txtIPBit.Text = converterService.ConvertDottedDecimalToBinary(txtIP.Text);
            txtSubnetBit.Text = converterService.ConvertDottedDecimalToBinary(subnetService.GetAllSubnetMasks().ElementAt(cmbSubnet.SelectedIndex).ToString());

            txtNetworkAddressBit.Text = converterService.ConvertDottedDecimalToBinary(addressService.GetNetworkAddress(txtIP.Text, cmbSubnet.SelectedIndex));
            txtNetworkAddressDD.Text = addressService.GetNetworkAddress(txtIP.Text, cmbSubnet.SelectedIndex);

            txtFirstHostAddressBit.Text = converterService.ConvertDottedDecimalToBinary(addressService.GetFirstHost(txtIP.Text, cmbSubnet.SelectedIndex));
            txtFirstHostAddressDD.Text = addressService.GetFirstHost(txtIP.Text, cmbSubnet.SelectedIndex);

            txtLastHostAddressBit.Text = converterService.ConvertDottedDecimalToBinary(addressService.GetLastHost(txtIP.Text, cmbSubnet.SelectedIndex));
            txtLastHostAddressDD.Text = addressService.GetLastHost(txtIP.Text, cmbSubnet.SelectedIndex);

            txtBroadcastAddressBit.Text = converterService.ConvertDottedDecimalToBinary(addressService.GetBroadcast(txtIP.Text, cmbSubnet.SelectedIndex));
            txtBroadcastAddressDD.Text = addressService.GetBroadcast(txtIP.Text, cmbSubnet.SelectedIndex);

            txtMaxNumberOfHosts.Text = addressService.GetMaxHosts(txtIP.Text, cmbSubnet.SelectedIndex);
            txtNetworkClass.Text = addressService.GetNetworkClass(txtIP.Text);
            txtNetworkType.Text = addressService.GetNetworkType(txtIP.Text);
        }
        private void FillComboBox()
        {
            foreach (string address in subnetService.GetAllCIDR())
            {
                cmbSubnet.Items.Add(address);
            }
            cmbSubnet.SelectedIndex = 0;
        }
        private void cmbSubnet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
        }

    }
}
