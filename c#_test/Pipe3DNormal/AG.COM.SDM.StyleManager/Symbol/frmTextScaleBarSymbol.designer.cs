namespace AG.COM.SDM.StyleManager
{
    partial class frmTextScaleBarSymbol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTextScaleBarSymbol));
            this.label1 = new System.Windows.Forms.Label();
            this.cbxCategory = new System.Windows.Forms.ComboBox();
            this.lvwSymbol = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.stypicPreview = new AG.COM.SDM.Utility.Controls.StylePicture();
            this.gBxMarkerSymbol = new System.Windows.Forms.GroupBox();
            this.cPkMarker = new AG.COM.SDM.Utility.Controls.ColorPicker();
            this.nudSize = new System.Windows.Forms.NumericUpDown();
            this.nudAngle = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.butProperty = new System.Windows.Forms.Button();
            this.butMoreSymbol = new System.Windows.Forms.Button();
            this.butSave = new System.Windows.Forms.Button();
            this.butRedo = new System.Windows.Forms.Button();
            this.butOk = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.gBxLineSymbol = new System.Windows.Forms.GroupBox();
            this.nudLineWidth = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cPkLine = new AG.COM.SDM.Utility.Controls.ColorPicker();
            this.gBxFillSymbol = new System.Windows.Forms.GroupBox();
            this.nudFillOutlineWidth = new System.Windows.Forms.NumericUpDown();
            this.cPkFillOutLine = new AG.COM.SDM.Utility.Controls.ColorPicker();
            this.cPkFill = new AG.COM.SDM.Utility.Controls.ColorPicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlSymbolSetting = new System.Windows.Forms.Panel();
            this.gBxNorthArrow = new System.Windows.Forms.GroupBox();
            this.cPkNorthArrowColor = new AG.COM.SDM.Utility.Controls.ColorPicker();
            this.cbxIsVisible = new System.Windows.Forms.CheckBox();
            this.nudNorthArrowSize = new System.Windows.Forms.NumericUpDown();
            this.nudNorthArrowRotate = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.gBxScaleBar = new System.Windows.Forms.GroupBox();
            this.butProperties = new System.Windows.Forms.Button();
            this.chkScaleBarIsVisible = new System.Windows.Forms.CheckBox();
            this.gBxText = new System.Windows.Forms.GroupBox();
            this.cbxFontDeleteLine = new System.Windows.Forms.CheckBox();
            this.cbxFontItalic = new System.Windows.Forms.CheckBox();
            this.cbxFontUnderline = new System.Windows.Forms.CheckBox();
            this.cbxFontBold = new System.Windows.Forms.CheckBox();
            this.cboTextFont = new System.Windows.Forms.ComboBox();
            this.cpkTextColor = new AG.COM.SDM.Utility.Controls.ColorPicker();
            this.nudTextSize = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stypicPreview)).BeginInit();
            this.gBxMarkerSymbol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAngle)).BeginInit();
            this.gBxLineSymbol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLineWidth)).BeginInit();
            this.gBxFillSymbol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFillOutlineWidth)).BeginInit();
            this.gBxNorthArrow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNorthArrowSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNorthArrowRotate)).BeginInit();
            this.gBxScaleBar.SuspendLayout();
            this.gBxText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTextSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "分　 类：";
            this.label1.Visible = false;
            // 
            // cbxCategory
            // 
            this.cbxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCategory.FormattingEnabled = true;
            this.cbxCategory.Location = new System.Drawing.Point(91, 15);
            this.cbxCategory.Name = "cbxCategory";
            this.cbxCategory.Size = new System.Drawing.Size(173, 20);
            this.cbxCategory.Sorted = true;
            this.cbxCategory.TabIndex = 1;
            this.cbxCategory.Visible = false;
            this.cbxCategory.SelectedIndexChanged += new System.EventHandler(this.cbxCategory_SelectedIndexChanged);
            // 
            // lvwSymbol
            // 
            this.lvwSymbol.Location = new System.Drawing.Point(12, 12);
            this.lvwSymbol.Name = "lvwSymbol";
            this.lvwSymbol.Size = new System.Drawing.Size(252, 406);
            this.lvwSymbol.TabIndex = 2;
            this.lvwSymbol.UseCompatibleStateImageBehavior = false;
            this.lvwSymbol.SelectedIndexChanged += new System.EventHandler(this.lvwSymbol_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.stypicPreview);
            this.groupBox1.Location = new System.Drawing.Point(281, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 103);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "预　览";
            // 
            // stypicPreview
            // 
            this.stypicPreview.Item = null;
            this.stypicPreview.Location = new System.Drawing.Point(6, 15);
            this.stypicPreview.Name = "stypicPreview";
            this.stypicPreview.Size = new System.Drawing.Size(146, 82);
            this.stypicPreview.Symbol = null;
            this.stypicPreview.TabIndex = 1;
            this.stypicPreview.TabStop = false;
            // 
            // gBxMarkerSymbol
            // 
            this.gBxMarkerSymbol.Controls.Add(this.cPkMarker);
            this.gBxMarkerSymbol.Controls.Add(this.nudSize);
            this.gBxMarkerSymbol.Controls.Add(this.nudAngle);
            this.gBxMarkerSymbol.Controls.Add(this.label4);
            this.gBxMarkerSymbol.Controls.Add(this.label3);
            this.gBxMarkerSymbol.Controls.Add(this.label2);
            this.gBxMarkerSymbol.Location = new System.Drawing.Point(462, 26);
            this.gBxMarkerSymbol.Name = "gBxMarkerSymbol";
            this.gBxMarkerSymbol.Size = new System.Drawing.Size(158, 134);
            this.gBxMarkerSymbol.TabIndex = 4;
            this.gBxMarkerSymbol.TabStop = false;
            this.gBxMarkerSymbol.Text = "选项";
            // 
            // cPkMarker
            // 
            this.cPkMarker.BackColor = System.Drawing.SystemColors.Window;
            this.cPkMarker.Context = null;
            this.cPkMarker.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cPkMarker.Location = new System.Drawing.Point(73, 27);
            this.cPkMarker.Name = "cPkMarker";
            this.cPkMarker.ReadOnly = false;
            this.cPkMarker.Size = new System.Drawing.Size(74, 21);
            this.cPkMarker.TabIndex = 15;
            this.cPkMarker.Text = "White";
            this.cPkMarker.Value = System.Drawing.Color.White;
            this.cPkMarker.ValueChanged += new System.EventHandler(this.cPkMarker_ValueChanged);
            // 
            // nudSize
            // 
            this.nudSize.DecimalPlaces = 2;
            this.nudSize.Location = new System.Drawing.Point(74, 62);
            this.nudSize.Name = "nudSize";
            this.nudSize.Size = new System.Drawing.Size(74, 21);
            this.nudSize.TabIndex = 7;
            this.nudSize.ValueChanged += new System.EventHandler(this.nudSize_ValueChanged);
            // 
            // nudAngle
            // 
            this.nudAngle.DecimalPlaces = 2;
            this.nudAngle.Location = new System.Drawing.Point(74, 95);
            this.nudAngle.Name = "nudAngle";
            this.nudAngle.Size = new System.Drawing.Size(75, 21);
            this.nudAngle.TabIndex = 6;
            this.nudAngle.ValueChanged += new System.EventHandler(this.nudAngle_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "旋　转：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "大　小：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "颜　色：";
            // 
            // butProperty
            // 
            this.butProperty.Location = new System.Drawing.Point(287, 310);
            this.butProperty.Name = "butProperty";
            this.butProperty.Size = new System.Drawing.Size(152, 23);
            this.butProperty.TabIndex = 5;
            this.butProperty.Text = "符　号　属　性";
            this.butProperty.UseVisualStyleBackColor = true;
            this.butProperty.Visible = false;
            // 
            // butMoreSymbol
            // 
            this.butMoreSymbol.Location = new System.Drawing.Point(287, 339);
            this.butMoreSymbol.Name = "butMoreSymbol";
            this.butMoreSymbol.Size = new System.Drawing.Size(152, 23);
            this.butMoreSymbol.TabIndex = 6;
            this.butMoreSymbol.Text = "更　多　符　号";
            this.butMoreSymbol.UseVisualStyleBackColor = true;
            this.butMoreSymbol.Click += new System.EventHandler(this.butMoreSymbol_Click);
            // 
            // butSave
            // 
            this.butSave.Location = new System.Drawing.Point(287, 368);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(68, 23);
            this.butSave.TabIndex = 7;
            this.butSave.Text = "保　存";
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Visible = false;
            // 
            // butRedo
            // 
            this.butRedo.Location = new System.Drawing.Point(371, 368);
            this.butRedo.Name = "butRedo";
            this.butRedo.Size = new System.Drawing.Size(68, 23);
            this.butRedo.TabIndex = 8;
            this.butRedo.Text = "重 置";
            this.butRedo.UseVisualStyleBackColor = true;
            this.butRedo.Visible = false;
            this.butRedo.Click += new System.EventHandler(this.butRedo_Click);
            // 
            // butOk
            // 
            this.butOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.butOk.Location = new System.Drawing.Point(287, 397);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(68, 23);
            this.butOk.TabIndex = 9;
            this.butOk.Text = "确 定";
            this.butOk.UseVisualStyleBackColor = true;
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(371, 397);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(68, 23);
            this.butCancel.TabIndex = 10;
            this.butCancel.Text = "取 消";
            this.butCancel.UseVisualStyleBackColor = true;
            // 
            // gBxLineSymbol
            // 
            this.gBxLineSymbol.Controls.Add(this.nudLineWidth);
            this.gBxLineSymbol.Controls.Add(this.label5);
            this.gBxLineSymbol.Controls.Add(this.label6);
            this.gBxLineSymbol.Controls.Add(this.cPkLine);
            this.gBxLineSymbol.Location = new System.Drawing.Point(462, 166);
            this.gBxLineSymbol.Name = "gBxLineSymbol";
            this.gBxLineSymbol.Size = new System.Drawing.Size(158, 93);
            this.gBxLineSymbol.TabIndex = 11;
            this.gBxLineSymbol.TabStop = false;
            this.gBxLineSymbol.Text = "选项";
            // 
            // nudLineWidth
            // 
            this.nudLineWidth.DecimalPlaces = 2;
            this.nudLineWidth.Location = new System.Drawing.Point(74, 59);
            this.nudLineWidth.Name = "nudLineWidth";
            this.nudLineWidth.Size = new System.Drawing.Size(74, 21);
            this.nudLineWidth.TabIndex = 9;
            this.nudLineWidth.ValueChanged += new System.EventHandler(this.nudLineWidth_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "颜　色：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "宽  度：";
            // 
            // cPkLine
            // 
            this.cPkLine.BackColor = System.Drawing.SystemColors.Window;
            this.cPkLine.Context = null;
            this.cPkLine.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cPkLine.Location = new System.Drawing.Point(73, 23);
            this.cPkLine.Name = "cPkLine";
            this.cPkLine.ReadOnly = false;
            this.cPkLine.Size = new System.Drawing.Size(74, 21);
            this.cPkLine.TabIndex = 15;
            this.cPkLine.Text = "White";
            this.cPkLine.Value = System.Drawing.Color.White;
            this.cPkLine.ValueChanged += new System.EventHandler(this.cPkLine_ValueChanged);
            // 
            // gBxFillSymbol
            // 
            this.gBxFillSymbol.Controls.Add(this.nudFillOutlineWidth);
            this.gBxFillSymbol.Controls.Add(this.cPkFillOutLine);
            this.gBxFillSymbol.Controls.Add(this.cPkFill);
            this.gBxFillSymbol.Controls.Add(this.label7);
            this.gBxFillSymbol.Controls.Add(this.label8);
            this.gBxFillSymbol.Controls.Add(this.label9);
            this.gBxFillSymbol.Location = new System.Drawing.Point(462, 278);
            this.gBxFillSymbol.Name = "gBxFillSymbol";
            this.gBxFillSymbol.Size = new System.Drawing.Size(158, 128);
            this.gBxFillSymbol.TabIndex = 12;
            this.gBxFillSymbol.TabStop = false;
            this.gBxFillSymbol.Text = "选项";
            // 
            // nudFillOutlineWidth
            // 
            this.nudFillOutlineWidth.DecimalPlaces = 2;
            this.nudFillOutlineWidth.Location = new System.Drawing.Point(73, 59);
            this.nudFillOutlineWidth.Name = "nudFillOutlineWidth";
            this.nudFillOutlineWidth.Size = new System.Drawing.Size(74, 21);
            this.nudFillOutlineWidth.TabIndex = 4;
            this.nudFillOutlineWidth.ValueChanged += new System.EventHandler(this.nudFillOutlineWidth_ValueChanged);
            // 
            // cPkFillOutLine
            // 
            this.cPkFillOutLine.BackColor = System.Drawing.SystemColors.Window;
            this.cPkFillOutLine.Context = null;
            this.cPkFillOutLine.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cPkFillOutLine.Location = new System.Drawing.Point(73, 91);
            this.cPkFillOutLine.Name = "cPkFillOutLine";
            this.cPkFillOutLine.ReadOnly = false;
            this.cPkFillOutLine.Size = new System.Drawing.Size(74, 21);
            this.cPkFillOutLine.TabIndex = 15;
            this.cPkFillOutLine.Text = "White";
            this.cPkFillOutLine.Value = System.Drawing.Color.White;
            this.cPkFillOutLine.ValueChanged += new System.EventHandler(this.cPkFillOutLine_ValueChanged);
            // 
            // cPkFill
            // 
            this.cPkFill.BackColor = System.Drawing.SystemColors.Window;
            this.cPkFill.Context = null;
            this.cPkFill.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cPkFill.Location = new System.Drawing.Point(73, 23);
            this.cPkFill.Name = "cPkFill";
            this.cPkFill.ReadOnly = false;
            this.cPkFill.Size = new System.Drawing.Size(74, 21);
            this.cPkFill.TabIndex = 15;
            this.cPkFill.Text = "White";
            this.cPkFill.Value = System.Drawing.Color.White;
            this.cPkFill.ValueChanged += new System.EventHandler(this.cPkFill_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "填充颜色：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "边线宽度：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "边线颜色：";
            // 
            // pnlSymbolSetting
            // 
            this.pnlSymbolSetting.BackColor = System.Drawing.SystemColors.Control;
            this.pnlSymbolSetting.Location = new System.Drawing.Point(282, 129);
            this.pnlSymbolSetting.Name = "pnlSymbolSetting";
            this.pnlSymbolSetting.Size = new System.Drawing.Size(156, 138);
            this.pnlSymbolSetting.TabIndex = 13;
            // 
            // gBxNorthArrow
            // 
            this.gBxNorthArrow.Controls.Add(this.cPkNorthArrowColor);
            this.gBxNorthArrow.Controls.Add(this.cbxIsVisible);
            this.gBxNorthArrow.Controls.Add(this.nudNorthArrowSize);
            this.gBxNorthArrow.Controls.Add(this.nudNorthArrowRotate);
            this.gBxNorthArrow.Controls.Add(this.label12);
            this.gBxNorthArrow.Controls.Add(this.label10);
            this.gBxNorthArrow.Controls.Add(this.label11);
            this.gBxNorthArrow.Location = new System.Drawing.Point(210, 448);
            this.gBxNorthArrow.Name = "gBxNorthArrow";
            this.gBxNorthArrow.Size = new System.Drawing.Size(158, 147);
            this.gBxNorthArrow.TabIndex = 14;
            this.gBxNorthArrow.TabStop = false;
            this.gBxNorthArrow.Text = "选项";
            // 
            // cPkNorthArrowColor
            // 
            this.cPkNorthArrowColor.BackColor = System.Drawing.SystemColors.Window;
            this.cPkNorthArrowColor.Context = null;
            this.cPkNorthArrowColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cPkNorthArrowColor.Location = new System.Drawing.Point(73, 54);
            this.cPkNorthArrowColor.Name = "cPkNorthArrowColor";
            this.cPkNorthArrowColor.ReadOnly = false;
            this.cPkNorthArrowColor.Size = new System.Drawing.Size(74, 21);
            this.cPkNorthArrowColor.TabIndex = 15;
            this.cPkNorthArrowColor.Text = "White";
            this.cPkNorthArrowColor.Value = System.Drawing.Color.White;
            this.cPkNorthArrowColor.ValueChanged += new System.EventHandler(this.cPkNorthArrowColor_ValueChanged);
            // 
            // cbxIsVisible
            // 
            this.cbxIsVisible.AutoSize = true;
            this.cbxIsVisible.Location = new System.Drawing.Point(17, 118);
            this.cbxIsVisible.Name = "cbxIsVisible";
            this.cbxIsVisible.Size = new System.Drawing.Size(48, 16);
            this.cbxIsVisible.TabIndex = 9;
            this.cbxIsVisible.Text = "显示";
            this.cbxIsVisible.UseVisualStyleBackColor = true;
            // 
            // nudNorthArrowSize
            // 
            this.nudNorthArrowSize.DecimalPlaces = 2;
            this.nudNorthArrowSize.Location = new System.Drawing.Point(73, 22);
            this.nudNorthArrowSize.Name = "nudNorthArrowSize";
            this.nudNorthArrowSize.Size = new System.Drawing.Size(74, 21);
            this.nudNorthArrowSize.TabIndex = 7;
            this.nudNorthArrowSize.ValueChanged += new System.EventHandler(this.nudNorthArrowSize_ValueChanged);
            // 
            // nudNorthArrowRotate
            // 
            this.nudNorthArrowRotate.DecimalPlaces = 2;
            this.nudNorthArrowRotate.Location = new System.Drawing.Point(74, 86);
            this.nudNorthArrowRotate.Name = "nudNorthArrowRotate";
            this.nudNorthArrowRotate.Size = new System.Drawing.Size(75, 21);
            this.nudNorthArrowRotate.TabIndex = 6;
            this.nudNorthArrowRotate.ValueChanged += new System.EventHandler(this.nudNorthArrowRotate_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 90);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 2;
            this.label12.Text = "旋　转：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 1;
            this.label10.Text = "大　小：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "颜　色：";
            // 
            // gBxScaleBar
            // 
            this.gBxScaleBar.Controls.Add(this.butProperties);
            this.gBxScaleBar.Controls.Add(this.chkScaleBarIsVisible);
            this.gBxScaleBar.Location = new System.Drawing.Point(14, 448);
            this.gBxScaleBar.Name = "gBxScaleBar";
            this.gBxScaleBar.Size = new System.Drawing.Size(158, 147);
            this.gBxScaleBar.TabIndex = 14;
            this.gBxScaleBar.TabStop = false;
            this.gBxScaleBar.Text = "选项";
            // 
            // butProperties
            // 
            this.butProperties.Location = new System.Drawing.Point(34, 45);
            this.butProperties.Name = "butProperties";
            this.butProperties.Size = new System.Drawing.Size(95, 26);
            this.butProperties.TabIndex = 10;
            this.butProperties.Text = "属  性";
            this.butProperties.UseVisualStyleBackColor = true;
            this.butProperties.Click += new System.EventHandler(this.butProperties_Click);
            // 
            // chkScaleBarIsVisible
            // 
            this.chkScaleBarIsVisible.AutoSize = true;
            this.chkScaleBarIsVisible.Location = new System.Drawing.Point(34, 96);
            this.chkScaleBarIsVisible.Name = "chkScaleBarIsVisible";
            this.chkScaleBarIsVisible.Size = new System.Drawing.Size(48, 16);
            this.chkScaleBarIsVisible.TabIndex = 9;
            this.chkScaleBarIsVisible.Text = "显示";
            this.chkScaleBarIsVisible.UseVisualStyleBackColor = true;
            // 
            // gBxText
            // 
            this.gBxText.Controls.Add(this.cbxFontDeleteLine);
            this.gBxText.Controls.Add(this.cbxFontItalic);
            this.gBxText.Controls.Add(this.cbxFontUnderline);
            this.gBxText.Controls.Add(this.cbxFontBold);
            this.gBxText.Controls.Add(this.cboTextFont);
            this.gBxText.Controls.Add(this.cpkTextColor);
            this.gBxText.Controls.Add(this.nudTextSize);
            this.gBxText.Controls.Add(this.label13);
            this.gBxText.Controls.Add(this.label14);
            this.gBxText.Controls.Add(this.label15);
            this.gBxText.Location = new System.Drawing.Point(394, 448);
            this.gBxText.Name = "gBxText";
            this.gBxText.Size = new System.Drawing.Size(158, 191);
            this.gBxText.TabIndex = 15;
            this.gBxText.TabStop = false;
            this.gBxText.Text = "选项";
            // 
            // cbxFontDeleteLine
            // 
            this.cbxFontDeleteLine.AutoSize = true;
            this.cbxFontDeleteLine.Location = new System.Drawing.Point(87, 161);
            this.cbxFontDeleteLine.Name = "cbxFontDeleteLine";
            this.cbxFontDeleteLine.Size = new System.Drawing.Size(60, 16);
            this.cbxFontDeleteLine.TabIndex = 17;
            this.cbxFontDeleteLine.Text = "删除线";
            this.cbxFontDeleteLine.UseVisualStyleBackColor = true;
            this.cbxFontDeleteLine.CheckedChanged += new System.EventHandler(this.cbxFontDeleteLine_CheckedChanged);
            // 
            // cbxFontItalic
            // 
            this.cbxFontItalic.AutoSize = true;
            this.cbxFontItalic.Location = new System.Drawing.Point(87, 136);
            this.cbxFontItalic.Name = "cbxFontItalic";
            this.cbxFontItalic.Size = new System.Drawing.Size(48, 16);
            this.cbxFontItalic.TabIndex = 17;
            this.cbxFontItalic.Text = "斜体";
            this.cbxFontItalic.UseVisualStyleBackColor = true;
            this.cbxFontItalic.CheckedChanged += new System.EventHandler(this.cbxFontItalic_CheckedChanged);
            // 
            // cbxFontUnderline
            // 
            this.cbxFontUnderline.AutoSize = true;
            this.cbxFontUnderline.Location = new System.Drawing.Point(17, 161);
            this.cbxFontUnderline.Name = "cbxFontUnderline";
            this.cbxFontUnderline.Size = new System.Drawing.Size(60, 16);
            this.cbxFontUnderline.TabIndex = 17;
            this.cbxFontUnderline.Text = "下划线";
            this.cbxFontUnderline.UseVisualStyleBackColor = true;
            this.cbxFontUnderline.CheckedChanged += new System.EventHandler(this.cbxFontUnderline_CheckedChanged);
            // 
            // cbxFontBold
            // 
            this.cbxFontBold.AutoSize = true;
            this.cbxFontBold.Location = new System.Drawing.Point(17, 136);
            this.cbxFontBold.Name = "cbxFontBold";
            this.cbxFontBold.Size = new System.Drawing.Size(48, 16);
            this.cbxFontBold.TabIndex = 17;
            this.cbxFontBold.Text = "粗体";
            this.cbxFontBold.UseVisualStyleBackColor = true;
            this.cbxFontBold.CheckedChanged += new System.EventHandler(this.cbxFontBold_CheckedChanged);
            // 
            // cboTextFont
            // 
            this.cboTextFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTextFont.FormattingEnabled = true;
            this.cboTextFont.Location = new System.Drawing.Point(17, 105);
            this.cboTextFont.Name = "cboTextFont";
            this.cboTextFont.Size = new System.Drawing.Size(129, 20);
            this.cboTextFont.TabIndex = 16;
            this.cboTextFont.SelectedIndexChanged += new System.EventHandler(this.cboTextFont_SelectedIndexChanged);
            // 
            // cpkTextColor
            // 
            this.cpkTextColor.BackColor = System.Drawing.SystemColors.Window;
            this.cpkTextColor.Context = null;
            this.cpkTextColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cpkTextColor.Location = new System.Drawing.Point(72, 22);
            this.cpkTextColor.Name = "cpkTextColor";
            this.cpkTextColor.ReadOnly = false;
            this.cpkTextColor.Size = new System.Drawing.Size(74, 21);
            this.cpkTextColor.TabIndex = 15;
            this.cpkTextColor.Text = "White";
            this.cpkTextColor.Value = System.Drawing.Color.White;
            this.cpkTextColor.ValueChanged += new System.EventHandler(this.cpkTextColor_ValueChanged);
            // 
            // nudTextSize
            // 
            this.nudTextSize.DecimalPlaces = 2;
            this.nudTextSize.Location = new System.Drawing.Point(73, 53);
            this.nudTextSize.Name = "nudTextSize";
            this.nudTextSize.Size = new System.Drawing.Size(74, 21);
            this.nudTextSize.TabIndex = 7;
            this.nudTextSize.ValueChanged += new System.EventHandler(this.nudTextSize_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(15, 57);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 1;
            this.label13.Text = "大　小：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(14, 85);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "字  体：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(14, 26);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 0;
            this.label15.Text = "颜　色：";
            // 
            // frmTextScaleBarSymbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 430);
            this.Controls.Add(this.gBxText);
            this.Controls.Add(this.gBxScaleBar);
            this.Controls.Add(this.gBxNorthArrow);
            this.Controls.Add(this.pnlSymbolSetting);
            this.Controls.Add(this.gBxLineSymbol);
            this.Controls.Add(this.gBxFillSymbol);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOk);
            this.Controls.Add(this.butRedo);
            this.Controls.Add(this.butSave);
            this.Controls.Add(this.butMoreSymbol);
            this.Controls.Add(this.butProperty);
            this.Controls.Add(this.gBxMarkerSymbol);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lvwSymbol);
            this.Controls.Add(this.cbxCategory);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTextScaleBarSymbol";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "符号选择器";
            this.Load += new System.EventHandler(this.frmSymbolSelector_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stypicPreview)).EndInit();
            this.gBxMarkerSymbol.ResumeLayout(false);
            this.gBxMarkerSymbol.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAngle)).EndInit();
            this.gBxLineSymbol.ResumeLayout(false);
            this.gBxLineSymbol.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLineWidth)).EndInit();
            this.gBxFillSymbol.ResumeLayout(false);
            this.gBxFillSymbol.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFillOutlineWidth)).EndInit();
            this.gBxNorthArrow.ResumeLayout(false);
            this.gBxNorthArrow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNorthArrowSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNorthArrowRotate)).EndInit();
            this.gBxScaleBar.ResumeLayout(false);
            this.gBxScaleBar.PerformLayout();
            this.gBxText.ResumeLayout(false);
            this.gBxText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTextSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxCategory;
        private System.Windows.Forms.ListView lvwSymbol;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gBxMarkerSymbol;
        private System.Windows.Forms.NumericUpDown nudSize;
        private System.Windows.Forms.NumericUpDown nudAngle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butProperty;
        private System.Windows.Forms.Button butMoreSymbol;
        private System.Windows.Forms.Button butSave;
        private System.Windows.Forms.Button butRedo;
        private System.Windows.Forms.Button butOk;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.GroupBox gBxLineSymbol;
        private System.Windows.Forms.GroupBox gBxFillSymbol;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudLineWidth;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudFillOutlineWidth;
        private System.Windows.Forms.Panel pnlSymbolSetting;
        private System.Windows.Forms.GroupBox gBxNorthArrow;
        private System.Windows.Forms.NumericUpDown nudNorthArrowSize;
        private System.Windows.Forms.NumericUpDown nudNorthArrowRotate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox cbxIsVisible;
        private System.Windows.Forms.GroupBox gBxScaleBar;
        private System.Windows.Forms.CheckBox chkScaleBarIsVisible;
        private System.Windows.Forms.Button butProperties;
        private AG.COM.SDM.Utility.Controls.StylePicture stypicPreview;
        private AG.COM.SDM.Utility.Controls.ColorPicker cPkMarker;
        private AG.COM.SDM.Utility.Controls.ColorPicker cPkLine;
        private AG.COM.SDM.Utility.Controls.ColorPicker cPkFillOutLine;
        private AG.COM.SDM.Utility.Controls.ColorPicker cPkFill;
        private AG.COM.SDM.Utility.Controls.ColorPicker cPkNorthArrowColor;
        private System.Windows.Forms.GroupBox gBxText;
        private System.Windows.Forms.CheckBox cbxFontDeleteLine;
        private System.Windows.Forms.CheckBox cbxFontItalic;
        private System.Windows.Forms.CheckBox cbxFontUnderline;
        private System.Windows.Forms.CheckBox cbxFontBold;
        private System.Windows.Forms.ComboBox cboTextFont;
        private AG.COM.SDM.Utility.Controls.ColorPicker cpkTextColor;
        private System.Windows.Forms.NumericUpDown nudTextSize;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
    }
}