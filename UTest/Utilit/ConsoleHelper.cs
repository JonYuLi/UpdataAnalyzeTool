using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTest.Utilit
{
    public class ConsoleHelper
    {
        /// <summary>
        /// 用户从键盘上按下的数字键
        /// </summary>
        /// <returns></returns>
        public static int GetNumKey()
        {
            //var numKey = Console.ReadKey();
            //Console.WriteLine();

            //if (numKey.Key >= ConsoleKey.NumPad0 && numKey.Key <= ConsoleKey.NumPad9)
            //{
            //    return numKey.Key - ConsoleKey.NumPad0; //小键盘的数字键
            //}
            //if (numKey.Key >= ConsoleKey.D0 && numKey.Key <= ConsoleKey.D9)
            //{
            //    return numKey.Key - ConsoleKey.D0;      //键盘主区域的数字键
            //}
            //return 0;
            var line = Console.ReadLine();
            try
            {
                return Int32.Parse(line);
            }
            catch
            {
                return 0; 
            }

            //return 0;
        }

        /// <summary>
        /// 在控制台上打印数组b1, 与b2数组不同的数据会以红色显示
        /// </summary>
        /// <param name="b1">要输出的数组</param>
        /// <param name="b2">用来比较的数组</param>
        /// <param name="rowLength">一行输出十进制数的个数</param>
        /// <param name="cursorLeft">光标向右的偏移</param>
        public static void WriteByteArray(byte[] b1, byte[] b2, int rowLength = 0, int cursorLeft = 0)
        {
            var len = Math.Min(b1.Length, b2.Length);
            for (int i = 0; i < len; i++)
            {
                if (b1[i] != b2[i])
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(b1[i].ToString("X2") + " ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(b1[i].ToString("X2") + " ");
                }
                if (rowLength != 0 && (i + 1) % rowLength == 0)
                {
                    Console.WriteLine();
                    Console.CursorLeft += cursorLeft;
                }
            }
            for (int i = len; i < b1.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(b2[i].ToString("X2") + " ");
                Console.ResetColor();
                if (rowLength != 0 && (i + 1) % rowLength == 0)
                {
                    Console.WriteLine();
                    Console.CursorLeft += cursorLeft;
                }
            }
        }
    }
}
