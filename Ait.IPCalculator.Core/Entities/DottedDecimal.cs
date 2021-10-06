namespace Ait.IPCalculator.Core.Entities
{
    public class DottedDecimal
    {
        public DottedDecimal(byte first, byte second, byte third, byte fourth)
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
