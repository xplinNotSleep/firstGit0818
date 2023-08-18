namespace AG.COM.SDM.GeoDataBase
{
    partial class SpatialReferenceDialog
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblSaveAsInfo = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblSelectInfo = new System.Windows.Forms.Label();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.lblDetailInfo = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtDetailInfo = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtXPrecision = new System.Windows.Forms.TextBox();
            this.txtYMin = new System.Windows.Forms.TextBox();
            this.txtYMax = new System.Windows.Forms.TextBox();
            this.txtXMax = new System.Windows.Forms.TextBox();
            this.txtXMin = new System.Windows.Forms.TextBox();
            this.txtXYDescription = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtZDescription = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtZPrecision = new System.Windows.Forms.TextBox();
            this.txtZMax = new System.Windows.Forms.TextBox();
            this.txtZMin = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txtMDescription = new System.Windows.Forms.TextBox();
            this.txtMMax = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMPrecision = new System.Windows.Forms.TextBox();
            this.txtMMin = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(237, 403);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(313, 403);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(70, 24);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "应用(&A)";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(161, 403);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 24);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(8, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(375, 385);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.lblSaveAsInfo);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.lblSelectInfo);
            this.tabPage1.Controls.Add(this.btnSaveAs);
            this.tabPage1.Controls.Add(this.btnImport);
            this.tabPage1.Controls.Add(this.btnClear);
            this.tabPage1.Controls.Add(this.btnSelect);
            this.tabPage1.Controls.Add(this.lblDetailInfo);
            this.tabPage1.Controls.Add(this.txtName);
            this.tabPage1.Controls.Add(this.lblName);
            this.tabPage1.Controls.Add(this.txtDetailInfo);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(367, 360);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "坐标系统";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(109, 307);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 12);
            this.label13.TabIndex = 11;
            this.label13.Text = " Feature Dataset)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(109, 295);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(221, 12);
            this.label12.TabIndex = 11;
            this.label12.Text = "从已存在的数据文件中导入坐标信息(如 ";
            // 
            // lblSaveAsInfo
            // 
            this.lblSaveAsInfo.AutoSize = true;
            this.lblSaveAsInfo.Location = new System.Drawing.Point(109, 332);
            this.lblSaveAsInfo.Name = "lblSaveAsInfo";
            this.lblSaveAsInfo.Size = new System.Drawing.Size(185, 12);
            this.lblSaveAsInfo.TabIndex = 9;
            this.lblSaveAsInfo.Text = "保存坐标系统信息到指定目录文件";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(109, 238);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 12);
            this.label14.TabIndex = 7;
            this.label14.Text = "清除坐标系统";
            // 
            // lblSelectInfo
            // 
            this.lblSelectInfo.AutoSize = true;
            this.lblSelectInfo.Location = new System.Drawing.Point(109, 267);
            this.lblSelectInfo.Name = "lblSelectInfo";
            this.lblSelectInfo.Size = new System.Drawing.Size(185, 12);
            this.lblSelectInfo.TabIndex = 7;
            this.lblSelectInfo.Text = "选择已预先定义好的坐标系统文件";
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Enabled = false;
            this.btnSaveAs.Location = new System.Drawing.Point(17, 327);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(75, 23);
            this.btnSaveAs.TabIndex = 6;
            this.btnSaveAs.Text = "另存为…";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(17, 295);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 5;
            this.btnImport.Text = "导入…";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(17, 231);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(17, 263);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "选择…";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // lblDetailInfo
            // 
            this.lblDetailInfo.AutoSize = true;
            this.lblDetailInfo.Location = new System.Drawing.Point(15, 51);
            this.lblDetailInfo.Name = "lblDetailInfo";
            this.lblDetailInfo.Size = new System.Drawing.Size(53, 12);
            this.lblDetailInfo.TabIndex = 3;
            this.lblDetailInfo.Text = "详细信息";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.SystemColors.Control;
            this.txtName.Location = new System.Drawing.Point(62, 18);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(291, 21);
            this.txtName.TabIndex = 2;
            this.txtName.TextChanged += new System.EventHandler(this.BtnApplyChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(15, 22);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(29, 12);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "名称";
            // 
            // txtDetailInfo
            // 
            this.txtDetailInfo.BackColor = System.Drawing.SystemColors.Control;
            this.txtDetailInfo.Location = new System.Drawing.Point(17, 66);
            this.txtDetailInfo.Multiline = true;
            this.txtDetailInfo.Name = "txtDetailInfo";
            this.txtDetailInfo.ReadOnly = true;
            this.txtDetailInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDetailInfo.Size = new System.Drawing.Size(336, 159);
            this.txtDetailInfo.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txtXPrecision);
            this.tabPage2.Controls.Add(this.txtYMin);
            this.tabPage2.Controls.Add(this.txtYMax);
            this.tabPage2.Controls.Add(this.txtXMax);
            this.tabPage2.Controls.Add(this.txtXMin);
            this.tabPage2.Controls.Add(this.txtXYDescription);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(367, 360);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "X/Y Domain";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "分辨率:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(188, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "最大X值:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "最小Y值:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "最大Y值:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "最小X值:";
            // 
            // txtXPrecision
            // 
            this.txtXPrecision.Location = new System.Drawing.Point(73, 161);
            this.txtXPrecision.Name = "txtXPrecision";
            this.txtXPrecision.Size = new System.Drawing.Size(100, 21);
            this.txtXPrecision.TabIndex = 5;
            this.txtXPrecision.Tag = "100000";
            this.txtXPrecision.Text = "100000";
            this.txtXPrecision.TextChanged += new System.EventHandler(this.BtnApplyChanged);
            this.txtXPrecision.Leave += new System.EventHandler(this.ValidateNumberChars);
            // 
            // txtYMin
            // 
            this.txtYMin.Location = new System.Drawing.Point(73, 127);
            this.txtYMin.Name = "txtYMin";
            this.txtYMin.Size = new System.Drawing.Size(100, 21);
            this.txtYMin.TabIndex = 4;
            this.txtYMin.Tag = "-10000";
            this.txtYMin.Text = "-10000";
            this.txtYMin.TextChanged += new System.EventHandler(this.BtnApplyChanged);
            this.txtYMin.Leave += new System.EventHandler(this.ValidateNumberChars);
            // 
            // txtYMax
            // 
            this.txtYMax.Location = new System.Drawing.Point(247, 127);
            this.txtYMax.Name = "txtYMax";
            this.txtYMax.Size = new System.Drawing.Size(100, 21);
            this.txtYMax.TabIndex = 3;
            this.txtYMax.Tag = "11474.83645";
            this.txtYMax.Text = "11474.83645";
            this.txtYMax.TextChanged += new System.EventHandler(this.BtnApplyChanged);
            this.txtYMax.Leave += new System.EventHandler(this.ValidateNumberChars);
            // 
            // txtXMax
            // 
            this.txtXMax.Location = new System.Drawing.Point(247, 93);
            this.txtXMax.Name = "txtXMax";
            this.txtXMax.Size = new System.Drawing.Size(100, 21);
            this.txtXMax.TabIndex = 2;
            this.txtXMax.Tag = "11474.83645";
            this.txtXMax.Text = "11474.83645";
            this.txtXMax.TextChanged += new System.EventHandler(this.BtnApplyChanged);
            this.txtXMax.Leave += new System.EventHandler(this.ValidateNumberChars);
            // 
            // txtXMin
            // 
            this.txtXMin.Location = new System.Drawing.Point(73, 93);
            this.txtXMin.Name = "txtXMin";
            this.txtXMin.Size = new System.Drawing.Size(100, 21);
            this.txtXMin.TabIndex = 1;
            this.txtXMin.Tag = "-10000";
            this.txtXMin.Text = "-10000";
            this.txtXMin.TextChanged += new System.EventHandler(this.BtnApplyChanged);
            this.txtXMin.Leave += new System.EventHandler(this.ValidateNumberChars);
            // 
            // txtXYDescription
            // 
            this.txtXYDescription.BackColor = System.Drawing.SystemColors.Control;
            this.txtXYDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtXYDescription.Location = new System.Drawing.Point(18, 28);
            this.txtXYDescription.Multiline = true;
            this.txtXYDescription.Name = "txtXYDescription";
            this.txtXYDescription.Size = new System.Drawing.Size(336, 48);
            this.txtXYDescription.TabIndex = 0;
            this.txtXYDescription.Text = "   要素类的坐标范围或域范围,取决于最大X/Y值、最小X/Y值及其精度。精度指每单位长度包含系统单位的数量,体现了分辨率的高低。";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtZDescription);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.txtZPrecision);
            this.tabPage3.Controls.Add(this.txtZMax);
            this.tabPage3.Controls.Add(this.txtZMin);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(367, 360);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Z Domain";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtZDescription
            // 
            this.txtZDescription.BackColor = System.Drawing.SystemColors.Control;
            this.txtZDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtZDescription.Location = new System.Drawing.Point(18, 28);
            this.txtZDescription.Multiline = true;
            this.txtZDescription.Name = "txtZDescription";
            this.txtZDescription.Size = new System.Drawing.Size(326, 43);
            this.txtZDescription.TabIndex = 17;
            this.txtZDescription.Text = "   要素类的坐标范围或域范围,取决于最小Z值、最大Z值及精度值。精度指每单位长度包含系统单位的数量，体现了分辨率的高低。";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "分辨率:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(188, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "最大Z值:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "最小Z值:";
            // 
            // txtZPrecision
            // 
            this.txtZPrecision.Location = new System.Drawing.Point(73, 127);
            this.txtZPrecision.Name = "txtZPrecision";
            this.txtZPrecision.Size = new System.Drawing.Size(100, 21);
            this.txtZPrecision.TabIndex = 13;
            this.txtZPrecision.Tag = "100000";
            this.txtZPrecision.Text = "100000";
            this.txtZPrecision.TextChanged += new System.EventHandler(this.BtnApplyChanged);
            this.txtZPrecision.Leave += new System.EventHandler(this.ValidateNumberChars);
            // 
            // txtZMax
            // 
            this.txtZMax.Location = new System.Drawing.Point(247, 93);
            this.txtZMax.Name = "txtZMax";
            this.txtZMax.Size = new System.Drawing.Size(100, 21);
            this.txtZMax.TabIndex = 12;
            this.txtZMax.Tag = "21474.83645";
            this.txtZMax.Text = "21474.83645";
            this.txtZMax.TextChanged += new System.EventHandler(this.BtnApplyChanged);
            this.txtZMax.Leave += new System.EventHandler(this.ValidateNumberChars);
            // 
            // txtZMin
            // 
            this.txtZMin.Location = new System.Drawing.Point(73, 93);
            this.txtZMin.Name = "txtZMin";
            this.txtZMin.Size = new System.Drawing.Size(100, 21);
            this.txtZMin.TabIndex = 11;
            this.txtZMin.Tag = "0";
            this.txtZMin.Text = "0";
            this.txtZMin.TextChanged += new System.EventHandler(this.BtnApplyChanged);
            this.txtZMin.Leave += new System.EventHandler(this.ValidateNumberChars);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.txtMDescription);
            this.tabPage4.Controls.Add(this.txtMMax);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.txtMPrecision);
            this.tabPage4.Controls.Add(this.txtMMin);
            this.tabPage4.Location = new System.Drawing.Point(4, 21);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(367, 360);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "M Domain";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // txtMDescription
            // 
            this.txtMDescription.BackColor = System.Drawing.SystemColors.Control;
            this.txtMDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMDescription.Location = new System.Drawing.Point(18, 28);
            this.txtMDescription.Multiline = true;
            this.txtMDescription.Name = "txtMDescription";
            this.txtMDescription.Size = new System.Drawing.Size(326, 44);
            this.txtMDescription.TabIndex = 23;
            this.txtMDescription.Text = "   要素类的坐标范围或域范围,取决于最小M值、最大M值及精度值。精度指每单位长度包含系统单位的数量，体现了分辨率的高低。";
            // 
            // txtMMax
            // 
            this.txtMMax.Location = new System.Drawing.Point(247, 93);
            this.txtMMax.Name = "txtMMax";
            this.txtMMax.Size = new System.Drawing.Size(100, 21);
            this.txtMMax.TabIndex = 22;
            this.txtMMax.Tag = "21474.83645";
            this.txtMMax.Text = "21474.83645";
            this.txtMMax.TextChanged += new System.EventHandler(this.BtnApplyChanged);
            this.txtMMax.Leave += new System.EventHandler(this.ValidateNumberChars);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 131);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 21;
            this.label9.Text = "分辨率:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(188, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "最大M值:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 97);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 19;
            this.label11.Text = "最小M值:";
            // 
            // txtMPrecision
            // 
            this.txtMPrecision.Location = new System.Drawing.Point(73, 127);
            this.txtMPrecision.Name = "txtMPrecision";
            this.txtMPrecision.Size = new System.Drawing.Size(100, 21);
            this.txtMPrecision.TabIndex = 18;
            this.txtMPrecision.Tag = "100000";
            this.txtMPrecision.Text = "100000";
            this.txtMPrecision.TextChanged += new System.EventHandler(this.BtnApplyChanged);
            this.txtMPrecision.Leave += new System.EventHandler(this.ValidateNumberChars);
            // 
            // txtMMin
            // 
            this.txtMMin.Location = new System.Drawing.Point(73, 93);
            this.txtMMin.Name = "txtMMin";
            this.txtMMin.Size = new System.Drawing.Size(100, 21);
            this.txtMMin.TabIndex = 17;
            this.txtMMin.Tag = "0";
            this.txtMMin.Text = "0";
            this.txtMMin.TextChanged += new System.EventHandler(this.BtnApplyChanged);
            this.txtMMin.Leave += new System.EventHandler(this.ValidateNumberChars);
            // 
            // SpatialReferenceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 431);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpatialReferenceDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "空间参考属性";
            this.Load += new System.EventHandler(this.SpatialReferenceDialog_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblSaveAsInfo;
        private System.Windows.Forms.Label lblSelectInfo;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label lblDetailInfo;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtDetailInfo;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtXPrecision;
        private System.Windows.Forms.TextBox txtYMin;
        private System.Windows.Forms.TextBox txtYMax;
        private System.Windows.Forms.TextBox txtXMax;
        private System.Windows.Forms.TextBox txtXMin;
        private System.Windows.Forms.TextBox txtXYDescription;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtZDescription;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtZPrecision;
        private System.Windows.Forms.TextBox txtZMax;
        private System.Windows.Forms.TextBox txtZMin;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txtMDescription;
        private System.Windows.Forms.TextBox txtMMax;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtMPrecision;
        private System.Windows.Forms.TextBox txtMMin;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnClear;

    }
}