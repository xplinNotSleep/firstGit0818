
namespace AG.Pipe.Analyst3DModel
{
    partial class FormMap
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMap));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.TCol_LayerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TCol_Detail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TCol_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TCol_SP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TCol_EP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TCol_LayerName,
            this.TCol_Detail,
            this.TCol_No,
            this.TCol_SP,
            this.TCol_EP});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 320);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(800, 198);
            this.dataGridView1.TabIndex = 7;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(0, 28);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(800, 292);
            this.axMapControl1.TabIndex = 8;
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 0);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(800, 28);
            this.axToolbarControl1.TabIndex = 9;
            // 
            // TCol_LayerName
            // 
            this.TCol_LayerName.HeaderText = "图层名称";
            this.TCol_LayerName.Name = "TCol_LayerName";
            this.TCol_LayerName.ReadOnly = true;
            this.TCol_LayerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TCol_Detail
            // 
            this.TCol_Detail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TCol_Detail.HeaderText = "描述";
            this.TCol_Detail.Name = "TCol_Detail";
            this.TCol_Detail.ReadOnly = true;
            this.TCol_Detail.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TCol_No
            // 
            this.TCol_No.HeaderText = "编号";
            this.TCol_No.Name = "TCol_No";
            this.TCol_No.ReadOnly = true;
            this.TCol_No.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TCol_No.Visible = false;
            // 
            // TCol_SP
            // 
            this.TCol_SP.HeaderText = "起点";
            this.TCol_SP.Name = "TCol_SP";
            this.TCol_SP.ReadOnly = true;
            this.TCol_SP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TCol_SP.Visible = false;
            this.TCol_SP.Width = 150;
            // 
            // TCol_EP
            // 
            this.TCol_EP.HeaderText = "终点";
            this.TCol_EP.Name = "TCol_EP";
            this.TCol_EP.ReadOnly = true;
            this.TCol_EP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TCol_EP.Visible = false;
            this.TCol_EP.Width = 150;
            // 
            // FormMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormMap";
            this.Size = new System.Drawing.Size(800, 518);
            this.Text = "转换日志";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TCol_LayerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TCol_Detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn TCol_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn TCol_SP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TCol_EP;
    }
}