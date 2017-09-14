using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;

namespace WindowsFormsApp1
{
    class AlwaysTop : WindowsFunction
    {
        public static bool isTop= false;

        public AlwaysTop(IntPtr hWnd) : base(hWnd) { }
        
        public override void Execute()
        {
            switch (isTop)//启动和取消置顶
            {
                case true:
                    isTop = false;
                    break;
                case false:
                    isTop = true;
                    break;
            }

            SetWindowPos(_hWnd, (isTop)?-1:-2, 0, 0, 0, 0, 1 | 2);
        }
        #region WindowAPI

        [DllImport("user32.dll")]
        //下面的hWndInsertAfter = -1 时置顶，hWndInsertAfter = -2时取消置顶
        private static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);
      
        #endregion
    }
}
