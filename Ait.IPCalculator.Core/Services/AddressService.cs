using Ait.IPCalculator.Core.Entities;
using Ait.IPCalculator.Core.Enums;
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
        public string GetFirstHost(string address, int cidr)
        {
            Address firstHost = SetAddress(GetNetworkAddress(address, cidr));
            firstHost.FourthOctet++;

            return firstHost.ToString();
        }
        public string GetLastHost(string address, int cidr)
        {
            Address broadcastAddress = SetAddress(GetBroadcast(address, cidr));
            broadcastAddress.FourthOctet--;

            return broadcastAddress.ToString();
        }
        public string GetBroadcast(string address, int cidr)
        {
            Address networkAddress = SetAddress(GetNetworkAddress(address, cidr));
            string binary = converterService.ConvertDottedDecimalToBinary(networkAddress.ToString());
            string splitBinary = binary.Substring(0, cidr);
            string broadcastAddress = splitBinary;

            for (int i = 0; i < (32 - splitBinary.Length); i++)
                broadcastAddress += "1";

            broadcastAddress = converterService.ConvertBinaryToDottedDecimal(broadcastAddress);

            return broadcastAddress;
        }
        public string GetNetworkClass(string input)
        {
            Address address = SetAddress(input);

            // A class : first byte 0-127
            if (address.FirstOctet <= 127)
                return NetworkClass.A.ToString();
            // B class : first byte 128-191
            else if (128 <= address.FirstOctet && address.FirstOctet <= 191)
                return NetworkClass.B.ToString();
            // C class : first byte 192-223
            else if (192 <= address.FirstOctet && address.FirstOctet <= 223)
                return NetworkClass.C.ToString();
            // D class : first byte 224-239
            else if (224 <= address.FirstOctet && address.FirstOctet <= 239)
                return NetworkClass.D.ToString();
            // E class : first byte 240-255
            else
                return NetworkClass.E.ToString();
        }

        private Address SetAddress(string input)
        {
            string[] splitAddress = input.Split('.');
            byte[] convertedArray = splitAddress.Select(x => byte.Parse(x)).ToArray();

            return new Address(convertedArray[0], convertedArray[1], convertedArray[2], convertedArray[3]);
        }

    }
}
