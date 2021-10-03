using Ait.IPCalculator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ait.IPCalculator.Core.Services
{
    public class AddressService
    {
        private readonly SubnetService subnetService;
        private readonly ConverterService converterService;

        public AddressService()
        {
            subnetService = new SubnetService();
            converterService = new ConverterService();
        }
        public string GetNetworkAddress(string address, int cidr)
        {
            Address networkAddress = SetAddress(address);
            Address subnetMaskAddress = subnetService.GetAllSubnetMasks().ElementAt(cidr);

            Address subnet = new Address(
                (byte)(networkAddress.FirstOctet & subnetMaskAddress.FirstOctet),
                (byte)(networkAddress.SecondOctet & subnetMaskAddress.SecondOctet),
                (byte)(networkAddress.ThirdOctet & subnetMaskAddress.ThirdOctet),
                (byte)(networkAddress.FourthOctet & subnetMaskAddress.FourthOctet)
                );

            return subnet.ToString();
        }

        private Address SetAddress(string input)
        {
            string[] splitAddress = input.Split('.');
            byte[] convertedArray = splitAddress.Select(x => byte.Parse(x)).ToArray();

            return new Address(convertedArray[0], convertedArray[1], convertedArray[2], convertedArray[3]);
        }

    }
}
