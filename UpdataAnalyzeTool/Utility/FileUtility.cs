using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UpdataAnalyzeTool.Utility
{
    public static class FileUtility
    {

        /// <summary>
        /// 从二进制文件中获取数据到byte[],调用此函数是先声明一个byte[], 但不要初始化。
        /// </summary>
        /// <param name="filePathName">文件路径和文件名</param>
        /// <param name="data">引用的数组</param>
        /// <returns>0 正常 1 异常</returns>
        public static int GetDataFromBinFile(string filePathName, out byte[] data)
        {
            try {
                FileStream fs = File.OpenRead(filePathName);
                data = new byte[fs.Length];
                fs.Read(data, 0, (int)fs.Length);
                fs.Close();
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                data = new byte[0];
                return 1;
            }
        }

        public static int GetDataFormTxtFile(string filePathName, out byte[] data)
        {
            try
            {
                StreamReader fs = new StreamReader(filePathName);
                string str;

                List<byte[]> lb = new List<byte[]>();

                while ((str = fs.ReadLine()) != null)
                { 
                    str += " ";  //确保能匹配到最后一个十六进制的字段
                    
                    byte[] tmp = ParseTxtFileLine(str);
                    lb.Add(tmp);
                }

                var newLen = 0;

                foreach (byte[] bb in lb)
                {
                    newLen += bb.Length;
                }

                //Console.WriteLine(newLen);

                data = new byte[newLen];

                var curLen = 0;
                foreach (byte[] bb in lb)
                {
                    bb.CopyTo(data, curLen);
                    curLen += bb.Length;
                }

                fs.Close();
                return 0;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + ex);
                data = new byte[0];
                return 1;
            }
        }

        private static byte[] ParseTxtFileLine(string line)
        {
            Regex reg = new Regex("([a-fA-F0-9]{2})\\s");
            MatchCollection mc = reg.Matches(line);
            byte[] ret = new byte[mc.Count];
            for(int i = 0; i < mc.Count; i ++)
            {
                ret[i] = TxtToByte(mc[i].ToString());
            }
            return ret;
        }

        private static byte TxtToByte(string str)
        {
            byte[] tmp;
            tmp = Encoding.ASCII.GetBytes(str);
            return (byte)(GetHex(tmp[0]) << 4 | GetHex(tmp[1]));
        }

        private static byte GetHex(byte b)
        {
            if (b >= 'a' && b <= 'f')
            {
                return (byte)(b - 'a' + 10);
            }
            if (b >= 'A' && b <= 'F')
            {
                return (byte)(b - 'A' + 10);
            }
            if (b >= '0' && b <= '9')
            {
                return (byte)(b - '0');
            }
            return 0;
        }
    }
}
