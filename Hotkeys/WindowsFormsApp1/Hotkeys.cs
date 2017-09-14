using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Hotkeys
    {
        public enum fsModifiers
        {
            Alt = 0x0001,
            Control = 0x0002,
            Shift = 0x0004,
            Window = 0x0008,
        }

        
        private IntPtr _hWnd;

        public Hotkeys(IntPtr hWnd)
        {
            this._hWnd = hWnd;
        }
       
        public void RegisterHotKeys()//注册热键
        {
            RegisterHotKey(_hWnd, 1, (uint)fsModifiers.Control, (uint)Keys.M);//放大缩小
            RegisterHotKey(_hWnd, 2, (uint)fsModifiers.Control, (uint)Keys.T);//置顶
            RegisterHotKey(_hWnd, 3, (uint)fsModifiers.Control, (uint)Keys.A);//关闭程序
            RegisterHotKey(_hWnd, 4, (uint)fsModifiers.Control, (uint)Keys.O);//半透明
            RegisterHotKey(_hWnd, 5, 0, 112);//F1 微信
            RegisterHotKey(_hWnd, 6, (uint)fsModifiers.Control, (uint)Keys.N);//新建文本文档
            RegisterHotKey(_hWnd, 7, (uint)fsModifiers.Control, (uint)Keys.F);//新建文件夹
            RegisterHotKey(_hWnd, 8, (uint)fsModifiers.Control, (uint)Keys.C);//打开控制面板
        }

        public void UnregisterHotKey()//注销热键
        {
            UnregisterHotKey(_hWnd, 1);
            UnregisterHotKey(_hWnd, 2);
            UnregisterHotKey(_hWnd, 3);
            UnregisterHotKey(_hWnd, 4);
            UnregisterHotKey(_hWnd, 5);
            UnregisterHotKey(_hWnd, 6);
            UnregisterHotKey(_hWnd, 7);
            UnregisterHotKey(_hWnd, 8);


        }

        #region WindowsAPI

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        #endregion

    }
}
