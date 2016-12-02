using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdataAnalyzeTool.Domain;
using UpdataAnalyzeTool.Utility;

namespace UpdataAnalyzeTool.Model
{
    public class USW_Recv : SW
    {
        public const int m_length = 10;
        public byte[] packageNum { get; set; }
        public byte[] response { get; set; }
        public byte[] packageCheck { get; set; }

        public USW_Recv(byte[] source)
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
            this.body = ByteUtility.GetSubByte(this.source, 4, 3);
            this.end = ByteUtility.GetSubByte(this.source, 7, 3);
            this.parseBody();
        }

        private void parseBody()
        {
            packageNum = ByteUtility.GetSubByte(this.body, 0, 1);
            response = ByteUtility.GetSubByte(this.body, 1, 1);
            packageCheck = ByteUtility.GetSubByte(this.body, 2, 1);
            this.CheckValid();
        }

        private void CheckValid()
        {
            if (!ByteUtility.Compares(start, System.Text.Encoding.Default.GetBytes("USW:")))
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
