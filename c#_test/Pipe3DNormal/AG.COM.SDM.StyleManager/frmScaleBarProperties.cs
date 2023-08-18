using System;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StyleManager
{
    public partial class frmScaleBarProperties : Form
    {
        private object m_ScaleBar;
        public object ScaleBar
        {
            get { return m_ScaleBar; }
            set { m_ScaleBar = value; }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public frmScaleBarProperties()
        {
            InitializeComponent();
        }

        private void frmScaleBarProperties_Load(object sender, EventArgs e)
        {
            //初始化控件
            InitialControls();
            //初始化比例尺默认值
            InitialScaleBarProperties();
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            SetScaleBar();
            this.DialogResult = DialogResult.OK;
        }

        private void sbtMarkDivisionSymbol_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            frmSymbolSelector frmSymbolSelectorNew = new frmSymbolSelector();
            if (sbtMarkDivisionSymbol.Symbol != null)
            { frmSymbolSelectorNew.InitialSymbol = sbtMarkDivisionSymbol.Symbol; }
            frmSymbolSelectorNew.SymbolType = SymbolType.stLineSymbol;
            if (frmSymbolSelectorNew.ShowDialog() == DialogResult.OK)
            {
                sbtMarkDivisionSymbol.Symbol = frmSymbolSelectorNew.SelectedSymbol as ISymbol;
            }
            sbtMarkDivisionSymbol.Refresh();
            this.Cursor = Cursors.Default;
        }

        private void sbtMarkSubdivisionSymbol_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            frmSymbolSelector frmSymbolSelectorNew = new frmSymbolSelector();
            if (sbtMarkSubdivisionSymbol.Symbol != null)
            { frmSymbolSelectorNew.InitialSymbol = sbtMarkSubdivisionSymbol.Symbol; }
            frmSymbolSelectorNew.SymbolType = SymbolType.stLineSymbol;
            if (frmSymbolSelectorNew.ShowDialog() == DialogResult.OK)
            {
                sbtMarkSubdivisionSymbol.Symbol = frmSymbolSelectorNew.SelectedSymbol as ISymbol;
            }
            sbtMarkSubdivisionSymbol.Refresh();
            this.Cursor = Cursors.Default;
        }

        private void sbtScaleBar1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            frmSymbolSelector frmSymbolSelectorNew = new frmSymbolSelector();
            if (sbtScaleBar1.Symbol != null)
            { frmSymbolSelectorNew.InitialSymbol = sbtScaleBar1.Symbol; }
            if (m_ScaleBar is IScaleLine)   //线
            {
                frmSymbolSelectorNew.SymbolType = SymbolType.stLineSymbol;
                if (frmSymbolSelectorNew.ShowDialog() == DialogResult.OK)
                {
                    sbtScaleBar1.Symbol = frmSymbolSelectorNew.SelectedSymbol as ISymbol;
                }
            }
            else if (m_ScaleBar is IDoubleFillScaleBar)    //面        
            {
                frmSymbolSelectorNew.SymbolType = SymbolType.stFillSymbol;
                if (frmSymbolSelectorNew.ShowDialog() == DialogResult.OK)
                {
                    sbtScaleBar1.Symbol = frmSymbolSelectorNew.SelectedSymbol as ISymbol;
                }
            }
            else                                          //面ISingleFillScaleBar
            {
                frmSymbolSelectorNew.SymbolType = SymbolType.stFillSymbol;
                if (frmSymbolSelectorNew.ShowDialog() == DialogResult.OK)
                {
                    sbtScaleBar1.Symbol = frmSymbolSelectorNew.SelectedSymbol as ISymbol;
                }
            }
            sbtScaleBar1.Refresh();
            this.Cursor = Cursors.Default;
        }

        private void sbtScaleBar2_Click(object sender, EventArgs e)
        {
            if ((m_ScaleBar is IDoubleFillScaleBar) == false)
                return;
            this.Cursor = Cursors.WaitCursor;
            frmSymbolSelector frmSymbolSelectorNew = new frmSymbolSelector();
            frmSymbolSelectorNew.SymbolType = SymbolType.stFillSymbol;
            if (sbtScaleBar2.Symbol != null)
            { frmSymbolSelectorNew.InitialSymbol = sbtScaleBar2.Symbol; }
            if (frmSymbolSelectorNew.ShowDialog() == DialogResult.OK)
            {
                sbtScaleBar2.Symbol = frmSymbolSelectorNew.SelectedSymbol as ISymbol;
                sbtScaleBar2.Refresh();
            }
            this.Cursor = Cursors.Default;
        }

        private void cboMarksFrequency_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboMarksFrequency.Text == "无")
            {
                cboMarksPosition.Enabled = false;
                nudMarkHeight.Enabled = false;
                nudSubMarkheight.Enabled = false;
                sbtMarkDivisionSymbol.Enabled = false;
                sbtMarkSubdivisionSymbol.Enabled = false;
            }
            else
            {
                cboMarksPosition.Enabled = true;
                nudMarkHeight.Enabled = true;
                nudSubMarkheight.Enabled = true;
                sbtMarkDivisionSymbol.Enabled = true;
                sbtMarkSubdivisionSymbol.Enabled = true;
            }
        }

        #region 自定义函数
        //初始化控件
        private void InitialControls()
        {
            //单位
            nudLabelGap.Value = 0;
            nudLabelGap.Maximum = Int32.MaxValue;
            nudLabelGap.Minimum = Int32.MinValue;
            nudLabelGap.Increment = 1;
            nudLabelGap.ThousandsSeparator = false;
            //数字
            nudNumberGap.Value = 0;
            nudNumberGap.Maximum = Int32.MaxValue;
            nudNumberGap.Minimum = Int32.MinValue;
            nudNumberGap.Increment = 1;
            nudNumberGap.ThousandsSeparator = false;
            //分割
            nudMarkHeight.Value = 0;
            nudMarkHeight.Maximum = Int32.MaxValue;
            nudMarkHeight.Minimum = Int32.MinValue;
            nudMarkHeight.Increment = 1;
            nudMarkHeight.ThousandsSeparator = false;
            nudSubMarkheight.Value = 0;
            nudSubMarkheight.Maximum = Int32.MaxValue;
            nudSubMarkheight.Minimum = Int32.MinValue;
            nudSubMarkheight.Increment = 1;
            nudSubMarkheight.ThousandsSeparator = false;
            //刻度
            nudDivisionValue.Value = 0;
            nudDivisionValue.Maximum = System.Convert.ToDecimal(Int64.MaxValue);
            nudDivisionValue.Minimum = 1;
            nudDivisionValue.Increment = 1;
            nudDivisionValue.DecimalPlaces = 2;
            nudDivisionValue.ThousandsSeparator = false;
            nudDivisions.Value = 0;
            nudDivisions.Maximum = Int32.MaxValue;
            nudDivisions.Minimum = 1;
            nudDivisions.Increment = 1;
            nudDivisions.ThousandsSeparator = false;
            nudSubdivisions.Value = 0;
            nudSubdivisions.Maximum = Int32.MaxValue;
            nudSubdivisions.Minimum = 0;
            nudSubdivisions.Increment = 1;
            nudSubdivisions.ThousandsSeparator = false;
            chkZeroDivision.Checked = false;
        }

        private void SetScaleBar()
        {
            IScaleBar pScaleBar = m_ScaleBar as IScaleBar;

            //单位
            //分割单位设置
            pScaleBar.Units = CommonFunction.MapUnits(tbxDivisionUnit.Text);
            //标签位置设置
            switch (cboLabelPosition.Text)
            {
                case "比例尺上":                 //"above bar"
                    pScaleBar.UnitLabelPosition = esriScaleBarPos.esriScaleBarAbove;
                    break;
                case "标签前":                   //"before labels"
                    pScaleBar.UnitLabelPosition = esriScaleBarPos.esriScaleBarBeforeLabels;
                    break;
                case "标签后":                   //"after labels"
                    pScaleBar.UnitLabelPosition = esriScaleBarPos.esriScaleBarAfterLabels;
                    break;
                case "比例尺前":                 //"before bar"
                    pScaleBar.UnitLabelPosition = esriScaleBarPos.esriScaleBarBeforeBar;
                    break;
                case "比例尺后":                 //"after bar"
                    pScaleBar.UnitLabelPosition = esriScaleBarPos.esriScaleBarAfterBar;
                    break;
                case "比例尺下":                 //"below bar"
                    pScaleBar.UnitLabelPosition = esriScaleBarPos.esriScaleBarBelow;
                    break;
                default:
                    break;
            }
            //标注内容与标注与比例尺间距的设置
            pScaleBar.UnitLabel = tbxLabel.Text;
            pScaleBar.UnitLabelGap = System.Convert.ToDouble(nudLabelGap.Value);
            //单位文字颜色的设置
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = cpkUnitLabelColor.Value.R;
            pRgbColor.Green = cpkUnitLabelColor.Value.G;
            pRgbColor.Blue = cpkUnitLabelColor.Value.B;
            ITextSymbol pTextSymbol = pScaleBar.UnitLabelSymbol;
            pTextSymbol.Color = pRgbColor as IColor;
            pScaleBar.UnitLabelSymbol = pTextSymbol;

            //数字
            //数字标签的风格设置
            switch (cboNumberFrequency.Text)
            {
                case "无":                               //"no labels"
                    pScaleBar.LabelFrequency = esriScaleBarFrequency.esriScaleBarNone;
                    break;
                case "单一":                             //"single labels"
                    pScaleBar.LabelFrequency = esriScaleBarFrequency.esriScaleBarOne;
                    break;
                case "零分度":                           //"end(and zero)"
                    pScaleBar.LabelFrequency = esriScaleBarFrequency.esriScaleBarMajorDivisions;
                    break;
                case "分割":                             //"divisions"
                    pScaleBar.LabelFrequency = esriScaleBarFrequency.esriScaleBarDivisions;
                    break;
                case "首分割":                           //"divisions and first mid point"
                    pScaleBar.LabelFrequency = esriScaleBarFrequency.esriScaleBarDivisionsAndFirstMidpoint;
                    break;
                case "双分割":                           //"divisions and first subdivisions"
                    pScaleBar.LabelFrequency = esriScaleBarFrequency.esriScaleBarDivisionsAndFirstSubdivisions;
                    break;
                case "全分割":                           //"divisions and all subdivisions"
                    pScaleBar.LabelFrequency = esriScaleBarFrequency.esriScaleBarDivisionsAndSubdivisions;
                    break;
                default:
                    break;
            }
            //数字标签的位置设置
            switch (cboNumberPosition.Text)
            {
                case "比例尺上":
                    pScaleBar.LabelPosition = esriVertPosEnum.esriAbove;
                    break;
                case "比例尺顶":                         //"Align to top of bar"
                    pScaleBar.LabelPosition = esriVertPosEnum.esriTop;
                    break;
                case "比例尺中":                         //"Center on bar"
                    pScaleBar.LabelPosition = esriVertPosEnum.esriOn;
                    break;
                case "比例尺底":                         //"Align to bottom of bar"
                    pScaleBar.LabelPosition = esriVertPosEnum.esriBottom;
                    break;
                case "比例尺下":
                    pScaleBar.LabelPosition = esriVertPosEnum.esriBelow;
                    break;
                default:
                    break;
            }
            //数字标签与图形间的间距
            pScaleBar.LabelGap = System.Convert.ToDouble(nudNumberGap.Value);

            //分割
            IScaleMarks pScaleMarks = pScaleBar as IScaleMarks;
            //分割符号风格
            switch (cboMarksFrequency.Text)
            {
                case "无":                   //"no marks"
                    pScaleMarks.MarkFrequency = esriScaleBarFrequency.esriScaleBarNone;
                    break;
                case "单一":                //"single"
                    pScaleMarks.MarkFrequency = esriScaleBarFrequency.esriScaleBarOne;
                    break;
                case "零分度":
                    pScaleMarks.MarkFrequency = esriScaleBarFrequency.esriScaleBarMajorDivisions;
                    break;
                case "分割":
                    pScaleMarks.MarkFrequency = esriScaleBarFrequency.esriScaleBarDivisions;
                    break;
                case "首分割":
                    pScaleMarks.MarkFrequency = esriScaleBarFrequency.esriScaleBarDivisionsAndFirstMidpoint;
                    break;
                case "双分割":
                    pScaleMarks.MarkFrequency = esriScaleBarFrequency.esriScaleBarDivisionsAndFirstSubdivisions;
                    break;
                case "全分割":
                    pScaleMarks.MarkFrequency = esriScaleBarFrequency.esriScaleBarDivisionsAndSubdivisions;
                    break;
                default:
                    break;
            }
            //分割符号的位置
            switch (cboMarksPosition.Text)
            {
                case "比例尺上":
                    pScaleMarks.MarkPosition = esriVertPosEnum.esriAbove;
                    break;
                case "比例尺顶":
                    pScaleMarks.MarkPosition = esriVertPosEnum.esriTop;
                    break;
                case "比例尺中":
                    pScaleMarks.MarkPosition = esriVertPosEnum.esriOn;
                    break;
                case "比例尺底":
                    pScaleMarks.MarkPosition = esriVertPosEnum.esriBottom;
                    break;
                case "比例尺下":
                    pScaleMarks.MarkPosition = esriVertPosEnum.esriBelow;
                    break;
                default:
                    break;
            }
            //分割符
            if (sbtMarkDivisionSymbol.Symbol != null)
            {
                pScaleMarks.DivisionMarkSymbol = sbtMarkDivisionSymbol.Symbol as ILineSymbol;
                pScaleMarks.DivisionMarkHeight = System.Convert.ToDouble(nudMarkHeight.Value);
            }
            //子分割符
            if (sbtMarkSubdivisionSymbol.Symbol != null)
            {
                pScaleMarks.SubdivisionMarkSymbol = sbtMarkSubdivisionSymbol.Symbol as ILineSymbol;
                pScaleMarks.SubdivisionMarkHeight = System.Convert.ToDouble(nudSubMarkheight.Value);
            }

            //刻度
            pScaleBar.Division = System.Convert.ToDouble(tbxDivisionValue.Text);
            pScaleBar.Divisions = (short)nudDivisions.Value;
            pScaleBar.Subdivisions = (short)nudSubdivisions.Value;
            if (chkZeroDivision.Checked == true)
            { pScaleBar.DivisionsBeforeZero = 1; }
            else
            { pScaleBar.DivisionsBeforeZero = 0; }

            //其它
            //标注文字的颜色
            pRgbColor = new RgbColorClass();
            pRgbColor.Red = cpkTextColor.Value.R;
            pRgbColor.Green = cpkTextColor.Value.G;
            pRgbColor.Blue = cpkTextColor.Value.B;
            pTextSymbol = pScaleBar.LabelSymbol;
            pTextSymbol.Color = pRgbColor as IColor;
            pScaleBar.LabelSymbol = pTextSymbol;
            //比例尺符号样式（线符号或面符号）
            if (pScaleBar is IScaleLine)                //线
            {
                IScaleLine pScaleLine = pScaleBar as IScaleLine;
                if (sbtScaleBar1.Symbol != null)
                { pScaleLine.LineSymbol = sbtScaleBar1.Symbol as ILineSymbol; }
            }
            else if (pScaleBar is ISingleFillScaleBar)  //面
            {
                ISingleFillScaleBar pSingleFillScaleBar = pScaleBar as ISingleFillScaleBar;
                if (sbtScaleBar1.Symbol != null)
                { pSingleFillScaleBar.FillSymbol = sbtScaleBar1.Symbol as IFillSymbol; }
            }
            else if (pScaleBar is IDoubleFillScaleBar)  //面
            {
                IDoubleFillScaleBar pDoubleFillScaleBar = pScaleBar as IDoubleFillScaleBar;
                if (sbtScaleBar1.Symbol != null)
                { pDoubleFillScaleBar.FillSymbol1 = sbtScaleBar1.Symbol as IFillSymbol; }
                if (sbtScaleBar2.Symbol != null)
                { pDoubleFillScaleBar.FillSymbol2 = sbtScaleBar2.Symbol as IFillSymbol; }
            }
        }

        //根据比例尺样式设置控件显示值
        private void InitialScaleBarProperties()
        {
            IScaleBar pScaleBar = m_ScaleBar as IScaleBar;

            //单位
            //分割单位设置
            tbxDivisionUnit.Text = CommonFunction.MapUnitsName(pScaleBar.Units);
            //标签位置设置
            switch (pScaleBar.UnitLabelPosition)
            {
                case esriScaleBarPos.esriScaleBarAbove:
                    cboLabelPosition.Text = "比例尺上";
                    break;
                case esriScaleBarPos.esriScaleBarBeforeLabels:
                    cboLabelPosition.Text = "标签前";
                    break;
                case esriScaleBarPos.esriScaleBarAfterLabels:
                    cboLabelPosition.Text = "标签后";
                    break;
                case esriScaleBarPos.esriScaleBarBeforeBar:
                    cboLabelPosition.Text = "比例尺前";
                    break;
                case esriScaleBarPos.esriScaleBarAfterBar:
                    cboLabelPosition.Text = "比例尺后";
                    break;
                case esriScaleBarPos.esriScaleBarBelow:
                    cboLabelPosition.Text = "比例尺下";
                    break;
                default:
                    break;
            }
            //标注内容与标注与比例尺间距的设置
            tbxLabel.Text = tbxDivisionUnit.Text;
            nudLabelGap.Value = System.Convert.ToDecimal(pScaleBar.UnitLabelGap);
            //单位标注颜色的设置
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor = pScaleBar.UnitLabelSymbol.Color as IRgbColor;
            cpkUnitLabelColor.Value = CommonFunction.TransvertColor(pRgbColor as IColor);

            //数字
            //数字标签的风格设置
            switch (pScaleBar.LabelFrequency)
            {
                case esriScaleBarFrequency.esriScaleBarNone:
                    cboNumberFrequency.Text = "无";
                    break;
                case esriScaleBarFrequency.esriScaleBarOne:
                    cboNumberFrequency.Text = "单一";
                    break;
                case esriScaleBarFrequency.esriScaleBarMajorDivisions:
                    cboNumberFrequency.Text = "零分度";
                    break;
                case esriScaleBarFrequency.esriScaleBarDivisions:
                    cboNumberFrequency.Text = "分割";
                    break;
                case esriScaleBarFrequency.esriScaleBarDivisionsAndFirstMidpoint:
                    cboNumberFrequency.Text = "首分割";
                    break;
                case esriScaleBarFrequency.esriScaleBarDivisionsAndFirstSubdivisions:
                    cboNumberFrequency.Text = "双分割";
                    break;
                case esriScaleBarFrequency.esriScaleBarDivisionsAndSubdivisions:
                    cboNumberFrequency.Text = "全分割";
                    break;
                default:
                    break;
            }
            //数字标签的位置设置
            switch (pScaleBar.LabelPosition)
            {
                case esriVertPosEnum.esriAbove:
                    cboNumberPosition.Text = "比例尺上";
                    break;
                case esriVertPosEnum.esriTop:
                    cboNumberPosition.Text = "比例尺顶";
                    break;
                case esriVertPosEnum.esriOn:
                    cboNumberPosition.Text = "比例尺中";
                    break;
                case esriVertPosEnum.esriBottom:
                    cboNumberPosition.Text = "比例尺底";
                    break;
                case esriVertPosEnum.esriBelow:
                    cboNumberPosition.Text = "比例尺下";
                    break;
                default:
                    break;
            }
            //数字标签与图形间的间距
            nudNumberGap.Value = System.Convert.ToDecimal(pScaleBar.LabelGap);

            //分割
            IScaleMarks pScaleMarks = pScaleBar as IScaleMarks;
            //分割符号风格
            switch (pScaleMarks.MarkFrequency)
            {
                case esriScaleBarFrequency.esriScaleBarNone:
                    cboMarksFrequency.Text = "无";
                    break;
                case esriScaleBarFrequency.esriScaleBarOne:
                    cboMarksFrequency.Text = "单一";
                    break;
                case esriScaleBarFrequency.esriScaleBarMajorDivisions:
                    cboMarksFrequency.Text = "零分度";
                    break;
                case esriScaleBarFrequency.esriScaleBarDivisions:
                    cboMarksFrequency.Text = "分割";
                    break;
                case esriScaleBarFrequency.esriScaleBarDivisionsAndFirstMidpoint:
                    cboMarksFrequency.Text = "首分割";
                    break;
                case esriScaleBarFrequency.esriScaleBarDivisionsAndFirstSubdivisions:
                    cboMarksFrequency.Text = "双分割";
                    break;
                case esriScaleBarFrequency.esriScaleBarDivisionsAndSubdivisions:
                    cboMarksFrequency.Text = "全分割";
                    break;
                default:
                    break;
            }
            //分割符号的位置
            switch (pScaleMarks.MarkPosition)
            {
                case esriVertPosEnum.esriAbove:
                    cboMarksPosition.Text = "比例尺上";
                    break;
                case esriVertPosEnum.esriTop:
                    cboMarksPosition.Text = "比例尺顶";
                    break;
                case esriVertPosEnum.esriOn:
                    cboMarksPosition.Text = "比例尺中";
                    break;
                case esriVertPosEnum.esriBottom:
                    cboMarksPosition.Text = "比例尺底";
                    break;
                case esriVertPosEnum.esriBelow:
                    cboMarksPosition.Text = "比例尺下";
                    break;
                default:
                    break;
            }
            //分割符
            if (pScaleMarks.DivisionMarkSymbol != null)
            { sbtMarkDivisionSymbol.Symbol = pScaleMarks.DivisionMarkSymbol as ISymbol; }
            nudMarkHeight.Value = System.Convert.ToDecimal(pScaleMarks.DivisionMarkHeight);
            //子分割符
            if (pScaleMarks.SubdivisionMarkSymbol != null)
            { sbtMarkSubdivisionSymbol.Symbol = pScaleMarks.SubdivisionMarkSymbol as ISymbol; }
            nudSubMarkheight.Value = System.Convert.ToDecimal(pScaleMarks.SubdivisionMarkHeight);

            //刻度
            tbxDivisionValue.Text = pScaleBar.Division.ToString();
            nudDivisions.Value = System.Convert.ToDecimal(pScaleBar.Divisions);
            nudSubdivisions.Value = System.Convert.ToDecimal(pScaleBar.Subdivisions);
            if (pScaleBar.DivisionsBeforeZero == 0)
            { chkZeroDivision.Checked = false; }
            else
            { chkZeroDivision.Checked = true; }

            //其它
            //标注文字的颜色
            pRgbColor = new RgbColorClass();
            pRgbColor = pScaleBar.LabelSymbol.Color as IRgbColor;
            cpkTextColor.Value = CommonFunction.TransvertColor(pRgbColor as IColor);
            //比例尺符号样式（线符号或面符号）
            if (pScaleBar is IDoubleFillScaleBar)
            {
                if ((pScaleBar as IDoubleFillScaleBar).FillSymbol1 != null)
                { sbtScaleBar1.Symbol = (pScaleBar as IDoubleFillScaleBar).FillSymbol1 as ISymbol; }
                if ((pScaleBar as IDoubleFillScaleBar).FillSymbol2 != null)
                { sbtScaleBar2.Symbol = (pScaleBar as IDoubleFillScaleBar).FillSymbol1 as ISymbol; }
            }
            else if (pScaleBar is IScaleLine)
            {
                if ((pScaleBar as IScaleLine).LineSymbol != null)
                { sbtScaleBar1.Symbol = (pScaleBar as IScaleLine).LineSymbol as ISymbol; }
            }
            else if (pScaleBar is ISingleFillScaleBar)
            {
                if ((pScaleBar as ISingleFillScaleBar).FillSymbol != null)
                { sbtScaleBar1.Symbol = (pScaleBar as ISingleFillScaleBar).FillSymbol as ISymbol; }
            }
            sbtScaleBar1.Refresh();
            sbtScaleBar2.Refresh();
        }
        #endregion
    }
}