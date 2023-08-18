namespace AG.Pipe.Analyst3DModel
{
    partial class FormTable2Spatial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTable2Spatial));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tool_NewScheme = new System.Windows.Forms.ToolStripButton();
            this.tool_OpenScheme = new System.Windows.Forms.ToolStripButton();
            this.tool_SaveScheme = new System.Windows.Forms.ToolStripButton();
            this.tool_SaveAsScheme = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tool_AddData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tool_RunCheck = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.treeCheckScheme = new System.Windows.Forms.TreeView();
            this.propNodeAttribute = new System.Windows.Forms.PropertyGrid();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_NewScheme,
            this.tool_OpenScheme,
            this.tool_SaveScheme,
            this.tool_SaveAsScheme,
            this.toolStripSeparator1,
            this.tool_AddData,
            this.toolStripSeparator2,
            this.tool_RunCheck,
            this.toolStripSeparator5,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1032, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tool_NewScheme
            // 
            this.tool_NewScheme.Image = ((System.Drawing.Image)(resources.GetObject("tool_NewScheme.Image")));
            this.tool_NewScheme.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_NewScheme.Name = "tool_NewScheme";
            this.tool_NewScheme.Size = new System.Drawing.Size(63, 24);
            this.tool_NewScheme.Text = "新建";
            this.tool_NewScheme.Click += new System.EventHandler(this.menu_NewScheme_Click);
            // 
            // tool_OpenScheme
            // 
            this.tool_OpenScheme.Image = ((System.Drawing.Image)(resources.GetObject("tool_OpenScheme.Image")));
            this.tool_OpenScheme.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_OpenScheme.Name = "tool_OpenScheme";
            this.tool_OpenScheme.Size = new System.Drawing.Size(63, 24);
            this.tool_OpenScheme.Text = "打开";
            this.tool_OpenScheme.Click += new System.EventHandler(this.menu_OpenScheme_Click);
            // 
            // tool_SaveScheme
            // 
            this.tool_SaveScheme.Image = ((System.Drawing.Image)(resources.GetObject("tool_SaveScheme.Image")));
            this.tool_SaveScheme.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_SaveScheme.Name = "tool_SaveScheme";
            this.tool_SaveScheme.Size = new System.Drawing.Size(63, 24);
            this.tool_SaveScheme.Text = "保存";
            this.tool_SaveScheme.Click += new System.EventHandler(this.menu_SaveScheme_Click);
            // 
            // tool_SaveAsScheme
            // 
            this.tool_SaveAsScheme.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_SaveAsScheme.Image = ((System.Drawing.Image)(resources.GetObject("tool_SaveAsScheme.Image")));
            this.tool_SaveAsScheme.ImageTransparentColor = System.Drawing.Color.Gray;
            this.tool_SaveAsScheme.Name = "tool_SaveAsScheme";
            this.tool_SaveAsScheme.Size = new System.Drawing.Size(29, 24);
            this.tool_SaveAsScheme.Text = "另存为";
            this.tool_SaveAsScheme.Click += new System.EventHandler(this.menu_SavaAsScheme_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // tool_AddData
            // 
            this.tool_AddData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_AddData.Image = ((System.Drawing.Image)(resources.GetObject("tool_AddData.Image")));
            this.tool_AddData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_AddData.Name = "tool_AddData";
            this.tool_AddData.Size = new System.Drawing.Size(29, 24);
            this.tool_AddData.Text = "加载数据";
            this.tool_AddData.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            this.toolStripSeparator2.Visible = false;
            // 
            // tool_RunCheck
            // 
            this.tool_RunCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_RunCheck.Image = ((System.Drawing.Image)(resources.GetObject("tool_RunCheck.Image")));
            this.tool_RunCheck.ImageTransparentColor = System.Drawing.SystemColors.Window;
            this.tool_RunCheck.Name = "tool_RunCheck";
            this.tool_RunCheck.Size = new System.Drawing.Size(29, 24);
            this.tool_RunCheck.Text = "开始转换";
            this.tool_RunCheck.Click += new System.EventHandler(this.menu_RunCheck_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(88, 24);
            this.toolStripButton1.Text = "设置数据源";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(103, 24);
            this.toolStripButton2.Text = "保存转换日志";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // treeCheckScheme
            // 
            this.treeCheckScheme.CheckBoxes = true;
            this.treeCheckScheme.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeCheckScheme.ItemHeight = 18;
            this.treeCheckScheme.Location = new System.Drawing.Point(0, 27);
            this.treeCheckScheme.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treeCheckScheme.Name = "treeCheckScheme";
            this.treeCheckScheme.Size = new System.Drawing.Size(325, 707);
            this.treeCheckScheme.TabIndex = 6;
            this.treeCheckScheme.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeCheckScheme_AfterCheck);
            this.treeCheckScheme.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeCheckScheme_AfterSelect);
            // 
            // propNodeAttribute
            // 
            this.propNodeAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propNodeAttribute.Location = new System.Drawing.Point(325, 27);
            this.propNodeAttribute.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.propNodeAttribute.Name = "propNodeAttribute";
            this.propNodeAttribute.Size = new System.Drawing.Size(707, 707);
            this.propNodeAttribute.TabIndex = 7;
            // 
            // FormTable2Spatial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 734);
            this.Controls.Add(this.propNodeAttribute);
            this.Controls.Add(this.treeCheckScheme);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.LookAndFeel.SkinName = "VS2010";
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "FormTable2Spatial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图形化工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            this.Load += new System.EventHandler(this.FormTable2Spatial_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tool_AddData;
        private System.Windows.Forms.ToolStripButton tool_OpenScheme;
        private System.Windows.Forms.ToolStripButton tool_SaveScheme;
        private System.Windows.Forms.ToolStripButton tool_SaveAsScheme;

        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tool_RunCheck;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tool_NewScheme;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.TreeView treeCheckScheme;
        private System.Windows.Forms.PropertyGrid propNodeAttribute;
    }
}

