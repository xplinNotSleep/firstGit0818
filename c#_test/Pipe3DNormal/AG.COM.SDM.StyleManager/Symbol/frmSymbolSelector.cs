using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using AG.COM.SDM.StylePropertyEdit;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using stdole;

namespace AG.COM.SDM.StyleManager
{
    public partial class frmSymbolSelector : Form
    {
        private object m_ScaleBar;
        private ISymbol m_InitialSymbol;
        private object m_SelectedSymbol;
        private IList<string> m_strStylePath = new List<string>();
        private IMarkerNorthArrow m_MarkerNorthArrow = new MarkerNorthArrowClass();
        private IStyleGalleryClass m_StyleGalleryClass = null;
        private esriSymbologyStyleClass m_SymbologyStyleClass;
        
        public frmSymbolSelector()
        {
            InitializeComponent();
        }

        private string m_MapUnits;
        public string MapUnits
        {
            get { return m_MapUnits; }
            set { m_MapUnits = value; }
        }

        private double m_ScaleDivision;
        public double ScaleDivision
        {
            get { return m_ScaleDivision; }
            set { m_ScaleDivision = value; }
        }

        private object m_PreviewItem;
        public object PreviewItem
        {
            get { return m_PreviewItem; }
            set { m_PreviewItem = value; }
        }

        private IStyleGalleryClass m_PreviewStyleGalleryClass;
        public IStyleGalleryClass PreviewStyleGalleryClass
        {
            get { return m_PreviewStyleGalleryClass; }
            set { m_PreviewStyleGalleryClass = value; }
        }

        private SymbolType m_SymbolType = SymbolType.stFillSymbol;
        public SymbolType SymbolType
        {
            get { return m_SymbolType; }
            set { m_SymbolType = value; }
        }

        public ISymbol InitialSymbol
        {
            get { return this.m_InitialSymbol; }
            set { this.m_InitialSymbol = value; }
        }

        public object SelectedSymbol
        {
            get
            {
                if (m_SymbolType == SymbolType.stNorthArrows )
                {
                    if (cbxIsVisible.Checked == true)
                    { return this.stypicPreview.Symbol as object; }
                    else
                    { return null; }
                }
                else if (m_SymbolType == SymbolType.stScaleBars)
                {
                    if (chkScaleBarIsVisible.Checked == true)
                    { return this.stypicPreview.Item; }
                    else
                    { return null; }
                }
                else if (m_SymbolType == SymbolType.stLegendItems)
                {
                    return m_SelectedSymbol;
                }
                else
                {
                    return this.stypicPreview.Symbol as object;
                }
            }
        }

        private void frmSymbolSelector_Load(object sender, EventArgs e)
        {
            //根据传入参数设置m_strStyleGalleryClass(符号类别归属)
            this.InitialParameter();
            //初始化预览框
            this.InitialPreview();
            //赋予符号预览一个默认值
            if (m_SymbolType == SymbolType.stScaleBars)
            {
                this.stypicPreview.Item = m_PreviewItem;
                this.stypicPreview.StyleGalleryClass = m_PreviewStyleGalleryClass;
                this.stypicPreview.Refresh();
                this.chkScaleBarIsVisible.Checked = true;
                m_ScaleBar = m_PreviewItem;
                m_SelectedSymbol = m_PreviewItem;
                m_StyleGalleryClass = m_PreviewStyleGalleryClass;
            }
            //初始化符号样式选项
            this.InitialSymbolStyle();
        }

        //刷新SymbologyControl
        private void RefreshSymbologyControl()
        {
            if (m_strStylePath.Count <= 1)
                return;
            this.SymbologyControl.Clear();
            for (int i = 0; i < m_strStylePath.Count; i++)
            {
                this.SymbologyControl.LoadStyleFile(m_strStylePath[i]);
            }
            this.SymbologyControl.StyleClass = m_SymbologyStyleClass;
        }

        //初始化模块变量参数及控件
        
