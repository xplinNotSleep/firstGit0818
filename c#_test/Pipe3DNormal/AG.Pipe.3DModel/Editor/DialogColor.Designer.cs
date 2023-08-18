
namespace AG.Pipe.Analyst3DModel.Editor
{
    partial class DialogColor
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
            this.colorPicker1 = new AG.COM.SDM.Utility.Controls.ColorPicker();
            this.SuspendLayout();
            // 
            // colorPicker1
            // 
            this.colorPicker1.BackColor = System.Drawing.SystemColors.Window;
            this.colorPicker1.Context = null;
            this.colorPicker1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.colorPicker1.Location = new System.Drawing.Point(38, 13);
            this.colorPicker1.Name = "colorPicker1";
            this.colorPicker1.ReadOnly = false;
            this.colorPicker1.Size = new System.Drawing.Size(186, 21);
            this.colorPicker1.TabIndex = 0;
            this.colorPicker1.Text = "Window";
            this.colorPicker1.Value = System.Drawing.SystemColors.Window;
            // 
            // DialogColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 148);
            this.Controls.Add(this.colorPicker1);
            this.Name = "DialogColor";
            this.Text = "DialogColor";
            this.ResumeLayout(false);

        }

        #endregion

        private COM.SDM.Utility.Controls.ColorPicker colorPicker1;
    }
}