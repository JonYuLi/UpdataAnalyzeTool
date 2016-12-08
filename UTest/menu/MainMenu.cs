using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTest.Utilit;

namespace UTest.menu
{
    public static class MainMenu
    {
        public static int GetMainCmdId()
        {
            Console.WriteLine("[1] Select encrypted bin file.");
            Console.WriteLine("[2] Select comm data bin/txt file.");
            Console.WriteLine("[3] Analyze Data.");
            Console.WriteLine("[4] Compare Updata Package.");
            Console.WriteLine("[9] Clear Screen.");

            return ConsoleHelper.GetNumKey();
        }
    }
}
