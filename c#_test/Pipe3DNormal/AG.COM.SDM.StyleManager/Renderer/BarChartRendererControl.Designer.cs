namespace AG.COM.SDM.StyleManager.Renderer
{
    partial class BarChartRendererControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BarChartRendererControl));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.nudSpace = new System.Windows.Forms.NumericUpDown();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cboNormalization = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nudSymbolSize = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbtColumn = new System.Windows.Forms.RadioButton();
            this.rbtBar = new System.Windows.Forms.RadioButton();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxShowAxes = new System.Windows.Forms.CheckBox();
            this.cbxNotOverlap = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.butExclusionProperty = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.butDDDown = new System.Windows.Forms.Button();
            this.butDDUp = new System.Windows.Forms.Button();
            this.butRemoveAll = new System.Windows.Forms.Button();
            this.butRemove = new System.Windows.Forms.Button();
            this.butAdd = new System.Windows.Forms.Button();
            this.lbxFields = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbo3DThickness = new System.Windows.Forms.ComboBox();
            this.chk3DDisplay = new System.Windows.Forms.CheckBox();
            this.sbtBackground = new AG.COM.SDM.Utility.Controls.StyleButton();
            this.sbtAxes = new AG.COM.SDM.Utility.Controls.StyleButton();
            this.slvBarChart = new AG.COM.SDM.Utility.Controls.StyleListView();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSymbolSize)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.nudSpace);
            this.groupBox4.Controls.Add(this.nudWidth);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(1, 272);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(128, 94);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "柱状";
            // 
            // nudSpace
            // 
            this.nudSpace.Location = new System.Drawing.Point(53, 56);
            this.nudSpace.Name = "nudSpace";
            this.nudSpace.Size = new System.Drawing.Size(63, 21);
            this.nudSpace.TabIndex = 3;
            // 
            // nudWidth
            // 
            this.nudWidth.Location = new System.Drawing.Point(53, 25);
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(63, 21);
            this.nudWidth.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "间隔：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "宽度：";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cboNormalization);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.nudSymbolSize);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Location = new System.Drawing.Point(304, 174);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(158, 94);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "符号";
            // 
            // cboNormalization
            // 
            this.cboNormalization.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNormalization.FormattingEnabled = true;
            this.cboNormalization.Location = new System.Drawing.Point(61, 58);
            this.cboNormalization.Name = "cboNormalization";
            this.cboNormalization.Size = new System.Drawing.Size(82, 20);
            this.cboNormalization.TabIndex = 3;
            this.cboNormalization.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "除数：";
            this.label6.Visible = false;
            // 
            // nudSymbolSize
            // 
            this.nudSymbolSize.Location = new System.Drawing.Point(61, 24);
            this.nudSymbolSize.Name = "nudSymbolSize";
            this.nudSymbolSize.Size = new System.Drawing.Size(60, 21);
            this.nudSymbolSize.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(119, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "像素";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "最大值：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbtColumn);
            this.groupBox3.Controls.Add(this.rbtBar);
            this.groupBox3.Controls.Add(this.pictureBox4);
            this.groupBox3.Controls.Add(this.pictureBox3);
            this.groupBox3.Location = new System.Drawing.Point(137, 173);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(158, 94);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "方向";
            // 
            // rbtColumn
            // 
            this.rbtColumn.AutoSize = true;
            this.rbtColumn.Location = new System.Drawing.Point(88, 74);
            this.rbtColumn.Name = "rbtColumn";
            this.rbtColumn.Size = new System.Drawing.Size(47, 16);
            this.rbtColumn.TabIndex = 2;
            this.rbtColumn.TabStop = true;
            this.rbtColumn.Text = "纵向";
            this.rbtColumn.UseVisualStyleBackColor = true;
            // 
            // rbtBar
            // 
            this.rbtBar.AutoSize = true;
            this.rbtBar.Location = new System.Drawing.Point(16, 75);
            this.rbtBar.Name = "rbtBar";
            this.rbtBar.Size = new System.Drawing.Size(47, 16);
            this.rbtBar.TabIndex = 1;
            this.rbtBar.TabStop = true;
            this.rbtBar.Text = "横向";
            this.rbtBar.UseVisualStyleBackColor = true;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(88, 17);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(53, 51);
            this.pictureBox4.TabIndex = 0;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(16, 17);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(53, 51);
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbxShowAxes);
            this.groupBox2.Controls.Add(this.sbtAxes);
            this.groupBox2.Location = new System.Drawing.Point(1, 173);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(128, 94);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "坐标轴";
            // 
            // cbxShowAxes
            // 
            this.cbxShowAxes.AutoSize = true;
            this.cbxShowAxes.Location = new System.Drawing.Point(20, 25);
            this.cbxShowAxes.Name = "cbxShowAxes";
            this.cbxShowAxes.Size = new System.Drawing.Size(48, 16);
            this.cbxShowAxes.TabIndex = 1;
            this.cbxShowAxes.Text = "显示";
            this.cbxShowAxes.UseVisualStyleBackColor = true;
            this.cbxShowAxes.CheckedChanged += new System.EventHandler(this.cbxShowAxes_CheckedChanged);
            // 
            // cbxNotOverlap
            // 
            this.cbxNotOverlap.AutoSize = true;
            this.cbxNotOverlap.Location = new System.Drawing.Point(18, 62);
            this.cbxNotOverlap.Name = "cbxNotOverlap";
            this.cbxNotOverlap.Size = new System.Drawing.Size(72, 16);
            this.cbxNotOverlap.TabIndex = 8;
            this.cbxNotOverlap.Text = "消除重叠";
            this.cbxNotOverlap.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "背景：";
            // 
            // butExclusionProperty
            // 
            this.butExclusionProperty.Location = new System.Drawing.Point(249, 59);
            this.butExclusionProperty.Name = "butExclusionProperty";
            this.butExclusionProperty.Size = new System.Drawing.Size(63, 23);
            this.butExclusionProperty.TabIndex = 4;
            this.butExclusionProperty.Text = "属性过滤";
            this.butExclusionProperty.UseVisualStyleBackColor = true;
            this.butExclusionProperty.Click += new System.EventHandler(this.butExclusionProperty_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.slvBarChart);
            this.groupBox1.Controls.Add(this.butDDDown);
            this.groupBox1.Controls.Add(this.butDDUp);
            this.groupBox1.Controls.Add(this.butRemoveAll);
            this.groupBox1.Controls.Add(this.butRemove);
            this.groupBox1.Controls.Add(this.butAdd);
            this.groupBox1.Controls.Add(this.lbxFields);
            this.groupBox1.Location = new System.Drawing.Point(1, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 167);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段选择";
            // 
            // butDDDown
            // 
            this.butDDDown.Image = ((System.Drawing.Image)(resources.GetObject("butDDDown.Image")));
            this.butDDDown.Location = new System.Drawing.Point(424, 92);
            this.butDDDown.Name = "butDDDown";
            this.butDDDown.Size = new System.Drawing.Size(25, 30);
            this.butDDDown.TabIndex = 6;
            this.butDDDown.UseVisualStyleBackColor = true;
            this.butDDDown.Click += new System.EventHandler(this.butDDDown_Click);
            // 
            // butDDUp
            // 
            this.butDDUp.Image = ((System.Drawing.Image)(resources.GetObject("butDDUp.Image")));
            this.butDDUp.Location = new System.Drawing.Point(424, 56);
            this.butDDUp.Name = "butDDUp";
            this.butDDUp.Size = new System.Drawing.Size(25, 30);
            this.butDDUp.TabIndex = 5;
            this.butDDUp.UseVisualStyleBackColor = true;
            this.butDDUp.Click += new System.EventHandler(this.butDDUp_Click);
            // 
            // butRemoveAll
            // 
            this.butRemoveAll.Location = new System.Drawing.Point(135, 114);
            this.butRemoveAll.Name = "butRemoveAll";
            this.butRemoveAll.Size = new System.Drawing.Size(34, 23);
            this.butRemoveAll.TabIndex = 2;
            this.butRemoveAll.Text = "<<";
            this.butRemoveAll.UseVisualStyleBackColor = true;
            this.butRemoveAll.Click += new System.EventHandler(this.butRemoveAll_Click);
            // 
            // butRemove
            // 
            this.butRemove.Location = new System.Drawing.Point(135, 79);
            this.butRemove.Name = "butRemove";
            this.butRemove.Size = new System.Drawing.Size(34, 23);
            this.butRemove.TabIndex = 2;
            this.butRemove.Text = "<";
            this.butRemove.UseVisualStyleBackColor = true;
            this.butRemove.Click += new System.EventHandler(this.butRemove_Click);
            // 
            // butAdd
            // 
            this.butAdd.Location = new System.Drawing.Point(135, 44);
            this.butAdd.Name = "butAdd";
            this.butAdd.Size = new System.Drawing.Size(34, 23);
            this.butAdd.TabIndex = 2;
            this.butAdd.Text = ">";
            this.butAdd.UseVisualStyleBackColor = true;
            this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
            // 
            // lbxFields
            // 
            this.lbxFields.FormattingEnabled = true;
            this.lbxFields.ItemHeight = 12;
            this.lbxFields.Location = new System.Drawing.Point(8, 20);
            this.lbxFields.Name = "lbxFields";
            this.lbxFields.Size = new System.Drawing.Size(120, 136);
            this.lbxFields.Sorted = true;
            this.lbxFields.TabIndex = 0;
            this.lbxFields.DoubleClick += new System.EventHandler(this.lbxFields_DoubleClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(27, 411);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(137, 85);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(204, 411);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(35, 33);
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.sbtBackground);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.cbo3DThickness);
            this.groupBox6.Controls.Add(this.chk3DDisplay);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.cbxNotOverlap);
            this.groupBox6.Controls.Add(this.butExclusionProperty);
            this.groupBox6.Location = new System.Drawing.Point(137, 272);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(325, 94);
            this.groupBox6.TabIndex = 16;
            this.groupBox6.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(144, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "三维厚度：";
            // 
            // cbo3DThickness
            // 
            this.cbo3DThickness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo3DThickness.FormattingEnabled = true;
            this.cbo3DThickness.Location = new System.Drawing.Point(144, 64);
            this.cbo3DThickness.Name = "cbo3DThickness";
            this.cbo3DThickness.Size = new System.Drawing.Size(72, 20);
            this.cbo3DThickness.TabIndex = 15;
            // 
            // chk3DDisplay
            // 
            this.chk3DDisplay.AutoSize = true;
            this.chk3DDisplay.Location = new System.Drawing.Point(144, 20);
            this.chk3DDisplay.Name = "chk3DDisplay";
            this.chk3DDisplay.Size = new System.Drawing.Size(72, 16);
            this.chk3DDisplay.TabIndex = 14;
            this.chk3DDisplay.Text = "三维视图";
            this.chk3DDisplay.UseVisualStyleBackColor = true;
            this.chk3DDisplay.CheckedChanged += new System.EventHandler(this.chk3DDisplay_CheckedChanged);
            // 
            // sbtBackground
            // 
            this.sbtBackground.Location = new System.Drawing.Point(63, 21);
            this.sbtBackground.Name = "sbtBackground";
            this.sbtBackground.Size = new System.Drawing.Size(66, 27);
            this.sbtBackground.Symbol = null;
            this.sbtBackground.TabIndex = 17;
            //this.sbtBackground.UseVisualStyleBackColor = true;
            this.sbtBackground.Click += new System.EventHandler(this.sbtBackground_Click);
            // 
            // sbtAxes
            // 
            this.sbtAxes.Location = new System.Drawing.Point(20, 53);
            this.sbtAxes.Name = "sbtAxes";
            this.sbtAxes.Size = new System.Drawing.Size(71, 23);
            this.sbtAxes.Symbol = null;
            this.sbtAxes.TabIndex = 17;
            //this.sbtAxes.UseVisualStyleBackColor = true;
            this.sbtAxes.Click += new System.EventHandler(this.sbtAxes_Click);
            // 
            // slvBarChart
            // 
            this.slvBarChart.FullRowSelect = true;
            this.slvBarChart.Location = new System.Drawing.Point(177, 20);
            this.slvBarChart.MultiSelect = false;
            this.slvBarChart.Name = "slvBarChart";
            this.slvBarChart.OwnerDraw = true;
            this.slvBarChart.Size = new System.Drawing.Size(238, 136);
            this.slvBarChart.TabIndex = 17;
            this.slvBarChart.UseCompatibleStateImageBehavior = false;
            this.slvBarChart.OnCellDblClick += new AG.COM.SDM.Utility.Controls.OnCellDblClickDelegate(this.slvBarChart_OnCellDblClick);
            // 
            // BarChartRendererControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "BarChartRendererControl";
            this.Size = new System.Drawing.Size(471, 375);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSymbolSize)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown nudSymbolSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbtColumn;
        private System.Windows.Forms.RadioButton rbtBar;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbxShowAxes;
        private System.Windows.Forms.CheckBox cbxNotOverlap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butExclusionProperty;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button butDDDown;
        private System.Windows.Forms.Button butDDUp;
        private System.Windows.Forms.Button butRemoveAll;
        private System.Windows.Forms.Button butRemove;
        private System.Windows.Forms.Button butAdd;
        private System.Windows.Forms.ListBox lbxFields;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown nudSpace;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboNormalization;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox chk3DDisplay;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbo3DThickness;
        private AG.COM.SDM.Utility.Controls.StyleButton sbtAxes;
        private AG.COM.SDM.Utility.Controls.StyleButton sbtBackground;
        private AG.COM.SDM.Utility.Controls.StyleListView slvBarChart;
    }
}
