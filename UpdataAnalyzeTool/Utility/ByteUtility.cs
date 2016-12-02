using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdataAnalyzeTool.Utility
{
    public static class ByteUtility
    {
        public static byte[]  GetSubByte(byte[] source, int start, int length)
        {
            if (source.Length < 1)
            {
                return null;
            }

            byte[] retBytes = new byte[length];
            int k = 0;

            if (start >= source.Length)
            {
                return null;
            }
            for (int i = start; i < source.Length; i++)
            {
                retBytes[k] = source[i];
                k++;
                if (k >= length)
                {
                    break;
                }
            }

            return retBytes;
        }

        public static byte[] MakeReverse(byte[] reverse)
        {
            byte[] ret = new byte[reverse.Length];
            int index = reverse.Length;

            foreach (byte bb in reverse)
            {
                index--;
                ret[index] = bb;
            }

            return ret;
        }

        public static bool Compares(byte[] b1, byte[] b2)
        {
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++) 
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }
    }
}
