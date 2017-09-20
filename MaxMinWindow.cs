using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;

namespace WindowsFormsApp1
{
    class MaxMinWindow: WindowsFunction
    {
        public enum nCmdShow
        {
            NORMAL = 1,
            MIN = 2,
            MAX = 3
        }

        private struct WindowPlacement
        {
            public int length;
            public int flags;
            public int showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rectangle rcNormalPosition;
        }
        public MaxMinWindow(IntPtr hWnd):base(hWnd){}

        public override void Execute()
        {
            WindowPlacement wp = new WindowPlacement();
            GetWindowPlacement(_hWnd, ref wp);
            nCmdShow windowState = nCmdShow.NORMAL;
            switch(wp.showCmd)//改变状态
            {
                case (int)nCmdShow.MAX:
                    windowState = nCmdShow.MIN;
                    break;
                case (int)nCmdShow.NORMAL:
                    windowState = nCmdShow.MAX;
                    break;
                case (int)nCmdShow.MIN:
                    windowState = nCmdShow.NORMAL;
                    break;
            }
            ShowWindowAsync(_hWnd, (int)windowState);//当前状态传到这里
        }
        #region WindowAPI

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);//该函数设置由不同线程产生的窗口的显示状态
        [DllImport("user32.dll")]
        private static extern bool GetWindowPlacement(IntPtr hWnd, ref WindowPlacement lpwndpl); //返回指定窗口的显示状态以及被恢复的、最大化的和最小化的窗口位置

        #endregion
    }
}
