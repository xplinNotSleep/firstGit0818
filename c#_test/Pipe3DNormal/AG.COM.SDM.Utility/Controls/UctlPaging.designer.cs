namespace AG.COM.SDM.Utility.Controls
{
    partial class UctlPaging
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UctlPaging));
            this.toolStripItem = new System.Windows.Forms.ToolStrip();
            this.tsbFirstPage = new System.Windows.Forms.ToolStripButton();
            this.tsbPreviousPage = new System.Windows.Forms.ToolStripButton();
            this.txtCurrentPage = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel41 = new System.Windows.Forms.ToolStripLabel();
            this.tslAllpage = new System.Windows.Forms.ToolStripLabel();
            this.tsbNextPage = new System.Windows.Forms.ToolStripButton();
            this.tsbLastPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel45 = new System.Windows.Forms.ToolStripLabel();
            this.tslPageCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel47 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tslCurrentPageRecordCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel48 = new System.Windows.Forms.ToolStripLabel();
            this.tslAllRecordCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel50 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.txtRecordPerPage = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripItem
            // 
            this.toolStripItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFirstPage,
            this.tsbPreviousPage,
            this.txtCurrentPage,
            this.toolStripLabel41,
            this.tslAllpage,
            this.tsbNextPage,
            this.tsbLastPage,
            this.toolStripSeparator25,
            this.toolStripLabel45,
            this.tslPageCount,
            this.toolStripLabel47,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tslCurrentPageRecordCount,
            this.toolStripSeparator2,
            this.toolStripLabel48,
            this.tslAllRecordCount,
            this.toolStripLabel50,
            this.toolStripLabel3,
            this.txtRecordPerPage,
            this.toolStripLabel2});
            this.toolStripItem.Location = new System.Drawing.Point(0, 0);
            this.toolStripItem.Name = "toolStripItem";
            this.toolStripItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripItem.Size = new System.Drawing.Size(436, 26);
            this.toolStripItem.TabIndex = 1;
            this.toolStripItem.Text = "toolStrip1";
            // 
            // tsbFirstPage
            // 
            this.tsbFirstPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFirstPage.Image = ((System.Drawing.Image)(resources.GetObject("tsbFirstPage.Image")));
            this.tsbFirstPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFirstPage.Name = "tsbFirstPage";
            this.tsbFirstPage.Size = new System.Drawing.Size(23, 23);
            this.tsbFirstPage.Text = "首页";
            this.tsbFirstPage.Click += new System.EventHandler(this.tsbFirstPage_Click);
            // 
            // tsbPreviousPage
            // 
            this.tsbPreviousPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPreviousPage.Image = ((System.Drawing.Image)(resources.GetObject("tsbPreviousPage.Image")));
            this.tsbPreviousPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPreviousPage.Name = "tsbPreviousPage";
            this.tsbPreviousPage.Size = new System.Drawing.Size(23, 23);
            this.tsbPreviousPage.Text = "前一页";
            this.tsbPreviousPage.Click += new System.EventHandler(this.tsbPreviousPage_Click);
            // 
            // txtCurrentPage
            // 
            this.txtCurrentPage.AutoSize = false;
            this.txtCurrentPage.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.txtCurrentPage.Name = "txtCurrentPage";
            this.txtCurrentPage.Size = new System.Drawing.Size(25, 22);
            this.txtCurrentPage.Text = "1";
            this.txtCurrentPage.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCurrentPage.ToolTipText = "当前页";
            this.txtCurrentPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCurrentPage_KeyPress);
            // 
            // toolStripLabel41
            // 
            this.toolStripLabel41.Name = "toolStripLabel41";
            this.toolStripLabel41.Size = new System.Drawing.Size(13, 23);
            this.toolStripLabel41.Text = "/";
            // 
            // tslAllpage
            // 
            this.tslAllpage.Name = "tslAllpage";
            this.tslAllpage.Size = new System.Drawing.Size(15, 23);
            this.tslAllpage.Text = "1";
            // 
            // tsbNextPage
            // 
            this.tsbNextPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNextPage.Image = ((System.Drawing.Image)(resources.GetObject("tsbNextPage.Image")));
            this.tsbNextPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNextPage.Name = "tsbNextPage";
            this.tsbNextPage.Size = new System.Drawing.Size(23, 23);
            this.tsbNextPage.Text = "下一页";
            this.tsbNextPage.Click += new System.EventHandler(this.tsbNextPage_Click);
            // 
            // tsbLastPage
            // 
            this.tsbLastPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLastPage.Image = ((System.Drawing.Image)(resources.GetObject("tsbLastPage.Image")));
            this.tsbLastPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLastPage.Name = "tsbLastPage";
            this.tsbLastPage.Size = new System.Drawing.Size(23, 23);
            this.tsbLastPage.Text = "最后一页";
            this.tsbLastPage.Click += new System.EventHandler(this.tsbLastPage_Click);
            // 
            // toolStripSeparator25
            // 
            this.toolStripSeparator25.Name = "toolStripSeparator25";
            this.toolStripSeparator25.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripLabel45
            // 
            this.toolStripLabel45.Name = "toolStripLabel45";
            this.toolStripLabel45.Size = new System.Drawing.Size(20, 23);
            this.toolStripLabel45.Text = "共";
            // 
            // tslPageCount
            // 
            this.tslPageCount.Name = "tslPageCount";
            this.tslPageCount.Size = new System.Drawing.Size(15, 23);
            this.tslPageCount.Text = "1";
            // 
            // toolStripLabel47
            // 
            this.toolStripLabel47.Name = "toolStripLabel47";
            this.toolStripLabel47.Size = new System.Drawing.Size(20, 23);
            this.toolStripLabel47.Text = "页";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(44, 23);
            this.toolStripLabel1.Text = "本页数";
            // 
            // tslCurrentPageRecordCount
            // 
            this.tslCurrentPageRecordCount.Name = "tslCurrentPageRecordCount";
            this.tslCurrentPageRecordCount.Size = new System.Drawing.Size(15, 23);
            this.tslCurrentPageRecordCount.Text = "0";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripLabel48
            // 
            this.toolStripLabel48.Name = "toolStripLabel48";
            this.toolStripLabel48.Size = new System.Drawing.Size(20, 23);
            this.toolStripLabel48.Text = "共";
            // 
            // tslAllRecordCount
            // 
            this.tslAllRecordCount.Name = "tslAllRecordCount";
            this.tslAllRecordCount.Size = new System.Drawing.Size(15, 23);
            this.tslAllRecordCount.Text = "0";
            // 
            // toolStripLabel50
            // 
            this.toolStripLabel50.Name = "toolStripLabel50";
            this.toolStripLabel50.Size = new System.Drawing.Size(44, 23);
            this.toolStripLabel50.Text = "条记录";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(56, 17);
            this.toolStripLabel3.Text = "每页显示";
            // 
            // txtRecordPerPage
            // 
            this.txtRecordPerPage.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.txtRecordPerPage.Name = "txtRecordPerPage";
            this.txtRecordPerPage.ReadOnly = true;
            this.txtRecordPerPage.Size = new System.Drawing.Size(40, 23);
            this.txtRecordPerPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRecordPerPage_KeyPress);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(20, 17);
            this.toolStripLabel2.Text = "条";
            // 
            // UctlPaging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripItem);
            this.Name = "UctlPaging";
            this.Size = new System.Drawing.Size(436, 26);
            this.toolStripItem.ResumeLayout(false);
            this.toolStripItem.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripItem;
        private System.Windows.Forms.ToolStripButton tsbFirstPage;
        private System.Windows.Forms.ToolStripButton tsbPreviousPage;
        private System.Windows.Forms.ToolStripLabel toolStripLabel41;
        private System.Windows.Forms.ToolStripLabel tslAllpage;
        private System.Windows.Forms.ToolStripButton tsbNextPage;
        private System.Windows.Forms.ToolStripButton tsbLastPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator25;
        private System.Windows.Forms.ToolStripLabel toolStripLabel45;
        private System.Windows.Forms.ToolStripLabel tslPageCount;
        private System.Windows.Forms.ToolStripLabel toolStripLabel47;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel tslCurrentPageRecordCount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel48;
        private System.Windows.Forms.ToolStripLabel tslAllRecordCount;
        private System.Windows.Forms.ToolStripLabel toolStripLabel50;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox txtCurrentPage;
        private System.Windows.Forms.ToolStripTextBox txtRecordPerPage;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;

    }
}
