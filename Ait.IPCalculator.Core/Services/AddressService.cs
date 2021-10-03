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
        public string GetMaxHosts(string address, int cidr)
        {
            double maxHosts = Math.Pow(2, (32 - cidr)) - 2;
            return maxHosts.ToString();
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
        public string GetNetworkType(string input)
        {
            Address address = SetAddress(input);
            // Private addresses : 10.X.X.X, 172.16.X.X – 172.31.X.X, 192.168.X.X
            if (address.FirstOctet == 10 || (address.FirstOctet == 172 && (16 <= address.SecondOctet && address.SecondOctet <= 31) || (address.FirstOctet == 192 && address.SecondOctet == 168)))
                return NetworkType.Private.ToString();
            // Shared address space : 100.64.0.1 - 100.127.255.254
            else if (address.FirstOctet == 100 && (64 <= address.SecondOctet && address.SecondOctet <= 127) && (1 <= address.FourthOctet && address.FourthOctet <= 254))
                return NetworkType.Shared.ToString();
            // Loopback addresses : 127.X.X.X
            else if (address.FirstOctet == 127)
                return NetworkType.Loopback.ToString();
            // Link-local addresses : 169.254.X.X
            else if (address.FirstOctet == 169 && address.SecondOctet == 254)
                return NetworkType.LinkLocal.ToString();
            // Experimental addresses : 240.X.X.X – 255.X.X.X
            else if (240 <= address.FirstOctet && address.FirstOctet <= 255)
                return NetworkType.Experimental.ToString();
            // Test-net addresses : 192.0.2.0 - 192.0.2.255
            else if (address.FirstOctet == 192 && address.SecondOctet == 0 && address.ThirdOctet == 2)
                return NetworkType.TestNet.ToString();
            // Public addresses 
            else
                return NetworkType.Public.ToString();
        }

        private static Address SetAddress(string input)
        {
            string[] splitAddress = input.Split('.');
            byte[] convertedArray = splitAddress.Select(x => byte.Parse(x)).ToArray();

            return new Address(convertedArray[0], convertedArray[1], convertedArray[2], convertedArray[3]);
        }

    }
}
