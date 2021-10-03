﻿using Ait.IPCalculator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ait.IPCalculator.Core.Services
{
    public class SubnetService
    {
        public SubnetService()
        {
        }
        public List<Address> GetAllSubnetMasks()
        {
            List<Address> addresses = new List<Address>();
            List<byte> bytes = GetSubnetMaskBytes();
            Address address;

            foreach (byte byteValue in bytes)
            {
                address = new Address(byteValue, 0, 0, 0);
                addresses.Add(address);
            }
            foreach (byte byteValue in bytes)
            {
                address = new Address(255, byteValue, 0, 0);
                addresses.Add(address);
            }
            foreach (byte byteValue in bytes)
            {
                address = new Address(255, 255, byteValue, 0);
                addresses.Add(address);
            }
            foreach (byte byteValue in bytes)
            {
                address = new Address(255, 255, 255, byteValue);
                addresses.Add(address);
            }

            return addresses;
        }
        public List<string> GetAllCIDR()
        {
            List<Address> addresses = GetAllSubnetMasks();
            List<string> convertedAddresses = new List<string>();

            foreach (Address address in addresses)
            {
                convertedAddresses.Add(address.ToString() + $"/{addresses.IndexOf(address)}");
            }

            return convertedAddresses;
        }

        private List<byte> GetSubnetMaskBytes()
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