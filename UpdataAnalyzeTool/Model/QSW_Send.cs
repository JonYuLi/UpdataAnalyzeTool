using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdataAnalyzeTool.Domain;
using UpdataAnalyzeTool.Utility;

namespace UpdataAnalyzeTool.Model
{
    public class QSW_Send : SW
    {
        public const int m_length = 8;
        public byte[] packageCheck { get; set; }

        public QSW_Send(byte[] source)
        {
            if (source.Length >= m_length)
            {
                this.source = source;
                this.parse();
            }
        }

        public QSW_Send()
        {
            this.source = new byte[] { 0x51, 0x53, 0x57, 0x3A, 0x00, 0x45, 0x4E, 0x44 };
            this.parse();
        }

        private void parse()
        {
            this.start = ByteUtility.GetSubByte(this.source, 0, 4);
            this.body = ByteUtility.GetSubByte(this.source, 4, 1);
            this.end = ByteUtility.GetSubByte(this.source, 5, 3);
            this.parseBody();
        }

        private void parseBody()
        {
            packageCheck = ByteUtility.GetSubByte(this.body, 0, 1);
            this.CheckValid();
        }

        private void CheckValid()
        {
            if (!ByteUtility.Compares(start, System.Text.Encoding.Default.GetBytes("QSW:")))
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
