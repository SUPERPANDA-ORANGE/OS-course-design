using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace WindowsFormsApp1
{
    class CreateFolder : WindowsFunction
    {
        public CreateFolder(IntPtr hWnd) : base(hWnd) { }
        public override void Execute()
        {
            string path = "C:\\Users\\Administrator\\Desktop";
            string filename = "";
            filename = path + "\\temp";
            Directory.CreateDirectory(filename);
            MessageBox.Show("ok");
        }

        #region WindowAPI
        [DllImport("kernel32.dll")]
        public static extern int WinExec(string appPath, int operType);
        #endregion
    }
}
