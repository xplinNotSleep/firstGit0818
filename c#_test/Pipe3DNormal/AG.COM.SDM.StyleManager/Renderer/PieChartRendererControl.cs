using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Display;
using AG.COM.SDM.Utility.Wrapers;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.StyleManager.Renderer
{
    public partial class PieChartRendererControl : UserControl,IRendererControl
    {
        public PieChartRendererControl()
        {
            InitializeComponent();
        }

        #region IRendererControl 成员

        private IGeoFeatureLayer m_GeoLayer;
        private string m_strClause;
        private decimal m_FixSize=0;
        private decimal m_SumSize=0;
        private Boolean m_InitialFlag=false;
        public void SetGeoLayer(ESRI.ArcGIS.Carto.IGeoFeatureLayer layer)
        {
            m_InitialFlag = true;
            m_GeoLayer = layer;
            if (m_GeoLayer.Renderer is IPieChartRenderer)
                m_Renderer = m_GeoLayer.Renderer as IPieChartRenderer;
            else
                m_Renderer = new ChartRendererClass();

            slvPieChart.Items.Clear();
            lbxFields.Items.Clear();

            //初始化字段
            IList<IField> fields =CommonFunction.GetNumbleField(layer.FeatureClass.Fields);
            int i;
            for (i = 0; i <= fields.Count - 1; i++)
            {
                lbxFields.Items.Add(new FieldWrapper(fields[i]));
            }
            
            //初始化符号大小
            this.nudSymbolSize.Value = 0;
            this.nudSymbolSize.Minimum = 0;
            this.nudSymbolSize.Maximum = int.MaxValue;
            this.nudSymbolSize.DecimalPlaces = 0;
            this.nudSymbolSize.ThousandsSeparator = false;
            this.nudSymbolSize.Increment = 1;

            //初始化slvPieChart表头
            this.slvPieChart.View = View.Details;
            this.slvPieChart.Columns.Clear();
            this.slvPieChart.Columns.Add("符号", 90, HorizontalAlignment.Left);
            this.slvPieChart.Columns.Add("字段", 145, HorizontalAlignment.Left);
            //用于设置每列的高度
            ImageList pImageList = new ImageList();
            pImageList.ImageSize = new Size(16, 18);
            this.slvPieChart.SmallImageList = pImageList;

            Boolean bIsMakerLayer = false; ;
            //点状图层显示控件处理
            if (StyleHelper.GetSymbolType(m_GeoLayer) == SymbolType.stMarkerSymbol)
            {
                this.sbtBackground.Visible = false;
                this.label2.Visible = false;
                bIsMakerLayer = true;
            }

            //初始化３Ｄ控件参数
            cbo3DThickness.Items.Clear();
            for (i = 0; i <= 15; i++)
            {
                cbo3DThickness.Items.Add(i);
            }
            cbo3DTilt.Items.Clear();
            for (i = 0; i <= 90; i++)
            {
                cbo3DTilt.Items.Add(i);
            }

            //根据默认渲染初始化默认值
            if (((m_Renderer as IChartRenderer).BaseSymbol != null) && ((m_Renderer as IChartRenderer).ChartSymbol is IPieChartSymbol))
            {
                //设置字段
                IRendererFields pRendererFields = (m_Renderer as IRendererFields);
                int fieldCount = pRendererFields.FieldCount;
                IPieChartSymbol pPieChartSymbol = (m_Renderer as IChartRenderer).ChartSymbol as IPieChartSymbol;
                ISymbolArray pSymbolArray = pPieChartSymbol as ISymbolArray;
                string[] subItems;
                for (i = 0; i < fieldCount; i++)
                {
                    //删除lbxFields列表中字段
                    string strRemoveField =CommonFunction.GetFieldAliasName(m_GeoLayer.FeatureClass,pRendererFields.get_Field(i));
                    for (int k = 0; k < lbxFields.Items.Count; k++)
                    {
                        if (lbxFields.Items[k].ToString() == strRemoveField)
                        {
                            lbxFields.Items.Remove(lbxFields.Items[k]);
                            break;
                        }
                    }
                    subItems = new string[1];
                    subItems[0] = strRemoveField;
                    slvPieChart.AddSymbolItem(pSymbolArray.get_Symbol(i), subItems);
                }
                //设置背景
                sbtBackground.Symbol = (m_Renderer as IChartRenderer).BaseSymbol;
                //设置绘图时针方向
                if (pPieChartSymbol.Clockwise == true)
                { rbtGeographic.Checked = true; }
                else
                { rbtArithmetric.Checked = true; }
                //设置绘图渐变方式
                if (m_Renderer.ProportionalBySum == true)
                {
                    rbtFieldSum.Checked = true;
                    nudSymbolSize.Value = System.Convert.ToDecimal(m_Renderer.MinValue);
                    m_FixSize = 32;
                }
                else
                {
                    rbtFixedSize.Checked = true;
                    //设置符号
                    nudSymbolSize.Value = System.Convert.ToDecimal(m_Renderer.MinSize);
                }
                //设置边线
                if (pPieChartSymbol.UseOutline == true)
                {
                    this.cbxShowOutline.Checked = true;
                    this.sbtOutline.Enabled = true;
                    sbtOutline.Symbol = pPieChartSymbol.Outline as ISymbol;
                    sbtOutline.Refresh();
                }
                else
                {
                    ILineSymbol pLineSymbol = new SimpleLineSymbolClass();
                    IRgbColor pRgbColor = new RgbColorClass();
                    pRgbColor.Red = 110;
                    pRgbColor.Green = 110;
                    pRgbColor.Blue = 110;
                    pLineSymbol.Color = pRgbColor as IColor;
                    pLineSymbol.Width = 1;
                    sbtOutline.Symbol = pLineSymbol as ISymbol;
                    sbtOutline.Refresh();
                    this.cbxShowOutline.Checked = false;
                    this.sbtOutline.Enabled = false;
                }
                //设置重叠效果
                if ((m_Renderer as IChartRenderer).UseOverposter == true)
                { cbxNotOverlap.Checked = true; }
                else
                { cbxNotOverlap.Checked = false; }
                //设置３Ｄ参数
                if ((pPieChartSymbol as I3DChartSymbol).Display3D == true)
                {
                    chk3DDisplay.Checked = true;
                    cbo3DThickness.Text = (pPieChartSymbol as I3DChartSymbol).Thickness.ToString();
                    cbo3DTilt.Text = (pPieChartSymbol as I3DChartSymbol).Tilt.ToString();
                    cbo3DThickness.Enabled = true;
                    cbo3DTilt.Enabled = true;
                }
                else
                {
                    chk3DDisplay.Checked = false;
                    cbo3DThickness.Text = System.Convert.ToString(5);
                    cbo3DTilt.Text = System.Convert.ToString(45);
                    cbo3DThickness.Enabled = false;
                    cbo3DTilt.Enabled = false;
                }

                //获取过滤Clause
                m_strClause = (m_Renderer as IDataExclusion).ExclusionClause;
            }
            else
            {
                //初始化默认背景符号
                if (bIsMakerLayer == false)
                {
                    ISymbol pSymbol;
                    pSymbol = StyleHelper.GetInitialRandomSymbol(m_GeoLayer);
                    sbtBackground.Symbol = pSymbol;
                    sbtBackground.Refresh();
                }
                //设置初始重叠处理
                this.cbxNotOverlap.Checked = true;
                this.rbtArithmetric.Checked = true;
                this.nudSymbolSize.Value = 32;
                m_FixSize = 32;
                this.rbtFixedSize.Checked = true;
                //设置背景边线
                ILineSymbol pLineSymbol = new SimpleLineSymbolClass();
                IRgbColor pRgbColor = new RgbColorClass();
                pRgbColor.Red = 110;
                pRgbColor.Green = 110;
                pRgbColor.Blue = 110;
                pLineSymbol.Color = pRgbColor as IColor;
                pLineSymbol.Width = 1;
                sbtOutline.Symbol = pLineSymbol as ISymbol;
                sbtOutline.Refresh();
                this.cbxShowOutline.Checked = false;
                this.sbtOutline.Enabled = false;
                //3D设置
                chk3DDisplay.Checked = true;
                cbo3DTilt.Text = System.Convert.ToString(45);
                cbo3DThickness.SelectedIndex = 10;           //默认厚度为10
            }
            m_InitialFlag = false;
        }

        private IPieChartRenderer m_Renderer = null;
        public ESRI.ArcGIS.Carto.IFeatureRenderer Renderer
        {
            get
            {
                if (m_Renderer == null)
                    m_Renderer = new ChartRendererClass();
                else
                    m_Renderer = new ChartRendererClass();
                //获取字段最大值和最小值
                double[] Value;
                if (slvPieChart.Items.Count != 0)
                { Value = StyleHelper.GetMaxAndMinValue(slvPieChart,m_GeoLayer); }
                else
                { Value=new double[2];}

                //构造一个饼图符号
                IPieChartSymbol pPieChartSymbol = new PieChartSymbolClass();
                //绘制的时针方法
                if (rbtGeographic.Checked == true)
                    pPieChartSymbol.Clockwise = true;
                else if (rbtArithmetric.Checked == true)
                    pPieChartSymbol.Clockwise = false;
                //饼图外轮廓线设置
                if (cbxShowOutline.Checked == true)
                {
                    pPieChartSymbol.UseOutline = true;
                    pPieChartSymbol.Outline = sbtOutline.Symbol as ILineSymbol;
                }
                else
                    pPieChartSymbol.UseOutline = false;
                //设置饼图大小及代表的点值
                IChartSymbol pChartSymbol = pPieChartSymbol as IChartSymbol;
                pChartSymbol.MaxValue = Value[0];
                IMarkerSymbol pMarkerSymbol = pPieChartSymbol as IMarkerSymbol;
                pMarkerSymbol.Size =System.Convert.ToDouble (nudSymbolSize.Value);

                IRendererFields pRendererFields;
                pRendererFields = m_Renderer as IRendererFields;
                ISymbolArray pSymbolArray;
                pSymbolArray = pPieChartSymbol as ISymbolArray;
                string strField;
                if (slvPieChart.Items.Count != 0)
                {
                    for (int i = 0; i < slvPieChart.Items.Count; i++)
                    {
                        strField =CommonFunction.GetFieldName(m_GeoLayer.FeatureClass,slvPieChart.Items[i].SubItems[1].Text);
                        //隔离空值字段（空值字段无法进行饼状图渲染）
                        if (StyleHelper.IsVoidField(m_GeoLayer, strField) != true)
                        {
                            pRendererFields.AddField(strField, strField);
                            pSymbolArray.AddSymbol(slvPieChart.Items[i].Tag as ISymbol);
                        }
                    }
                }

                //设置背景（点状图层除外）
                if (sbtBackground.Symbol != null)
                {
                    (m_Renderer as IChartRenderer).BaseSymbol = sbtBackground.Symbol;
                }
                (m_Renderer as IChartRenderer).ChartSymbol = pPieChartSymbol as IChartSymbol;
                //根据符号渐变方式设置渲染属性
                if (rbtFixedSize.Checked == true)
                {
                    m_Renderer.MinSize =System.Convert.ToDouble(nudSymbolSize.Value);
                    m_Renderer.MinValue = Value[1];
                    m_Renderer.ProportionalBySum = false;
                }
                else if (rbtFieldSum.Checked == true)
                {
                    m_Renderer.MinSize = System.Convert.ToDouble(nudSymbolSize.Value);
                    m_Renderer.MinValue = Value[1];
                    m_Renderer.ProportionalBySum = true;
                }
                //渲染重叠处理设置
                if (cbxNotOverlap.Checked == true)
                { (m_Renderer as IChartRenderer).UseOverposter = true; }
                else
                { (m_Renderer as IChartRenderer).UseOverposter = false; }
                //产生图例对象
                (m_Renderer as IChartRenderer).CreateLegend();

                //设置渲染过滤
                if (m_strClause != "")
                {
                    IDataExclusion pDataExclusion = m_Renderer as IDataExclusion;
                    pDataExclusion.ExclusionClause = m_strClause;
                    pDataExclusion.ShowExclusionClass = false;
                }

                //３Ｄ场景设置
                I3DChartSymbol p3DChartSymbol = pPieChartSymbol as I3DChartSymbol;
                if (chk3DDisplay.Checked == true)
                {
                    p3DChartSymbol.Display3D = true;
                    p3DChartSymbol.Thickness =System.Convert.ToDouble(cbo3DThickness.Text);
                    p3DChartSymbol.Tilt = System.Convert.ToInt32(cbo3DTilt.Text);
                }
                else
                { p3DChartSymbol.Display3D = false; }

                return m_Renderer as IFeatureRenderer;
            }
        }

        public string RendererName
        {
            get { return "饼状图"; }
        }

        public Control DisplayControl
        {
            get { return this; }
        }

        public Image Icon
        {
            get { return pictureBox2.Image; }
        }

        public Image DisplayImage
        {
            get { return pictureBox1.Image; }
        }

        public bool CheckRenderer(ESRI.ArcGIS.Carto.IFeatureRenderer renderer)
        {
            if (renderer is IChartRenderer)
            {
                if ((renderer as IChartRenderer).ChartSymbol is IPieChartSymbol)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        #endregion

        private void butAdd_Click(object sender, EventArgs e)
        {
            if (lbxFields.SelectedItems.Count == 0)
                return;
            string strSelectedItem = lbxFields.SelectedItem.ToString();
            lbxFields.Items.Remove(lbxFields.SelectedItem);

            string[] subItems;
            subItems = new string[1];
            subItems[0] = strSelectedItem;
            ISimpleFillSymbol pSymbol = new SimpleFillSymbolClass();
            pSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
            pSymbol.Color = CommonFunction.GetRandomColor();
            ILineSymbol pLineSymbol;
            pLineSymbol = pSymbol.Outline;
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = 110;
            pRgbColor.Green = 110;
            pRgbColor.Blue = 110;
            pLineSymbol.Color = pRgbColor as IColor;
            pLineSymbol.Width = 0.4;
            pSymbol.Outline = pLineSymbol;
            slvPieChart.AddSymbolItem(pSymbol as ISymbol, subItems);
        }

        private void butRemove_Click(object sender, EventArgs e)
        {
            if (slvPieChart.SelectedItems.Count == 0)
                return;
            string strSelectedItem = slvPieChart.SelectedItems[0].SubItems[1].Text.ToString();
            slvPieChart.Items.Remove(slvPieChart.SelectedItems[0]);

            lbxFields.Items.Add(strSelectedItem);
        }

        private void butRemoveAll_Click(object sender, EventArgs e)
        {
            if (slvPieChart.Items.Count == 0)
                return;
            //初始化字段
            lbxFields.Items.Clear();
            IList<IField> fields = CommonFunction.GetNumbleField(m_GeoLayer.FeatureClass.Fields);
            int i;
            for (i = 0; i <= fields.Count - 1; i++)
            {
                lbxFields.Items.Add(new FieldWrapper(fields[i]));
            }

            slvPieChart.Items.Clear();
        }

        private void butDDUp_Click(object sender, EventArgs e)
        {
            if (slvPieChart.SelectedItems.Count != 1)
                return;
            
            if (slvPieChart.Items[0].Selected)
                return;
            int index = slvPieChart.SelectedIndices[0];
            ListViewItem preItem = slvPieChart.Items[index - 1];
            ListViewItem curItem = slvPieChart.Items[index];
            slvPieChart.Items.Remove(preItem);
            slvPieChart.Items.Insert(index, preItem);
            slvPieChart.Refresh();
        }

        private void butDDDown_Click(object sender, EventArgs e)
        {
            if (slvPieChart.SelectedItems.Count != 1)
                return;

            if (slvPieChart.Items[slvPieChart.Items.Count - 1].Selected)
                return;
            int index = slvPieChart.SelectedIndices[0];
            ListViewItem nextItem = slvPieChart.Items[index + 1];
            ListViewItem curItem = slvPieChart.Items[index];
            slvPieChart.Items.Remove(curItem);
            slvPieChart.Items.Insert(index + 1, curItem);
            slvPieChart.Refresh();
        }

        private void lbxFields_DoubleClick(object sender, EventArgs e)
        {
            butAdd_Click(sender, e);
        }

        private void sbtBackground_Click(object sender, EventArgs e)
        {
            //鼠标等待
            this.Cursor = Cursors.WaitCursor;
            frmSymbolSelector frmSymbolSelectorNew = new frmSymbolSelector();
            frmSymbolSelectorNew.InitialSymbol = sbtBackground.Symbol;
            frmSymbolSelectorNew.SymbolType = SymbolType.stFillSymbol;
            if (frmSymbolSelectorNew.ShowDialog() == DialogResult.OK)
            {
                sbtBackground.Symbol = frmSymbolSelectorNew.SelectedSymbol as ISymbol;
                this.sbtBackground.Refresh();
            }
            this.Cursor = Cursors.Default;
        }

        private void slvPieChart_OnCellDblClick(ListViewItem item, ListViewItem.ListViewSubItem subItem)
        {
            //鼠标等待
            this.Cursor = Cursors.WaitCursor;
            frmSymbolSelector frmSymbolSelectorNew = new frmSymbolSelector();
            frmSymbolSelectorNew.InitialSymbol = slvPieChart.SelectedItems[0].Tag as ISymbol;
            frmSymbolSelectorNew.SymbolType = SymbolType.stFillSymbol;
            if (frmSymbolSelectorNew.ShowDialog() == DialogResult.OK)
            {
                slvPieChart.SelectedItems[0].Tag = frmSymbolSelectorNew.SelectedSymbol;
                this.slvPieChart.Refresh();
            }
            this.Cursor = Cursors.Default;
        }

        private void sbtOutline_Click(object sender, EventArgs e)
        {
            //鼠标等待
            this.Cursor = Cursors.WaitCursor;
            frmSymbolSelector frmSymbolSelectorNew = new frmSymbolSelector();
            frmSymbolSelectorNew.InitialSymbol = sbtOutline.Symbol;
            frmSymbolSelectorNew.SymbolType = SymbolType.stLineSymbol;
            if (frmSymbolSelectorNew.ShowDialog() == DialogResult.OK)
            {
                sbtOutline.Symbol = frmSymbolSelectorNew.SelectedSymbol as ISymbol;
                this.sbtOutline.Refresh();
            }
            this.Cursor = Cursors.Default;
        }

        private void cbxShowOutline_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxShowOutline.Checked == true)
            { sbtOutline.Enabled = true; }
            else
            { sbtOutline.Enabled = false; }
        }

        //处理符号值（算法统ＡｒｃＭａｐ）
        private void rbtFixedSize_CheckedChanged(object sender, EventArgs e)
        {
            if (m_InitialFlag == true)
                return;
            if (slvPieChart.Items.Count == 0)
                return;
            if ((m_Renderer as IChartRenderer).BaseSymbol != null)
            {
                if (rbtFixedSize.Checked == true && m_FixSize != 0)
                { nudSymbolSize.Value = m_FixSize; }
                else if (rbtFixedSize.Checked == true && m_FixSize == 0)
                {
                    nudSymbolSize.Value = System.Convert.ToDecimal(m_Renderer.MinSize);
                }
            }
            else
            {
                if (rbtFixedSize.Checked == true)
                {
                    if (m_FixSize != nudSymbolSize.Value && nudSymbolSize.Value !=m_SumSize)
                    {
                        m_FixSize = nudSymbolSize.Value;
                    }
                }
                if (nudSymbolSize.Value == m_SumSize)
                { nudSymbolSize.Value = m_FixSize; }
            }
        }

        //处理符号值（算法统ＡｒｃＭａｐ）
        private void rbtFieldSum_CheckedChanged(object sender, EventArgs e)
        {
            if (m_InitialFlag == true)
                return;
            if (slvPieChart.Items.Count == 0)
                return;
            if ((m_Renderer as IChartRenderer).BaseSymbol != null)
            {
                if (rbtFieldSum.Checked == true && m_SumSize != 0)
                { nudSymbolSize.Value = m_SumSize; }
                else if (rbtFieldSum.Checked == true && m_SumSize == 0)
                { nudSymbolSize.Value = System.Convert.ToDecimal(m_Renderer.MinValue); }
            }
            else
            {
                if (rbtFieldSum.Checked == true)
                {
                    if ( nudSymbolSize.Value==m_FixSize)
                    {
                        double[] Value;
                        Value =StyleHelper.GetMaxAndMinValue(slvPieChart,m_GeoLayer);
                        if (m_SumSize != System.Convert.ToDecimal(Value[1]) && m_SumSize!=0)
                        {
                            nudSymbolSize.Value = m_SumSize;
                        }
                        if (m_SumSize == 0)
                        {
                            m_SumSize = System.Convert.ToDecimal(Value[1]);
                            nudSymbolSize.Value = m_SumSize;
                        }
                        if (m_SumSize == System.Convert.ToDecimal(Value[1]))
                            nudSymbolSize.Value = m_SumSize;
                    }
                }
            }
        }

        //处理符号值（算法统ＡｒｃＭａｐ）
        private void nudSymbolSize_ValueChanged(object sender, EventArgs e)
        {
            if (m_InitialFlag == true)
                return;
            if (rbtFixedSize.Checked == true)
            { m_FixSize = nudSymbolSize.Value; }
            else if (rbtFieldSum.Checked == true)
            { m_SumSize = nudSymbolSize.Value; }
        }

        private void butExclusionProperty_Click(object sender, EventArgs e)
        {
            frmDotDensityRendererExclusion frmDotDensityRendererExclusionNew = new frmDotDensityRendererExclusion();
            frmDotDensityRendererExclusionNew.Clause = m_strClause;
            if (frmDotDensityRendererExclusionNew.ShowDialog() == DialogResult.OK)
            {
                m_strClause = frmDotDensityRendererExclusionNew.Clause;
            }
        }

        private void chk3DDisplay_CheckedChanged(object sender, EventArgs e)
        {
            if (chk3DDisplay.Checked == true)
            {
                cbo3DThickness.Enabled = true;
                cbo3DTilt.Enabled = true;
            }
            else
            {
                cbo3DThickness.Enabled = false;
                cbo3DTilt.Enabled = false;
            }
        }
    }
}
