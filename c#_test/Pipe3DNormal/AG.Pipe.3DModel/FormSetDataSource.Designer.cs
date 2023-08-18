
namespace AG.Pipe.Analyst3DModel
{
    partial class FormSetDataSource
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listDataSource = new AG.Pipe.Analyst3DModel.ListViewEdit();
            this.columnHeader1 = ((AG.Pipe.Analyst3DModel.ALAN_ColumnHeader)(new AG.Pipe.Analyst3DModel.ALAN_ColumnHeader()));
            this.columnHeader2 = ((AG.Pipe.Analyst3DModel.ALAN_ColumnHeader)(new AG.Pipe.Analyst3DModel.ALAN_ColumnHeader()));
            this.txtDataSource = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listDataSource);
            this.groupBox1.Controls.Add(this.txtDataSource);
            this.groupBox1.Controls.Add(this.btnSelect);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(15, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(496, 491);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // listDataSource
            // 
            this.listDataSource.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listDataSource.ComboBoxBgColor = System.Drawing.Color.LightBlue;
            this.listDataSource.ComboBoxFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listDataSource.EditBgColor = System.Drawing.Color.LightBlue;
            this.listDataSource.EditFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listDataSource.FullRowSelect = true;
            this.listDataSource.GridLines = true;
            this.listDataSource.HideSelection = false;
            this.listDataSource.Location = new System.Drawing.Point(28, 71);
            this.listDataSource.Name = "listDataSource";
            this.listDataSource.Size = new System.Drawing.Size(452, 412);
            this.listDataSource.TabIndex = 6;
            this.listDataSource.UseCompatibleStateImageBehavior = false;
            this.listDataSource.View = System.Windows.Forms.View.Details;
            this.listDataSource.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listDataSource_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.ColumnStyle = AG.Pipe.Analyst3DModel.ALAN_ListViewColumnStyle.ReadOnly;
            this.columnHeader1.Text = "规则核查图层";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.ColumnStyle = AG.Pipe.Analyst3DModel.ALAN_ListViewColumnStyle.ComboBox;
            this.columnHeader2.Text = "源数据图层";
            this.columnHeader2.Width = 120;
            // 
            // txtDataSource
            // 
            this.txtDataSource.Location = new System.Drawing.Point(28, 37);
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.ReadOnly = true;
            this.txtDataSource.Size = new System.Drawing.Size(357, 22);
            this.txtDataSource.TabIndex = 4;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(393, 37);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(87, 27);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = "选择…";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "选择数据源";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(408, 513);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 27);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(314, 513);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 27);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "源数据类型";
            this.columnHeader3.Width = 120;
            // 
            // FormSetDataSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 553);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.LookAndFeel.SkinName = "VS2010";
            this.Name = "FormSetDataSource";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置数据源";
            this.Load += new System.EventHandler(this.FormSetDataSource_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDataSource;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private ListViewEdit listDataSource;
        private ALAN_ColumnHeader columnHeader1;
        private ALAN_ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}