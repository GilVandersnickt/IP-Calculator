using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ait.IPCalculator.Core.Entities
{
    public class Address
    {
        public Address(byte first, byte second, byte third, byte fourth)
        {
            FirstOctet = first;
            SecondOctet = second;
            ThirdOctet = third;
            FourthOctet = fourth;
        }
        public byte FirstOctet { get; set; }
        public byte SecondOctet { get; set; }
        public byte ThirdOctet { get; set; }
        public byte FourthOctet { get; set; }
        public override string ToString()
        {
            return $"{FirstOctet}.{SecondOctet}.{ThirdOctet}.{FourthOctet}";
        }
    }
}
