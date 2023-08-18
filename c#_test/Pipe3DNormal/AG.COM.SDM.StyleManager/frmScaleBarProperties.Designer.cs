using AG.COM.SDM.Utility.Controls;

namespace AG.COM.SDM.StyleManager
{
    partial class frmScaleBarProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScaleBarProperties));
            this.butOk = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxDivisionValue = new System.Windows.Forms.TextBox();
            this.chkZeroDivision = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudSubdivisions = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudDivisionValue = new System.Windows.Forms.NumericUpDown();
            this.nudDivisions = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbxDivisionUnit = new System.Windows.Forms.TextBox();
            this.cpkUnitLabelColor = new AG.COM.SDM.Utility.Controls.ColorPicker();
            this.label9 = new System.Windows.Forms.Label();
            this.nudLabelGap = new System.Windows.Forms.NumericUpDown();
            this.tbxLabel = new System.Windows.Forms.TextBox();
            this.cboLabelPosition = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboDivisionUnit = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.nudNumberGap = new System.Windows.Forms.NumericUpDown();
            this.cboNumberPosition = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboNumberFrequency = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.sbtMarkSubdivisionSymbol = new AG.COM.SDM.Utility.Controls.StyleButton();
            this.sbtMarkDivisionSymbol = new AG.COM.SDM.Utility.Controls.StyleButton();
            this.nudSubMarkheight = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.nudMarkHeight = new System.Windows.Forms.NumericUpDown();
            this.cboMarksPosition = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cboMarksFrequency = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cpkTextColor = new AG.COM.SDM.Utility.Controls.ColorPicker();
            this.sbtScaleBar2 = new AG.COM.SDM.Utility.Controls.StyleButton();
            this.sbtScaleBar1 = new AG.COM.SDM.Utility.Controls.StyleButton();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSubdivisions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDivisionValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDivisions)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLabelGap)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberGap)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSubMarkheight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMarkHeight)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(256, 385);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(62, 23);
            this.butOk.TabIndex = 1;
            this.butOk.Text = "确定";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(329, 385);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(62, 23);
            this.butCancel.TabIndex = 1;
            this.butCancel.Text = "取消";
            this.butCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxDivisionValue);
            this.groupBox1.Controls.Add(this.chkZeroDivision);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nudSubdivisions);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nudDivisionValue);
            this.groupBox1.Controls.Add(this.nudDivisions);
            this.groupBox1.Location = new System.Drawing.Point(213, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 142);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "刻度";
            // 
            // tbxDivisionValue
            // 
            this.tbxDivisionValue.BackColor = System.Drawing.SystemColors.Control;
            this.tbxDivisionValue.Location = new System.Drawing.Point(92, 19);
            this.tbxDivisionValue.Name = "tbxDivisionValue";
            this.tbxDivisionValue.ReadOnly = true;
            this.tbxDivisionValue.Size = new System.Drawing.Size(76, 21);
            this.tbxDivisionValue.TabIndex = 3;
            // 
            // chkZeroDivision
            // 
            this.chkZeroDivision.AutoSize = true;
            this.chkZeroDivision.Location = new System.Drawing.Point(21, 113);
            this.chkZeroDivision.Name = "chkZeroDivision";
            this.chkZeroDivision.Size = new System.Drawing.Size(120, 16);
            this.chkZeroDivision.TabIndex = 2;
            this.chkZeroDivision.Text = "原点左侧显示一段";
            this.chkZeroDivision.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "子段数目：";
            // 
            // nudSubdivisions
            // 
            this.nudSubdivisions.Location = new System.Drawing.Point(92, 80);
            this.nudSubdivisions.Name = "nudSubdivisions";
            this.nudSubdivisions.Size = new System.Drawing.Size(76, 21);
            this.nudSubdivisions.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "比例尺：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "分段数目：";
            // 
            // nudDivisionValue
            // 
            this.nudDivisionValue.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.nudDivisionValue.Location = new System.Drawing.Point(92, 318);
            this.nudDivisionValue.Name = "nudDivisionValue";
            this.nudDivisionValue.ReadOnly = true;
            this.nudDivisionValue.Size = new System.Drawing.Size(76, 21);
            this.nudDivisionValue.TabIndex = 0;
            this.nudDivisionValue.Visible = false;
            // 
            // nudDivisions
            // 
            this.nudDivisions.Location = new System.Drawing.Point(92, 49);
            this.nudDivisions.Name = "nudDivisions";
            this.nudDivisions.Size = new System.Drawing.Size(76, 21);
            this.nudDivisions.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbxDivisionUnit);
            this.groupBox2.Controls.Add(this.cpkUnitLabelColor);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.nudLabelGap);
            this.groupBox2.Controls.Add(this.tbxLabel);
            this.groupBox2.Controls.Add(this.cboLabelPosition);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(187, 154);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "单位";
            // 
            // tbxDivisionUnit
            // 
            this.tbxDivisionUnit.BackColor = System.Drawing.SystemColors.Control;
            this.tbxDivisionUnit.Location = new System.Drawing.Point(91, 21);
            this.tbxDivisionUnit.Name = "tbxDivisionUnit";
            this.tbxDivisionUnit.ReadOnly = true;
            this.tbxDivisionUnit.Size = new System.Drawing.Size(76, 21);
            this.tbxDivisionUnit.TabIndex = 4;
            // 
            // cpkUnitLabelColor
            // 
            this.cpkUnitLabelColor.BackColor = System.Drawing.SystemColors.Window;
            this.cpkUnitLabelColor.Context = null;
            this.cpkUnitLabelColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cpkUnitLabelColor.Location = new System.Drawing.Point(90, 122);
            this.cpkUnitLabelColor.Name = "cpkUnitLabelColor";
            this.cpkUnitLabelColor.ReadOnly = false;
            this.cpkUnitLabelColor.Size = new System.Drawing.Size(77, 21);
            this.cpkUnitLabelColor.TabIndex = 4;
            this.cpkUnitLabelColor.Text = "White";
            this.cpkUnitLabelColor.Value = System.Drawing.Color.White;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 4;
            this.label9.Text = "间　　距：";
            // 
            // nudLabelGap
            // 
            this.nudLabelGap.Location = new System.Drawing.Point(91, 87);
            this.nudLabelGap.Name = "nudLabelGap";
            this.nudLabelGap.Size = new System.Drawing.Size(76, 21);
            this.nudLabelGap.TabIndex = 3;
            // 
            // tbxLabel
            // 
            this.tbxLabel.Location = new System.Drawing.Point(91, 460);
            this.tbxLabel.Name = "tbxLabel";
            this.tbxLabel.Size = new System.Drawing.Size(76, 21);
            this.tbxLabel.TabIndex = 2;
            this.tbxLabel.Visible = false;
            // 
            // cboLabelPosition
            // 
            this.cboLabelPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLabelPosition.FormattingEnabled = true;
            this.cboLabelPosition.Items.AddRange(new object[] {
            "比例尺上",
            "标签前",
            "标签后",
            "比例尺前",
            "比例尺后",
            "比例尺下"});
            this.cboLabelPosition.Location = new System.Drawing.Point(91, 54);
            this.cboLabelPosition.Name = "cboLabelPosition";
            this.cboLabelPosition.Size = new System.Drawing.Size(76, 20);
            this.cboLabelPosition.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(19, 126);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 0;
            this.label17.Text = "颜　　色：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 464);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "标签内容：";
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "标签位置：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "划分单位：";
            // 
            // cboDivisionUnit
            // 
            this.cboDivisionUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDivisionUnit.FormattingEnabled = true;
            this.cboDivisionUnit.Items.AddRange(new object[] {
            "厘米",
            "度",
            "分米",
            "英尺",
            "英寸",
            "千米",
            "米",
            "英里",
            "毫米",
            "海里",
            "像素",
            "未知",
            "码"});
            this.cboDivisionUnit.Location = new System.Drawing.Point(147, 432);
            this.cboDivisionUnit.Name = "cboDivisionUnit";
            this.cboDivisionUnit.Size = new System.Drawing.Size(76, 20);
            this.cboDivisionUnit.TabIndex = 1;
            this.cboDivisionUnit.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.nudNumberGap);
            this.groupBox3.Controls.Add(this.cboNumberPosition);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cboNumberFrequency);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(213, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(187, 123);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数字";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 90);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 6;
            this.label10.Text = "间　　距：";
            // 
            // nudNumberGap
            // 
            this.nudNumberGap.Location = new System.Drawing.Point(92, 85);
            this.nudNumberGap.Name = "nudNumberGap";
            this.nudNumberGap.Size = new System.Drawing.Size(76, 21);
            this.nudNumberGap.TabIndex = 5;
            // 
            // cboNumberPosition
            // 
            this.cboNumberPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNumberPosition.FormattingEnabled = true;
            this.cboNumberPosition.Items.AddRange(new object[] {
            "比例尺上",
            "比例尺顶",
            "比例尺中",
            "比例尺底",
            "比例尺下"});
            this.cboNumberPosition.Location = new System.Drawing.Point(92, 53);
            this.cboNumberPosition.Name = "cboNumberPosition";
            this.cboNumberPosition.Size = new System.Drawing.Size(76, 20);
            this.cboNumberPosition.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "标签位置：";
            // 
            // cboNumberFrequency
            // 
            this.cboNumberFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNumberFrequency.FormattingEnabled = true;
            this.cboNumberFrequency.Items.AddRange(new object[] {
            "无",
            "单一",
            "零分度",
            "分割",
            "首分割",
            "双分割",
            "全分割"});
            this.cboNumberFrequency.Location = new System.Drawing.Point(92, 22);
            this.cboNumberFrequency.Name = "cboNumberFrequency";
            this.cboNumberFrequency.Size = new System.Drawing.Size(76, 20);
            this.cboNumberFrequency.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "标签风格：";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.sbtMarkSubdivisionSymbol);
            this.groupBox4.Controls.Add(this.sbtMarkDivisionSymbol);
            this.groupBox4.Controls.Add(this.nudSubMarkheight);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.nudMarkHeight);
            this.groupBox4.Controls.Add(this.cboMarksPosition);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.cboMarksFrequency);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Location = new System.Drawing.Point(12, 172);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(187, 204);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "分割";
            // 
            // sbtMarkSubdivisionSymbol
            // 
            this.sbtMarkSubdivisionSymbol.Location = new System.Drawing.Point(103, 164);
            this.sbtMarkSubdivisionSymbol.Name = "sbtMarkSubdivisionSymbol";
            this.sbtMarkSubdivisionSymbol.Size = new System.Drawing.Size(64, 23);
            this.sbtMarkSubdivisionSymbol.Symbol = null;
            this.sbtMarkSubdivisionSymbol.TabIndex = 7;
            this.sbtMarkSubdivisionSymbol.Click += new System.EventHandler(this.sbtMarkSubdivisionSymbol_Click);
            // 
            // sbtMarkDivisionSymbol
            // 
            this.sbtMarkDivisionSymbol.Location = new System.Drawing.Point(103, 106);
            this.sbtMarkDivisionSymbol.Name = "sbtMarkDivisionSymbol";
            this.sbtMarkDivisionSymbol.Size = new System.Drawing.Size(64, 23);
            this.sbtMarkDivisionSymbol.Symbol = null;
            this.sbtMarkDivisionSymbol.TabIndex = 7;
            this.sbtMarkDivisionSymbol.Click += new System.EventHandler(this.sbtMarkDivisionSymbol_Click);
            // 
            // nudSubMarkheight
            // 
            this.nudSubMarkheight.Location = new System.Drawing.Point(21, 165);
            this.nudSubMarkheight.Name = "nudSubMarkheight";
            this.nudSubMarkheight.Size = new System.Drawing.Size(76, 21);
            this.nudSubMarkheight.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 142);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 12);
            this.label11.TabIndex = 6;
            this.label11.Text = "子分割符高度：";
            // 
            // nudMarkHeight
            // 
            this.nudMarkHeight.Location = new System.Drawing.Point(21, 106);
            this.nudMarkHeight.Name = "nudMarkHeight";
            this.nudMarkHeight.Size = new System.Drawing.Size(76, 21);
            this.nudMarkHeight.TabIndex = 5;
            // 
            // cboMarksPosition
            // 
            this.cboMarksPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMarksPosition.FormattingEnabled = true;
            this.cboMarksPosition.Items.AddRange(new object[] {
            "比例尺上",
            "比例尺顶",
            "比例尺中",
            "比例尺底",
            "比例尺下"});
            this.cboMarksPosition.Location = new System.Drawing.Point(91, 53);
            this.cboMarksPosition.Name = "cboMarksPosition";
            this.cboMarksPosition.Size = new System.Drawing.Size(76, 20);
            this.cboMarksPosition.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 84);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "符号高度：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 57);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 0;
            this.label13.Text = "符号位置：";
            // 
            // cboMarksFrequency
            // 
            this.cboMarksFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMarksFrequency.FormattingEnabled = true;
            this.cboMarksFrequency.Items.AddRange(new object[] {
            "无",
            "单一",
            "零分度",
            "分割",
            "首分割",
            "双分割",
            "全分割"});
            this.cboMarksFrequency.Location = new System.Drawing.Point(91, 21);
            this.cboMarksFrequency.Name = "cboMarksFrequency";
            this.cboMarksFrequency.Size = new System.Drawing.Size(76, 20);
            this.cboMarksFrequency.TabIndex = 1;
            this.cboMarksFrequency.SelectedValueChanged += new System.EventHandler(this.cboMarksFrequency_SelectedValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(20, 26);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "符号风格：";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cpkTextColor);
            this.groupBox5.Controls.Add(this.sbtScaleBar2);
            this.groupBox5.Controls.Add(this.sbtScaleBar1);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Location = new System.Drawing.Point(213, 289);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(187, 87);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "其它";
            // 
            // cpkTextColor
            // 
            this.cpkTextColor.BackColor = System.Drawing.SystemColors.Window;
            this.cpkTextColor.Context = null;
            this.cpkTextColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cpkTextColor.Location = new System.Drawing.Point(91, 18);
            this.cpkTextColor.Name = "cpkTextColor";
            this.cpkTextColor.ReadOnly = false;
            this.cpkTextColor.Size = new System.Drawing.Size(77, 21);
            this.cpkTextColor.TabIndex = 4;
            this.cpkTextColor.Text = "White";
            this.cpkTextColor.Value = System.Drawing.Color.White;
            // 
            // sbtScaleBar2
            // 
            this.sbtScaleBar2.Location = new System.Drawing.Point(132, 51);
            this.sbtScaleBar2.Name = "sbtScaleBar2";
            this.sbtScaleBar2.Size = new System.Drawing.Size(46, 23);
            this.sbtScaleBar2.Symbol = null;
            this.sbtScaleBar2.TabIndex = 7;
            this.sbtScaleBar2.Click += new System.EventHandler(this.sbtScaleBar2_Click);
            // 
            // sbtScaleBar1
            // 
            this.sbtScaleBar1.Location = new System.Drawing.Point(80, 51);
            this.sbtScaleBar1.Name = "sbtScaleBar1";
            this.sbtScaleBar1.Size = new System.Drawing.Size(46, 23);
            this.sbtScaleBar1.Symbol = null;
            this.sbtScaleBar1.TabIndex = 7;
            this.sbtScaleBar1.Click += new System.EventHandler(this.sbtScaleBar1_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(21, 56);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 0;
            this.label16.Text = "比例尺：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(21, 22);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 0;
            this.label15.Text = "文字颜色：";
            // 
            // frmScaleBarProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 420);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOk);
            this.Controls.Add(this.cboDivisionUnit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmScaleBarProperties";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "属性";
            this.Load += new System.EventHandler(this.frmScaleBarProperties_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSubdivisions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDivisionValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDivisions)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLabelGap)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberGap)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSubMarkheight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMarkHeight)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butOk;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudSubdivisions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudDivisions;
        private System.Windows.Forms.CheckBox chkZeroDivision;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboLabelPosition;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboDivisionUnit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cboNumberPosition;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboNumberFrequency;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudLabelGap;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudNumberGap;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudMarkHeight;
        private System.Windows.Forms.ComboBox cboMarksPosition;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cboMarksFrequency;
        private System.Windows.Forms.Label label14;
        private StyleButton sbtMarkSubdivisionSymbol;
        private StyleButton sbtMarkDivisionSymbol;
        private System.Windows.Forms.NumericUpDown nudSubMarkheight;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private StyleButton sbtScaleBar1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudDivisionValue;
        private StyleButton sbtScaleBar2;
        private System.Windows.Forms.Label label17;
        private AG.COM.SDM.Utility.Controls.ColorPicker cpkUnitLabelColor;
        private AG.COM.SDM.Utility.Controls.ColorPicker cpkTextColor;
        private System.Windows.Forms.TextBox tbxDivisionUnit;
        private System.Windows.Forms.TextBox tbxDivisionValue;
    }
}