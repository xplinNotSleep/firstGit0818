namespace AG.COM.SDM.StyleManager.Renderer
{
    partial class PieChartRendererControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PieChartRendererControl));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbtFieldSum = new System.Windows.Forms.RadioButton();
            this.nudSymbolSize = new System.Windows.Forms.NumericUpDown();
            this.rbtFixedSize = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbtArithmetric = new System.Windows.Forms.RadioButton();
            this.rbtGeographic = new System.Windows.Forms.RadioButton();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxShowOutline = new System.Windows.Forms.CheckBox();
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbo3DTilt = new System.Windows.Forms.ComboBox();
            this.cbo3DThickness = new System.Windows.Forms.ComboBox();
            this.chk3DDisplay = new System.Windows.Forms.CheckBox();
            this.sbtOutline = new AG.COM.SDM.Utility.Controls.StyleButton();
            this.sbtBackground = new AG.COM.SDM.Utility.Controls.StyleButton();
            this.slvPieChart = new AG.COM.SDM.Utility.Controls.StyleListView();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSymbolSize)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 429);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(138, 88);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(166, 445);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(36, 36);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbtFieldSum);
            this.groupBox5.Controls.Add(this.nudSymbolSize);
            this.groupBox5.Controls.Add(this.rbtFixedSize);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Location = new System.Drawing.Point(285, 174);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(178, 94);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "符号大小";
            // 
            // rbtFieldSum
            // 
            this.rbtFieldSum.AutoSize = true;
            this.rbtFieldSum.Location = new System.Drawing.Point(20, 67);
            this.rbtFieldSum.Name = "rbtFieldSum";
            this.rbtFieldSum.Size = new System.Drawing.Size(71, 16);
            this.rbtFieldSum.TabIndex = 1;
            this.rbtFieldSum.TabStop = true;
            this.rbtFieldSum.Text = "字段总和";
            this.rbtFieldSum.UseVisualStyleBackColor = true;
            this.rbtFieldSum.CheckedChanged += new System.EventHandler(this.rbtFieldSum_CheckedChanged);
            // 
            // nudSymbolSize
            // 
            this.nudSymbolSize.Location = new System.Drawing.Point(67, 16);
            this.nudSymbolSize.Name = "nudSymbolSize";
            this.nudSymbolSize.Size = new System.Drawing.Size(60, 21);
            this.nudSymbolSize.TabIndex = 1;
            this.nudSymbolSize.ValueChanged += new System.EventHandler(this.nudSymbolSize_ValueChanged);
            // 
            // rbtFixedSize
            // 
            this.rbtFixedSize.AutoSize = true;
            this.rbtFixedSize.Location = new System.Drawing.Point(20, 43);
            this.rbtFixedSize.Name = "rbtFixedSize";
            this.rbtFixedSize.Size = new System.Drawing.Size(71, 16);
            this.rbtFixedSize.TabIndex = 0;
            this.rbtFixedSize.TabStop = true;
            this.rbtFixedSize.Text = "固定大小";
            this.rbtFixedSize.UseVisualStyleBackColor = true;
            this.rbtFixedSize.CheckedChanged += new System.EventHandler(this.rbtFixedSize_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(127, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "像素";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "最小值：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbtArithmetric);
            this.groupBox3.Controls.Add(this.rbtGeographic);
            this.groupBox3.Controls.Add(this.pictureBox4);
            this.groupBox3.Controls.Add(this.pictureBox3);
            this.groupBox3.Location = new System.Drawing.Point(120, 173);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(155, 95);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "方向";
            // 
            // rbtArithmetric
            // 
            this.rbtArithmetric.AutoSize = true;
            this.rbtArithmetric.Location = new System.Drawing.Point(87, 74);
            this.rbtArithmetric.Name = "rbtArithmetric";
            this.rbtArithmetric.Size = new System.Drawing.Size(59, 16);
            this.rbtArithmetric.TabIndex = 2;
            this.rbtArithmetric.TabStop = true;
            this.rbtArithmetric.Text = "逆时针";
            this.rbtArithmetric.UseVisualStyleBackColor = true;
            // 
            // rbtGeographic
            // 
            this.rbtGeographic.AutoSize = true;
            this.rbtGeographic.Location = new System.Drawing.Point(15, 75);
            this.rbtGeographic.Name = "rbtGeographic";
            this.rbtGeographic.Size = new System.Drawing.Size(59, 16);
            this.rbtGeographic.TabIndex = 1;
            this.rbtGeographic.TabStop = true;
            this.rbtGeographic.Text = "顺时针";
            this.rbtGeographic.UseVisualStyleBackColor = true;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(87, 17);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(53, 51);
            this.pictureBox4.TabIndex = 0;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(15, 17);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(53, 51);
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.sbtOutline);
            this.groupBox2.Controls.Add(this.cbxShowOutline);
            this.groupBox2.Location = new System.Drawing.Point(0, 174);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(109, 94);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "边线";
            // 
            // cbxShowOutline
            // 
            this.cbxShowOutline.AutoSize = true;
            this.cbxShowOutline.Location = new System.Drawing.Point(8, 25);
            this.cbxShowOutline.Name = "cbxShowOutline";
            this.cbxShowOutline.Size = new System.Drawing.Size(48, 16);
            this.cbxShowOutline.TabIndex = 1;
            this.cbxShowOutline.Text = "显示";
            this.cbxShowOutline.UseVisualStyleBackColor = true;
            this.cbxShowOutline.CheckedChanged += new System.EventHandler(this.cbxShowOutline_CheckedChanged);
            // 
            // cbxNotOverlap
            // 
            this.cbxNotOverlap.AutoSize = true;
            this.cbxNotOverlap.Location = new System.Drawing.Point(16, 68);
            this.cbxNotOverlap.Name = "cbxNotOverlap";
            this.cbxNotOverlap.Size = new System.Drawing.Size(72, 16);
            this.cbxNotOverlap.TabIndex = 8;
            this.cbxNotOverlap.Text = "消除重叠";
            this.cbxNotOverlap.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "背景：";
            // 
            // butExclusionProperty
            // 
            this.butExclusionProperty.Location = new System.Drawing.Point(377, 62);
            this.butExclusionProperty.Name = "butExclusionProperty";
            this.butExclusionProperty.Size = new System.Drawing.Size(75, 23);
            this.butExclusionProperty.TabIndex = 4;
            this.butExclusionProperty.Text = "属性过滤";
            this.butExclusionProperty.UseVisualStyleBackColor = true;
            this.butExclusionProperty.Click += new System.EventHandler(this.butExclusionProperty_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.slvPieChart);
            this.groupBox1.Controls.Add(this.butDDDown);
            this.groupBox1.Controls.Add(this.butDDUp);
            this.groupBox1.Controls.Add(this.butRemoveAll);
            this.groupBox1.Controls.Add(this.butRemove);
            this.groupBox1.Controls.Add(this.butAdd);
            this.groupBox1.Controls.Add(this.lbxFields);
            this.groupBox1.Location = new System.Drawing.Point(0, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(463, 167);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段选择";
            // 
            // butDDDown
            // 
            this.butDDDown.Image = ((System.Drawing.Image)(resources.GetObject("butDDDown.Image")));
            this.butDDDown.Location = new System.Drawing.Point(425, 91);
            this.butDDDown.Name = "butDDDown";
            this.butDDDown.Size = new System.Drawing.Size(25, 30);
            this.butDDDown.TabIndex = 6;
            this.butDDDown.UseVisualStyleBackColor = true;
            this.butDDDown.Click += new System.EventHandler(this.butDDDown_Click);
            // 
            // butDDUp
            // 
            this.butDDUp.Image = ((System.Drawing.Image)(resources.GetObject("butDDUp.Image")));
            this.butDDUp.Location = new System.Drawing.Point(425, 55);
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
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.sbtBackground);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.cbo3DTilt);
            this.groupBox4.Controls.Add(this.cbo3DThickness);
            this.groupBox4.Controls.Add(this.chk3DDisplay);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.butExclusionProperty);
            this.groupBox4.Controls.Add(this.cbxNotOverlap);
            this.groupBox4.Location = new System.Drawing.Point(0, 271);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(463, 96);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(153, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "旋转角度：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(153, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "三维厚度：";
            // 
            // cbo3DTilt
            // 
            this.cbo3DTilt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo3DTilt.FormattingEnabled = true;
            this.cbo3DTilt.Location = new System.Drawing.Point(220, 65);
            this.cbo3DTilt.Name = "cbo3DTilt";
            this.cbo3DTilt.Size = new System.Drawing.Size(82, 20);
            this.cbo3DTilt.TabIndex = 15;
            // 
            // cbo3DThickness
            // 
            this.cbo3DThickness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo3DThickness.FormattingEnabled = true;
            this.cbo3DThickness.Location = new System.Drawing.Point(220, 40);
            this.cbo3DThickness.Name = "cbo3DThickness";
            this.cbo3DThickness.Size = new System.Drawing.Size(82, 20);
            this.cbo3DThickness.TabIndex = 15;
            // 
            // chk3DDisplay
            // 
            this.chk3DDisplay.AutoSize = true;
            this.chk3DDisplay.Location = new System.Drawing.Point(153, 20);
            this.chk3DDisplay.Name = "chk3DDisplay";
            this.chk3DDisplay.Size = new System.Drawing.Size(72, 16);
            this.chk3DDisplay.TabIndex = 14;
            this.chk3DDisplay.Text = "三维显示";
            this.chk3DDisplay.UseVisualStyleBackColor = true;
            this.chk3DDisplay.CheckedChanged += new System.EventHandler(this.chk3DDisplay_CheckedChanged);
            // 
            // sbtOutline
            // 
            this.sbtOutline.Location = new System.Drawing.Point(8, 54);
            this.sbtOutline.Name = "sbtOutline";
            this.sbtOutline.Size = new System.Drawing.Size(71, 23);
            this.sbtOutline.Symbol = null;
            this.sbtOutline.TabIndex = 3;
            //this.sbtOutline.UseVisualStyleBackColor = true;
            this.sbtOutline.Click += new System.EventHandler(this.sbtOutline_Click);
            // 
            // sbtBackground
            // 
            this.sbtBackground.Location = new System.Drawing.Point(59, 20);
            this.sbtBackground.Name = "sbtBackground";
            this.sbtBackground.Size = new System.Drawing.Size(69, 27);
            this.sbtBackground.Symbol = null;
            this.sbtBackground.TabIndex = 17;
            //this.sbtBackground.UseVisualStyleBackColor = true;
            this.sbtBackground.Click += new System.EventHandler(this.sbtBackground_Click);
            // 
            // slvPieChart
            // 
            this.slvPieChart.FullRowSelect = true;
            this.slvPieChart.Location = new System.Drawing.Point(178, 20);
            this.slvPieChart.MultiSelect = false;
            this.slvPieChart.Name = "slvPieChart";
            this.slvPieChart.OwnerDraw = true;
            this.slvPieChart.Size = new System.Drawing.Size(235, 136);
            this.slvPieChart.TabIndex = 18;
            this.slvPieChart.UseCompatibleStateImageBehavior = false;
            this.slvPieChart.OnCellDblClick += new AG.COM.SDM.Utility.Controls.OnCellDblClickDelegate(this.slvPieChart_OnCellDblClick);
            // 
            // PieChartRendererControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "PieChartRendererControl";
            this.Size = new System.Drawing.Size(473, 378);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
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
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button butExclusionProperty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button butDDDown;
        private System.Windows.Forms.Button butDDUp;
        private System.Windows.Forms.Button butRemoveAll;
        private System.Windows.Forms.Button butRemove;
        private System.Windows.Forms.Button butAdd;
        private System.Windows.Forms.ListBox lbxFields;
        private System.Windows.Forms.CheckBox cbxNotOverlap;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbxShowOutline;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbtArithmetric;
        private System.Windows.Forms.RadioButton rbtGeographic;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.RadioButton rbtFieldSum;
        private System.Windows.Forms.RadioButton rbtFixedSize;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudSymbolSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chk3DDisplay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbo3DTilt;
        private System.Windows.Forms.ComboBox cbo3DThickness;
        private AG.COM.SDM.Utility.Controls.StyleButton sbtOutline;
        private AG.COM.SDM.Utility.Controls.StyleButton sbtBackground;
        private AG.COM.SDM.Utility.Controls.StyleListView slvPieChart;
    }
}
