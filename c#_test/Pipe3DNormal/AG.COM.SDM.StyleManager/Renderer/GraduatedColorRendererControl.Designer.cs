namespace AG.COM.SDM.StyleManager.Renderer
{
    partial class ClassBreaksRendererControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassBreaksRendererControl));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboNormalization = new System.Windows.Forms.ComboBox();
            this.cboValue = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboClasses = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.scbColorRamp = new AG.COM.SDM.Utility.Controls.StyleComboBox();
            this.slvGCRenderer = new AG.COM.SDM.Utility.Controls.StyleListView();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(173, 398);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(129, 84);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(338, 398);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(37, 34);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboNormalization);
            this.groupBox1.Controls.Add(this.cboValue);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(1, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 86);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段";
            // 
            // cboNormalization
            // 
            this.cboNormalization.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNormalization.FormattingEnabled = true;
            this.cboNormalization.Location = new System.Drawing.Point(84, 52);
            this.cboNormalization.Name = "cboNormalization";
            this.cboNormalization.Size = new System.Drawing.Size(133, 20);
            this.cboNormalization.TabIndex = 3;
            this.cboNormalization.SelectedIndexChanged += new System.EventHandler(this.cboNormalization_SelectedIndexChanged);
            // 
            // cboValue
            // 
            this.cboValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboValue.FormattingEnabled = true;
            this.cboValue.Location = new System.Drawing.Point(84, 20);
            this.cboValue.Name = "cboValue";
            this.cboValue.Size = new System.Drawing.Size(133, 20);
            this.cboValue.TabIndex = 2;
            this.cboValue.SelectedIndexChanged += new System.EventHandler(this.cboValue_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "除数字段：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "被除数字段：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboClasses);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(244, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(217, 50);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "分级";
            // 
            // cboClasses
            // 
            this.cboClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClasses.FormattingEnabled = true;
            this.cboClasses.Location = new System.Drawing.Point(88, 16);
            this.cboClasses.Name = "cboClasses";
            this.cboClasses.Size = new System.Drawing.Size(91, 20);
            this.cboClasses.TabIndex = 5;
            this.cboClasses.SelectedIndexChanged += new System.EventHandler(this.cboClasses_SelectedIndexChanged_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "分级个数：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(242, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "颜色带：";
            // 
            // scbColorRamp
            // 
            this.scbColorRamp.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.scbColorRamp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scbColorRamp.FormattingEnabled = true;
            this.scbColorRamp.Location = new System.Drawing.Point(291, 55);
            this.scbColorRamp.Name = "scbColorRamp";
            this.scbColorRamp.Size = new System.Drawing.Size(170, 22);
            this.scbColorRamp.StyleGalleryClass = null;
            this.scbColorRamp.TabIndex = 7;
            this.scbColorRamp.SelectedIndexChanged += new System.EventHandler(this.scbColorRamp_SelectedIndexChanged);
            // 
            // slvGCRenderer
            // 
            this.slvGCRenderer.FullRowSelect = true;
            this.slvGCRenderer.Location = new System.Drawing.Point(1, 92);
            this.slvGCRenderer.Name = "slvGCRenderer";
            this.slvGCRenderer.OwnerDraw = true;
            this.slvGCRenderer.Size = new System.Drawing.Size(460, 274);
            this.slvGCRenderer.TabIndex = 8;
            this.slvGCRenderer.UseCompatibleStateImageBehavior = false;
            this.slvGCRenderer.OnCellDblClick += new AG.COM.SDM.Utility.Controls.OnCellDblClickDelegate(this.slvGCRenderer_OnCellDblClick);
            // 
            // ClassBreaksRendererControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.slvGCRenderer);
            this.Controls.Add(this.scbColorRamp);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ClassBreaksRendererControl";
            this.Size = new System.Drawing.Size(470, 375);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboNormalization;
        private System.Windows.Forms.ComboBox cboValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboClasses;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private AG.COM.SDM.Utility.Controls.StyleComboBox scbColorRamp;
        private AG.COM.SDM.Utility.Controls.StyleListView slvGCRenderer;


    }
}
