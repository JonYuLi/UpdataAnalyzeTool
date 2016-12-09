using IapServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdataAnalyzeTool.Model;
using UpdataAnalyzeTool.Utility;
using IapCLR;
using System.Text.RegularExpressions;
using UpdataAnalyzeTool.Repository;
using UpdataAnalyzeTool.Domain;
using System.IO;
using UTest.menu;
using UTest.Commands;
using System.Windows.Forms;

namespace UTest
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome! Input num key to select.\n");

            CmdParse cp = new CmdParse();

            while (true)
            {
                var mainMenuId = menu.MainMenu.GetMainCmdId();
                switch (mainMenuId)
                {
                    case 1:
                        GetBinRepository.Execute();
                        break;
                    case 2:
                        GetComRepository.Execute();
                        break;
                    case 3:
                        AnalyzeMain.Do();
                        break;
                    case 4:
                        ParseSubMenu_Compare();
                        break;
                    case 9:
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Bad Select!");
                        break;
                }
                Console.WriteLine();
            }
        }

        private static void ParseSubMenu_Compare()
        {
            var subMenuId = SubMenu.CompareUpadataPackage();
            switch (subMenuId)
            {
                case 1:
                    CompareSSW.Execute();
                    break;
                case 2:
                    CompareUSW.Execute(0);
                    break;
                case 3:
                    CompareUSW.Execute(1);
                    break;
                case 4:

                    break;
                case 9:
                    return;
                default:
                    Console.WriteLine("Bad Select!");
                    break;
            }
            Console.WriteLine();
        }
    }
}
