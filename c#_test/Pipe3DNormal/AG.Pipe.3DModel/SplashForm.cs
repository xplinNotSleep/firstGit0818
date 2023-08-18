using System;
using System.Threading;
using System.Windows.Forms;

namespace AG.Pipe.Analyst3DModel
{
    public delegate void SetTextCallback(string text);
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
            this.FormClosing += SplashForm_FormClosing;
        }
        bool IsFormClosing = true;
        private void SplashForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsFormClosing = false;
        }

        int m = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (m)
            {
                case 0:
                    lblMessage.Text = "正在初始化许可.";
                    break;
                case 1:
                    lblMessage.Text = "正在初始化许可..";
                    break;
                case 2:
                    lblMessage.Text = "正在初始化许可...";
                    break;
            }
            Application.DoEvents();
            m++;
            if (m > 3)
            {
                m = 0;
            }
        }
        private static Thread _SplashThread = null;

        private void SplashForm_Load(object sender, EventArgs e)
        {

            _SplashThread = new Thread(new ThreadStart(delegate ()
            {
                while (IsFormClosing)
                {
                    switch (m)
                    {
                        case 0:
                            SetText("正在初始化许可.");
                            break;
                        case 1:
                            SetText("正在初始化许可..");
                            break;
                        case 2:
                            SetText("正在初始化许可...");
                            break;
                    }
                    Application.DoEvents();
                    m++;
                    if (m > 3)
                    {
                        m = 0;
                    }
                    Thread.Sleep(1000);
                }

            }));

            _SplashThread.IsBackground = true;
            _SplashThread.SetApartmentState(ApartmentState.STA);
            _SplashThread.Start();
        }
        private void SetText(string text)
        {

            if (this.lblMessage.InvokeRequired)
            {
                while (!this.lblMessage.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.lblMessage.Disposing || this.lblMessage.IsDisposed)
                        return;
                }
                SetTextCallback d = new SetTextCallback(SetText);
                this.lblMessage.Invoke(d, new object[] { text });
            }
            else
            {
                this.lblMessage.Text = text;
            }
        }
    }
}
