namespace AG.COM.SDM.GeoDataBase
{
    partial class FormVersionManager
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
            this.gxTreeView1 = new AG.COM.SDM.GeoDataBase.GxTreeView();
            this.SuspendLayout();
            // 
            // gxTreeView1
            // 
            this.gxTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gxTreeView1.Location = new System.Drawing.Point(0, 0);
            this.gxTreeView1.Name = "gxTreeView1";
            this.gxTreeView1.OperateState = AG.COM.SDM.GeoDataBase.EnumOperateState.Normal;
            this.gxTreeView1.ShowCheckBoxes = false;
            this.gxTreeView1.Size = new System.Drawing.Size(409, 444);
            this.gxTreeView1.TabIndex = 0;
            // 
            // FormVersionManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 444);
            this.Controls.Add(this.gxTreeView1);                     
            this.Name = "FormVersionManager";           
            this.TabText = "版本管理";
            this.Text = "版本管理";
            this.Load += new System.EventHandler(this.FormVersionManager_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private GxTreeView gxTreeView1;
    }
}