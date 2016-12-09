using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTest.menu;

namespace UTest.Commands
{
    public class GetComRepository
    {
        public static void Execute()
        {
            Console.WriteLine("Select the file Type.");
            var fileType = SubMenu.SelectFileType();
            if (fileType != 1 && fileType != 2)
            {
                Console.WriteLine("Select the file Type Error!.");
                return;
            }

            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (Repository.Repository.ComRepository(dialog.FileName, fileType - 1))
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
