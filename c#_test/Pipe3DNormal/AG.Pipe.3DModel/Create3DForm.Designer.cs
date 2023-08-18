
namespace AG.Pipe.Analyst3DModel
{
    partial class Create3DForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Create3DForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.生成模型ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.符号管理器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axSceneControl1 = new ESRI.ArcGIS.Controls.AxSceneControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加数据 = new System.Windows.Forms.ToolStripMenuItem();
            this.移除图层 = new System.Windows.Forms.ToolStripMenuItem();
            this.全部移除 = new System.Windows.Forms.ToolStripMenuItem();
            this.更改颜色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加数据1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axSceneControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.生成模型ToolStripMenuItem,
            this.符号管理器ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1176, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 生成模型ToolStripMenuItem
            // 
            this.生成模型ToolStripMenuItem.Name = "生成模型ToolStripMenuItem";
            this.生成模型ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.生成模型ToolStripMenuItem.Text = "生成管线模型";
            this.生成模型ToolStripMenuItem.Click += new System.EventHandler(this.生成模型ToolStripMenuItem_Click);
            // 
            // 符号管理器ToolStripMenuItem
            // 
            this.符号管理器ToolStripMenuItem.Name = "符号管理器ToolStripMenuItem";
            this.符号管理器ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.符号管理器ToolStripMenuItem.Text = "符号管理器";
            this.符号管理器ToolStripMenuItem.Click += new System.EventHandler(this.符号管理器ToolStripMenuItem_Click);
            // 
            // axSceneControl1
            // 
            this.axSceneControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axSceneControl1.Location = new System.Drawing.Point(0, 0);
            this.axSceneControl1.Name = "axSceneControl1";
            this.axSceneControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSceneControl1.OcxState")));
            this.axSceneControl1.Size = new System.Drawing.Size(940, 762);
            this.axSceneControl1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 59);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.axTOCControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.axSceneControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1176, 762);
            this.splitContainer1.SplitterDistance = 231;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 2;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTOCControl1.Location = new System.Drawing.Point(0, 0);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(231, 762);
            this.axTOCControl1.TabIndex = 0;
            this.axTOCControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl1_OnMouseDown);
            this.axTOCControl1.OnDoubleClick += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnDoubleClickEventHandler(this.axTOCControl_OnDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.axToolbarControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1176, 34);
            this.panel1.TabIndex = 3;
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 0);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(1176, 28);
            this.axToolbarControl1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加数据,
            this.移除图层,
            this.全部移除,
            this.更改颜色ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 92);
            // 
            // 添加数据
            // 
            this.添加数据.Name = "添加数据";
            this.添加数据.Size = new System.Drawing.Size(124, 22);
            this.添加数据.Text = "添加数据";
            this.添加数据.Click += new System.EventHandler(this.添加数据_Click);
            // 
            // 移除图层
            // 
            this.移除图层.Name = "移除图层";
            this.移除图层.Size = new System.Drawing.Size(124, 22);
            this.移除图层.Text = "移除图层";
            this.移除图层.Click += new System.EventHandler(this.移除图层_Click);
            // 
            // 全部移除
            // 
            this.全部移除.Name = "全部移除";
            this.全部移除.Size = new System.Drawing.Size(124, 22);
            this.全部移除.Text = "全部移除";
            this.全部移除.Click += new System.EventHandler(this.全部移除_Click);
            // 
            // 更改颜色ToolStripMenuItem
            // 
            this.更改颜色ToolStripMenuItem.Name = "更改颜色ToolStripMenuItem";
            this.更改颜色ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.更改颜色ToolStripMenuItem.Text = "更改颜色";
            this.更改颜色ToolStripMenuItem.Visible = false;
            this.更改颜色ToolStripMenuItem.Click += new System.EventHandler(this.更改颜色_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加数据1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(125, 26);
            // 
            // 添加数据1
            // 
            this.添加数据1.Name = "添加数据1";
            this.添加数据1.Size = new System.Drawing.Size(124, 22);
            this.添加数据1.Text = "添加数据";
            this.添加数据1.Click += new System.EventHandler(this.添加数据_Click);
            // 
            // Create3DForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 821);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "VS2010";
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Create3DForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "三维建模工具标准版";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axSceneControl1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private ESRI.ArcGIS.Controls.AxSceneControl axSceneControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 添加数据;
        private System.Windows.Forms.ToolStripMenuItem 移除图层;
        private System.Windows.Forms.ToolStripMenuItem 全部移除;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 添加数据1;
        private System.Windows.Forms.ToolStripMenuItem 更改颜色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生成模型ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 符号管理器ToolStripMenuItem;
    }
}