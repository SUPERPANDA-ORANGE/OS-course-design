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
        public static int setkeytemp;
        public static int setkey1;
        public static int setkey2;
        public static int setkey3;
        public static int setkey4;
        public static int setkey5;
        public static int setkey6;
        public static int setkey7;
        public static int setkey8;
        public static int setkey9;
        public static int setkey10;

        public static int comandtemp;
        public static int comand1;
        public static int comand2;
        public static int comand3;
        public static int comand4;
        public static int comand5;
        public static int comand6;
        public static int comand7;
        public static int comand8;
        public static int comand9;
        public static int comand10;

        private IntPtr _hWnd;

        public Hotkeys(IntPtr hWnd)
        {
            this._hWnd = hWnd;
        }

        public void RegisterHotKeys(int id)//注册热键
        {
            switch (id)
            {
                case 1:
                    RegisterHotKey(_hWnd, 1, (uint)comand1, (uint)setkey1);//放大缩小还原
                    break;
                case 2:
                    RegisterHotKey(_hWnd, 2, (uint)comand2, (uint)setkey2);//置顶
                    break;
                case 3:
                    RegisterHotKey(_hWnd, 3, (uint)comand3, (uint)setkey3);//关闭程序
                    break;
                case 4:
                    RegisterHotKey(_hWnd, 4, (uint)comand4, (uint)setkey4);//半透明
                    break;
                case 5:
                    RegisterHotKey(_hWnd, 5, (uint)comand5, (uint)setkey5);//微信
                    break;
                case 6:
                    RegisterHotKey(_hWnd, 6, (uint)comand6, (uint)setkey6);//新建文本文档
                    break;
                case 7:
                    RegisterHotKey(_hWnd, 7, (uint)comand7, (uint)setkey7);//新建文件夹
                    break;
                case 8:
                    RegisterHotKey(_hWnd, 8, (uint)comand8, (uint)setkey8);//打开控制面板   
                    break;
                case 9:
                    RegisterHotKey(_hWnd, 9, (uint)comand9, (uint)setkey9);//打开控制面板   
                    break;
                case 10:
                    RegisterHotKey(_hWnd, 10, (uint)comand10, (uint)setkey10);//打开控制面板   
                    break;
            }
        }

        public void UnregisterHotKey(int id)//注销热键
        {
            switch (id)
            {
                case 1:
                    UnregisterHotKey(_hWnd, 1);
                    break;
                case 2:
                     UnregisterHotKey(_hWnd, 2);
                    break;
                case 3:
                    UnregisterHotKey(_hWnd, 3);
                    break;
                case 4:
                    UnregisterHotKey(_hWnd, 4);
                    break;
                case 5:
                    UnregisterHotKey(_hWnd, 5);
                    break;
                case 6:
                    UnregisterHotKey(_hWnd, 6);
                    break;
                case 7:
                    UnregisterHotKey(_hWnd, 7);
                    break;
                case 8:
                    UnregisterHotKey(_hWnd, 8);
                    break;
                case 9:
                    UnregisterHotKey(_hWnd, 9);
                    break;
                case 10:
                    UnregisterHotKey(_hWnd, 10);
                    break;
            }

        }

        #region WindowsAPI

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);//注册热键

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);//注销热键
        #endregion

    }
}
