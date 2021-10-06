using Ait.IPCalculator.Core.Entities;
using System;
using System.Collections.Generic;

namespace Ait.IPCalculator.Core.Services
{
    public class SubnetService
    {
        public SubnetService()
        {
        }
        public List<DottedDecimal> GetAllSubnetMasks()
        {
            List<DottedDecimal> addresses = new List<DottedDecimal>();
            List<byte> bytes = GetSubnetMaskBytes();
            DottedDecimal address;

            foreach (byte byteValue in bytes)
            {
                address = new DottedDecimal(byteValue, 0, 0, 0);
                addresses.Add(address);
            }
            foreach (byte byteValue in bytes)
            {
                address = new DottedDecimal(255, byteValue, 0, 0);
                addresses.Add(address);
            }
            foreach (byte byteValue in bytes)
            {
                address = new DottedDecimal(255, 255, byteValue, 0);
                addresses.Add(address);
            }
            foreach (byte byteValue in bytes)
            {
                address = new DottedDecimal(255, 255, 255, byteValue);
                addresses.Add(address);
            }

            addresses.RemoveAt(addresses.Count - 1);

            return addresses;
        }
        public List<string> GetAllCIDR()
        {
            List<DottedDecimal> addresses = GetAllSubnetMasks();
            List<string> convertedAddresses = new List<string>();

            foreach (DottedDecimal address in addresses)
            {
                convertedAddresses.Add(address.ToString() + $" (/{addresses.IndexOf(address)})");
            }

            return convertedAddresses;
        }

        private static List<byte> GetSubnetMaskBytes()
        {
            List<byte> bytes = new List<byte>();
            string binary = "";

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (j < i)
                        binary += "1";
                    else
                        binary += "0";
                }
                bytes.Add(Convert.ToByte(binary, 2));
                binary = "";
            }
            return bytes;
        }

    }
}
