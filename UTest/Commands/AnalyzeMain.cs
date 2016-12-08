using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTest.Commands
{
    public class AnalyzeMain
    {
        /// <summary>
        /// 分析数据时调用此函数
        /// </summary>
        public static void Do()
        {
            try
            {
                AnalyzeSSW();
                AnalyzeUSW();
                AnalyzeQSW();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// 分析QSW响应包
        /// </summary>
        private static void AnalyzeQSW_resp()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 分析SSW的响应包
        /// </summary>
        private static void AnalyzeSSW_resp()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 对比QSW数据包
        /// </summary>
        private static void AnalyzeQSW()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 对比USW数据包
        /// </summary>
        private static void AnalyzeUSW()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 对比SSW数据包
        /// </summary>
        private static void AnalyzeSSW()
        {
            throw new NotImplementedException();
        }
    }
}
