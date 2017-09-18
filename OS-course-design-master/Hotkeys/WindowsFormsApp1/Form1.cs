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

        [DllImport("user32.dll")]//隐藏textbox里的的光标
        static extern bool HideCaret(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);//自动触发按键

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
            for (int i = 1; i < 9; i++)
            {
                hotkey.RegisterHotKeys(i);
            }
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //hotkey.UnregisterHotKey();
            // this.WindowState = FormWindowState.Minimized;
            keybd_event((byte)17, 0, 0x0002, 0);
        }





        protected override void WndProc(ref Message keyPressed)
        {
            if (keyPressed.Msg == 0x0312)//按下按钮
            {
                IntPtr selectedWindow = GetForegroundWindow();//获取选中的窗口
                IntPtr hotkeyID = keyPressed.WParam;//获取id
                WindowsFunction function = _factory.CreateFunction(selectedWindow, hotkeyID);//在factory找对应的case
                function.Execute();//运行对应的id
            }
            base.WndProc(ref keyPressed);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //还原窗体显示    
            WindowState = FormWindowState.Normal;
            //激活窗体并给予它焦点
            this.Activate();
            //任务栏区显示图标
            this.ShowInTaskbar = true;
            //托盘区图标隐藏
            notifyIcon1.Visible = false;
            Form1_Load(sender, e);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //判断是否选择的是最小化按钮
            if (WindowState == FormWindowState.Minimized)
            {
                //隐藏任务栏区图标
                this.ShowInTaskbar = false;
                //图标显示在托盘区
                notifyIcon1.Visible = true;
                Form1_Load(sender, e);
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            //{
            //    // 关闭所有的线程
            //    this.Dispose();
            //    this.Close();
            //}
            //else
            //{
            //    e.Cancel = true;
            //}
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {

        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
        }

        private void idetifyKey(object sender, KeyEventArgs e)//按键识别
        {
            if (e.Shift)
            {
                (sender as TextBox).Text = "shift + ";

                if (e.Shift && e.KeyValue > 64)
                {
                    (sender as TextBox).Text = "shift + " + Convert.ToChar(e.KeyValue).ToString();
                    (sender as TextBox).MaxLength = 9;
                    Hotkeys.comandtemp = 0x0004;
                    if ((sender as TextBox).Text.Length == 9) Hotkeys.setkeytemp = e.KeyValue;
                }
            }
            else if (e.Control)
            {
                (sender as TextBox).Text = "ctrl + ";
                if (e.Control && e.KeyValue > 64)
                {
                    (sender as TextBox).Text = "ctrl + " + Convert.ToChar(e.KeyValue).ToString();
                    (sender as TextBox).MaxLength = 8;
                    Hotkeys.comandtemp = 0x0002;
                    if ((sender as TextBox).Text.Length == 8) Hotkeys.setkeytemp = e.KeyValue;
                }
            }
            else if (e.Alt)
            {
                (sender as TextBox).Text = "alt + ";
                if (e.Alt && e.KeyValue > 64)
                {
                    (sender as TextBox).Text = "alt + " + Convert.ToChar(e.KeyValue).ToString();
                    (sender as TextBox).MaxLength = 7;
                    Hotkeys.comandtemp = 0x0001;
                    if ((sender as TextBox).Text.Length == 7) Hotkeys.setkeytemp = e.KeyValue;
                }
            }
            else
            {
                (sender as TextBox).Text = Convert.ToChar(e.KeyValue).ToString();
                (sender as TextBox).MaxLength = 1;
                Hotkeys.comandtemp = 0;
                Hotkeys.setkeytemp = e.KeyValue;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            idetifyKey(textBox1, e);//按键识别
            Hotkeys.comand1 = Hotkeys.comandtemp;//将识别到的按键临时值赋给comand1
            Hotkeys.setkey1 = Hotkeys.setkeytemp;//将识别到的按键临时值赋给setkey1
            hotkey.UnregisterHotKey(1);//首先注销之前的热键
            hotkey.RegisterHotKeys(1);//注册新的热键
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            idetifyKey(textBox2, e);
            Hotkeys.comand2 = Hotkeys.comandtemp;
            Hotkeys.setkey2 = Hotkeys.setkeytemp;
            hotkey.UnregisterHotKey(2);
            hotkey.RegisterHotKeys(2);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            idetifyKey(textBox3, e);
            Hotkeys.comand3 = Hotkeys.comandtemp;
            Hotkeys.setkey3 = Hotkeys.setkeytemp;
            hotkey.UnregisterHotKey(3);
            hotkey.RegisterHotKeys(3);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            HideCaret(textBox3.Handle);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            HideCaret(textBox4.Handle);
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            idetifyKey(textBox4, e);
            Hotkeys.comand4 = Hotkeys.comandtemp;
            Hotkeys.setkey4 = Hotkeys.setkeytemp;
            hotkey.UnregisterHotKey(4);
            hotkey.RegisterHotKeys(4);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            HideCaret(textBox5.Handle);
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            idetifyKey(textBox5, e);
            Hotkeys.comand5 = Hotkeys.comandtemp;
            Hotkeys.setkey5 = Hotkeys.setkeytemp;
            hotkey.UnregisterHotKey(5);
            hotkey.RegisterHotKeys(5);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            HideCaret(textBox6.Handle);
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            idetifyKey(textBox6, e);
            Hotkeys.comand6 = Hotkeys.comandtemp;
            Hotkeys.setkey6 = Hotkeys.setkeytemp;
            hotkey.UnregisterHotKey(6);
            hotkey.RegisterHotKeys(6);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            HideCaret(textBox7.Handle);
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            idetifyKey(textBox7, e);
            Hotkeys.comand7 = Hotkeys.comandtemp;
            Hotkeys.setkey7 = Hotkeys.setkeytemp;
            hotkey.UnregisterHotKey(7);
            hotkey.RegisterHotKeys(7);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            HideCaret(textBox8.Handle);
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            idetifyKey(textBox8, e);
            Hotkeys.comand8 = Hotkeys.comandtemp;
            Hotkeys.setkey8 = Hotkeys.setkeytemp;
            hotkey.UnregisterHotKey(8);
            hotkey.RegisterHotKeys(8);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(textBox1.Handle);
        }

        private void textBox2_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(textBox2.Handle);
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
        
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Transparency.isTransparent = (trackBar1.Value * 24 + 15);
            if(trackBar1.Value==10) Transparency.isTransparent=125;
            if(Hotkeys.comand4 == 0x0004)//shift
            {
                keybd_event((byte)16, 0, 0, 0);//自动按键
                keybd_event((byte)Hotkeys.setkey4, 0, 0, 0);
                keybd_event((byte)16, 0, 2, 0);//参数2放开按键
                keybd_event((byte)Hotkeys.setkey4, 0, 2, 0);
            }
            else if (Hotkeys.comand4 == 0x0002)//control
            {
                keybd_event((byte)17, 0, 0, 0);
                keybd_event((byte)Hotkeys.setkey4, 0, 0, 0);
                keybd_event((byte)17, 0, 2, 0);
                keybd_event((byte)Hotkeys.setkey4, 0, 2, 0);
            }
            else if (Hotkeys.comand4 == 0x0001)//alt
            {
                keybd_event((byte)18, 0, 0, 0);
                
                keybd_event((byte)Hotkeys.setkey4, 0, 0, 0);
                keybd_event((byte)18, 0, 2, 0);
                keybd_event((byte)Hotkeys.setkey4, 0, 2, 0);
            }
            else if (Hotkeys.comand4 == 0)
            {
                keybd_event((byte)Hotkeys.setkey4, 0, 0, 0);
                keybd_event((byte)Hotkeys.setkey4, 0, 2, 0);
            }


        }

        private void textBox3_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(textBox3.Handle);
        }

        private void textBox4_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(textBox4.Handle);
        }

        private void textBox5_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(textBox5.Handle);
        }

        private void textBox6_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(textBox6.Handle);
        }

        private void textBox7_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(textBox7.Handle);
        }

        private void textBox8_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(textBox8.Handle);
        }

        
    }
}
