using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using stdole;

namespace AG.COM.SDM.StyleManager
{
    public partial class frmTextScaleBarSymbol : SkinForm
    {
        private object m_ScaleBar;
        private string m_strStyleGalleryClass;
        private Boolean m_bIsInitialed;
        private ISymbol m_InitialSymbol;
        private object m_SelectedSymbol;
        private ImageList m_Imagelist;
        private int m_intImageIndex;
        private IList<string> m_strStylePath = new List<string>();
        private IMarkerNorthArrow m_MarkerNorthArrow = new MarkerNorthArrowClass();
        private IStyleGalleryClass m_StyleGalleryClass = null;
       
        public frmTextScaleBarSymbol()
        {
            this.m_bIsInitialed = false;
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
            get 
            {
                return m_SymbolType; 
            }
            set
            {
                m_SymbolType = value;
            }
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
                if (m_SymbolType == SymbolType.stNorthArrows)
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
                else
                {
                    return this.stypicPreview.Symbol as object;
                }
            }
        }

        private void frmSymbolSelector_Load(object sender, EventArgs e)
        {
            this.m_bIsInitialed = false;
            //根据传入参数设置m_strStyleGalleryClass(符号类别归属)
            this.InitialParameter();
            //初始化Listview框
            this.InitialListViewSymbol();
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

            this.m_bIsInitialed = true;
        }

        //初始化模块变量参数及控件
        private void InitialParameter()
        {
            string strPath = CommonConstString.STR_StylePath + @"\p408_cc.ServerStyle";
			this.m_strStylePath.Add(strPath);
            if (System.IO.File.Exists(m_strStylePath[0]) != true)
                return;
            if (this.m_SymbolType == SymbolType.stMarkerSymbol)
            {
                this.m_strStyleGalleryClass = "Marker Symbols";
            }
            else if (this.m_SymbolType == SymbolType.stLineSymbol)
            {
                this.m_strStyleGalleryClass = "Line Symbols";
            }
            else if (this.m_SymbolType == SymbolType.stFillSymbol)
            {
                this.m_strStyleGalleryClass = "Fill Symbols";
            }
            else if (this.m_SymbolType == SymbolType.stTextSymbol)
            {
                this.m_strStyleGalleryClass = "Text Symbols";
            }
            else if (this.m_SymbolType == SymbolType.stNorthArrows)
            {
                this.m_strStyleGalleryClass = "North Arrows";
            }
            else if (this.m_SymbolType == SymbolType.stScaleBars)
            {
                this.m_strStyleGalleryClass = "Scale Bars";
            }
            else
                return;
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
            //初始化类型列表
            this.cbxCategory.Items.Clear();
            this.cbxCategory.Refresh();
            //文字的大小控件的初始设置
            this.nudTextSize.Value = 1;
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
        }

