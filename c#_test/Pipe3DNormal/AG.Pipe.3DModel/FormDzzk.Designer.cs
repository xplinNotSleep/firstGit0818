
namespace AG.Pipe.Analyst3DModel
{
    partial class FormDzzk
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDzzk));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新建模型方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeCheckScheme = new System.Windows.Forms.TreeView();
            this.axSceneControl1 = new ESRI.ArcGIS.Controls.AxSceneControl();
            this.propNodeAttribute = new System.Windows.Forms.PropertyGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tool_SaveScheme = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tool_RunCheck = new System.Windows.Forms.ToolStripButton();
            this.tool_SetDataSource = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axSceneControl1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建模型方案ToolStripMenuItem,
            this.粘贴ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 48);
            // 
            // 新建模型方案ToolStripMenuItem
            // 
            this.新建模型方案ToolStripMenuItem.Name = "新建模型方案ToolStripMenuItem";
            this.新建模型方案ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.新建模型方案ToolStripMenuItem.Text = "新建模型方案";
            this.新建模型方案ToolStripMenuItem.Click += new System.EventHandler(this.新建模型方案ToolStripMenuItem_Click);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem,
            this.toolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(101, 48);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuItem1.Text = "复制";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(61, 4);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
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
            this.splitContainer1.Size = new System.Drawing.Size(933, 587);
            this.splitContainer1.SplitterDistance = 323;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 7;
            // 
            // treeCheckScheme
            // 
            this.treeCheckScheme.CheckBoxes = true;
            this.treeCheckScheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeCheckScheme.ItemHeight = 18;
            this.treeCheckScheme.Location = new System.Drawing.Point(0, 0);
            this.treeCheckScheme.Name = "treeCheckScheme";
            this.treeCheckScheme.Size = new System.Drawing.Size(323, 587);
            this.treeCheckScheme.TabIndex = 0;
            this.treeCheckScheme.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeCheckScheme_AfterCheck);
            this.treeCheckScheme.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeCheckScheme_AfterSelect);
            this.treeCheckScheme.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeCheckScheme_NodeMouseClick);
            // 
            // axSceneControl1
            // 
            this.axSceneControl1.Location = new System.Drawing.Point(84, 325);
            this.axSceneControl1.Name = "axSceneControl1";
            this.axSceneControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSceneControl1.OcxState")));
            this.axSceneControl1.Size = new System.Drawing.Size(265, 265);
            this.axSceneControl1.TabIndex = 1;
            this.axSceneControl1.Visible = false;
            // 
            // propNodeAttribute
            // 
            this.propNodeAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propNodeAttribute.Location = new System.Drawing.Point(0, 0);
            this.propNodeAttribute.Name = "propNodeAttribute";
            this.propNodeAttribute.Size = new System.Drawing.Size(605, 587);
            this.propNodeAttribute.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_SaveScheme,
            this.toolStripSeparator2,
            this.tool_RunCheck,
            this.tool_SetDataSource});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(933, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tool_SaveScheme
            // 
            this.tool_SaveScheme.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_SaveScheme.Image = ((System.Drawing.Image)(resources.GetObject("tool_SaveScheme.Image")));
            this.tool_SaveScheme.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_SaveScheme.Name = "tool_SaveScheme";
            this.tool_SaveScheme.Size = new System.Drawing.Size(23, 22);
            this.tool_SaveScheme.Text = "保存数据核查方案";
            this.tool_SaveScheme.Click += new System.EventHandler(this.tool_SaveScheme_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tool_RunCheck
            // 
            this.tool_RunCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_RunCheck.Image = ((System.Drawing.Image)(resources.GetObject("tool_RunCheck.Image")));
            this.tool_RunCheck.ImageTransparentColor = System.Drawing.SystemColors.Window;
            this.tool_RunCheck.Name = "tool_RunCheck";
            this.tool_RunCheck.Size = new System.Drawing.Size(23, 22);
            this.tool_RunCheck.Text = "运行";
            this.tool_RunCheck.Click += new System.EventHandler(this.tool_RunCheck_Click);
            // 
            // tool_SetDataSource
            // 
            this.tool_SetDataSource.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_SetDataSource.Image = ((System.Drawing.Image)(resources.GetObject("tool_SetDataSource.Image")));
            this.tool_SetDataSource.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_SetDataSource.Name = "tool_SetDataSource";
            this.tool_SetDataSource.Size = new System.Drawing.Size(23, 22);
            this.tool_SetDataSource.Text = "设置数据源";
            this.tool_SetDataSource.Click += new System.EventHandler(this.tool_SetDataSource_Click);
            // 
            // FormDzzk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 612);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "VS2010";
            this.Name = "FormDzzk";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地质数据模型转换";
            this.Load += new System.EventHandler(this.FormDzzk_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axSceneControl1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
    }
}