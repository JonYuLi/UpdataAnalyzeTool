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

                AnalyzeSSW_resp(comRepo.sswRecvList);

                AnalyzeUSW_resp(comRepo.uswRecvList, binRepo.uswSendList.Count);

                if (comRepo.qswRecvList.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("QSW Response Packages Not Found!");
                    Console.ResetColor();
                }
                else
                {
                    AnalyzeQSW_resp(comRepo.qswRecvList[comRepo.qswRecvList.Count - 1], binRepo.uswSendList.Count);
                }
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
        private static void AnalyzeUSW_resp(List<USW_Recv> comList, int packageNum)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < packageNum; i++)
            {
                USW_Recv usw = comList.FindLast(p => p.packageNum[0] == i);
                if (usw == null)
                {
                    Console.WriteLine("USW Response Package Lost[{0}]", i);
                }
                else
                {
                    if (usw.response[0] != 0)
                    {
                        Console.WriteLine("USW Response Package ERROR Code [{1}], in [{0}]", i, usw.response[0]);
                    }
                }
            }
            Console.ResetColor();
            Console.WriteLine("Analyzed USW Response Packages Done! Total count : {0}\n", comList.Count);
        }

        /// <summary>
        /// 分析QSW响应包
        /// </summary>
        private static void AnalyzeQSW_resp(QSW_Recv qsw, int packagesNum)
        {
            Console.WriteLine("QSW Response in last package:");
            if (qsw.updataStatus[0] != 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Status: " + qsw.updataStatus[0].ToString("X2") + " " + 
                    QSWMessage.UpdateStatusMsg((UpdateStatus)qsw.updataStatus[0]));
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Status: " + qsw.updataStatus[0].ToString("X2") + " " +
                    QSWMessage.UpdateStatusMsg((UpdateStatus)qsw.updataStatus[0]));
            }
            if (qsw.updataResult[0] != 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Result: " + qsw.updataResult[0].ToString("X2") + " " +
                    QSWMessage.UpdateResultMsg((UpdateResult)qsw.updataResult[0]));
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Result: " + qsw.updataResult[0].ToString("X2") + " " +
                    QSWMessage.UpdateResultMsg((UpdateResult)qsw.updataResult[0]));
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(QSWMessage.CheckPackagesFlag(qsw.packageFlag, packagesNum));
            Console.ResetColor();
        }

        /// <summary>
        /// 分析SSW的响应包
        /// </summary>
        private static void AnalyzeSSW_resp(List<SSW_Recv> comList)
        {
            Console.WriteLine("SSW response packages. Total count : {0}", comList.Count);
            if (comList.Count < 1)
            {
                return;
            }
            Console.Write("The Response Code in last package is ");
            if (comList[comList.Count - 1].response[0] == 0)
            {
                Console.WriteLine(comList[comList.Count - 1].response[0].ToString("X2"));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(comList[comList.Count - 1].response[0].ToString("X2"));
                Console.ResetColor();
            }
            Console.WriteLine();
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
                    Console.WriteLine("QSW Package ERROR: [{0}]", i);
                }
            }
            Console.ResetColor();
            Console.WriteLine("Analyzed QSW Packages Done! Total count : {0}\n", comList.Count);
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
            Console.WriteLine("Analyzed USW Packages Done! Total count(encrypted bin file): {0}", binList.Count);
            Console.WriteLine("Total count(com data file): {0}\n", comList.Count);
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
