using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdataAnalyzeTool.Model;
using UpdataAnalyzeTool.Repository;
using UTest.Utilit;

namespace UTest.Commands
{
    public class CompareUSW
    {
        /// <summary>
        /// type : 0 num为加密软件中的序号， 0 num为Com数据中的包次序号
        /// </summary>
        /// <param name="type"></param>
        public static void Execute(int type)
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
            if(type == 0)
                Comparer(binRepo.uswSendList, comRepo.uswSendList);
            else
                Comparer(comRepo.uswSendList, binRepo.uswSendList);
        }

        private static void Comparer(List<USW_Send> uswList1, List<USW_Send> uswList2)
        {
            Console.WriteLine("Total packages: {0}. Input package num.", uswList1.Count);
            var numStr = Console.ReadLine();
            try
            {
                var num = Convert.ToInt32(numStr);
                if (num >= uswList1.Count)
                {
                    Console.WriteLine("Out of Range!");
                }

                var usw = uswList2.FindLast(p => p.packageNum[0] == uswList1[num].packageNum[0]);
                if (usw != null)
                {
                    ConsoleHelper.WriteByteArray(uswList1[num].body, usw.body, 16);
                    Console.CursorTop -= usw.body.Length / 16;
                    Console.CursorLeft = 16 * 3 + 5;
                    ConsoleHelper.WriteByteArray(usw.body, uswList1[num].body, 16, 16 * 3 + 5);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Package lost");
                }
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
