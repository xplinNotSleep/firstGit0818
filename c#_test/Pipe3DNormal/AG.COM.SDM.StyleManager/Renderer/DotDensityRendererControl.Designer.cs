namespace AG.COM.SDM.StyleManager.Renderer
{
    partial class DotDensityRendererControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DotDensityRendererControl));
            this.tbxDotValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboDotSymbolSize = new System.Windows.Forms.ComboBox();
            this.butExclusionProperty = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.butDDDown = new System.Windows.Forms.Button();
            this.butDDUp = new System.Windows.Forms.Button();
            this.butRemoveAll = new System.Windows.Forms.Button();
            this.butRemove = new System.Windows.Forms.Button();
            this.butAdd = new System.Windows.Forms.Button();
            this.lbxFields = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbtFixPlace = new System.Windows.Forms.RadioButton();
            this.rbtNotFixPlace = new System.Windows.Forms.RadioButton();
            this.cpkBackgroundColor = new AG.COM.SDM.Utility.Controls.ColorPicker();
            this.sbtBorderline = new AG.COM.SDM.Utility.Controls.StyleButton();
            this.slvDotDensity = new AG.COM.SDM.Utility.Controls.StyleListView();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxDotValue
            // 
            this.tbxDotValue.Location = new System.Drawing.Point(116, 55);
            this.tbxDotValue.Name = "tbxDotValue";
            this.tbxDotValue.Size = new System.Drawing.Size(75, 21);
            this.tbxDotValue.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "点值大小：";
            // 
            // cboDotSymbolSize
            // 
            this.cboDotSymbolSize.FormattingEnabled = true;
            this.cboDotSymbolSize.Location = new System.Drawing.Point(116, 25);
            this.cboDotSymbolSize.Name = "cboDotSymbolSize";
            this.cboDotSymbolSize.Size = new System.Drawing.Size(75, 20);
            this.cboDotSymbolSize.TabIndex = 5;
            this.cboDotSymbolSize.SelectedIndexChanged += new System.EventHandler(this.cboDotSymbolSize_SelectedIndexChanged);
            // 
            // butExclusionProperty
            // 
            this.butExclusionProperty.Location = new System.Drawing.Point(137, 161);
            this.butExclusionProperty.Name = "butExclusionProperty";
            this.butExclusionProperty.Size = new System.Drawing.Size(75, 23);
            this.butExclusionProperty.TabIndex = 4;
            this.butExclusionProperty.Text = "属性过滤";
            this.butExclusionProperty.UseVisualStyleBackColor = true;
            this.butExclusionProperty.Click += new System.EventHandler(this.butExclusionProperty_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "点符号大小：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.sbtBorderline);
            this.groupBox2.Controls.Add(this.cpkBackgroundColor);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(2, 174);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(228, 192);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "背景";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "背景色：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "边线：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.slvDotDensity);
            this.groupBox1.Controls.Add(this.butDDDown);
            this.groupBox1.Controls.Add(this.butDDUp);
            this.groupBox1.Controls.Add(this.butRemoveAll);
            this.groupBox1.Controls.Add(this.butRemove);
            this.groupBox1.Controls.Add(this.butAdd);
            this.groupBox1.Controls.Add(this.lbxFields);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 167);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段选择";
            // 
            // butDDDown
            // 
            this.butDDDown.Image = ((System.Drawing.Image)(resources.GetObject("butDDDown.Image")));
            this.butDDDown.Location = new System.Drawing.Point(422, 92);
            this.butDDDown.Name = "butDDDown";
            this.butDDDown.Size = new System.Drawing.Size(25, 30);
            this.butDDDown.TabIndex = 6;
            this.butDDDown.UseVisualStyleBackColor = true;
            this.butDDDown.Click += new System.EventHandler(this.butDDDown_Click);
            // 
            // butDDUp
            // 
            this.butDDUp.Image = ((System.Drawing.Image)(resources.GetObject("butDDUp.Image")));
            this.butDDUp.Location = new System.Drawing.Point(422, 56);
            this.butDDUp.Name = "butDDUp";
            this.butDDUp.Size = new System.Drawing.Size(25, 30);
            this.butDDUp.TabIndex = 5;
            this.butDDUp.UseVisualStyleBackColor = true;
            this.butDDUp.Click += new System.EventHandler(this.butDDUp_Click);
            // 
            // butRemoveAll
            // 
            this.butRemoveAll.Location = new System.Drawing.Point(138, 114);
            this.butRemoveAll.Name = "butRemoveAll";
            this.butRemoveAll.Size = new System.Drawing.Size(34, 23);
            this.butRemoveAll.TabIndex = 2;
            this.butRemoveAll.Text = "<<";
            this.butRemoveAll.UseVisualStyleBackColor = true;
            this.butRemoveAll.Click += new System.EventHandler(this.butRemoveAll_Click);
            // 
            // butRemove
            // 
            this.butRemove.Location = new System.Drawing.Point(138, 79);
            this.butRemove.Name = "butRemove";
            this.butRemove.Size = new System.Drawing.Size(34, 23);
            this.butRemove.TabIndex = 2;
            this.butRemove.Text = "<";
            this.butRemove.UseVisualStyleBackColor = true;
            this.butRemove.Click += new System.EventHandler(this.butRemove_Click);
            // 
            // butAdd
            // 
            this.butAdd.Location = new System.Drawing.Point(138, 44);
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
            this.lbxFields.Location = new System.Drawing.Point(10, 20);
            this.lbxFields.Name = "lbxFields";
            this.lbxFields.Size = new System.Drawing.Size(120, 136);
            this.lbxFields.Sorted = true;
            this.lbxFields.TabIndex = 0;
            this.lbxFields.DoubleClick += new System.EventHandler(this.lbxFields_DoubleClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(58, 409);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(130, 86);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(246, 409);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(35, 35);
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbtFixPlace);
            this.groupBox3.Controls.Add(this.butExclusionProperty);
            this.groupBox3.Controls.Add(this.rbtNotFixPlace);
            this.groupBox3.Controls.Add(this.tbxDotValue);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.cboDotSymbolSize);
            this.groupBox3.Location = new System.Drawing.Point(241, 174);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(221, 192);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "点状";
            // 
            // rbtFixPlace
            // 
            this.rbtFixPlace.AutoSize = true;
            this.rbtFixPlace.Location = new System.Drawing.Point(30, 116);
            this.rbtFixPlace.Name = "rbtFixPlace";
            this.rbtFixPlace.Size = new System.Drawing.Size(71, 16);
            this.rbtFixPlace.TabIndex = 8;
            this.rbtFixPlace.TabStop = true;
            this.rbtFixPlace.Text = "固定位置";
            this.rbtFixPlace.UseVisualStyleBackColor = true;
            // 
            // rbtNotFixPlace
            // 
            this.rbtNotFixPlace.AutoSize = true;
            this.rbtNotFixPlace.Location = new System.Drawing.Point(30, 94);
            this.rbtNotFixPlace.Name = "rbtNotFixPlace";
            this.rbtNotFixPlace.Size = new System.Drawing.Size(83, 16);
            this.rbtNotFixPlace.TabIndex = 8;
            this.rbtNotFixPlace.TabStop = true;
            this.rbtNotFixPlace.Text = "不固定位置";
            this.rbtNotFixPlace.UseVisualStyleBackColor = true;
            // 
            // cpkBackgroundColor
            // 
            this.cpkBackgroundColor.BackColor = System.Drawing.SystemColors.Window;
            this.cpkBackgroundColor.Context = null;
            this.cpkBackgroundColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cpkBackgroundColor.Location = new System.Drawing.Point(69, 79);
            this.cpkBackgroundColor.Name = "cpkBackgroundColor";
            this.cpkBackgroundColor.ReadOnly = false;
            this.cpkBackgroundColor.Size = new System.Drawing.Size(145, 21);
            this.cpkBackgroundColor.TabIndex = 4;
            this.cpkBackgroundColor.Text = "White";
            this.cpkBackgroundColor.Value = System.Drawing.Color.White;
            // 
            // sbtBorderline
            // 
            this.sbtBorderline.Location = new System.Drawing.Point(84, 25);
            this.sbtBorderline.Name = "sbtBorderline";
            this.sbtBorderline.Size = new System.Drawing.Size(88, 31);
            this.sbtBorderline.Symbol = null;
            this.sbtBorderline.TabIndex = 5;
            //this.sbtBorderline.UseVisualStyleBackColor = true;
            this.sbtBorderline.Click += new System.EventHandler(this.sbtBorderline_Click);
            // 
            // slvDotDensity
            // 
            this.slvDotDensity.FullRowSelect = true;
            this.slvDotDensity.Location = new System.Drawing.Point(180, 20);
            this.slvDotDensity.MultiSelect = false;
            this.slvDotDensity.Name = "slvDotDensity";
            this.slvDotDensity.OwnerDraw = true;
            this.slvDotDensity.Size = new System.Drawing.Size(232, 136);
            this.slvDotDensity.TabIndex = 6;
            this.slvDotDensity.UseCompatibleStateImageBehavior = false;
            this.slvDotDensity.OnCellDblClick += new AG.COM.SDM.Utility.Controls.OnCellDblClickDelegate(this.slvDotDensity_OnCellDblClick);
            // 
            // DotDensityRendererControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Name = "DotDensityRendererControl";
            this.Size = new System.Drawing.Size(468, 372);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button butRemoveAll;
        private System.Windows.Forms.Button butRemove;
        private System.Windows.Forms.Button butAdd;
        private System.Windows.Forms.ListBox lbxFields;
        private System.Windows.Forms.Button butDDDown;
        private System.Windows.Forms.Button butDDUp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button butExclusionProperty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ComboBox cboDotSymbolSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxDotValue;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbtFixPlace;
        private System.Windows.Forms.RadioButton rbtNotFixPlace;
        private AG.COM.SDM.Utility.Controls.StyleButton sbtBorderline;
        private AG.COM.SDM.Utility.Controls.ColorPicker cpkBackgroundColor;
        private AG.COM.SDM.Utility.Controls.StyleListView slvDotDensity;
    }
}
