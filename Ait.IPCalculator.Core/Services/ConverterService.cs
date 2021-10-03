using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ait.IPCalculator.Core.Services
{
    public class ConverterService
    {
        private static byte[] SplitAddress(string address)
        {
            string[] splitAddress = address.Split('.');
            byte[] convertedArray = splitAddress.Select(x => byte.Parse(x)).ToArray();
            return convertedArray;
        }

    }
}
