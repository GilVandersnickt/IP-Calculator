using Ait.IPCalculator.Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ait.IPCalculator.Core.Services
{
    public class ConverterService
    {
        public ConverterService()
        {
        }
        public string ConvertDottedDecimalToBinary(string input)
        {
            byte[] byteValues = SplitAddress(input);
            string binary = "";
            foreach (byte byteValue in byteValues)
            {
                binary += ConvertBitArrayToString(ConvertByteToBitArray(byteValue));
            }
            return binary;
        }
        public string ConvertBinaryToDottedDecimal(string input)
        {
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < (input.Length / 8); ++i)
            {
                bytes.Add(Convert.ToByte(input.Substring(8 * i, 8), 2));
            }
            Address dottedDecimal = new Address(bytes[0], bytes[1], bytes[2], bytes[3]);

            return dottedDecimal.ToString();
        }

        private static byte[] SplitAddress(string address)
        {
            string[] splitAddress = address.Split('.');
            byte[] convertedArray = splitAddress.Select(x => byte.Parse(x)).ToArray();
            return convertedArray;
        }
        private static BitArray ConvertByteToBitArray(byte input)
        {
            BitArray temporaryBitArray = new BitArray(BitConverter.GetBytes(input));
            BitArray bitArray = new BitArray(8);
            // Trim bitArray from 16 to 8 bits
            for (int i = 0; i < 8; i++)
                bitArray[i] = temporaryBitArray[i];

            ReverseBitArray(bitArray);
            return bitArray;
        }
        private static void ReverseBitArray(BitArray input)
        {
            int length = input.Length;

            for (int i = 0; i < (length / 2); i++)
            {
                bool bit = input[i];
                input[i] = input[length - i - 1];
                input[length - i - 1] = bit;
            }
        }
        private static string ConvertBitArrayToString(BitArray input)
        {
            string convertedBitArray = "";
            foreach (bool inputValue in input)
            {
                if (inputValue)
                    convertedBitArray += "1";
                else
                    convertedBitArray += "0";
            }
            return convertedBitArray;
        }

    }
}
