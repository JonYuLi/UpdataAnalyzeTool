using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdataAnalyzeTool.Domain;
using UpdataAnalyzeTool.Utility;

namespace UpdataAnalyzeTool.Model
{
    public class SSW_Send : SW
    {
        public const int m_length = 25;
        public byte[] fileType { get; set; }
        public byte[] fileLength { get; set; }
        public byte[] check1 { get; set; }
        public byte[] check2 { get; set; }
        public byte[] check3 { get; set; }
        public byte[] nPackageNum { get; set; }
        public byte[] newVersion { get; set; }
        public byte[] reserve { get; set; }
        public byte[] packageCheck { get; set; }

        public SSW_Send(byte[] source)
        {
            if (source.Length >= m_length)
            {
                this.source = source;
                this.parse();
            }
        }

        private void parse()
        {
            this.start = ByteUtility.GetSubByte(this.source, 0, 4);
            this.body = ByteUtility.GetSubByte(this.source, 4, 18);
            this.end = ByteUtility.GetSubByte(this.source, 22, 3);
            this.parseBody();
        }

        private void parseBody()
        {
            fileType = ByteUtility.GetSubByte(body, 0, 1);
            fileLength = ByteUtility.GetSubByte(body, 1, 2);
            check1 = ByteUtility.GetSubByte(body, 3, 2);
            check2 = ByteUtility.GetSubByte(body, 5, 2);
            check3 = ByteUtility.GetSubByte(body, 7, 4);
            nPackageNum = ByteUtility.GetSubByte(body, 11, 2);
            newVersion = ByteUtility.GetSubByte(body, 13, 2);
            reserve = ByteUtility.GetSubByte(body, 15, 2);
            packageCheck = ByteUtility.GetSubByte(body, 17, 1);
            this.CheckValid();
        }

        public void MakeReverse()
        {
            fileLength = ByteUtility.MakeReverse(fileLength);
            check1 = ByteUtility.MakeReverse(check1);
            check2 = ByteUtility.MakeReverse(check2);
            check3 = ByteUtility.MakeReverse(check3);
            nPackageNum = ByteUtility.MakeReverse(nPackageNum);
            newVersion = ByteUtility.MakeReverse(newVersion);
            reserve = ByteUtility.MakeReverse(reserve);
        }

        private void CheckValid()
        {
            if (!ByteUtility.Compares(start, System.Text.Encoding.Default.GetBytes("SSW:")))
            {
                return;
            }
            if (!ByteUtility.Compares(end, System.Text.Encoding.Default.GetBytes("END")))
            {
                return;
            }
            isValid = true;
        }
    }
}
