using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTest.Utilit;

namespace UTest.menu
{
    public static class SubMenu
    {
        public static int Analyze()
        {
            return 0;
        }

        /// <summary>
        /// Sub Menu Compare Upatada Package
        /// </summary>
        /// <returns></returns>
        public static int CompareUpadataPackage()
        {
            Console.WriteLine("[1] Compare SSW package by comm data.");
            Console.WriteLine("[2] Compare USW package by bin file data.");
            Console.WriteLine("[3] Compare USW package by comm data.");
            Console.WriteLine("[4] Compare QSW Package by comm data.");
            Console.WriteLine("[9] Return main menu.");

            return ConsoleHelper.GetNumKey();
        }

        public static int SelectFileType()
        {
            Console.WriteLine("[1] Bin File.");
            Console.WriteLine("[2] Txt File.");

            return ConsoleHelper.GetNumKey();
        }
    }
}
