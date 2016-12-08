using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTest.Utilit
{
    public class ConsoleHelper
    {
        public static int GetNumKey()
        {
            var numKey = Console.ReadKey();
            Console.WriteLine();

            if (numKey.Key >= ConsoleKey.NumPad0 && numKey.Key <= ConsoleKey.NumPad9)
            {
                return numKey.Key - ConsoleKey.NumPad0;
            }
            if (numKey.Key >= ConsoleKey.D0 && numKey.Key <= ConsoleKey.D9)
            {
                return numKey.Key - ConsoleKey.D0;
            }
            return 0;
        }
    }
}
