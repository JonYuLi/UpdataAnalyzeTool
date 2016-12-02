using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdataAnalyzeTool.Utility;

namespace UpdataAnalyzeTool.Domain
{
    public class SW
    {
        public byte[] start { get; set; }
        public byte[] end { get; set; }
        public byte[] source { get; set; }
        public byte[] body { get; set; }
        public bool isValid { get; set; }
    }
}
