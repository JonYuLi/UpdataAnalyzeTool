using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdataAnalyzeTool.Repository;

namespace UTest.Repository
{
    public class Repository
    {
        private static UpdataRepository comRepo;
        private static UpdataRepository binRepo;

        public static bool ComRepository(string file, int fileType)
        {
            try
            {
                if (comRepo != null)
                {
                    comRepo = null;
                }
                comRepo = new UpdataRepository(file, fileType);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool BinRepository(string file)
        {
            try
            {
                if (binRepo != null)
                {
                    binRepo = null;
                }
                binRepo = new UpdataRepository(file);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static UpdataRepository ComRepository()
        {
            return comRepo;
        }

        public static UpdataRepository BinRepository()
        {
            return binRepo;
        }
    }
}
