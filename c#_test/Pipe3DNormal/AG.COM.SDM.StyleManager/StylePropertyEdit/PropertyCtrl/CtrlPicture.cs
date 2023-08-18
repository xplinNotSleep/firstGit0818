using System;
using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.StyleManager;
using ESRI.ArcGIS.ADF.COMSupport;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
    /// <summary>
    /// 图片标记/图片线/图片填充
    /// </summary>
    public partial class CtrlPicture : CtrlPropertyBase
    {
        #region 初始化
        public CtrlPicture(EnumSymbolType sType)
        {
            InitializeComponent();
            Init(sType);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sType"></param>
        private void Init(EnumSymbolType sType)
        {
            switch (sType)
            {
                case EnumSymbolType.SymbolTypeMarker:
                    this.AccessibleName = "图片标记";
                    this.label4.Text = "X偏移：";
                    this.label5.Text = "Y偏移：";
                    this.numericUpDownXOffset.Minimum = -1000;
                    this.numericUpDownXOffset.Maximum = 1000;
                    this.numericUpDownYOffset.Minimum = -1000;
                    this.numericUpDownYOffset.Maximum = 1000;
                    this.btnSelOutline.Visible = false;
                    break;
                case EnumSymbolType.SymbolTypeLine:
                    this.AccessibleName = "图片线";
                    this.tableLayoutPanel1.Controls.Remove(this.panel1);
                    this.label7.Text = "宽度：";
                    this.numericUpDownAngleAndWidth.Minimum = 0;
                    this.numericUpDownAngleAndWidth.Maximum = 1000;
                    this.btnSelOutline.Visible = false;
                    break;
                case EnumSymbolType.SymbolTypeFill:
                    this.AccessibleName = "图片填充";
                    this.tableLayoutPanel1.Controls.Remove(this.panel1);
                    this.btnSelOutline.Visible = true;
                    break;
            }

        }

        private void CtrlPicture_Load(object sender, EventArgs e)
        {
            if (this.m_pCtrlSymbol != null)
            {
                // 根据符号属性设置当前窗体控件值
                if (m_pCtrlSymbol is IPictureMarkerSymbol)
                {
                    IPictureMarkerSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureMarkerSymbol;
                    this.numericUpDownSize.Value = (decimal)pCtrlSymbol.Size;
                    this.numericUpDownAngleAndWidth.Value = (decimal)pCtrlSymbol.Angle;
                    this.numericUpDownXOffset.Value = (decimal)pCtrlSymbol.XOffset;
                    this.numericUpDownYOffset.Value = (decimal)pCtrlSymbol.YOffset;

                    this.colorPickerBackground.Value = StyleCommon.GetColor(pCtrlSymbol.BackgroundColor);
                    this.colorPickerTransparent.Value = StyleCommon.GetColor(pCtrlSymbol.BitmapTransparencyColor);

                    this.checkBoxSwapColor.Checked = pCtrlSymbol.SwapForeGroundBackGroundColor;

                    // 如果Picture为空，则选择一个图片文件
                    if (null == pCtrlSymbol.Picture) btnSelPicture_Click(null, null);
                }
                else if (m_pCtrlSymbol is IPictureLineSymbol)
                {
                    IPictureLineSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureLineSymbol;
                    this.numericUpDownAngleAndWidth.Value = (decimal)pCtrlSymbol.Width;
                    this.numericUpDownXOffset.Value = (decimal)pCtrlSymbol.XScale;
                    this.numericUpDownYOffset.Value = (decimal)pCtrlSymbol.YScale;

                    this.colorPickerBackground.Value = StyleCommon.GetColor(pCtrlSymbol.BackgroundColor);
                    this.colorPickerTransparent.Value = StyleCommon.GetColor(pCtrlSymbol.BitmapTransparencyColor);

                    this.checkBoxSwapColor.Checked = pCtrlSymbol.SwapForeGroundBackGroundColor;

                    // 如果Picture为空，则选择一个图片文件
                    if (null == pCtrlSymbol.Picture) btnSelPicture_Click(null, null);
                }
                else if (m_pCtrlSymbol is IPictureFillSymbol)
                {
                    IPictureFillSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureFillSymbol;
                    this.numericUpDownAngleAndWidth.Value = (decimal)pCtrlSymbol.Angle;
                    this.numericUpDownXOffset.Value = (decimal)pCtrlSymbol.XScale;
                    this.numericUpDownYOffset.Value = (decimal)pCtrlSymbol.YScale;

                    this.colorPickerBackground.Value = StyleCommon.GetColor(pCtrlSymbol.BackgroundColor);
                    this.colorPickerTransparent.Value = StyleCommon.GetColor(pCtrlSymbol.BitmapTransparencyColor);

                    this.checkBoxSwapColor.Checked = pCtrlSymbol.SwapForeGroundBackGroundColor;

                    // 如果Picture为空，则选择一个图片文件
                    if (null == pCtrlSymbol.Picture) btnSelPicture_Click(null, null);
                }
            }
            m_bInitComplete = true;
        }
        #endregion

        #region 用户更改属性值
        /// <summary>
        /// 选择图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog oDlg = new OpenFileDialog();
            oDlg.Filter = "图片文件(*.bmp;*.emf;*.gif;*.jpg;*.tif;)|*.bmp;*.emf;*.gif;*.jpg;*.tif";

            if (oDlg.ShowDialog() == DialogResult.OK)
            {
                if (this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null) return;

                string strConvertFile = ConvertPictureFile(oDlg.FileName);
                if (string.IsNullOrEmpty(strConvertFile)) return;

                System.Drawing.Bitmap dotNetBmp = new System.Drawing.Bitmap(strConvertFile);
                if (m_pCtrlSymbol is IPictureMarkerSymbol)
                {
                    IPictureMarkerSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureMarkerSymbol;
                    pCtrlSymbol.Picture = OLE.GetIPictureDispFromBitmap(dotNetBmp) as stdole.IPictureDisp;
                }
                else if (m_pCtrlSymbol is IPictureLineSymbol)
                {
                    IPictureLineSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureLineSymbol;
                    pCtrlSymbol.Picture = OLE.GetIPictureDispFromBitmap(dotNetBmp) as stdole.IPictureDisp;
                }
                else if (m_pCtrlSymbol is IPictureFillSymbol)
                {
                    IPictureFillSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureFillSymbol;
                    pCtrlSymbol.Picture = OLE.GetIPictureDispFromBitmap(dotNetBmp) as stdole.IPictureDisp;
                }
                this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
                this.textBoxPicture.Text = oDlg.FileName;
            }
        }

        /// <summary>
        /// 转换图片文件
        /// </summary>
        /// <param name="strFileName">图片文件</param>
        /// <returns>转换后的图片文件</returns>
        private string ConvertPictureFile(string strFileName)
        {
            string strConvertFile = "";
            if (strFileName.ToLower().EndsWith(".bmp") || strFileName.ToLower().EndsWith(".emf")) return strFileName;
            else
            {
                try
                {
                    Random autoRand = new Random();
                    string strNow = System.DateTime.Now.ToString("s");
                    strConvertFile = string.Format("{0}Pic{1}{2}.bmp", GetTempPath(), strNow.Replace(":", ""), autoRand.Next());
                    Image img = System.Drawing.Bitmap.FromFile(strFileName);
                    img.Save(strConvertFile);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("转换图像文件时发生错误！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return strConvertFile;
        }

        /// <summary>
        /// 获取临时目录
        /// </summary>
        /// <returns></returns>
        private string GetTempPath()
        {
            string strTempPath = "";
            try
            {
                strTempPath = string.Format("{0}StyleConvertPicTemp\\", System.IO.Path.GetTempPath());
                if (!System.IO.Directory.Exists(strTempPath)) System.IO.Directory.CreateDirectory(strTempPath);
            }
            catch (System.Exception ex)
            {
                strTempPath = "";
                MessageBox.Show("创建转换图像临时目录时发生错误！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return strTempPath;
        }

        /// <summary>
        /// 大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownSize_ValueChanged(object sender, EventArgs e)
        {
            if (!m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null) return;
            if (m_pCtrlSymbol is IPictureMarkerSymbol)
            {
                IPictureMarkerSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureMarkerSymbol;
                if (this.numericUpDownSize.Value > 0) pCtrlSymbol.Size = (double)this.numericUpDownSize.Value;
                this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
            }
        }

        /// <summary>
        /// 角度或宽度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownAngleAndWidth_ValueChanged(object sender, EventArgs e)
        {
            if (!m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null) return;
            if (m_pCtrlSymbol is IPictureMarkerSymbol)
            {
                IPictureMarkerSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureMarkerSymbol;
                pCtrlSymbol.Angle = (double)this.numericUpDownAngleAndWidth.Value;
            }
            else if (m_pCtrlSymbol is IPictureLineSymbol)
            {
                IPictureLineSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureLineSymbol;
                pCtrlSymbol.Width = (double)this.numericUpDownAngleAndWidth.Value;
            }
            else if (m_pCtrlSymbol is IPictureFillSymbol)
            {
                IPictureFillSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureFillSymbol;
                pCtrlSymbol.Angle = (double)this.numericUpDownAngleAndWidth.Value;
            }
            this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
        }

        /// <summary>
        /// X偏移或X比例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownXOffset_ValueChanged(object sender, EventArgs e)
        {
            if (!m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null) return;
            if (m_pCtrlSymbol is IPictureMarkerSymbol)
            {
                IPictureMarkerSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureMarkerSymbol;
                pCtrlSymbol.XOffset = (double)this.numericUpDownXOffset.Value;
            }
            else if (m_pCtrlSymbol is IPictureLineSymbol)
            {
                IPictureLineSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureLineSymbol;
                if (this.numericUpDownXOffset.Value > 0) pCtrlSymbol.XScale = (double)this.numericUpDownXOffset.Value;
            }
            else if (m_pCtrlSymbol is IPictureFillSymbol)
            {
                IPictureFillSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureFillSymbol;
                if (this.numericUpDownXOffset.Value > 0) pCtrlSymbol.XScale = (double)this.numericUpDownXOffset.Value;
            }
            this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
        }

        /// <summary>
        /// Y偏移或Y比例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownYOffset_ValueChanged(object sender, EventArgs e)
        {
            if (!m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null) return;
            if (m_pCtrlSymbol is IPictureMarkerSymbol)
            {
                IPictureMarkerSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureMarkerSymbol;
                pCtrlSymbol.YOffset = (double)this.numericUpDownYOffset.Value;
            }
            else if (m_pCtrlSymbol is IPictureLineSymbol)
            {
                IPictureLineSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureLineSymbol;
                if (this.numericUpDownYOffset.Value > 0) pCtrlSymbol.YScale = (double)this.numericUpDownYOffset.Value;
            }
            else if (m_pCtrlSymbol is IPictureFillSymbol)
            {
                IPictureFillSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureFillSymbol;
                if (this.numericUpDownYOffset.Value > 0) pCtrlSymbol.YScale = (double)this.numericUpDownYOffset.Value;
            }
            this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
        }

        /// <summary>
        /// 前景色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorPickerForeground_ValueChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 背景色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorPickerBackground_ValueChanged(object sender, EventArgs e)
        {
            if (!m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null) return;
            if (m_pCtrlSymbol is IPictureMarkerSymbol)
            {
                IPictureMarkerSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureMarkerSymbol;
                pCtrlSymbol.BackgroundColor = StyleCommon.GetRgbColor(this.colorPickerBackground.Value);
            }
            else if (m_pCtrlSymbol is IPictureLineSymbol)
            {
                IPictureLineSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureLineSymbol;
                pCtrlSymbol.BackgroundColor = StyleCommon.GetRgbColor(this.colorPickerBackground.Value);
            }
            else if (m_pCtrlSymbol is IPictureFillSymbol)
            {
                IPictureFillSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureFillSymbol;
                pCtrlSymbol.BackgroundColor = StyleCommon.GetRgbColor(this.colorPickerBackground.Value);
            }
            this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
        }

        /// <summary>
        /// 透明色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorPickerTransparent_ValueChanged(object sender, EventArgs e)
        {
            if (!m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null) return;
            if (m_pCtrlSymbol is IPictureMarkerSymbol)
            {
                IPictureMarkerSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureMarkerSymbol;
                pCtrlSymbol.BitmapTransparencyColor = StyleCommon.GetRgbColor(this.colorPickerTransparent.Value);
            }
            else if (m_pCtrlSymbol is IPictureLineSymbol)
            {
                IPictureLineSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureLineSymbol;
                pCtrlSymbol.BitmapTransparencyColor = StyleCommon.GetRgbColor(this.colorPickerTransparent.Value);
            }
            else if (m_pCtrlSymbol is IPictureFillSymbol)
            {
                IPictureFillSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureFillSymbol;
                pCtrlSymbol.BitmapTransparencyColor = StyleCommon.GetRgbColor(this.colorPickerTransparent.Value);
            }
            this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
        }

        /// <summary>
        /// 轮廓线符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelOutline_Click(object sender, EventArgs e)
        {
            if (!m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null) return;
            if (m_pCtrlSymbol is IPictureFillSymbol)
            {
                frmSymbolSelector frm = new frmSymbolSelector();
                frm.SymbolType = SymbolType.stLineSymbol;
                IPictureFillSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureFillSymbol;
                frm.InitialSymbol = pCtrlSymbol.Outline as ISymbol;
                if (frm.ShowDialog() != DialogResult.OK) return;
                pCtrlSymbol.Outline = frm.SelectedSymbol as ILineSymbol;
                this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
            }
        }

        /// <summary>
        /// 交换前景色和背景色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSwapColor_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null) return;
            if (m_pCtrlSymbol is IPictureMarkerSymbol)
            {
                IPictureMarkerSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureMarkerSymbol;
                pCtrlSymbol.SwapForeGroundBackGroundColor = this.checkBoxSwapColor.Checked;
            }
            else if (m_pCtrlSymbol is IPictureLineSymbol)
            {
                IPictureLineSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureLineSymbol;
                pCtrlSymbol.SwapForeGroundBackGroundColor = this.checkBoxSwapColor.Checked;
            }
            else if (m_pCtrlSymbol is IPictureFillSymbol)
            {
                IPictureFillSymbol pCtrlSymbol = m_pCtrlSymbol as IPictureFillSymbol;
                pCtrlSymbol.SwapForeGroundBackGroundColor = this.checkBoxSwapColor.Checked;
            }
            this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
        }
        #endregion
    }
}
