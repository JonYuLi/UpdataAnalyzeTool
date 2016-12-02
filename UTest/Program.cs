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

namespace UTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Cmd Help!");
            Console.WriteLine("  OpenSoftFile [file path name]");
            Console.WriteLine("  OpenComFile bin|txt [file path name]");
            Console.WriteLine("  Analyze");
            Console.WriteLine("  Compare(SSW|USW|QSW) [packageNum]");
            Console.WriteLine("\n");


            CmdParse cp = new CmdParse();
            while (true)
            {
                var cmdLine1 = Console.ReadLine();
                var cmd = cmdLine1.Split(' ');
                if (cmd[0].ToLower() == "opensoftfile")
                {
                    if (cmd.Length < 2)
                    {
                        Console.WriteLine("use: OpenSoftFile FilePathName");
                        continue;
                    }

                    cp.OpenBinFile(cmd[1]);
                }
                else if (cmd[0].ToLower() == "opencomfile")
                {
                    if (cmd.Length < 3)
                    {
                        Console.WriteLine("use: OpenComFile bin|txt FilePathName");
                        continue;
                    }
                    cp.OpenComFile(cmd[2], cmd[1]);
                }
                else if (cmd[0].ToLower() == "analyze")
                {
                    cp.Analyze();
                }
                else if (cmd[0].ToLower() == "comparessw")
                {
                    if (cmd.Length < 2)
                    {
                        Console.WriteLine("use: CompareSSW num");
                        continue;
                    }
                    cp.CompareSSW(Convert.ToInt32(cmd[1]));
                }
                else if (cmd[0].ToLower() == "compareusw")
                {
                    if (cmd.Length < 2)
                    {
                        Console.WriteLine("use: CompareUSW num");
                        continue;
                    }
                    cp.CompareUSW(Convert.ToInt32(cmd[1]));
                }
                else if (cmd[0] == "")
                {

                }
                else
                {
                    Console.WriteLine("Unkown Command!");
                }
            }
        }
    }
}
