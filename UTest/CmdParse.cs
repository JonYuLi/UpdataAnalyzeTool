using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdataAnalyzeTool.Repository;
using UpdataAnalyzeTool.Utility;

namespace UTest
{
    public class CmdParse
    {
        private UpdataRepository updRep;
        private UpdataRepository binRep;

        public void OpenBinFile(string file)
        {
            try
            {
                FileStream fs = File.OpenRead(file);
                fs.Close();
                binRep = new UpdataRepository(file);
                Console.WriteLine("OpenBinFile Done!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("OpenBinFile Error!\n" + ex.Message + "\n");
            }
        }

        public void OpenComFile(string file, string fileType)
        {
            try
            {
                if (fileType.ToLower() == "bin")
                {
                    FileStream fs = File.OpenRead(file);
                    fs.Close();
                    updRep = new UpdataRepository(file, 0);
                }
                else if (fileType.ToLower() == "txt")
                {
                    FileStream fs = File.OpenRead(file);
                    fs.Close();
                    updRep = new UpdataRepository(file, 1);
                }
                else
                {
                    Console.WriteLine("FileType is not available\n");
                }
                Console.WriteLine("OpenComFile Done!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("OpenComFile Error!\n" + ex.Message + "\n");
            }
        }

        public void Analyze()
        {
            if (this.binRep == null)
            {
                Console.WriteLine("BinFile has not Selected!\n");
                return;
            }
            if (this.updRep == null)
            {
                Console.WriteLine("ComFile has not Selected!\n");
                return;
            }
            
            this.AnalyzeSSWSendPack();
            this.AnalyzeUSWSendPack();
            this.AnalyzeQSWSendPack();
        }

        public void CompareSSW(int num)
        {
            var count = updRep.sswSendList.Count;
            if (num >= count || binRep.sswSendList.Count < 1) 
            {
                Console.Write("Num is out of range!\n");
                return;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("From Bin File: ");
            Console.ResetColor();
            for (int i = 0; i < binRep.sswSendList[0].body.Length; i++)
            {
                if (updRep.sswSendList[num].body[i] != binRep.sswSendList[0].body[i])
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(binRep.sswSendList[num].body[i].ToString("X2") + " ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(binRep.sswSendList[num].body[i].ToString("X2") + " ");
                }
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("From Com File: ");
            Console.ResetColor();
            for (int i = 0; i < binRep.sswSendList[0].body.Length; i++) 
            {
                if (updRep.sswSendList[num].body[i] != binRep.sswSendList[0].body[i])
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(updRep.sswSendList[num].body[i].ToString("X2") + " ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(updRep.sswSendList[num].body[i].ToString("X2") + " ");
                }
            }
            Console.WriteLine("\n");
        }

        public void CompareUSW(int num)
        {
            if (num >= binRep.uswSendList.Count)
            {
                Console.Write("Num is out of range!\n");
                return;
            }

            var index = updRep.uswSendList.FindLastIndex(p => p.packageNum[0] == num);
            if (index < 0)
            {
                Console.Write("package missed!\n");
                return;
            }

            var top = 1;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("From Bin File:");
            Console.ResetColor();

            for (int i = 0; i < binRep.uswSendList[num].body.Length; i++)
            {
                if (updRep.uswSendList[index].body[i] != binRep.uswSendList[num].body[i])
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(binRep.uswSendList[num].body[i].ToString("X2") + " ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(binRep.uswSendList[num].body[i].ToString("X2") + " ");
                }
                if ((i + 1) % 8 == 0)
                {
                    top++;
                    Console.WriteLine();
                }
            }

            Console.CursorTop -= top;
            Console.CursorLeft += 18;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("From Com File:");
            Console.CursorLeft += 30;
            Console.ResetColor();

            for (int i = 0; i < binRep.uswSendList[num].body.Length; i++)
            {
                if (updRep.uswSendList[index].body[i] != binRep.uswSendList[num].body[i])
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(updRep.uswSendList[index].body[i].ToString("X2") + " ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(updRep.uswSendList[index].body[i].ToString("X2") + " ");
                }
                if ((i + 1) % 8 == 0)
                {
                    Console.WriteLine();
                    Console.CursorLeft += 30;
                }
            }
            Console.WriteLine("\n");
        }

        private void AnalyzeQSWSendPack()
        {
            Console.WriteLine("the count of QSW send packages : " + updRep.qswSendList.Count);
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < updRep.qswSendList.Count; i++)
            {
                if (!ByteUtility.Compares(binRep.qswSendList[0].body, updRep.qswSendList[i].body))
                {
                    Console.WriteLine(string.Format("Error: QSW package is not right! [{0}]", i));
                }
            }
            Console.ResetColor();
            Console.WriteLine("Analyze QSW Send packages Done!\n");
        }

        private void AnalyzeUSWSendPack()
        {
            var ucount = updRep.uswSendList.Count;
            var bcount = binRep.uswSendList.Count;
            Console.WriteLine("the count of USW send packages in ComData: " + ucount);
            Console.WriteLine("the count of USW send packages in BinData: " + bcount);
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < bcount; i++)
            {
                var index = updRep.uswSendList.FindLastIndex(p => p.packageNum[0] == i);
                if (index < 0)
                {
                    Console.WriteLine(string.Format("Error: USW package is missing! [{0}]", i));
                    continue;
                }
                if (!ByteUtility.Compares(binRep.uswSendList[i].body, updRep.uswSendList[index].body))
                {
                    Console.WriteLine(string.Format("Error: USW package is not right! [{0}]", i));
                }
            }
            Console.ResetColor();
            Console.WriteLine("Analyze USW Send packages Done!\n");
        }

        private void AnalyzeSSWSendPack()
        {
            Console.WriteLine("the count of SSW send packages : " + updRep.sswSendList.Count);
            for (int i = 0; i < updRep.sswSendList.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (!ByteUtility.Compares(binRep.sswSendList[0].body, updRep.sswSendList[i].body))
                {
                    Console.WriteLine(string.Format("Error: SSW package is not right! [{0}]", i));
                }
                Console.ResetColor();
            }
            Console.WriteLine("Analyze SSW Send packages Done!\n");
        }
    }
}
