using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdataAnalyzeTool.Repository;
using UTest.Utilit;

namespace UTest.Commands
{
    public class CompareQSW
    {
        public static void Execute()
        {
            UpdataRepository comRepo = Repository.Repository.ComRepository();
            UpdataRepository binRepo = Repository.Repository.BinRepository();

            if (comRepo == null)
            {
                Console.WriteLine("The comm data file has not Selected!");
                return;
            }
            if (binRepo == null)
            {
                Console.WriteLine("The encrypted bin data file has not Selected!");
                return;
            }

            Console.WriteLine("Total packages: {0}. Input package num.", comRepo.qswSendList.Count);
            var numStr = Console.ReadLine();
            try
            {
                var num = Convert.ToInt32(numStr);
                if (num >= comRepo.qswSendList.Count)
                {
                    Console.WriteLine("Out of Range!");
                }
                Console.Write("Bin File: ");
                ConsoleHelper.WriteByteArray(binRepo.qswSendList[0].source, comRepo.qswSendList[num].source);
                Console.Write("\nCom File: ");
                ConsoleHelper.WriteByteArray(comRepo.qswSendList[num].source, binRepo.qswSendList[0].source);
                Console.WriteLine();
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR! Please retry.");
                Console.ResetColor();
            }
        }
    }
}