        private void InitialParameter()
        {
            if (this.m_SymbolType == SymbolType.stMarkerSymbol)
            {
                m_SymbologyStyleClass = esriSymbologyStyleClass.esriStyleClassMarkerSymbols;
            }
            else if (this.m_SymbolType == SymbolType.stLineSymbol)
            {
                m_SymbologyStyleClass = esriSymbologyStyleClass.esriStyleClassLineSymbols;
            }
            else if (this.m_SymbolType == SymbolType.stFillSymbol)
            {
                m_SymbologyStyleClass = esriSymbologyStyleClass.esriStyleClassFillSymbols;
            }
            else if (this.m_SymbolType == SymbolType.stTextSymbol)
            {
                m_SymbologyStyleClass = esriSymbologyStyleClass.esriStyleClassTextSymbols;
            }
            else if (this.m_SymbolType == SymbolType.stNorthArrows)
            {
                m_SymbologyStyleClass = esriSymbologyStyleClass.esriStyleClassNorthArrows;
            }
            else if (this.m_SymbolType == SymbolType.stScaleBars)
            {
                m_SymbologyStyleClass = esriSymbologyStyleClass.esriStyleClassScaleBars;
            }
            else if (this.m_SymbolType == SymbolType.stLegendItems)
            {
                m_SymbologyStyleClass = esriSymbologyStyleClass.esriStyleClassLegendItems;
            }
            else 
                return;

            string strPath = CommonConstString.STR_StylePath + @"\ESRI.ServerStyle";
            if (System.IO.File.Exists(strPath))
            {
                this.m_strStylePath.Add(strPath);
            }
            else
            {
                MessageBox.Show(strPath + "文件不存在！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            ///if (System.IO.File.Exists(m_strStylePath[0]) != true)
            ///    return;
            
           // //----------------------------Head
           // //警用符号：2008-7-24
           // strPath = CommonConstString.STR_StylePath + @"\PGIS.ServerStyle";
           // this.m_strStylePath.Add(strPath);
           // if (System.IO.File.Exists(m_strStylePath[0]) != true && System.IO.File.Exists(m_strStylePath[1]) != true)
           //     return;
           ////------------------------------End

            //初始化SymbologyControl
            this.SymbologyControl.DisplayStyle = esriSymbologyDisplayStyle.esriDisplayStyleIcon;
            for (int i = 0; i < m_strStylePath.Count; i++)
            {
                this.SymbologyControl.LoadStyleFile(m_strStylePath[i]);
            }
            this.SymbologyControl.StyleClass = m_SymbologyStyleClass;
            //设置字体大小选项
            this.nudSize.Value = 0;
            this.nudSize.Minimum = 0;
            this.nudSize.Maximum = decimal.MaxValue;
            this.nudSize.DecimalPlaces = 2;
            this.nudSize.ThousandsSeparator = false;
            this.nudSize.Increment = 1;
            //设置旋转角度选项
            this.nudAngle.Value = 0;
            this.nudAngle.Maximum = 360;
            this.nudAngle.Minimum = -360;
            this.nudAngle.Increment = 1;
            this.nudAngle.DecimalPlaces = 2;
            this.nudAngle.ThousandsSeparator = false;
            //指北针字体大小及旋转角度
            this.nudNorthArrowSize.Value = 0;
            this.nudNorthArrowSize.Minimum = 0;
            this.nudNorthArrowSize.Maximum = decimal.MaxValue;
            this.nudNorthArrowSize.DecimalPlaces = 2;
            this.nudNorthArrowSize.ThousandsSeparator = false;
            this.nudNorthArrowSize.Increment = 1;
            this.nudNorthArrowRotate.Value = 0;
            this.nudNorthArrowRotate.Maximum = 360;
            this.nudNorthArrowRotate.Minimum = -360;
            this.nudNorthArrowRotate.Increment = 1;
            this.nudNorthArrowRotate.DecimalPlaces = 2;
            this.nudNorthArrowRotate.ThousandsSeparator = false;
            //文字的大小控件的初始设置
            this.nudTextSize.Value = 0;
            this.nudTextSize.Minimum = 1;
            this.nudTextSize.Maximum = decimal.MaxValue;
            this.nudTextSize.DecimalPlaces = 2;
            this.nudTextSize.ThousandsSeparator = false;
            this.nudTextSize.Increment = 1;
            //文字字体名称控件的初始设置
            InstalledFontCollection fontColl = new InstalledFontCollection();
            FontFamily[] fontFamilies = fontColl.Families;
            this.cboTextFont.Items.Clear();
            foreach (FontFamily fontFamily in fontFamilies)
            {
                if (fontFamily.IsStyleAvailable(FontStyle.Regular))
                {
                    System.Drawing.Font font = new System.Drawing.Font(fontFamily, 20);
                    this.cboTextFont.Items.Add(font.Name);
                }
            }
            this.cboTextFont.SelectedIndex = 0;
            fontColl.Dispose(); 
        }

        //初始化符号样式选项
        private void InitialSymbolStyle()
        {
            Control ctl = null;
            //if (this.m_InitialSymbol == null)
            //    return;
            //初始化选择符号
            #region
            this.m_SelectedSymbol = this.m_InitialSymbol;
            if (this.m_SymbolType == SymbolType.stMarkerSymbol)         //点符号选项设置
            {
                this.gBxMarkerSymbol.Visible = true;
                this.gBxLineSymbol.Visible = false;
                this.gBxFillSymbol.Visible = false;
                this.gBxNorthArrow.Visible = false;
                this.gBxScaleBar.Visible = false;
                this.gBxText.Visible = false;
                ctl = gBxMarkerSymbol;
            }
            else if (this.m_SymbolType == SymbolType.stLineSymbol)      //线符号选项设置
            {
                this.gBxMarkerSymbol.Visible = false;
                this.gBxLineSymbol.Visible = true;
                this.gBxFillSymbol.Visible = false;
                this.gBxNorthArrow.Visible = false;
                this.gBxScaleBar.Visible = false;
                this.gBxText.Visible = false;
                ctl = gBxLineSymbol;
            }
            else if (this.m_SymbolType == SymbolType.stFillSymbol)      //面符号选项设置
            {
                this.gBxMarkerSymbol.Visible = false;
                this.gBxLineSymbol.Visible = false;
                this.gBxFillSymbol.Visible = true;
                this.gBxNorthArrow.Visible = false;
                this.gBxScaleBar.Visible = false;
                this.gBxText.Visible = false;
                ctl = gBxFillSymbol;
            }
            else if (this.m_SymbolType == SymbolType.stNorthArrows)
            {
                this.gBxMarkerSymbol.Visible = false;
                this.gBxLineSymbol.Visible = false;
                this.gBxFillSymbol.Visible = false;
                this.gBxNorthArrow.Visible = true;
                this.gBxScaleBar.Visible = false;
                this.gBxText.Visible = false;
                ctl = gBxNorthArrow;
            }
            else if (this.m_SymbolType == SymbolType.stScaleBars)
            {
                this.gBxMarkerSymbol.Visible = false;
                this.gBxLineSymbol.Visible = false;
                this.gBxFillSymbol.Visible = false;
                this.gBxNorthArrow.Visible = false;
                this.gBxScaleBar.Visible = true;
                this.gBxText.Visible = false;
                ctl = gBxScaleBar;
            }
            else if (this.m_SymbolType == SymbolType.stTextSymbol)
            {
                this.gBxMarkerSymbol.Visible = false;
                this.gBxLineSymbol.Visible = false;
                this.gBxFillSymbol.Visible = false;
                this.gBxNorthArrow.Visible = false;
                this.gBxScaleBar.Visible = false;
                this.gBxText.Visible = true;
                ctl = gBxText;
            }
            else
                return;
            pnlSymbolSetting.Height = ctl.Height + 5;
            pnlSymbolSetting.Controls.Clear();
            pnlSymbolSetting.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;
            #endregion
            //初始化符号样式选项
            this.RefreshSymbolStyle();
        }

        //初始化预览框
        private void InitialPreview()
        {
            this.stypicPreview.Symbol = this.m_InitialSymbol;
            this.stypicPreview.Refresh();
        }

        private bool m_FlagSymbolStyle;
        private void RefreshSymbolStyle()
        {
            m_FlagSymbolStyle = true;
            if (this.m_SelectedSymbol == null)
                return;
            if (this.m_SymbolType == SymbolType.stMarkerSymbol)         //刷新点符号选项
            {
                IMarkerSymbol pMarkerSymbol;
                pMarkerSymbol = this.m_SelectedSymbol as IMarkerSymbol;
                this.nudSize.Value = System.Convert.ToDecimal(pMarkerSymbol.Size);
                this.nudAngle.Value = System.Convert.ToDecimal(pMarkerSymbol.Angle);
                //将ESRI的Color转化为System的Color
                Color pColor = new Color();
				if (null != pMarkerSymbol.Color)
				{
					pColor = TransvertColor(pMarkerSymbol.Color);
					this.cPkMarker.Value = pColor;
				}
            }
            else if (this.m_SymbolType == SymbolType.stLineSymbol)      //刷新线符号选项
            {
                ILineSymbol pLineSymbol;
                pLineSymbol = this.m_SelectedSymbol as ILineSymbol;
                this.nudLineWidth.Value = System.Convert.ToDecimal(pLineSymbol.Width);
                //将ESRI的Color转化为System的Color
                Color pColor = new Color();
                pColor = TransvertColor(pLineSymbol.Color);
                this.cPkLine.Value = pColor;
            }
            else if (this.m_SymbolType == SymbolType.stFillSymbol)      //刷新面符号选项
            {
                IFillSymbol pFillSymbol;
                pFillSymbol = this.m_SelectedSymbol as IFillSymbol;
                this.nudFillOutlineWidth.Value = System.Convert.ToDecimal(pFillSymbol.Outline.Width);
                //填充颜色
                Color pColor = new Color();
                if (pFillSymbol.Color != null)
                {
                    //将ESRI的Color转化为System的Color
                    pColor = TransvertColor(pFillSymbol.Color);
                    this.cPkFill.Value = pColor;
                }
                //边线颜色
                pColor = TransvertColor(pFillSymbol.Outline.Color);
                this.cPkFillOutLine.Value = pColor;
            }
            else if (this.m_SymbolType == SymbolType.stNorthArrows)
            {
                IMarkerSymbol pMarkerSymbol;
                pMarkerSymbol = this.m_SelectedSymbol as IMarkerSymbol;
                this.nudNorthArrowSize.Value = System.Convert.ToDecimal(pMarkerSymbol.Size);
                this.nudNorthArrowRotate.Value = System.Convert.ToDecimal(pMarkerSymbol.Angle);
                //将ESRI的Color转化为System的Color
                Color pColor = new Color();
                pColor = TransvertColor(pMarkerSymbol.Color);
                this.cPkNorthArrowColor.Value = pColor;
                this.cbxIsVisible.Checked = true;
            }
            else if (this.m_SymbolType == SymbolType.stScaleBars)
            {
                chkScaleBarIsVisible.Checked = true;
            }
            else if (this.m_SymbolType == SymbolType.stTextSymbol)
            {
                ITextSymbol pTextSymbol = this.m_SelectedSymbol as ITextSymbol;
                Color pColor = new Color();
                pColor = TransvertColor(pTextSymbol.Color);
                IFontDisp pFontFisp = pTextSymbol.Font;
                this.cpkTextColor.Value = pColor;
                this.nudTextSize.Value = pFontFisp.Size;
                this.cbxFontBold.Checked = pFontFisp.Bold;
                this.cbxFontItalic.Checked = pFontFisp.Italic;
                this.cbxFontDeleteLine.Checked = pFontFisp.Strikethrough;
                this.cbxFontUnderline.Checked = pFontFisp.Underline;
                this.cboTextFont.SelectedIndex = this.GetTextFontIndex(this.cboTextFont, pFontFisp.Name);
            }
            else
            {
                m_FlagSymbolStyle = false;
                return;
            }
            m_FlagSymbolStyle = false;
        }

        /// <summary>
        /// 获取字体的Index
        /// </summary>
        /// <param name="pComboBox"></param>
        /// <param name="sFontName"></param>字体的名称
        /// <returns></returns>字体的Index
        private int GetTextFontIndex(ComboBox pComboBox, string sFontName)
        {
            if (pComboBox.Items.Count == 0)
                return -1;
            for (int i =0; i < pComboBox.Items.Count; i++)
            {
                if (pComboBox.Items[i].ToString() == sFontName)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 将ESRI的Color转化为System的Color
        /// </summary>
        /// <param name="pESRIColor"></param>
        /// <returns></returns>
        private Color TransvertColor(IColor pESRIColor)
        {
            int intColor;
            int intRed;
            int intGreen;
            int intBlue;
            int iTransparency;
            Color pColor=new Color();
            intColor = pESRIColor.RGB;
            intRed = intColor % 0x100;
            intGreen = intColor / 0x100 % 0x100;
            intBlue = intColor / 0x10000 % 0x100;
            iTransparency = pESRIColor.Transparency;
            pColor = Color.FromArgb(iTransparency,intRed, intGreen, intBlue);
            return pColor;
        }

        //根据设置修改点状符号大小
        private void nudSize_ValueChanged(object sender, EventArgs e)
        {
            if (m_FlagSymbolStyle == true)
                return;
            if(this.m_SelectedSymbol ==null)
                return;
            IMarkerSymbol pMarkerSymbol;
            pMarkerSymbol = this.m_SelectedSymbol as IMarkerSymbol;
            pMarkerSymbol.Size=System.Convert.ToDouble(this.nudSize.Value);
            this.m_SelectedSymbol = pMarkerSymbol;
            this.stypicPreview.Symbol = this.m_SelectedSymbol as ISymbol;
            this.stypicPreview.Refresh();
        }

        //根据设置修改点状符号旋转角度
        private void nudAngle_ValueChanged(object sender, EventArgs e)
        {
            if (m_FlagSymbolStyle == true)
                return;
            if (this.m_SelectedSymbol == null)
                return;
            IMarkerSymbol pMarkerSymbol;
            pMarkerSymbol = this.m_SelectedSymbol as IMarkerSymbol;
            pMarkerSymbol.Angle = System.Convert.ToDouble(this.nudAngle.Value);
            this.m_SelectedSymbol = pMarkerSymbol;
            this.stypicPreview.Symbol = this.m_SelectedSymbol as ISymbol;
            this.stypicPreview.Refresh();
        }

        //根据设置修改点状符号颜色
        private void cPkMarker_ValueChanged(object sender, EventArgs e)
        {
            if (m_FlagSymbolStyle == true)
                return;
            if (this.m_SelectedSymbol == null)
                return;
            //将System的Color转化为ESRI的Color
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = cPkMarker.Value.R;
            pRgbColor.Green = cPkMarker.Value.G;
            pRgbColor.Blue = cPkMarker.Value.B;
            pRgbColor.Transparency = this.cPkMarker.Value.A;

            IMarkerSymbol pMarkerSymbol;
            pMarkerSymbol = this.m_SelectedSymbol as IMarkerSymbol;
            pMarkerSymbol.Color = pRgbColor as IColor;
            this.m_SelectedSymbol = pMarkerSymbol;
            this.stypicPreview.Symbol = this.m_SelectedSymbol as ISymbol;
            this.stypicPreview.Refresh();
        }

        private void cPkLine_ValueChanged(object sender, EventArgs e)
        {
            if (m_FlagSymbolStyle == true)
                return;
            if (this.m_SelectedSymbol == null)
                return;
            //将System的Color转化为ESRI的Color
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = cPkLine.Value.R;
            pRgbColor.Green = cPkLine.Value.G;
            pRgbColor.Blue = cPkLine.Value.B;
            pRgbColor.Transparency = this.cPkLine.Value.A;

            ILineSymbol pLineSymbol;
            pLineSymbol = this.m_SelectedSymbol as ILineSymbol;
            pLineSymbol.Color = pRgbColor as IColor;
            this.m_SelectedSymbol = pLineSymbol;
            this.stypicPreview.Symbol = this.m_SelectedSymbol as ISymbol;
            this.stypicPreview.Refresh();
        }

        private void nudLineWidth_ValueChanged(object sender, EventArgs e)
        {
            if (m_FlagSymbolStyle == true)
                return;
            if (this.m_SelectedSymbol == null)
                return;
            ILineSymbol pLineSymbol;
            pLineSymbol = this.m_SelectedSymbol as ILineSymbol;
            pLineSymbol.Width = System.Convert.ToDouble(this.nudLineWidth.Value);
            this.m_SelectedSymbol = pLineSymbol;
            this.stypicPreview.Symbol = this.m_SelectedSymbol as ISymbol;
            this.stypicPreview.Refresh();
        }

        private void cPkFill_ValueChanged(object sender, EventArgs e)
        {
            if (m_FlagSymbolStyle == true)
                return;
            if (this.m_SelectedSymbol == null)
                return;
            //将System的Color转化为ESRI的Color
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = this.cPkFill.Value.R;
            pRgbColor.Green = this.cPkFill.Value.G;
            pRgbColor.Blue = this.cPkFill.Value.B;
            pRgbColor.Transparency = this.cPkFill.Value.A;

            IFillSymbol pFillSymbol;
            pFillSymbol = this.m_SelectedSymbol as IFillSymbol;
            pFillSymbol.Color = pRgbColor as IColor;
            this.m_SelectedSymbol = pFillSymbol;
            this.stypicPreview.Symbol = this.m_SelectedSymbol as ISymbol;
            this.stypicPreview.Refresh();
        }

        private void nudFillOutlineWidth_ValueChanged(object sender, EventArgs e)
        {
            if (m_FlagSymbolStyle == true)
                return;
            if (this.m_SelectedSymbol == null)
                return;
            IFillSymbol pFillSymbol;
            pFillSymbol = this.m_SelectedSymbol as IFillSymbol;
            ILineSymbol pLineSymbol;
            pLineSymbol = pFillSymbol.Outline;
            pLineSymbol.Width = System.Convert.ToDouble(this.nudFillOutlineWidth.Value);
            pFillSymbol.Outline = pLineSymbol;
            this.m_SelectedSymbol = pFillSymbol;
            this.stypicPreview.Symbol = this.m_SelectedSymbol as ISymbol;
            this.stypicPreview.Refresh();
        }

        private void cPkFillOutLine_ValueChanged(object sender, EventArgs e)
        {
            if (m_FlagSymbolStyle == true)
                return;
            if (this.m_SelectedSymbol == null)
                return;
            IFillSymbol pFillSymbol;
            //将System的Color转化为ESRI的Color
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = this.cPkFillOutLine.Value.R;
            pRgbColor.Green = this.cPkFillOutLine.Value.G;
            pRgbColor.Blue = this.cPkFillOutLine.Value.B;
            pRgbColor.Transparency = this.cPkFillOutLine.Value.A;

            pFillSymbol = this.m_SelectedSymbol as IFillSymbol;
            ILineSymbol pLineSymbol;
            pLineSymbol = pFillSymbol.Outline;
            pLineSymbol.Color = pRgbColor;
            pFillSymbol.Outline = pLineSymbol;
            this.m_SelectedSymbol = pFillSymbol;
            this.stypicPreview.Symbol = this.m_SelectedSymbol as ISymbol;
            this.stypicPreview.Refresh();
        }

        private void butRedo_Click(object sender, EventArgs e)
        {
            this.m_SelectedSymbol = this.m_InitialSymbol;
            this.stypicPreview.Symbol = this.m_SelectedSymbol as ISymbol;
            this.stypicPreview.Refresh();
        }

        private void butMoreSymbol_Click(object sender, EventArgs e)
        {
            OpenFileDialog ODialog = new OpenFileDialog();
            ODialog.Filter="ESRI ServerStyle(*.ServerStyle)|*.ServerStyle";
			ODialog.InitialDirectory = Application.StartupPath;
            if (ODialog.ShowDialog() == DialogResult.OK)
            {
                this.m_strStylePath.Add(ODialog.FileName.Trim());
                this.RefreshSymbologyControl();
            }
            ODialog.Dispose();
        }

        private void nudNorthArrowSize_ValueChanged(object sender, EventArgs e)
        {
            if (m_FlagSymbolStyle == true)
                return;
            if (this.m_SelectedSymbol == null)
                return;

            m_MarkerNorthArrow.MarkerSymbol = this.stypicPreview.Symbol as IMarkerSymbol;
            (m_MarkerNorthArrow as INorthArrow).Size = System.Convert.ToDouble(this.nudNorthArrowSize.Value);
            this.m_SelectedSymbol = m_MarkerNorthArrow.MarkerSymbol;
            this.stypicPreview.Symbol = this.m_SelectedSymbol as ISymbol;
            this.stypicPreview.Refresh();
        }

        private void nudNorthArrowRotate_ValueChanged(object sender, EventArgs e)
        {
            if (m_FlagSymbolStyle == true)
                return;
            if (this.m_SelectedSymbol == null)
                return;

            m_MarkerNorthArrow.MarkerSymbol = this.stypicPreview.Symbol as IMarkerSymbol;
            (m_MarkerNorthArrow as INorthArrow).CalibrationAngle = System.Convert.ToDouble(this.nudNorthArrowRotate.Value);
            this.m_SelectedSymbol = m_MarkerNorthArrow.MarkerSymbol;
            this.stypicPreview.Symbol = this.m_SelectedSymbol as ISymbol;
            this.stypicPreview.Refresh();
        }

        private void cPkNorthArrowColor_ValueChanged(object sender, EventArgs e)
        {
            if (m_FlagSymbolStyle == true)
                return;
            if (this.m_SelectedSymbol == null)
                return;
            //将System的Color转化为ESRI的Color
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = cPkNorthArrowColor.Value.R;
            pRgbColor.Green = cPkNorthArrowColor.Value.G;
            pRgbColor.Blue = cPkNorthArrowColor.Value.B;
            pRgbColor.Transparency = this.cPkNorthArrowColor.Value.A;

            m_MarkerNorthArrow.MarkerSymbol = this.stypicPreview.Symbol as IMarkerSymbol;
            (m_MarkerNorthArrow as INorthArrow).Color = pRgbColor as IColor;
            this.m_SelectedSymbol = m_MarkerNorthArrow.MarkerSymbol;
            this.stypicPreview.Symbol = this.m_SelectedSymbol as ISymbol;
            this.stypicPreview.Refresh();
        }

        private void butProperties_Click(object sender, EventArgs e)
        {
            frmScaleBarProperties frmScaleBarPropertiesNew = new frmScaleBarProperties();
            frmScaleBarPropertiesNew.ScaleBar = m_ScaleBar;
            if (frmScaleBarPropertiesNew.ShowDialog() == DialogResult.OK)
            {
                this.stypicPreview.Item = m_ScaleBar;
                this.stypicPreview.StyleGalleryClass = m_StyleGalleryClass;
                this.stypicPreview.Refresh();
            }
        }

        private void SymbologyControl_OnItemSelected(object sender, ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            IStyleGalleryItem pStyleGalleryItem = e.styleGalleryItem as IStyleGalleryItem;

            RefreshSymbol(pStyleGalleryItem);
        }

        private void cpkTextColor_ValueChanged(object sender, EventArgs e)
        {
            if (m_FlagSymbolStyle == true)
                return;
            if (this.m_SelectedSymbol == null)
                return;
            //将System的Color转化为ESRI的Color
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = cpkTextColor.Value.R;
            pRgbColor.Green = cpkTextColor.Value.G;
            pRgbColor.Blue = cpkTextColor.Value.B;
            pRgbColor.Transparency = this.cpkTextColor.Value.A;

            ITextSymbol pTextSymbol = this.m_SelectedSymbol as ITextSymbol;
            pTextSymbol.Color = pRgbColor as IColor;
            this.m_SelectedSymbol = pTextSymbol;
            this.stypicPreview.Symbol = this.m_SelectedSymbol as ISymbol;
            this.stypicPreview.Refresh();
        }

        private void cboTextFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_FlagSymbolStyle == true)
                return;
            if (this.m_SelectedSymbol == null)
                return;
            ITextSymbol pTextSymbol = this.m_SelectedSymbol as ITextSymbol;
            IFontDisp pFontDisp = pTextSymbol.Font;
            pFontDisp.Name = this.cboTextFont.Text;
            pTextSymbol.Font = pFontDisp;
            this.m_SelectedSymbol = pTextSymbol;
            this.stypicPreview.Symbol = this.m_SelectedSymbol as ISymbol;
            this.stypicPreview.Refresh();
        }

        private void nudTextSize_ValueChanged(object sender, EventArgs e)
        {
            if (m_FlagSymbolStyle == true)
                return;
            if (this.m_SelectedSymbol == null)
                return;
            ITextSymbol pTextSymbol = this.m_SelectedSymbol as ITextSymbol;
            IFontDisp pFontDisp = pTextSymbol.Font;
            pFontDisp.Size = nudTextSize.Value;
            pTextSymbol.Font = pFontDisp;
            this.m_SelectedSymbol = pTextSymbol;
            this.stypicPreview.Symbol = this.m_SelectedSymbol as ISymbol;
            this.stypicPreview.Refresh();
        }

        private void cbxFontBold_CheckedChanged(object sender, EventArgs e)
        {
            if (m_FlagSymbolStyle == true)
                return;
            if (this.m_SelectedSymbol == null)
                return;
            ITextSymbol pTextSymbol = this.m_SelectedSymbol as ITextSymbol;
            IFontDisp pFontDisp = pTextSymbol.Font;
            pFontDisp.Bold = cbxFontBold.Checked;
            pTextSymbol.Font = pFontDisp;
            this.m_SelectedSymbol = pTextSymbol;
            this.stypicPreview.Symbol = this.m_SelectedSymbol as ISymbol;
            this.stypicPreview.Refresh();
        }

        private void cbxFontItalic_CheckedChanged(object sender, EventArgs e)
        {
            if (m_FlagSymbolStyle == true)
                return;
            if (this.m_SelectedSymbol == null)
                return;
            ITextSymbol pTextSymbol = this.m_SelectedSymbol as ITextSymbol;
            IFontDisp pFontDisp = pTextSymbol.Font;
            pFontDisp.Italic = cbxFontItalic.Checked;
            pTextSymbol.Font = pFontDisp;
            this.m_SelectedSymbol = pTextSymbol;
            this.stypicPreview.Symbol = this.m_SelectedSymbol as ISymbol;
            this.stypicPreview.Refresh();
        }

        private void cbxFontUnderline_CheckedChanged(object sender, EventArgs e)
        {
            if (m_FlagSymbolStyle == true)
                return;
            if (this.m_SelectedSymbol == null)
                return;
            ITextSymbol pTextSymbol = this.m_SelectedSymbol as ITextSymbol;
            IFontDisp pFontDisp = pTextSymbol.Font;
            pFontDisp.Underline = cbxFontUnderline.Checked;
            pTextSymbol.Font = pFontDisp;
            this.m_SelectedSymbol = pTextSymbol;
            this.stypicPreview.Symbol = this.m_SelectedSymbol as ISymbol;
            this.stypicPreview.Refresh();
        }

        private void cbxFontDeleteLine_CheckedChanged(object sender, EventArgs e)
        {
            if (m_FlagSymbolStyle == true)
                return;
            if (this.m_SelectedSymbol == null)
                return;
            ITextSymbol pTextSymbol = this.m_SelectedSymbol as ITextSymbol;
            IFontDisp pFontDisp = pTextSymbol.Font;
            pFontDisp.Strikethrough = cbxFontDeleteLine.Checked;
            pTextSymbol.Font = pFontDisp;
            this.m_SelectedSymbol = pTextSymbol;
            this.stypicPreview.Symbol = this.m_SelectedSymbol as ISymbol;
            this.stypicPreview.Refresh();
        }

        private void frmSymbolSelector_FormClosed(object sender, FormClosedEventArgs e)
        {
            int refsLeft = 0;
            if (m_StyleGalleryClass != null)
            {
                do
                {
                    refsLeft = System.Runtime.InteropServices.Marshal.ReleaseComObject(m_StyleGalleryClass);
                }
                while (refsLeft > 0);
            }
            refsLeft = 0;
            if (m_PreviewStyleGalleryClass != null)
            {
                do
                {
                    refsLeft = System.Runtime.InteropServices.Marshal.ReleaseComObject(m_PreviewStyleGalleryClass);
                }
                while (refsLeft > 0);
            }
            if(SymbologyControl !=null)
            {
                SymbologyControl.Dispose();
            }
        }

        private void butProperty_Click(object sender, EventArgs e)
        {
            try
            {
                IStyleGalleryItem tStyleGalleryItem = new ServerStyleGalleryItemClass();
                tStyleGalleryItem.Item = m_SelectedSymbol;
                FormSymbolPropertyEdit frm = null;
                frm = new FormSymbolPropertyEdit(tStyleGalleryItem);
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    tStyleGalleryItem.Item = frm.Symbol;

                    RefreshSymbol(tStyleGalleryItem);
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        /// <summary>
        /// 刷新样式预览
        /// </summary>
        /// <param name="tStyleGalleryItem"></param>
        private void RefreshSymbol(IStyleGalleryItem tStyleGalleryItem)
        {
            if (m_SymbolType == SymbolType.stNorthArrows)
            {
                IMarkerSymbol pMarkerSymbol = (tStyleGalleryItem.Item as IMarkerNorthArrow).MarkerSymbol;
                this.stypicPreview.Symbol = pMarkerSymbol as ISymbol;
                this.m_SelectedSymbol = pMarkerSymbol as ISymbol;
                this.m_MarkerNorthArrow.MarkerSymbol = pMarkerSymbol;
            }
            else if (m_SymbolType == SymbolType.stScaleBars)
            {
                this.stypicPreview.StyleGalleryClass = m_StyleGalleryClass;
                this.stypicPreview.Item = tStyleGalleryItem.Item;
                m_ScaleBar = tStyleGalleryItem.Item;
                (m_ScaleBar as IScaleBar).Units = CommonFunction.MapUnits(m_MapUnits);
                (m_ScaleBar as IScaleBar).Division = m_ScaleDivision;
                (m_ScaleBar as IScaleBar).UnitLabel = m_MapUnits;
                this.m_SelectedSymbol = m_ScaleBar;
            }
            else if (m_SymbolType == SymbolType.stLegendItems)
            {
                this.stypicPreview.Symbol = tStyleGalleryItem as ISymbol;
                this.m_SelectedSymbol = tStyleGalleryItem.Item;
            }
            else
            {
                this.stypicPreview.Symbol = tStyleGalleryItem.Item as ISymbol;
                ITextSymbol pTextSymbol = tStyleGalleryItem.Item as ITextSymbol;
                if (pTextSymbol != null)
                {
                    ITextSymbol tSourseSymbol = this.m_SelectedSymbol as ITextSymbol;
                    pTextSymbol.Text = tSourseSymbol.Text;
                }
                this.m_SelectedSymbol = tStyleGalleryItem.Item as ISymbol;
            }
            this.stypicPreview.Refresh();
            this.RefreshSymbolStyle();
        }
    }
}