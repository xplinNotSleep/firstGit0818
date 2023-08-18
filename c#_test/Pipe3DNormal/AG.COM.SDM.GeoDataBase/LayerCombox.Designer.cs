namespace AG.COM.SDM.GeoDataBase
{
    partial class LayerCombox
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayerCombox));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.comboBoxTreeView1 = new AG.COM.SDM.Utility.Controls.ComboBoxTreeView();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "GroupLayer.bmp");
            this.imageList1.Images.SetKeyName(1, "Layer.bmp");
            // 
            // comboBoxTreeView1
            // 
            this.comboBoxTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxTreeView1.DropDownHeight = 300;
            this.comboBoxTreeView1.FormattingEnabled = true;
            this.comboBoxTreeView1.IntegralHeight = false;
            this.comboBoxTreeView1.Location = new System.Drawing.Point(0, 0);
            this.comboBoxTreeView1.Name = "comboBoxTreeView1";
            this.comboBoxTreeView1.QuickSearch = false;
            this.comboBoxTreeView1.Size = new System.Drawing.Size(205, 20);
            this.comboBoxTreeView1.TabIndex = 0;
            this.comboBoxTreeView1.SelectedIndexChanged += new System.EventHandler(this.comboBoxTreeView1_SelectedIndexChanged);
            // 
            // LayerCombox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxTreeView1);
            this.Name = "LayerCombox";
            this.Size = new System.Drawing.Size(205, 26);
            this.Load += new System.EventHandler(this.LayerCombox_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private AG.COM.SDM.Utility.Controls.ComboBoxTreeView comboBoxTreeView1;
    }
}
