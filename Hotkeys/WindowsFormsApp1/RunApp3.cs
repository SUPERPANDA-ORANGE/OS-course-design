using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;


namespace WindowsFormsApp1
{
    class RunApp3: WindowsFunction
    {
        public string appPath = "";
        public RunApp3(IntPtr hWnd) : base(hWnd) { }

        public override void Execute()
        {
            WinExec(appPath, 1);//参数1：最近的位置和大小，激活状态
        }

        #region WindowAPI
        [DllImport("kernel32.dll")]
        public static extern int WinExec(string appPath, int operType);
        #endregion
    }
}

