namespace AG.COM.SDM.Catalog.UI
{
    partial class DataConnectionControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtInstance = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btConnect = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboVersions = new System.Windows.Forms.ComboBox();
            this.pnlHisInfo = new System.Windows.Forms.Panel();
            this.dtHisTimestamp = new System.Windows.Forms.DateTimePicker();
            this.optHisTime = new System.Windows.Forms.RadioButton();
            this.optHisMarker = new System.Windows.Forms.RadioButton();
            this.cboHisMarkers = new System.Windows.Forms.ComboBox();
            this.optHistory = new System.Windows.Forms.RadioButton();
            this.optTrans = new System.Windows.Forms.RadioButton();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnlHisInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(11, 31);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(41, 12);
            label1.TabIndex = 0;
            label1.Text = "服务器";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(11, 63);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(29, 12);
            label2.TabIndex = 1;
            label2.Text = "实例";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(11, 95);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(41, 12);
            label3.TabIndex = 2;
            label3.Text = "数据库";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(11, 127);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(41, 12);
            label5.TabIndex = 2;
            label5.Text = "用户名";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(11, 159);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(29, 12);
            label6.TabIndex = 2;
            label6.Text = "密码";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(64, 27);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(100, 21);
            this.txtServer.TabIndex = 4;
            this.txtServer.TextChanged += new System.EventHandler(this.txtServer_TextChanged);      
            // 
            // txtInstance
            // 
            this.txtInstance.Location = new System.Drawing.Point(64, 59);
            this.txtInstance.Name = "txtInstance";
            this.txtInstance.Size = new System.Drawing.Size(100, 21);
            this.txtInstance.TabIndex = 5;
            this.txtInstance.Text = "5151";      
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(64, 91);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(100, 21);
            this.txtDatabase.TabIndex = 5;     
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(64, 123);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(100, 21);
            this.txtUser.TabIndex = 5;
            this.txtUser.Text = "sde";        
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(64, 155);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(100, 21);
            this.txtPassword.TabIndex = 5;        
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btConnect);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(label1);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(label2);
            this.groupBox1.Controls.Add(this.txtDatabase);
            this.groupBox1.Controls.Add(label3);
            this.groupBox1.Controls.Add(this.txtInstance);
            this.groupBox1.Controls.Add(label5);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(label6);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 219);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(81, 186);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(83, 22);
            this.btConnect.TabIndex = 6;
            this.btConnect.Text = "测试连接";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboVersions);
            this.groupBox2.Controls.Add(this.pnlHisInfo);
            this.groupBox2.Controls.Add(this.optHistory);
            this.groupBox2.Controls.Add(this.optTrans);
            this.groupBox2.Location = new System.Drawing.Point(195, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(251, 218);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "版本设置";
            // 
            // cboVersions
            // 
            this.cboVersions.FormattingEnabled = true;
            this.cboVersions.Location = new System.Drawing.Point(34, 40);
            this.cboVersions.Name = "cboVersions";
            this.cboVersions.Size = new System.Drawing.Size(203, 20);
            this.cboVersions.TabIndex = 6;
            // 
            // pnlHisInfo
            // 
            this.pnlHisInfo.Controls.Add(this.dtHisTimestamp);
            this.pnlHisInfo.Controls.Add(this.optHisTime);
            this.pnlHisInfo.Controls.Add(this.optHisMarker);
            this.pnlHisInfo.Controls.Add(this.cboHisMarkers);
            this.pnlHisInfo.Location = new System.Drawing.Point(15, 97);
            this.pnlHisInfo.Name = "pnlHisInfo";
            this.pnlHisInfo.Size = new System.Drawing.Size(222, 111);
            this.pnlHisInfo.TabIndex = 5;
            // 
            // dtHisTimestamp
            // 
            this.dtHisTimestamp.CustomFormat = "yyyy年MM月dd日 HH:mm:ss";
            this.dtHisTimestamp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtHisTimestamp.Location = new System.Drawing.Point(31, 79);
            this.dtHisTimestamp.Name = "dtHisTimestamp";
            this.dtHisTimestamp.Size = new System.Drawing.Size(176, 21);
            this.dtHisTimestamp.TabIndex = 2;
            // 
            // optHisTime
            // 
            this.optHisTime.AutoSize = true;
            this.optHisTime.Checked = true;
            this.optHisTime.Location = new System.Drawing.Point(17, 51);
            this.optHisTime.Name = "optHisTime";
            this.optHisTime.Size = new System.Drawing.Size(71, 16);
            this.optHisTime.TabIndex = 1;
            this.optHisTime.TabStop = true;
            this.optHisTime.Text = "历史时间";
            this.optHisTime.UseVisualStyleBackColor = true;
            // 
            // optHisMarker
            // 
            this.optHisMarker.AutoSize = true;
            this.optHisMarker.Location = new System.Drawing.Point(17, 1);
            this.optHisMarker.Name = "optHisMarker";
            this.optHisMarker.Size = new System.Drawing.Size(71, 16);
            this.optHisMarker.TabIndex = 1;
            this.optHisMarker.Text = "历史标签";
            this.optHisMarker.UseVisualStyleBackColor = true;
            this.optHisMarker.CheckedChanged += new System.EventHandler(this.optHisMarker_CheckedChanged);
            // 
            // cboHisMarkers
            // 
            this.cboHisMarkers.FormattingEnabled = true;
            this.cboHisMarkers.Location = new System.Drawing.Point(31, 25);
            this.cboHisMarkers.Name = "cboHisMarkers";
            this.cboHisMarkers.Size = new System.Drawing.Size(176, 20);
            this.cboHisMarkers.TabIndex = 0;
            // 
            // optHistory
            // 
            this.optHistory.AutoSize = true;
            this.optHistory.Location = new System.Drawing.Point(15, 76);
            this.optHistory.Name = "optHistory";
            this.optHistory.Size = new System.Drawing.Size(107, 16);
            this.optHistory.TabIndex = 4;
            this.optHistory.Text = "连接到历史版本";
            this.optHistory.UseVisualStyleBackColor = true;
            // 
            // optTrans
            // 
            this.optTrans.AutoSize = true;
            this.optTrans.Checked = true;
            this.optTrans.Location = new System.Drawing.Point(15, 15);
            this.optTrans.Name = "optTrans";
            this.optTrans.Size = new System.Drawing.Size(107, 16);
            this.optTrans.TabIndex = 4;
            this.optTrans.TabStop = true;
            this.optTrans.Text = "连接到事务版本";
            this.optTrans.UseVisualStyleBackColor = true;
            this.optTrans.CheckedChanged += new System.EventHandler(this.optTrans_CheckedChanged);
            // 
            // DataConnectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "DataConnectionControl";
            this.Size = new System.Drawing.Size(450, 224);
            this.Load += new System.EventHandler(this.DataConnectionControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pnlHisInfo.ResumeLayout(false);
            this.pnlHisInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtInstance;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton optHistory;
        private System.Windows.Forms.RadioButton optTrans;
        private System.Windows.Forms.Panel pnlHisInfo;
        private System.Windows.Forms.ComboBox cboVersions;
        private System.Windows.Forms.RadioButton optHisTime;
        private System.Windows.Forms.RadioButton optHisMarker;
        private System.Windows.Forms.ComboBox cboHisMarkers;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.DateTimePicker dtHisTimestamp;
    }
}
