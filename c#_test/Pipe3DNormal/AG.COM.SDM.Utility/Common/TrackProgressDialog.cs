using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility.Common
{
    /// <summary>
    /// 进度条对话框
    /// </summary>
    public partial class TrackProgressDialog : Form, ITrackProgress
    {
        private bool m_IsDisplayTotal = false;
        private bool m_IsContinue = true;
        private bool m_FinishClose = true;
        private bool m_btnShow = true;
        private bool m_IsMoveUp = false;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        /// <summary>
        /// 获取子进度条
        /// </summary>
        public ProgressBarControl SubTarckProgressBar
        {
            get { return progbarSub; }
        }

        /// <summary>
        /// 获取子进度说明文字
        /// </summary>
        public LabelControl SubTarckLabel
        {
            get { return lblSubtip; }
        }

        /// <summary>
        /// 控制按钮的显隐
        /// </summary>
        /// <returns></returns>
        public bool btnShow
        {
            set
            {
                this.m_btnShow = value;
            }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public TrackProgressDialog()
        {
            //初始化界面组件
            InitializeComponent();
        }

        private void TrackProgressDialog_Load(object sender, EventArgs e)
        {           
            if (this.m_btnShow == false)
            {               
                this.panel2.Visible = false;
            }
            if (this.m_IsDisplayTotal == false)
            {
                lblTotaltip.Visible = false;
                progBarTotal.Visible = false;
              
                this.Height = this.Height - 50 + 10;
            }
        }

        #region  ITrackProgress接口
        /// <summary>
        /// 是否继续运行,如果是则返回 true,否则返回 false
        /// </summary>
        public bool IsContinue
        {
            get
            {
                return this.m_IsContinue;
            }
        }

        /// <summary>
        /// 设置是否显示总进度栏 
        /// true 显示 false 不显示
        /// </summary>
        public bool DisplayTotal
        {
            set
            {
                this.m_IsDisplayTotal = value;
            }
        }

        /// <summary>
        /// 标识已完成处理操作
        /// </summary>
        public void SetFinish()
        {
            if (this.m_FinishClose == true)
            {
                this.Close();
            }
            else
            {
                this.btnCancel.Enabled = false;
                //this.btnClose.Enabled = true;
            }
        }

        /// <summary>
        /// 获取或设置完成后是否自动关闭窗体
        /// </summary>
        public bool AutoFinishClose
        {
            get
            {
                return this.m_FinishClose;
            }
            set
            {
                this.m_FinishClose = value;
            }
        }

        /// <summary>
        /// 设置标题栏信息
        /// </summary>
        public string Title
        {
            set
            {
                this.Title = value;
            }
        }

        /// <summary>
        /// 设置总进度条显示范围的最小值
        /// </summary>
        public int TotalMin
        {
            get
            {
                return this.progBarTotal.Properties.Minimum;
            }
            set
            {
                this.progBarTotal.Properties.Minimum = value;
            }
        }

        /// <summary>
        /// 设置总进度条显示范围的最大值
        /// </summary>
        public int TotalMax
        {
            get
            {
                return this.progBarTotal.Properties.Maximum;
            }
            set
            {
                this.progBarTotal.Properties.Maximum = value;
            }
        }

        /// <summary>
        /// 设置总进度条当前显示值
        /// </summary>
        public int TotalValue
        {
            get
            {
                return this.progBarTotal.Position;
            }
            set
            {
                if (value <= progBarTotal.Properties.Maximum && value >= progBarTotal.Properties.Minimum)
                {
                    this.progBarTotal.Position = value;
                }
            }
        }

        /// <summary>
        /// 获取当前总进度显示百分比
        /// </summary>
        public int TotalPercent
        {
            get
            {
                double percent = this.progBarTotal.Position * 100.0 / this.progBarTotal.Properties.Maximum;
                return Convert.ToInt32(Math.Floor(percent));
            }
        }

        /// <summary>
        /// 设置总进度提示信息
        /// </summary>
        public string TotalMessage
        {
            set
            {
                this.lblTotaltip.Text = value;
            }
        }

        /// <summary>
        /// 设置子进度条显示范围的最小值
        /// </summary>
        public int SubMin
        {
            get
            {
                return this.progbarSub.Properties.Minimum;
            }
            set
            {
                this.progbarSub.Properties.Minimum = value;
            }
        }

        /// <summary>
        /// 设置子进度条显示范围的最大值
        /// </summary>
        public int SubMax
        {
            get
            {
                return this.progbarSub.Properties.Maximum;
            }
            set
            {
                this.progbarSub.Properties.Maximum = value;
            }
        }

        /// <summary>
        /// 设置子进度条当前显示值
        /// </summary>
        public int SubValue
        {
            get
            {
                return this.progbarSub.Position;
            }
            set
            {
                if (value <= progbarSub.Properties.Maximum && value >= progbarSub.Properties.Minimum)
                {
                    this.progbarSub.Position = value;
                }
            }
        }

        /// <summary>
        /// 获取子进度显示百分比
        /// </summary>
        public int SubPercent
        {
            get
            {
                double percent = (this.progbarSub.Position * 1.0 / this.progbarSub.Properties.Maximum) * 100;
                return Convert.ToInt32(Math.Floor(percent));
            }
        }

        /// <summary>
        /// 设置子进度提示信息
        /// </summary>
        public string SubMessage
        {
            set
            {
                this.lblSubtip.Text = value;
            }
        }
        #endregion

        public void Show()
        {
            base.Show();

            //只有第一次show才要移动
            //if (m_IsMoveUp == false)
            //{
            //    //为防止进度条把其他Messagebox挡住，因此把进度条提高130像素
            //    this.Location = new Point(this.Location.X, this.Location.Y - 130);

            //    m_IsMoveUp = true;
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.m_IsContinue = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.m_IsContinue = false;
            this.Close();
        }
    }
}