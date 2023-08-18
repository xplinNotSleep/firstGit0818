namespace AG.COM.SDM.Catalog.UI
{
    partial class FormAddConnection
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
            ESRI.ArcGIS.esriSystem.IPropertySet propertySetClass1 = new ESRI.ArcGIS.esriSystem.PropertySetClass();
this.dataConnectionControl1 = new AG.COM.SDM.Catalog.UI.DataConnectionControl();
this.btOk = new System.Windows.Forms.Button();
this.btCancel = new System.Windows.Forms.Button();
this.SuspendLayout();
// 
// dataConnectionControl1
// 
this.dataConnectionControl1.ConnectionProperties = propertySetClass1;
this.dataConnectionControl1.Location = new System.Drawing.Point(0, 0);
this.dataConnectionControl1.Name = "dataConnectionControl1";
this.dataConnectionControl1.Size = new System.Drawing.Size(450, 224);
this.dataConnectionControl1.TabIndex = 2;
// 
// btOk
// 
this.btOk.Location = new System.Drawing.Point(276, 227);
this.btOk.Name = "btOk";
this.btOk.Size = new System.Drawing.Size(80, 25);
this.btOk.TabIndex = 1;
this.btOk.Text = "确定";
this.btOk.UseVisualStyleBackColor = true;
this.btOk.Click += new System.EventHandler(this.btOk_Click);
// 
// btCancel
// 
this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
this.btCancel.Location = new System.Drawing.Point(362, 227);
this.btCancel.Name = "btCancel";
this.btCancel.Size = new System.Drawing.Size(80, 25);
this.btCancel.TabIndex = 1;
this.btCancel.Text = "取消";
this.btCancel.UseVisualStyleBackColor = true;
// 
// FormAddConnection
// 
this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
this.ClientSize = new System.Drawing.Size(454, 257);
this.Controls.Add(this.btCancel);
this.Controls.Add(this.btOk);
this.Controls.Add(this.dataConnectionControl1);
this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
this.MaximizeBox = false;
this.MinimizeBox = false;
this.Name = "FormAddConnection";
this.ShowIcon = false;
this.ShowInTaskbar = false;
this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
this.Text = "数据库连接";
this.ResumeLayout(false);

        }

        #endregion

        private DataConnectionControl dataConnectionControl1;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;

    }
}