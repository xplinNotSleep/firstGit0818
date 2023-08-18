
namespace AG.Pipe.Analyst3DModel
{
    partial class FormPipe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPipe));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tool_SaveScheme = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tool_RunCheck = new System.Windows.Forms.ToolStripButton();
            this.tool_SetDataSource = new System.Windows.Forms.ToolStripButton();
            this.treeCheckScheme = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.axSceneControl1 = new ESRI.ArcGIS.Controls.AxSceneControl();
            this.propNodeAttribute = new System.Windows.Forms.PropertyGrid();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新建模型方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建图层组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新增管线模型方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新增管点模型方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.一键设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新建图层模型ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除模型方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axSceneControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_SaveScheme,
            this.toolStripSeparator2,
            this.tool_RunCheck,
            this.tool_SetDataSource});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1066, 27);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tool_SaveScheme
            // 
            this.tool_SaveScheme.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_SaveScheme.Image = ((System.Drawing.Image)(resources.GetObject("tool_SaveScheme.Image")));
            this.tool_SaveScheme.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_SaveScheme.Name = "tool_SaveScheme";
            this.tool_SaveScheme.Size = new System.Drawing.Size(29, 24);
            this.tool_SaveScheme.Text = "保存数据核查方案";
            this.tool_SaveScheme.Click += new System.EventHandler(this.tool_SaveScheme_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // tool_RunCheck
            // 
            this.tool_RunCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_RunCheck.Image = ((System.Drawing.Image)(resources.GetObject("tool_RunCheck.Image")));
            this.tool_RunCheck.ImageTransparentColor = System.Drawing.SystemColors.Window;
            this.tool_RunCheck.Name = "tool_RunCheck";
            this.tool_RunCheck.Size = new System.Drawing.Size(29, 24);
            this.tool_RunCheck.Text = "运行";
            this.tool_RunCheck.Click += new System.EventHandler(this.tool_RunCheck_Click);
            // 
            // tool_SetDataSource
            // 
            this.tool_SetDataSource.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_SetDataSource.Image = ((System.Drawing.Image)(resources.GetObject("tool_SetDataSource.Image")));
            this.tool_SetDataSource.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_SetDataSource.Name = "tool_SetDataSource";
            this.tool_SetDataSource.Size = new System.Drawing.Size(29, 24);
            this.tool_SetDataSource.Text = "设置数据源";
            this.tool_SetDataSource.Click += new System.EventHandler(this.tool_SetDataSource_Click);
            // 
            // treeCheckScheme
            // 
            this.treeCheckScheme.CheckBoxes = true;
            this.treeCheckScheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeCheckScheme.ItemHeight = 18;
            this.treeCheckScheme.Location = new System.Drawing.Point(0, 0);
            this.treeCheckScheme.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treeCheckScheme.Name = "treeCheckScheme";
            this.treeCheckScheme.Size = new System.Drawing.Size(353, 908);
            this.treeCheckScheme.TabIndex = 0;
            this.treeCheckScheme.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeCheckScheme_AfterCheck);
            this.treeCheckScheme.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeCheckScheme_AfterSelect);
            this.treeCheckScheme.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeCheckScheme_NodeMouseClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeCheckScheme);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.axSceneControl1);
            this.splitContainer1.Panel2.Controls.Add(this.propNodeAttribute);
            this.splitContainer1.Size = new System.Drawing.Size(1066, 908);
            this.splitContainer1.SplitterDistance = 353;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 7;
            // 
            // axSceneControl1
            // 
            this.axSceneControl1.Location = new System.Drawing.Point(84, 457);
            this.axSceneControl1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.axSceneControl1.Name = "axSceneControl1";
            this.axSceneControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSceneControl1.OcxState")));
            this.axSceneControl1.Size = new System.Drawing.Size(331, 331);
            this.axSceneControl1.TabIndex = 1;
            this.axSceneControl1.Visible = false;
            // 
            // propNodeAttribute
            // 
            this.propNodeAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propNodeAttribute.Location = new System.Drawing.Point(0, 0);
            this.propNodeAttribute.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.propNodeAttribute.Name = "propNodeAttribute";
            this.propNodeAttribute.Size = new System.Drawing.Size(707, 908);
            this.propNodeAttribute.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建模型方案ToolStripMenuItem,
            this.新建图层组ToolStripMenuItem,
            this.复制ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(169, 76);
            // 
            // 新建模型方案ToolStripMenuItem
            // 
            this.新建模型方案ToolStripMenuItem.Name = "新建模型方案ToolStripMenuItem";
            this.新建模型方案ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.新建模型方案ToolStripMenuItem.Text = "新建模型方案";
            this.新建模型方案ToolStripMenuItem.Click += new System.EventHandler(this.新建模型方案ToolStripMenuItem_Click);
            // 
            // 新建图层组ToolStripMenuItem
            // 
            this.新建图层组ToolStripMenuItem.Name = "新建图层组ToolStripMenuItem";
            this.新建图层组ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.新建图层组ToolStripMenuItem.Text = "新建图层组";
            this.新建图层组ToolStripMenuItem.Click += new System.EventHandler(this.新建图层组ToolStripMenuItem_Click);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新增管线模型方案ToolStripMenuItem,
            this.新增管点模型方案ToolStripMenuItem,
            this.一键设置ToolStripMenuItem,
            this.删除ToolStripMenuItem,
            this.toolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(199, 124);
            // 
            // 新增管线模型方案ToolStripMenuItem
            // 
            this.新增管线模型方案ToolStripMenuItem.Name = "新增管线模型方案ToolStripMenuItem";
            this.新增管线模型方案ToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.新增管线模型方案ToolStripMenuItem.Text = "新增管线模型方案";
            this.新增管线模型方案ToolStripMenuItem.Visible = false;
            // 
            // 新增管点模型方案ToolStripMenuItem
            // 
            this.新增管点模型方案ToolStripMenuItem.Name = "新增管点模型方案ToolStripMenuItem";
            this.新增管点模型方案ToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.新增管点模型方案ToolStripMenuItem.Text = "新增管点模型方案";
            this.新增管点模型方案ToolStripMenuItem.Visible = false;
            // 
            // 一键设置ToolStripMenuItem
            // 
            this.一键设置ToolStripMenuItem.Name = "一键设置ToolStripMenuItem";
            this.一键设置ToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.一键设置ToolStripMenuItem.Text = "一键设置";
            this.一键设置ToolStripMenuItem.Click += new System.EventHandler(this.一键设置ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(198, 24);
            this.toolStripMenuItem1.Text = "复制";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建图层模型ToolStripMenuItem,
            this.粘贴ToolStripMenuItem,
            this.删除模型方案ToolStripMenuItem});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(169, 76);
            // 
            // 新建图层模型ToolStripMenuItem
            // 
            this.新建图层模型ToolStripMenuItem.Name = "新建图层模型ToolStripMenuItem";
            this.新建图层模型ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.新建图层模型ToolStripMenuItem.Text = "新建图层模型";
            this.新建图层模型ToolStripMenuItem.Click += new System.EventHandler(this.新建图层模型ToolStripMenuItem_Click);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem_Click);
            // 
            // 删除模型方案ToolStripMenuItem
            // 
            this.删除模型方案ToolStripMenuItem.Name = "删除模型方案ToolStripMenuItem";
            this.删除模型方案ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.删除模型方案ToolStripMenuItem.Text = "删除方案";
            this.删除模型方案ToolStripMenuItem.Click += new System.EventHandler(this.删除模型方案ToolStripMenuItem_Click);
            // 
            // FormPipe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 935);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "VS2010";
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "FormPipe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "管线模型转换方案";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axSceneControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tool_SaveScheme;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tool_RunCheck;
        private System.Windows.Forms.ToolStripButton tool_SetDataSource;
        private System.Windows.Forms.TreeView treeCheckScheme;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid propNodeAttribute;
        private ESRI.ArcGIS.Controls.AxSceneControl axSceneControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 新建模型方案ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 新增管线模型方案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新增管点模型方案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 一键设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建图层组ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem 新建图层模型ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 删除模型方案ToolStripMenuItem;
    }
}