using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class WindowsFunctionFactory//添加功能函数
    {
        public WindowsFunction CreateFunction(IntPtr hwnd,IntPtr hotkeyNumber)
        {
            switch ((int)hotkeyNumber)
            {
                case 1:
                    return new MaxMinWindow(hwnd);
                case 2:
                    return new AlwaysTop(hwnd);
                case 3:
                    return new CloseWindow(hwnd);
                case 4:
                    return new Transparency(hwnd);
                case 5:
                    return new RunApp1(hwnd);
                case 6:
                    return new CreateNotepad(hwnd);
                case 7:
                    return new CreateFolder(hwnd);
                case 8:
                    return new Controller(hwnd);
            }
            throw new Exception();
        }
    }
}
