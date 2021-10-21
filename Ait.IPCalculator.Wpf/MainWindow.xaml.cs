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
        private const string ValidationErrorMessage = "Gelieve geldig ip adres in te geven.";

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
                MessageBox.Show(ValidationErrorMessage);
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
            return validatorService.ValidateIpAddress(txtIP.Text);
        }
        private void Calculate()
        {
            txtIPBit.Text = converterService.ConvertToBinary(txtIP.Text);
            txtSubnetBit.Text = converterService.ConvertToBinary(addressService.GetSubnet(cmbSubnet.SelectedIndex));

            txtNetworkAddressBit.Text = converterService.ConvertToBinary(addressService.GetNetworkAddress(txtIP.Text, cmbSubnet.SelectedIndex));
            txtNetworkAddressDD.Text = addressService.GetNetworkAddress(txtIP.Text, cmbSubnet.SelectedIndex);

            txtFirstHostAddressBit.Text = converterService.ConvertToBinary(addressService.GetFirstHost(txtIP.Text, cmbSubnet.SelectedIndex));
            txtFirstHostAddressDD.Text = addressService.GetFirstHost(txtIP.Text, cmbSubnet.SelectedIndex);

            txtLastHostAddressBit.Text = converterService.ConvertToBinary(addressService.GetLastHost(txtIP.Text, cmbSubnet.SelectedIndex));
            txtLastHostAddressDD.Text = addressService.GetLastHost(txtIP.Text, cmbSubnet.SelectedIndex);

            txtBroadcastAddressBit.Text = converterService.ConvertToBinary(addressService.GetBroadcast(txtIP.Text, cmbSubnet.SelectedIndex));
            txtBroadcastAddressDD.Text = addressService.GetBroadcast(txtIP.Text, cmbSubnet.SelectedIndex);

            txtMaxNumberOfHosts.Text = addressService.GetMaxHosts(cmbSubnet.SelectedIndex);
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
