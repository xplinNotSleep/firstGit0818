using System;
using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.StyleManager;
using ESRI.ArcGIS.ADF.COMSupport;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
    /// <summary>
    /// ͼƬ���/ͼƬ��/ͼƬ���
    /// </summary>
    public partial class CtrlPicture : CtrlPropertyBase
    {
        #region ��ʼ��
        public CtrlPicture(EnumSymbolType sType)
        {
            InitializeComponent();
            Init(sType);
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="sType"></param>
        private void Init(EnumSymbolType sType)
        {
            switch (sType)
            {
                case EnumSymbolType.SymbolTypeMarker:
                    this.AccessibleName = "ͼƬ���";
                    this.label4.Text = "Xƫ�ƣ�";
                    this.label5.Text = "Yƫ�ƣ�";
                    this.numericUpDownXOffset.Minimum = -1000;
                    this.numericUpDownXOffset.Maximum = 1000;
                    this.numericUpDownYOffset.Minimum = -1000;
                    this.numericUpDownYOffset.Maximum = 1000;
                    this.btnSelOutline.Visible = false;
                    break;
                case EnumSymbolType.SymbolTypeLine:
                    this.AccessibleName = "ͼƬ��";
                    this.tableLayoutPanel1.Controls.Remove(this.panel1);
                    this.label7.Text = "��ȣ�";
                    this.numericUpDownAngleAndWidth.Minimum = 0;
                    this.numericUpDownAngleAndWidth.Maximum = 1000;
                    this.btnSelOutline.Visible = false;
                    break;
                case EnumSymbolType.SymbolTypeFill:
                    this.AccessibleName = "ͼƬ���";
                    this.tableLayoutPanel1.Controls.Remove(this.panel1);
                    this.btnSelOutline.Visible = true;
                    break;
            }

        }

        private void CtrlPicture_Load(object sender, EventArgs e)
        {
            if (this.m_pCtrlSymbol != null)
            {
                // ���ݷ����������õ�ǰ����ؼ�ֵ
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

                    // ���PictureΪ�գ���ѡ��һ��ͼƬ�ļ�
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

                    // ���PictureΪ�գ���ѡ��һ��ͼƬ�ļ�
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

                    // ���PictureΪ�գ���ѡ��һ��ͼƬ�ļ�
                    if (null == pCtrlSymbol.Picture) btnSelPicture_Click(null, null);
                }
            }
            m_bInitComplete = true;
        }
        #endregion

        #region �û���������ֵ
        /// <summary>
        /// ѡ��ͼƬ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog oDlg = new OpenFileDialog();
            oDlg.Filter = "ͼƬ�ļ�(*.bmp;*.emf;*.gif;*.jpg;*.tif;)|*.bmp;*.emf;*.gif;*.jpg;*.tif";

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
        /// ת��ͼƬ�ļ�
        /// </summary>
        /// <param name="strFileName">ͼƬ�ļ�</param>
        /// <returns>ת�����ͼƬ�ļ�</returns>
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
                    MessageBox.Show("ת��ͼ���ļ�ʱ��������", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return strConvertFile;
        }

        /// <summary>
        /// ��ȡ��ʱĿ¼
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
                MessageBox.Show("����ת��ͼ����ʱĿ¼ʱ��������", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return strTempPath;
        }

        /// <summary>
        /// ��С
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
        /// �ǶȻ���
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
        /// Xƫ�ƻ�X����
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
        /// Yƫ�ƻ�Y����
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
        /// ǰ��ɫ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorPickerForeground_ValueChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// ����ɫ
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
        /// ͸��ɫ
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
        /// �����߷���
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
        /// ����ǰ��ɫ�ͱ���ɫ
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
