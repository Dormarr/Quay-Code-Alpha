﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Text;
using System.Threading.Tasks;
using STH1123.ReedSolomon;


namespace QuayCodeV2
{
    internal class ReedSolomonEncoding
    {
        int eccAmount;
        GenericGF field = new GenericGF(285, 256, 0);

        public int[] Encode(string input, int inputCount)
        {
            int fieldSize = input.Length;

            ReedSolomonEncoder rsE = new ReedSolomonEncoder(field);

            eccAmount = input.Length - inputCount;

            byte[] bytes = Encoding.UTF8.GetBytes(input);
            int[] bytesAsInts = bytes.Select(x => (int)x).ToArray();

            rsE.Encode(bytesAsInts, eccAmount);

            return bytesAsInts.ToArray();
        }

        public int[] Decode(byte[] input, int inputCount)
        {
            int[] erasures = new int[] { };

            ReedSolomonDecoder rsD = new ReedSolomonDecoder(field);

            int[] output = new int[input.Length];

            for(int i= 0; i < input.Length; i++)
            {
                output[i] = input[i];
            }

            eccAmount = inputCount / 2;

            if(rsD.Decode(output, eccAmount, erasures))
            {
                //data corrected.
                return output;
            }
            else
            {
                return null;
            }
        }
    }
}