        //初始化符号样式选项
        private void InitialSymbolStyle()
        {
            Control ctl = null;
            //初始化选择符号
            this.m_SelectedSymbol = this.m_InitialSymbol;
            if (this.m_SymbolType == SymbolType.stMarkerSymbol)         //点符号选项设置
            {
                this.gBxMarkerSymbol.Visible = true;
                this.gBxLineSymbol.Visible = false;
                this.gBxFillSymbol.Visible = false;
                this.gBxNorthArrow.Visible = false;
                this.gBxScaleBar.Visible = false;
                ctl = gBxMarkerSymbol;
            }
            else if (this.m_SymbolType == SymbolType.stLineSymbol)      //线符号选项设置
            {
                this.gBxMarkerSymbol.Visible = false;
                this.gBxLineSymbol.Visible = true;
                this.gBxFillSymbol.Visible = false;
                this.gBxNorthArrow.Visible = false;
                this.gBxScaleBar.Visible = false;
                ctl = gBxLineSymbol;
            }
            else if (this.m_SymbolType == SymbolType.stFillSymbol)      //面符号选项设置
            {
                this.gBxMarkerSymbol.Visible = false;
                this.gBxLineSymbol.Visible = false;
                this.gBxFillSymbol.Visible = true;
                this.gBxNorthArrow.Visible = false;
                this.gBxScaleBar.Visible = false;
                ctl = gBxFillSymbol;
            }
            else if (this.m_SymbolType == SymbolType.stNorthArrows)
            {
                this.gBxMarkerSymbol.Visible = false;
                this.gBxLineSymbol.Visible = false;
                this.gBxFillSymbol.Visible = false;
                this.gBxNorthArrow.Visible = true;
                this.gBxScaleBar.Visible = false;
                ctl = gBxNorthArrow;
            }
            else if (this.m_SymbolType == SymbolType.stScaleBars)
            {
                this.gBxMarkerSymbol.Visible = false;
                this.gBxLineSymbol.Visible = false;
                this.gBxFillSymbol.Visible = false;
                this.gBxNorthArrow.Visible = false;
                this.gBxScaleBar.Visible = true;
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

            //初始化符号样式选项
            this.RefreshSymbolStyle();
        }

        //初始化预览框
        private void InitialPreview()
        {
            this.stypicPreview.Symbol = this.m_InitialSymbol;
            this.stypicPreview.Refresh();
        }

        //初始化或更新Listview框
        private void InitialListViewSymbol()
        {
            string strStyleGalleryClass;
            strStyleGalleryClass = "";
            if (this.cbxCategory.Text.Trim().ToString() != "<All>")
                strStyleGalleryClass = this.cbxCategory.Text.Trim().ToString();
            int i;
            int j;
            //初始化变量
            this.lvwSymbol.Items.Clear();
            m_Imagelist = new ImageList();
            if (m_SymbolType == SymbolType.stTextSymbol)
            {
                m_Imagelist.ImageSize = new Size(160, 32);
            }
            else if (m_SymbolType == SymbolType.stScaleBars)
            {
                m_Imagelist.ImageSize = new Size(256, 32);
            }
            else
            {
                m_Imagelist.ImageSize = new Size(32, 32);
            }
            m_Imagelist.Images.Clear();
            this.lvwSymbol.LargeImageList = m_Imagelist;
            m_intImageIndex = 0;
            try
            {
                for (j = 0; j < this.m_strStylePath.Count; j++)
                {
                    //添加符号类别控制
                    IStyleGallery pStyleGallery = new ServerStyleGalleryClass();
                    IStyleGalleryStorage pStyleGalleryStorage;
                    pStyleGalleryStorage = pStyleGallery as IStyleGalleryStorage;
                    pStyleGalleryStorage.AddFile(m_strStylePath[j]);
                    IEnumBSTR pEnumBSTR = pStyleGallery.get_Categories(this.m_strStyleGalleryClass);
                    if (this.m_bIsInitialed == false)
                    {
                        if (j == 0)
                        {
                            this.cbxCategory.Items.Add("<All>");
                            this.cbxCategory.Text = "<All>";
                        }
                        pEnumBSTR.Reset();
                        string strCategory;
                        strCategory = pEnumBSTR.Next();
                        while (strCategory != null)
                        {
                            for (i = 0; i < this.cbxCategory.Items.Count; i++)
                            {
                                if (this.cbxCategory.Items[i].ToString().Trim() == strCategory)
                                    break;
                            }
                            if (i >= this.cbxCategory.Items.Count)
                                this.cbxCategory.Items.Add(strCategory);
                            strCategory = pEnumBSTR.Next();
                        }
                    }
                    else
                    {
                        pEnumBSTR.Reset();
                        string strCategory;
                        strCategory = pEnumBSTR.Next();
                        while (strCategory != null)
                        {
                            for (i = 0; i < this.cbxCategory.Items.Count; i++)
                            {
                                if (this.cbxCategory.Items[i].ToString().Trim() == strCategory)
                                    break;
                            }
                            if (i >= this.cbxCategory.Items.Count)
                                this.cbxCategory.Items.Add(strCategory);
                            strCategory = pEnumBSTR.Next();
                        }
                    }
                    //获取StyleGalleryClass
                    IStyleGalleryClass pStyleGalleryClass;
                    pStyleGalleryClass = null;
                    for (i = 0; i < pStyleGallery.ClassCount; i++)
                    {
                        pStyleGalleryClass = pStyleGallery.get_Class(i);
                        if (pStyleGalleryClass.Name == this.m_strStyleGalleryClass)
                            break;
                    }
                    //添加Listview符号显示
                    IEnumStyleGalleryItem pEnumStyleGalleryItem = new EnumServerStyleGalleryItemClass();
                    pEnumStyleGalleryItem = pStyleGallery.get_Items(this.m_strStyleGalleryClass, m_strStylePath[j], strStyleGalleryClass);
                    pEnumStyleGalleryItem.Reset();
                    IStyleGalleryItem pEnumItem;
                    Bitmap Bmp;
                    pEnumItem = pEnumStyleGalleryItem.Next();
                    while (pEnumItem != null)
                    {
                        if (m_SymbolType == SymbolType.stTextSymbol)
                        {
                            Bmp = StyleGalleryItemView.StyleGalleryItemToBmp(160, 32, pStyleGalleryClass, pEnumItem);
                        }
                        else if (m_SymbolType == SymbolType.stScaleBars)
                        {
                            Bmp = StyleGalleryItemView.StyleGalleryItemToBmp(256, 32, pStyleGalleryClass, pEnumItem);
                        }
                        else
                        {
                            Bmp = StyleGalleryItemView.StyleGalleryItemToBmp(32, 32, pStyleGalleryClass, pEnumItem);
                        }
                        m_Imagelist.Images.Add(Bmp);
                        this.lvwSymbol.Items.Add(this.m_strStylePath[j], pEnumItem.Name, m_intImageIndex);
                        m_intImageIndex++;
                        pEnumItem = pEnumStyleGalleryItem.Next();
                    }
                    //释放非托管变量
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumBSTR);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumStyleGalleryItem);
                    pStyleGalleryStorage.RemoveFile(m_strStylePath[j]);
                    //int refsLeft = 0;
                    //do
                    //{
                    //    refsLeft = System.Runtime.InteropServices.Marshal.ReleaseComObject(pStyleGalleryClass);
                    //}
                    //while (refsLeft > 0);
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_bIsInitialed == true)                 //避免因初始化时设置cbxCategory值而陷入死循环
                this.InitialListViewSymbol();
        }

