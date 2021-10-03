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
            FirstByte = first;
            SecondByte = second;
            ThirdByte = third;
            FourthByte = fourth;
        }
        public byte FirstByte { get; set; }
        public byte SecondByte { get; set; }
        public byte ThirdByte { get; set; }
        public byte FourthByte { get; set; }
        public override string ToString()
        {
            return $"{FirstByte}.{SecondByte}.{ThirdByte}.{FourthByte}";
        }
    }
}
