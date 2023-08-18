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
    public partial class BarChartRendererControl : UserControl,IRendererControl
    {
        public BarChartRendererControl()
        {
            InitializeComponent();
        }

        #region IRendererControl 成员

        private IGeoFeatureLayer m_GeoLayer;
        private string m_strClause;
        private Boolean m_InitialFlag = false;
        public void SetGeoLayer(ESRI.ArcGIS.Carto.IGeoFeatureLayer layer)
        {
            m_InitialFlag = true;
            m_GeoLayer = layer;
            if ((m_GeoLayer.Renderer is IChartRenderer)==true)
                m_Renderer = m_GeoLayer.Renderer as IChartRenderer;
            else
                m_Renderer = new ChartRendererClass();
            
            slvBarChart.Items.Clear();
            lbxFields.Items.Clear();

            //初始化字段及除数
            cboNormalization.Items.Add("无");
            cboNormalization.Items.Add("百分比");
            IList<IField> fields = CommonFunction.GetNumbleField(layer.FeatureClass.Fields);
            int i;
            for (i = 0; i <= fields.Count - 1; i++)
            {
                lbxFields.Items.Add(new FieldWrapper(fields[i]));
                cboNormalization.Items.Add(new FieldWrapper(fields[i]));
            }

            //初始化符号大小
            this.nudSymbolSize.Value = 0;
            this.nudSymbolSize.Minimum = 0;
            this.nudSymbolSize.Maximum = int.MaxValue;
            this.nudSymbolSize.DecimalPlaces = 0;
            this.nudSymbolSize.ThousandsSeparator = false;
            this.nudSymbolSize.Increment = 1;

            //初始化柱状宽度
            this.nudWidth.Value = 0;
            this.nudWidth.Minimum = 0;
            this.nudWidth.Maximum = int.MaxValue;
            this.nudWidth.DecimalPlaces = 0;
            this.nudWidth.ThousandsSeparator = false;
            this.nudWidth.Increment = 1;

            //初始化柱状间隔
            this.nudSpace.Value = 0;
            this.nudSpace.Minimum = 0;
            this.nudSpace.Maximum = int.MaxValue;
            this.nudSpace.DecimalPlaces = 0;
            this.nudSpace.ThousandsSeparator = false;
            this.nudSpace.Increment = 1;

            //初始化slvBarChart表头
            this.slvBarChart.View = View.Details;
            this.slvBarChart.Columns.Clear();
            this.slvBarChart.Columns.Add("符号", 90, HorizontalAlignment.Left);
            this.slvBarChart.Columns.Add("字段", 145, HorizontalAlignment.Left);
            //用于设置每列的高度
            ImageList pImageList = new ImageList();
            pImageList.ImageSize = new Size(16, 18);
            this.slvBarChart.SmallImageList = pImageList;

            Boolean bIsMakerLayer = false; ;
            //点状图层显示控件处理
            if (StyleHelper.GetSymbolType(m_GeoLayer) == SymbolType.stMarkerSymbol)
            {
                this.sbtBackground.Visible = false;
                this.label2.Visible = false;
                bIsMakerLayer = true;
            }
            //初始化３Ｄ厚度
            cbo3DThickness.Items.Clear();
            for (i = 0; i <= 15; i++)
            {
                cbo3DThickness.Items.Add(i);
            }

            //根据默认渲染初始化默认值
            if ((m_Renderer.BaseSymbol != null) && (m_Renderer.ChartSymbol is IBarChartSymbol))
            {
                //设置字段
                IRendererFields pRendererFields = (m_Renderer as IRendererFields);
                int fieldCount = pRendererFields.FieldCount;
                IBarChartSymbol pBarChartSymbol = (m_Renderer as IChartRenderer).ChartSymbol as IBarChartSymbol;
                ISymbolArray pSymbolArray = pBarChartSymbol as ISymbolArray;
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
                    slvBarChart.AddSymbolItem(pSymbolArray.get_Symbol(i), subItems);
                }
                //设置背景
                sbtBackground.Symbol = (m_Renderer as IChartRenderer).BaseSymbol;
                //设置绘图柱状方向
                if (pBarChartSymbol.VerticalBars == true)
                { rbtColumn.Checked = true; }
                else
                { rbtBar.Checked = true; }
                //设置柱状宽度及间隔
                nudWidth.Value = System.Convert.ToDecimal(pBarChartSymbol.Width);
                nudSpace.Value = System.Convert.ToDecimal(pBarChartSymbol.Spacing);
                //设置符号大小
                nudSymbolSize.Value = System.Convert.ToDecimal((pBarChartSymbol as IMarkerSymbol).Size);

                //设置柱状坐标系属性
                if (pBarChartSymbol.ShowAxes == true)
                {
                    this.cbxShowAxes.Checked = true;
                    this.sbtAxes.Enabled = true;
                    sbtAxes.Symbol = pBarChartSymbol.Axes as ISymbol;
                    sbtAxes.Refresh();
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
                    sbtAxes.Symbol = pLineSymbol as ISymbol;
                    sbtAxes.Refresh();
                    this.cbxShowAxes.Checked = false;
                    this.sbtAxes.Enabled = false;
                }
                //设置重叠效果
                if ((m_Renderer as IChartRenderer).UseOverposter == true)
                { cbxNotOverlap.Checked = true; }
                else
                { cbxNotOverlap.Checked = false; }
                //设置３Ｄ参数
                if ((pBarChartSymbol as I3DChartSymbol).Display3D == true)
                { 
                    chk3DDisplay.Checked = true;
                    cbo3DThickness.Enabled = true;
                }
                else
                { 
                    chk3DDisplay.Checked = false;
                    cbo3DThickness.Enabled = false;
                }
                cbo3DThickness.Text = (pBarChartSymbol as I3DChartSymbol).Thickness.ToString();

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
                this.rbtColumn.Checked = true;
                this.nudSymbolSize.Value = 48;
                //设置背景边线
                ILineSymbol pLineSymbol = new SimpleLineSymbolClass();
                IRgbColor pRgbColor = new RgbColorClass();
                pRgbColor.Red = 110;
                pRgbColor.Green = 110;
                pRgbColor.Blue = 110;
                pLineSymbol.Color = pRgbColor as IColor;
                pLineSymbol.Width = 1;
                sbtAxes.Symbol = pLineSymbol as ISymbol;
                sbtAxes.Refresh();
                this.cbxShowAxes.Checked = false;
                this.sbtAxes.Enabled = false;
                //设置柱状宽度和间隔
                nudWidth.Value = 6;
                nudSpace.Value = 0;
                //设置除数
                cboNormalization.SelectedIndex = 0;
                //设置３Ｄ参数
                chk3DDisplay.Checked = true;
                cbo3DThickness.SelectedIndex = 5;       //默认厚度为5
                cbo3DThickness.Enabled = true;
            }
            m_InitialFlag = false;
        }

        private IChartRenderer m_Renderer = null;
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
                if (slvBarChart.Items.Count != 0)
                { Value =StyleHelper.GetMaxAndMinValue(slvBarChart,m_GeoLayer); }
                else
                { Value = new double[2]; }

                //构造一个饼图符号
                IBarChartSymbol pBarChartSymbol = new BarChartSymbolClass();
                //绘制柱状的方向
                if (rbtColumn.Checked == true)
                { pBarChartSymbol.VerticalBars = true; }
                else
                {pBarChartSymbol.VerticalBars=false;}

                //柱状的宽度和间隔
                pBarChartSymbol.Width =System.Convert.ToDouble(nudWidth.Value);
                pBarChartSymbol.Spacing =System.Convert.ToDouble(nudSpace.Value);
                //设置柱状图的坐标系样式
                if (cbxShowAxes.Checked == true)
                { 
                    pBarChartSymbol.ShowAxes = true;
                    pBarChartSymbol.Axes = sbtAxes.Symbol as ILineSymbol;
                }
                else
                { pBarChartSymbol.ShowAxes=false;}

                //设置柱状大小及代表的最大点值
                IChartSymbol pChartSymbol = pBarChartSymbol as IChartSymbol;
                pChartSymbol.MaxValue = Value[0];
                IMarkerSymbol pMarkerSymbol = pBarChartSymbol as IMarkerSymbol;
                pMarkerSymbol.Size = System.Convert.ToDouble(nudSymbolSize.Value);

                IRendererFields pRendererFields;
                pRendererFields = m_Renderer as IRendererFields;
                ISymbolArray pSymbolArray;
                pSymbolArray = pBarChartSymbol as ISymbolArray;
                string strField;
                if (slvBarChart.Items.Count != 0)
                {
                    for (int i = 0; i < slvBarChart.Items.Count; i++)
                    {
                        strField =CommonFunction.GetFieldName(m_GeoLayer.FeatureClass,slvBarChart.Items[i].SubItems[1].Text);
                        pRendererFields.AddField(strField, strField);
                        pSymbolArray.AddSymbol(slvBarChart.Items[i].Tag as ISymbol);
                    }
                }

                //设置背景（点状图层除外）
                if (sbtBackground.Symbol != null)
                {
                    m_Renderer.BaseSymbol = sbtBackground.Symbol;
                }
                m_Renderer.ChartSymbol = pBarChartSymbol as IChartSymbol;

                //渲染重叠处理设置
                if (cbxNotOverlap.Checked == true)
                { m_Renderer.UseOverposter = true; }
                else
                { m_Renderer.UseOverposter = false; }
                //产生图例对象
                m_Renderer.CreateLegend();

                //设置渲染过滤
                if (m_strClause != "")
                {
                    IDataExclusion pDataExclusion = m_Renderer as IDataExclusion;
                    pDataExclusion.ExclusionClause = m_strClause;
                    pDataExclusion.ShowExclusionClass = false;
                }

                //３Ｄ场景设置
                I3DChartSymbol p3DChartSymbol = pBarChartSymbol as I3DChartSymbol;
                if (chk3DDisplay.Checked == true)
                { 
                    p3DChartSymbol.Display3D = true;
                    p3DChartSymbol.Thickness = System.Convert.ToDouble(cbo3DThickness.Text);
                }
                else
                { p3DChartSymbol.Display3D = false; }

                ////设置除数
                //if (cboNormalization.Text != "无")
                //{
                //    IDataNormalization pDataNormalization = m_Renderer as IDataNormalization;
                //    if (cboNormalization.Text == "百分比")
                //    {
                //        pDataNormalization.NormalizationTotal = GetFieldsValueTotal()/100;
                //        pDataNormalization.NormalizationType = esriDataNormalization.esriNormalizeByPercentOfTotal;
                //    }
                //    else
                //    {
                //        pDataNormalization.NormalizationField = cboNormalization.Text;
                //        pDataNormalization.NormalizationType = esriDataNormalization.esriNormalizeByField;
                //    }
                //}

                return m_Renderer as IFeatureRenderer;
            }
        }

        public string RendererName
        {
            get { return "柱状图"; }
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
                if ((renderer as IChartRenderer).ChartSymbol is IBarChartSymbol)
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
            slvBarChart.AddSymbolItem(pSymbol as ISymbol, subItems);
        }

        private void butRemove_Click(object sender, EventArgs e)
        {
            if (slvBarChart.SelectedItems.Count == 0)
                return;
            string strSelectedItem = slvBarChart.SelectedItems[0].SubItems[1].Text.ToString();
            slvBarChart.Items.Remove(slvBarChart.SelectedItems[0]);

            lbxFields.Items.Add(strSelectedItem);
        }

        private void butRemoveAll_Click(object sender, EventArgs e)
        {
            if (slvBarChart.Items.Count == 0)
                return;
            //初始化字段
            lbxFields.Items.Clear();
            IList<IField> fields = CommonFunction.GetNumbleField(m_GeoLayer.FeatureClass.Fields);
            int i;
            for (i = 0; i <= fields.Count - 1; i++)
            {
                lbxFields.Items.Add(new FieldWrapper(fields[i]));
            }

            slvBarChart.Items.Clear();
        }

        private void butDDUp_Click(object sender, EventArgs e)
        {
            if (slvBarChart.SelectedItems.Count != 1)
                return;

            if (slvBarChart.Items[0].Selected)
                return;
            int index = slvBarChart.SelectedIndices[0];
            ListViewItem preItem = slvBarChart.Items[index - 1];
            ListViewItem curItem = slvBarChart.Items[index];
            slvBarChart.Items.Remove(preItem);
            slvBarChart.Items.Insert(index, preItem);
            slvBarChart.Refresh();
        }

        private void butDDDown_Click(object sender, EventArgs e)
        {
            if (slvBarChart.SelectedItems.Count != 1)
                return;

            if (slvBarChart.Items[slvBarChart.Items.Count - 1].Selected)
                return;
            int index = slvBarChart.SelectedIndices[0];
            ListViewItem nextItem = slvBarChart.Items[index + 1];
            ListViewItem curItem = slvBarChart.Items[index];
            slvBarChart.Items.Remove(curItem);
            slvBarChart.Items.Insert(index + 1, curItem);
            slvBarChart.Refresh();
        }

        private void lbxFields_DoubleClick(object sender, EventArgs e)
        {
            butAdd_Click(sender, e);
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

        private void cbxShowAxes_CheckedChanged(object sender, EventArgs e)
        {
            if (m_InitialFlag == true)
                return;
            if (cbxShowAxes.Checked == true)
            { sbtAxes.Enabled = true; }
            else
            { sbtAxes.Enabled = false; }
        }

        private void sbtAxes_Click(object sender, EventArgs e)
        {
            //鼠标等待
            this.Cursor = Cursors.WaitCursor;
            frmSymbolSelector frmSymbolSelectorNew = new frmSymbolSelector();
            frmSymbolSelectorNew.InitialSymbol = sbtAxes.Symbol;
            frmSymbolSelectorNew.SymbolType = SymbolType.stLineSymbol;
            if (frmSymbolSelectorNew.ShowDialog() == DialogResult.OK)
            {
                sbtAxes.Symbol = frmSymbolSelectorNew.SelectedSymbol as ISymbol;
                this.sbtAxes.Refresh();
            }
            this.Cursor = Cursors.Default;
        }

        private void slvBarChart_OnCellDblClick(ListViewItem item, ListViewItem.ListViewSubItem subItem)
        {
            //鼠标等待
            this.Cursor = Cursors.WaitCursor;
            frmSymbolSelector frmSymbolSelectorNew = new frmSymbolSelector();
            frmSymbolSelectorNew.InitialSymbol = slvBarChart.SelectedItems[0].Tag as ISymbol;
            frmSymbolSelectorNew.SymbolType = SymbolType.stFillSymbol;
            if (frmSymbolSelectorNew.ShowDialog() == DialogResult.OK)
            {
                slvBarChart.SelectedItems[0].Tag = frmSymbolSelectorNew.SelectedSymbol;
                this.slvBarChart.Refresh();
            }
            this.Cursor = Cursors.Default;
        }

        private void chk3DDisplay_CheckedChanged(object sender, EventArgs e)
        {
            if (chk3DDisplay.Checked == true)
            { cbo3DThickness.Enabled = true; }
            else
            { cbo3DThickness.Enabled = false; }
        }
    }
}
