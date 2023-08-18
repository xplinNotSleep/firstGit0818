namespace AG.COM.SDM.GeoDataBase
{
    partial class FormQueryBuilder
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listFields = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.btnGetUnique = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtExpress = new System.Windows.Forms.TextBox();
            this.btnNotEqual = new System.Windows.Forms.Button();
            this.btnIs = new System.Windows.Forms.Button();
            this.listValues = new System.Windows.Forms.ListBox();
            this.btnEqual = new System.Windows.Forms.Button();
            this.btnLike = new System.Windows.Forms.Button();
            this.btnSmallEqual = new System.Windows.Forms.Button();
            this.btnBottomLine = new System.Windows.Forms.Button();
            this.btnOr = new System.Windows.Forms.Button();
            this.btnBigger = new System.Windows.Forms.Button();
            this.btnSmaller = new System.Windows.Forms.Button();
            this.btnBrackets = new System.Windows.Forms.Button();
            this.btnPercentage = new System.Windows.Forms.Button();
            this.btnBigEqual = new System.Windows.Forms.Button();
            this.btnAnd = new System.Windows.Forms.Button();
            this.btnNot = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClearSql = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listFields);
            this.groupBox2.Controls.Add(this.btnGetUnique);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtExpress);
            this.groupBox2.Controls.Add(this.btnNotEqual);
            this.groupBox2.Controls.Add(this.btnIs);
            this.groupBox2.Controls.Add(this.listValues);
            this.groupBox2.Controls.Add(this.btnEqual);
            this.groupBox2.Controls.Add(this.btnLike);
            this.groupBox2.Controls.Add(this.btnSmallEqual);
            this.groupBox2.Controls.Add(this.btnBottomLine);
            this.groupBox2.Controls.Add(this.btnOr);
            this.groupBox2.Controls.Add(this.btnBigger);
            this.groupBox2.Controls.Add(this.btnSmaller);
            this.groupBox2.Controls.Add(this.btnBrackets);
            this.groupBox2.Controls.Add(this.btnPercentage);
            this.groupBox2.Controls.Add(this.btnBigEqual);
            this.groupBox2.Controls.Add(this.btnAnd);
            this.groupBox2.Controls.Add(this.btnNot);
            this.groupBox2.Location = new System.Drawing.Point(5, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(344, 377);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // listFields
            // 
            this.listFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listFields.FullRowSelect = true;
            this.listFields.GridLines = true;
            this.listFields.Location = new System.Drawing.Point(15, 20);
            this.listFields.Name = "listFields";
            this.listFields.Size = new System.Drawing.Size(316, 111);
            this.listFields.TabIndex = 20;
            this.listFields.UseCompatibleStateImageBehavior = false;
            this.listFields.View = System.Windows.Forms.View.Details;
            this.listFields.DoubleClick += new System.EventHandler(this.listFields_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "字段别名";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 115;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "字段名称";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 140;
            // 
            // btnGetUnique
            // 
            this.btnGetUnique.Location = new System.Drawing.Point(157, 255);
            this.btnGetUnique.Name = "btnGetUnique";
            this.btnGetUnique.Size = new System.Drawing.Size(174, 23);
            this.btnGetUnique.TabIndex = 16;
            this.btnGetUnique.Text = "列出字段参考值";
            this.btnGetUnique.UseVisualStyleBackColor = true;
            this.btnGetUnique.Click += new System.EventHandler(this.btnGetUnique_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 293);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "SQL表达式:";
            // 
            // txtExpress
            // 
            this.txtExpress.Location = new System.Drawing.Point(15, 308);
            this.txtExpress.Multiline = true;
            this.txtExpress.Name = "txtExpress";
            this.txtExpress.Size = new System.Drawing.Size(316, 61);
            this.txtExpress.TabIndex = 17;
            // 
            // btnNotEqual
            // 
            this.btnNotEqual.Location = new System.Drawing.Point(59, 137);
            this.btnNotEqual.Name = "btnNotEqual";
            this.btnNotEqual.Size = new System.Drawing.Size(38, 23);
            this.btnNotEqual.TabIndex = 2;
            this.btnNotEqual.Text = "<>";
            this.btnNotEqual.UseVisualStyleBackColor = true;
            this.btnNotEqual.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnIs
            // 
            this.btnIs.Location = new System.Drawing.Point(15, 253);
            this.btnIs.Name = "btnIs";
            this.btnIs.Size = new System.Drawing.Size(38, 23);
            this.btnIs.TabIndex = 14;
            this.btnIs.Text = "Is";
            this.btnIs.UseVisualStyleBackColor = true;
            this.btnIs.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // listValues
            // 
            this.listValues.BackColor = System.Drawing.SystemColors.Control;
            this.listValues.Enabled = false;
            this.listValues.FormattingEnabled = true;
            this.listValues.ItemHeight = 12;
            this.listValues.Location = new System.Drawing.Point(157, 137);
            this.listValues.Name = "listValues";
            this.listValues.Size = new System.Drawing.Size(174, 112);
            this.listValues.TabIndex = 15;
            this.listValues.EnabledChanged += new System.EventHandler(this.listValues_EnabledChanged);
            this.listValues.DoubleClick += new System.EventHandler(this.listValues_DoubleClick);
            // 
            // btnEqual
            // 
            this.btnEqual.Location = new System.Drawing.Point(15, 137);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size(38, 23);
            this.btnEqual.TabIndex = 1;
            this.btnEqual.Text = "=";
            this.btnEqual.UseVisualStyleBackColor = true;
            this.btnEqual.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnLike
            // 
            this.btnLike.Location = new System.Drawing.Point(103, 137);
            this.btnLike.Name = "btnLike";
            this.btnLike.Size = new System.Drawing.Size(38, 23);
            this.btnLike.TabIndex = 3;
            this.btnLike.Text = "like";
            this.btnLike.UseVisualStyleBackColor = true;
            this.btnLike.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnSmallEqual
            // 
            this.btnSmallEqual.Location = new System.Drawing.Point(58, 195);
            this.btnSmallEqual.Name = "btnSmallEqual";
            this.btnSmallEqual.Size = new System.Drawing.Size(38, 23);
            this.btnSmallEqual.TabIndex = 8;
            this.btnSmallEqual.Text = "<=";
            this.btnSmallEqual.UseVisualStyleBackColor = true;
            this.btnSmallEqual.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnBottomLine
            // 
            this.btnBottomLine.Location = new System.Drawing.Point(15, 224);
            this.btnBottomLine.Name = "btnBottomLine";
            this.btnBottomLine.Size = new System.Drawing.Size(19, 23);
            this.btnBottomLine.TabIndex = 10;
            this.btnBottomLine.Text = "_";
            this.btnBottomLine.UseVisualStyleBackColor = true;
            this.btnBottomLine.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnOr
            // 
            this.btnOr.Location = new System.Drawing.Point(103, 195);
            this.btnOr.Name = "btnOr";
            this.btnOr.Size = new System.Drawing.Size(38, 23);
            this.btnOr.TabIndex = 9;
            this.btnOr.Text = "Or";
            this.btnOr.UseVisualStyleBackColor = true;
            this.btnOr.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnBigger
            // 
            this.btnBigger.Location = new System.Drawing.Point(15, 166);
            this.btnBigger.Name = "btnBigger";
            this.btnBigger.Size = new System.Drawing.Size(38, 23);
            this.btnBigger.TabIndex = 4;
            this.btnBigger.Text = ">";
            this.btnBigger.UseVisualStyleBackColor = true;
            this.btnBigger.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnSmaller
            // 
            this.btnSmaller.Location = new System.Drawing.Point(15, 195);
            this.btnSmaller.Name = "btnSmaller";
            this.btnSmaller.Size = new System.Drawing.Size(38, 23);
            this.btnSmaller.TabIndex = 7;
            this.btnSmaller.Text = "<";
            this.btnSmaller.UseVisualStyleBackColor = true;
            this.btnSmaller.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnBrackets
            // 
            this.btnBrackets.Location = new System.Drawing.Point(59, 224);
            this.btnBrackets.Name = "btnBrackets";
            this.btnBrackets.Size = new System.Drawing.Size(38, 23);
            this.btnBrackets.TabIndex = 12;
            this.btnBrackets.Text = "( )";
            this.btnBrackets.UseVisualStyleBackColor = true;
            this.btnBrackets.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnPercentage
            // 
            this.btnPercentage.Location = new System.Drawing.Point(32, 224);
            this.btnPercentage.Name = "btnPercentage";
            this.btnPercentage.Size = new System.Drawing.Size(19, 23);
            this.btnPercentage.TabIndex = 11;
            this.btnPercentage.Text = "%";
            this.btnPercentage.UseVisualStyleBackColor = true;
            this.btnPercentage.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnBigEqual
            // 
            this.btnBigEqual.Location = new System.Drawing.Point(58, 166);
            this.btnBigEqual.Name = "btnBigEqual";
            this.btnBigEqual.Size = new System.Drawing.Size(38, 23);
            this.btnBigEqual.TabIndex = 5;
            this.btnBigEqual.Text = ">=";
            this.btnBigEqual.UseVisualStyleBackColor = true;
            this.btnBigEqual.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnAnd
            // 
            this.btnAnd.Location = new System.Drawing.Point(103, 166);
            this.btnAnd.Name = "btnAnd";
            this.btnAnd.Size = new System.Drawing.Size(38, 23);
            this.btnAnd.TabIndex = 6;
            this.btnAnd.Text = "And";
            this.btnAnd.UseVisualStyleBackColor = true;
            this.btnAnd.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnNot
            // 
            this.btnNot.Location = new System.Drawing.Point(104, 224);
            this.btnNot.Name = "btnNot";
            this.btnNot.Size = new System.Drawing.Size(38, 23);
            this.btnNot.TabIndex = 13;
            this.btnNot.Text = "Not";
            this.btnNot.UseVisualStyleBackColor = true;
            this.btnNot.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(203, 385);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 24);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(279, 385);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 24);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnClearSql
            // 
            this.btnClearSql.Location = new System.Drawing.Point(5, 385);
            this.btnClearSql.Name = "btnClearSql";
            this.btnClearSql.Size = new System.Drawing.Size(70, 24);
            this.btnClearSql.TabIndex = 17;
            this.btnClearSql.Text = "清除SQL";
            this.btnClearSql.UseVisualStyleBackColor = true;
            this.btnClearSql.Click += new System.EventHandler(this.btnClearSql_Click);
            // 
            // FormQueryBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 415);
            this.Controls.Add(this.btnClearSql);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormQueryBuilder";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "条件过滤器";
            this.Load += new System.EventHandler(this.FormQueryBuilder_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGetUnique;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtExpress;
        private System.Windows.Forms.Button btnNotEqual;
        private System.Windows.Forms.Button btnIs;
        private System.Windows.Forms.ListBox listValues;
        private System.Windows.Forms.Button btnEqual;
        private System.Windows.Forms.Button btnLike;
        private System.Windows.Forms.Button btnSmallEqual;
        private System.Windows.Forms.Button btnBottomLine;
        private System.Windows.Forms.Button btnOr;
        private System.Windows.Forms.Button btnBigger;
        private System.Windows.Forms.Button btnSmaller;
        private System.Windows.Forms.Button btnBrackets;
        private System.Windows.Forms.Button btnPercentage;
        private System.Windows.Forms.Button btnBigEqual;
        private System.Windows.Forms.Button btnAnd;
        private System.Windows.Forms.Button btnNot;
        private System.Windows.Forms.ListView listFields;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClearSql;
    }
}