        private void lvwSymbol_SelectedIndexChanged(object sender, EventArgs e)
        {
            //根据选择获取并设置预览符号
            string strSymbolName;
            string strPath;
            int j;
            int i;
            if (this.lvwSymbol.SelectedItems.Count != 0)
            {
                strSymbolName = this.lvwSymbol.SelectedItems[0].Text.Trim();
                strPath = this.lvwSymbol.SelectedItems[0].Name.Trim();
            }
            else
                return;
            string strStyleGalleryClass;
            strStyleGalleryClass = "";
            if (this.cbxCategory.Text.Trim().ToString() != "<All>")
                strStyleGalleryClass = this.cbxCategory.Text.Trim().ToString();

            

            for (j = 0; j < this.m_strStylePath.Count; j++)
            {
                //获取符号
                IStyleGallery pStyleGallery = new ServerStyleGalleryClass();
                IStyleGalleryStorage pStyleGalleryStorage;
                pStyleGalleryStorage = pStyleGallery as IStyleGalleryStorage;
                pStyleGalleryStorage.AddFile(m_strStylePath[j]);

                //获取StyleGalleryClass
                for (i = 0; i < pStyleGallery.ClassCount; i++)
                {
                    m_StyleGalleryClass = pStyleGallery.get_Class(i);
                    if (m_StyleGalleryClass.Name == this.m_strStyleGalleryClass)
                        break;
                }

                IEnumStyleGalleryItem pEnumStyleGalleryItem = new EnumServerStyleGalleryItemClass();
                pEnumStyleGalleryItem = pStyleGallery.get_Items(this.m_strStyleGalleryClass, m_strStylePath[j], strStyleGalleryClass);
                pEnumStyleGalleryItem.Reset();
                IStyleGalleryItem pEnumItem;
                pEnumItem = pEnumStyleGalleryItem.Next();
                while (pEnumItem != null)
                {
                    if (pEnumItem.Name == strSymbolName)
                    {
                        if (m_SymbolType == SymbolType.stNorthArrows)
                        {
                            IMarkerSymbol pMarkerSymbol = (pEnumItem.Item as IMarkerNorthArrow).MarkerSymbol;
                            this.stypicPreview.Symbol = pMarkerSymbol as ISymbol;
                            this.m_SelectedSymbol = pMarkerSymbol as ISymbol;
                            this.m_MarkerNorthArrow.MarkerSymbol = pMarkerSymbol;
                        }
                        else if (m_SymbolType == SymbolType.stScaleBars)
                        {
                            this.stypicPreview.StyleGalleryClass= m_StyleGalleryClass;
                            this.stypicPreview.Item = pEnumItem.Item;
                            m_ScaleBar = pEnumItem.Item;
                            (m_ScaleBar as IScaleBar).Units = CommonFunction.MapUnits(m_MapUnits);
                            (m_ScaleBar as IScaleBar).Division = m_ScaleDivision;
                            (m_ScaleBar as IScaleBar).UnitLabel = m_MapUnits;
                            this.m_SelectedSymbol = m_ScaleBar;
                        }
                        else if (m_SymbolType == SymbolType.stTextSymbol)
                        {
                            this.m_SelectedSymbol = pEnumItem.Item as ISymbol;
                            if (pEnumItem.Name == "U.S. Route HWY" || pEnumItem.Name == "U.S. Interstate HWY")
                            { (this.m_SelectedSymbol as ITextSymbol).Text = "55"; }
                            else
                            { (this.m_SelectedSymbol as ITextSymbol).Text = "AaBbYyZz"; }
                            this.stypicPreview.Symbol = m_SelectedSymbol as ISymbol;
                        }
                        else
                        {
                            this.m_SelectedSymbol = pEnumItem.Item as ISymbol;
                            this.stypicPreview.Symbol = m_SelectedSymbol as ISymbol;
                        }
                        this.stypicPreview.Refresh();
                        this.RefreshSymbolStyle();
                        pStyleGalleryStorage.RemoveFile(m_strStylePath[j]);
                        return;
                    }
                    pEnumItem = pEnumStyleGalleryItem.Next();
                }
                //释放非托管变量
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumStyleGalleryItem);
                if (pEnumItem != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumItem);
            }
        }

        private bool m_FlagSymbolStyle;
        /// <summary>
        /// 根据选择的符号刷新符号样式选项
        /// </summary>
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
                pColor = TransvertColor(pMarkerSymbol.Color);
                this.cPkMarker.Value = pColor;
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
            for (int i = 0; i < pComboBox.Items.Count; i++)
            {
                if (pComboBox.Items[i].ToString() == sFontName)
                {
                    return i;
                }
            }
            return -1;
        }

        //将ESRI的Color转化为System的Color
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
            pColor = Color.FromArgb(iTransparency, intRed, intGreen, intBlue);
            return pColor;
        }

        //根据设置修改点状符号大小
        private void nudSize_ValueChanged(object sender, EventArgs e)
        {
            if (m_FlagSymbolStyle == true)
                return;
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
            pRgbColor.Transparency = cPkMarker.Value.A;

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
                this.InitialListViewSymbol();
            }
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
            pRgbColor.Transparency = cPkNorthArrowColor.Value.A;

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
            pRgbColor.Transparency = cpkTextColor.Value.A;

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
    }
}