using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ait.IPCalculator.Core.Services
{
    public class ValidatorService
    {
        public ValidatorService()
        {

        }
        public bool ValidateIpAddress(string ipAddress)
        {
            if (String.IsNullOrWhiteSpace(ipAddress))
                return false;

            string[] splitAddresses = ipAddress.Split('.');
            if (splitAddresses.Length != 4)
                return false;

            return splitAddresses.All(splitAddress => byte.TryParse(splitAddress, out byte result));
        }
    }
}
