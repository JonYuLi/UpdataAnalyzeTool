using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UTest.Commands
{
    public class GetBinRepository
    {
        public static void Execute()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (Repository.Repository.BinRepository(dialog.FileName))
                {
                    Console.WriteLine("Get File Data Done!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Get File Data Error!");
                    Console.ResetColor();
                }
            }
        }
    }
}
