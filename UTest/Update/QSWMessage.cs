using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTest
{
    public class QSWMessage
    {
        public static string UpdateStatusMsg(UpdateStatus us)
        {
            switch (us)
            {
                case UpdateStatus.recvFile:
                    return "应用程序接收升级文件中";
                case UpdateStatus.revcFileDone:
                    return "应用程序完成接收升级文件";
                case UpdateStatus.updateFile:
                    return "更新文件中";
                case UpdateStatus.updateFileDone:
                    return "更新完成 ";
                case UpdateStatus.softwareRun:
                    return "应用程序正常运行中 ";
                case UpdateStatus.recover:
                    return "恢复软件中";
                case UpdateStatus.recoverDone:
                    return "完成恢复软件 ";

            }
            return "Undefine Status";
        }

        public static string UpdateResultMsg(UpdateResult ur)
        {
            switch (ur)
            {
                case UpdateResult.updateDone:
                    return "升级完成 ";
                case UpdateResult.TimeOut:
                    return "升级超时";
                case UpdateResult.FLASHError:
                    return "FLASH 读写失败";
                case UpdateResult.CheckError:
                    return "校验错误";
                case UpdateResult.LengthError:
                    return "长度错误";
                case UpdateResult.waitingPackage:
                    return "等待升级包 ";
                case UpdateResult.SoftWareRunError:
                    return "应用软件无法正常运行";
            }
            return "Undefine Status";
        }

        public static string CheckPackagesFlag(byte[] packages, int total)
        {
            
            if (total < 1 && packages.Length < (total - 1 / 8))
                return "";

            string ret = "";
            for (int i = 0; i < total; i++)
            {
                if ((packages[i / 8] & (1 << 7 - (i % 8))) == 0)
                {
                    ret += "0x" + i.ToString("X2") + " ";
                }
            }
            if (ret != "")
                ret = "Lost Packages : " + ret;
            return ret;
        }
    }
}
