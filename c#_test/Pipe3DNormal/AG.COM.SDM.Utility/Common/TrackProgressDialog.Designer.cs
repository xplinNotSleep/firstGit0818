namespace AG.COM.SDM.Utility.Common
{
    partial class TrackProgressDialog
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblTotaltip = new DevExpress.XtraEditors.LabelControl();
            this.progBarTotal = new DevExpress.XtraEditors.ProgressBarControl();
            this.lblSubtip = new DevExpress.XtraEditors.LabelControl();
            this.progbarSub = new DevExpress.XtraEditors.ProgressBarControl();
            this.panel2 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progBarTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progbarSub.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel2)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblTotaltip);
            this.panelControl1.Controls.Add(this.progBarTotal);
            this.panelControl1.Controls.Add(this.lblSubtip);
            this.panelControl1.Controls.Add(this.progbarSub);
            this.panelControl1.Controls.Add(this.panel2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(399, 108);
            this.panelControl1.TabIndex = 0;
            // 
            // lblTotaltip
            // 
            this.lblTotaltip.Location = new System.Drawing.Point(11, 58);
            this.lblTotaltip.Name = "lblTotaltip";
            this.lblTotaltip.Size = new System.Drawing.Size(68, 14);
            this.lblTotaltip.TabIndex = 4;
            this.lblTotaltip.Text = "正在处理……";
            // 
            // progBarTotal
            // 
            this.progBarTotal.Location = new System.Drawing.Point(8, 78);
            this.progBarTotal.Name = "progBarTotal";
            this.progBarTotal.Size = new System.Drawing.Size(303, 18);
            this.progBarTotal.TabIndex = 3;
            // 
            // lblSubtip
            // 
            this.lblSubtip.Location = new System.Drawing.Point(11, 7);
            this.lblSubtip.Name = "lblSubtip";
            this.lblSubtip.Size = new System.Drawing.Size(68, 14);
            this.lblSubtip.TabIndex = 2;
            this.lblSubtip.Text = "正在处理……";
            // 
            // progbarSub
            // 
            this.progbarSub.Location = new System.Drawing.Point(8, 26);
            this.progbarSub.Name = "progbarSub";
            this.progbarSub.Size = new System.Drawing.Size(303, 18);
            this.progbarSub.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(317, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(80, 104);
            this.panel2.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(5, 29);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 24);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(5, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 24);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "停止操作";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // TrackProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 108);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrackProgressDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Load += new System.EventHandler(this.TrackProgressDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progBarTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progbarSub.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panel2;
        private DevExpress.XtraEditors.ProgressBarControl progbarSub;
        private DevExpress.XtraEditors.LabelControl lblSubtip;
        private DevExpress.XtraEditors.LabelControl lblTotaltip;
        private DevExpress.XtraEditors.ProgressBarControl progBarTotal;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnClose;


    }
}