using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string sClassName, String sAppName);//参数指向一个窗口

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();//获取当前窗口

        private IntPtr thisWindow;
        private Hotkeys hotkey;
        private WindowsFunctionFactory _factory = new WindowsFunctionFactory();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            thisWindow = FindWindow(null, "Form1");//通过名字查找
            hotkey = new Hotkeys(thisWindow);
            hotkey.RegisterHotKeys();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            hotkey.UnregisterHotKey();
        }

        protected override void WndProc(ref Message keyPressed)
        {
            if (keyPressed.Msg == 0x0312)//按下按钮
            {
                IntPtr selectedWindow = GetForegroundWindow();
                IntPtr hotkeyID = keyPressed.WParam;
                WindowsFunction function = _factory.CreateFunction(selectedWindow, hotkeyID);
                function.Execute();
                label1.Text = AlwaysTop.isTop.ToString();
            }
            base.WndProc(ref keyPressed);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
