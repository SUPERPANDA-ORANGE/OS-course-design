using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public abstract class WindowsFunction//窗口指针
    {
        protected readonly IntPtr _hWnd;

        protected WindowsFunction(IntPtr hwnd)
        {
            _hWnd = hwnd;
        }
        public abstract void Execute();
            
    }
}
