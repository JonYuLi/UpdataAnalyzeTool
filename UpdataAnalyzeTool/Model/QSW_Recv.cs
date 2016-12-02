using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdataAnalyzeTool.Domain;
using UpdataAnalyzeTool.Utility;

namespace UpdataAnalyzeTool.Model
{
    public class QSW_Recv : SW
    {
        public const int m_length = 32;
        public byte[] updataStatus { get; set; }
        public byte[] packageFlag { get; set; }
        public byte[] currentVersion { get; set; }
        public byte[] newVersion { get; set; }
        public byte[] updataResult { get; set; }
        public byte[] bootVersion { get; set; }
        public byte[] packageCheck { get; set; }

        public QSW_Recv(byte[] source)
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
            this.body = ByteUtility.GetSubByte(this.source, 4, 25);
            this.end = ByteUtility.GetSubByte(this.source, 29, 3);
            this.parseBody();
        }

        private void parseBody()
        {
            updataStatus = ByteUtility.GetSubByte(this.body, 0, 1);
            packageFlag = ByteUtility.GetSubByte(this.body, 1, 16);
            currentVersion = ByteUtility.GetSubByte(this.body, 17, 2);
            newVersion = ByteUtility.GetSubByte(this.body, 19, 2);
            updataResult = ByteUtility.GetSubByte(this.body, 21, 1);
            bootVersion = ByteUtility.GetSubByte(this.body, 22, 2);
            packageCheck = ByteUtility.GetSubByte(this.body, 24, 1);
            this.CheckValid();
        }

        private void CheckValid()
        {
            if (!ByteUtility.Compares(start, System.Text.Encoding.Default.GetBytes("QSW:")))
            {
                return ;
            }
            if (!ByteUtility.Compares(end, System.Text.Encoding.Default.GetBytes("END")))
            {
                return;
            }
            isValid = true;
        }
    }
}
