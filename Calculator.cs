﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;


namespace WindowsFormsApp1
{
    class Calculator: WindowsFunction
    {
        public string appPath = "calc.exe";
        public Calculator(IntPtr hWnd) : base(hWnd) { }

        public override void Execute()
        {
            WinExec(appPath, 1);//参数1：最近的位置和大小，激活状态
        }

        #region WindowAPI
        [DllImport("kernel32.dll")]
        public static extern int WinExec(string appPath, int operType);//启动exe
        #endregion
    }
}

