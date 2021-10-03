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
        private Address SetAddress(string input)
        {
            string[] splitAddress = input.Split('.');
            byte[] convertedArray = splitAddress.Select(x => byte.Parse(x)).ToArray();

            return new Address(convertedArray[0], convertedArray[1], convertedArray[2], convertedArray[3]);
        }

    }
}
