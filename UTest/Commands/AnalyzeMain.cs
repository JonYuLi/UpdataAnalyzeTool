using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdataAnalyzeTool.Model;
using UpdataAnalyzeTool.Repository;
using UpdataAnalyzeTool.Utility;

namespace UTest.Commands
{
    public class AnalyzeMain
    {
        /// <summary>
        /// 分析数据时调用此函数
        /// </summary>
        public static void Do()
        {
            UpdataRepository comRepo = Repository.Repository.ComRepository();
            UpdataRepository binRepo = Repository.Repository.BinRepository();
            try
            {
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

                var binSSW = binRepo.sswSendList[0];
                AnalyzeSSW(binSSW, comRepo.sswSendList);

                AnalyzeUSW(binRepo.uswSendList, comRepo.uswSendList);

                var binQSW = binRepo.qswSendList[0];
                AnalyzeQSW(binQSW, comRepo.qswSendList);

                AnalyzeSSW_resp();
                AnalyzeUSW_resp();
                AnalyzeQSW_resp();
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Analyze Data Error!");
                Console.ResetColor();
                Console.WriteLine(ex + ex.Message);
            }
        }

        /// <summary>
        /// 分析USW响应包
        /// </summary>
        private static void AnalyzeUSW_resp()
        {
            
        }

        /// <summary>
        /// 分析QSW响应包
        /// </summary>
        private static void AnalyzeQSW_resp()
        {
            
        }

        /// <summary>
        /// 分析SSW的响应包
        /// </summary>
        private static void AnalyzeSSW_resp()
        {
            
        }

        /// <summary>
        /// 对比QSW数据包
        /// </summary>
        private static void AnalyzeQSW(QSW_Send bin, List<QSW_Send> comList)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < comList.Count; i++)
            {
                if (!ByteUtility.Compares(bin.body, comList[i].body))
                {
                    Console.WriteLine("SSW Package ERROR: [{0}]", i);
                }
            }
            Console.ResetColor();
            Console.WriteLine("Analyzed SSW Packages Done! Totle count : {0}\n", comList.Count);
        }

        /// <summary>
        /// 对比USW数据包
        /// </summary>
        private static void AnalyzeUSW(List<USW_Send> binList, List<USW_Send> comList)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < binList.Count; i++)
            {
                var comUSW = comList.FindLast(p => p.packageNum[0] == i);
                if (comUSW != null)
                {
                    if (!ByteUtility.Compares(binList[i].body, comUSW.body))
                    {
                        Console.WriteLine("USW Package ERROR! Package Num : [{0}]", i);
                    }
                }
                else
                {
                    Console.WriteLine("USW Package Lost! Package Num : [{0}]", i);
                }
            }
            Console.ResetColor();
            Console.WriteLine("Analyzed SSW Packages Done! Totle count(encrypted bin file): {0}\n", binList.Count);
        }

        /// <summary>
        /// 对比SSW数据包
        /// </summary>
        private static void AnalyzeSSW(SSW_Send bin, List<SSW_Send> comList)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < comList.Count; i++)
            {
                if (!ByteUtility.Compares(bin.body, comList[i].body))
                {
                    Console.WriteLine("SSW Package ERROR: [{0}]", i);
                }
            }
            Console.ResetColor();
            Console.WriteLine("Analyzed SSW Packages Done! Totle count : {0}\n", comList.Count);
        }
    }
}
