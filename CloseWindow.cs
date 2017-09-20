using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;

namespace WindowsFormsApp1
{
    class CloseWindow : WindowsFunction
    {
        public CloseWindow(IntPtr hWnd) : base(hWnd) { }

        public override void Execute()
        {
            SendMessage(_hWnd, 0x10, 0, 0);//WM_CLOSE = 0x10，传递关闭窗口消息
        }

        #region WindowAPI

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);//用于关闭程序

        #endregion
    }
}
