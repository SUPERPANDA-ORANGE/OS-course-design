using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;

namespace WindowsFormsApp1
{
    class Transparency : WindowsFunction
    {
        public static int isTransparent = 255;
        public Transparency(IntPtr hWnd) : base(hWnd){}

        public override void Execute()
        {
            //启动和关闭透明效果。125为半透明，255为不透明
            if (isTransparent == 255) isTransparent = 125;
            else if(isTransparent == 125) isTransparent = 255;

            GetWindowLong(_hWnd, -20);//-20是设定一个新的扩展风格
            SetWindowLong(_hWnd, -20, 0x10 | 0x80000);//0x10可以选中窗口，0x20穿透窗口
            SetLayeredWindowAttributes(_hWnd, 0, (byte)isTransparent, 2);
        }

        #region WindowAPI
        [DllImport("user32.dll")]
        private static extern uint SetWindowLong(IntPtr hwnd, int nIndex, uint dwNewLong); //通过SetWindowLong设置窗口的属性

        [DllImport("user32.dll")]
        private static extern uint GetWindowLong(IntPtr hwnd, int nIndex);//通过GetWindowLong获得窗口的属性

        [DllImport("user32.dll")]
        public extern static bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);//bAlpha 设置透明度
        #endregion
    }
}
