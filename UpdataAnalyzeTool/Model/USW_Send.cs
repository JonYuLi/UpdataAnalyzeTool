﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdataAnalyzeTool.Domain;
using UpdataAnalyzeTool.Utility;

namespace UpdataAnalyzeTool.Model
{
    public class USW_Send : SW
    {
        public const int m_length = 523;
        public byte[] packageNum { get; set; }
        public byte[] dataLength { get; set; }
        public byte[] data { get; set; }
        public byte[] packageCheck { get; set; }

        public USW_Send(byte[] source)
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
            this.body = ByteUtility.GetSubByte(this.source, 4, 516);
            this.end = ByteUtility.GetSubByte(this.source, 520, 3);
            this.parseBody();
        }

        private void parseBody()
        {
            packageNum = ByteUtility.GetSubByte(this.body, 0, 1);
            dataLength = ByteUtility.GetSubByte(this.body, 1, 2);
            data = ByteUtility.GetSubByte(this.body, 3, 512);
            packageCheck = ByteUtility.GetSubByte(this.body, 515, 1);
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
