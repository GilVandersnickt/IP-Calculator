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
