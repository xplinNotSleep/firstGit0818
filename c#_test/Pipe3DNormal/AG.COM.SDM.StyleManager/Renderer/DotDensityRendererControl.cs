using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Display;
using AG.COM.SDM.Utility.Wrapers;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.StyleManager.Renderer
{
    public partial class DotDensityRendererControl : UserControl,IRendererControl
    {
        public DotDensityRendererControl()
        {
            InitializeComponent();
        }

        #region IRendererControl 成员

        private IGeoFeatureLayer m_GeoLayer;
        private string m_strClause;
        public void SetGeoLayer(ESRI.ArcGIS.Carto.IGeoFeatureLayer layer)
        {
            m_GeoLayer = layer;
            if (m_GeoLayer.Renderer is IDotDensityRenderer)
                m_Renderer = m_GeoLayer.Renderer as IDotDensityRenderer;
            else
                m_Renderer = new DotDensityRendererClass();

            cboDotSymbolSize.Items.Clear();
            slvDotDensity.Items.Clear();
            lbxFields.Items.Clear();

            //初始化字段
            IList<IField> fields = CommonFunction.GetNumbleField(layer.FeatureClass.Fields);
            int i;
            for (i = 0; i <= fields.Count - 1; i++)
            {
                lbxFields.Items.Add(new FieldWrapper(fields[i]));
            }

            //初始化符号大小框
            double j;
            for (j = 0.5; j <= 4.5; j = j + 0.5)
            {
                cboDotSymbolSize.Items.Add(j);
            }
            for (i = 5; i <= 11; i++)
            {
                cboDotSymbolSize.Items.Add(i);
            }

            //初始化slvDotDensity表头
            this.slvDotDensity.View = View.Details;
            this.slvDotDensity.Columns.Clear();
            this.slvDotDensity.Columns.Add("符号", 60, HorizontalAlignment.Left);
            this.slvDotDensity.Columns.Add("字段", 130, HorizontalAlignment.Left);
            //用于设置每列的高度
            ImageList pImageList = new ImageList();
            pImageList.ImageSize = new Size(16, 18);
            this.slvDotDensity.SmallImageList = pImageList;

            //根据默认渲染初始化默认值
            if (m_Renderer.DotDensitySymbol.BackgroundColor != null)
            {
                //设置字段
                IRendererFields pRendererFields = (m_Renderer as IRendererFields);
                int fieldCount = pRendererFields.FieldCount;
                IDotDensityFillSymbol pDotDensityFillSymbol = m_Renderer.DotDensitySymbol;
                ISymbolArray pSymbolArray = pDotDensityFillSymbol as ISymbolArray;
                string[] subItems;
                for (i = 0; i < fieldCount; i++)
                {
                    //删除lbxFields列表中字段
                    string strRemoveField =CommonFunction.GetFieldAliasName(m_GeoLayer.FeatureClass, pRendererFields.get_Field(i));
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
                    slvDotDensity.AddSymbolItem(pSymbolArray.get_Symbol(i), subItems);
                }
                //点符号大小
                cboDotSymbolSize.Text =System.Convert.ToString(pDotDensityFillSymbol.DotSize);
                //设置点值大小
                tbxDotValue.Text = m_Renderer.DotValue.ToString();
                //设置点位置状况
                if (pDotDensityFillSymbol.FixedPlacement == true)
                { rbtFixPlace.Checked = true; }
                else
                { rbtNotFixPlace.Checked = true; }
                //设置背景
                sbtBorderline.Symbol = pDotDensityFillSymbol.Outline as ISymbol;
                sbtBorderline.Refresh();
                Color pColor;
                pColor = CommonFunction.TransvertColor(pDotDensityFillSymbol.BackgroundColor);
                cpkBackgroundColor.Value = pColor;
                //获取过滤Clause
                m_strClause = (m_Renderer as IDataExclusion).ExclusionClause;
            }
            else
            {
                //设置背景边线
                ILineSymbol pLineSymbol=new SimpleLineSymbolClass();
                IRgbColor pRgbColor=new RgbColorClass();
                pRgbColor.Red=110;
                pRgbColor.Green=110;
                pRgbColor.Blue=110;
                pLineSymbol.Color = pRgbColor as IColor;
                pLineSymbol.Width = 1;
                sbtBorderline.Symbol = pLineSymbol as ISymbol;
                sbtBorderline.Refresh();

                //设置符号大小
                cboDotSymbolSize.SelectedIndex = 3;
                //设置点值大小
                tbxDotValue.Text = "0";
                //设置位置状况
                rbtNotFixPlace.Checked = true;
            }

        }

        private IDotDensityRenderer m_Renderer = null;
        public ESRI.ArcGIS.Carto.IFeatureRenderer Renderer
        {
            get
            {
                if (m_Renderer == null)
                    m_Renderer = new DotDensityRendererClass();
                else
                    m_Renderer = new DotDensityRendererClass();
                //构造一个点密度填充符号
                IDotDensityFillSymbol pDotDensityFillSymbol = new DotDensityFillSymbolClass();
                pDotDensityFillSymbol.DotSize = System.Convert.ToDouble(cboDotSymbolSize.Text);
                IRgbColor pRgbColor = new RgbColorClass();
                pRgbColor.Red = cpkBackgroundColor.Value.R;
                pRgbColor.Green = cpkBackgroundColor.Value.G;
                pRgbColor.Blue = cpkBackgroundColor.Value.B;
                pRgbColor.Transparency = cpkBackgroundColor.Value.A;
                pDotDensityFillSymbol.BackgroundColor = pRgbColor as IColor;
                pDotDensityFillSymbol.Outline = sbtBorderline.Symbol as ILineSymbol;

                IRendererFields pRendererFields;
                pRendererFields = m_Renderer as IRendererFields;
                ISymbolArray pSymbolArray;
                pSymbolArray = pDotDensityFillSymbol as ISymbolArray;
                string strField;
                if (slvDotDensity.Items.Count != 0)
                {
                    for (int i = 0; i < slvDotDensity.Items.Count; i++)
                    {
                        strField =CommonFunction.GetFieldName(m_GeoLayer.FeatureClass,slvDotDensity.Items[i].SubItems[1].Text);
                        //隔离空值字段（空值字段无法进行点密度渲染）
                        if (StyleHelper.IsVoidField(m_GeoLayer,strField)!= true)
                        {
                            pRendererFields.AddField(strField, strField);
                            pSymbolArray.AddSymbol(slvDotDensity.Items[i].Tag as ISymbol);
                        }
                    }
                }

                //设置着色对象的符号
                m_Renderer.DotDensitySymbol = pDotDensityFillSymbol;
                //设置DotValue的值
                m_Renderer.DotValue = System.Convert.ToDouble(tbxDotValue.Text.Trim());
                //设置点位置状况
                if (rbtFixPlace.Checked==true)
                { pDotDensityFillSymbol.FixedPlacement = true; }
                else
                { pDotDensityFillSymbol.FixedPlacement =false; }
                //设置渲染过滤
                if (m_strClause != "")
                {
                    IDataExclusion pDataExclusion = m_Renderer as IDataExclusion;
                    pDataExclusion.ExclusionClause = m_strClause;
                    pDataExclusion.ShowExclusionClass = false;
                }
                //为图例产生符号
                m_Renderer.CreateLegend();

                return m_Renderer as IFeatureRenderer;
            }
        }

        public string RendererName
        {
            get { return "点密度图"; }
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
            if (renderer is IDotDensityRenderer)
                return true;
            else
                return false;
        }

        #endregion

        private void sbtBorderline_Click(object sender, EventArgs e)
        {
            //鼠标等待
            this.Cursor = Cursors.WaitCursor;
            frmSymbolSelector frmSymbolSelectorNew = new frmSymbolSelector();
            frmSymbolSelectorNew.InitialSymbol = sbtBorderline.Symbol;
            frmSymbolSelectorNew.SymbolType = SymbolType.stLineSymbol;
            if (frmSymbolSelectorNew.ShowDialog() == DialogResult.OK)
            {
                sbtBorderline.Symbol = frmSymbolSelectorNew.SelectedSymbol as ISymbol;
                this.sbtBorderline.Refresh();
            }
            this.Cursor = Cursors.Default;
        }

        private void slvDotDensity_OnCellDblClick(ListViewItem item, ListViewItem.ListViewSubItem subItem)
        {
            //鼠标等待
            this.Cursor = Cursors.WaitCursor;
            frmSymbolSelector frmSymbolSelectorNew = new frmSymbolSelector();
            frmSymbolSelectorNew.InitialSymbol = slvDotDensity.SelectedItems[0].Tag as ISymbol;
            frmSymbolSelectorNew.SymbolType = SymbolType.stMarkerSymbol;
            //与Arcmap处理方法相同，以第一项符号大小为准
            IMarkerSymbol pMarkerSymbol;
            if (frmSymbolSelectorNew.ShowDialog() == DialogResult.OK)
            {
                if (slvDotDensity.SelectedItems[0].Index == 0)
                {
                    slvDotDensity.SelectedItems[0].Tag = frmSymbolSelectorNew.SelectedSymbol;
                    cboDotSymbolSize.Text = (frmSymbolSelectorNew.SelectedSymbol as IMarkerSymbol).Size.ToString();
                    for (int i = 1; i < slvDotDensity.Items.Count; i++)
                    {
                        pMarkerSymbol = slvDotDensity.Items[i].Tag as IMarkerSymbol;
                        pMarkerSymbol.Size = System.Convert.ToDouble(cboDotSymbolSize.Text);
                        slvDotDensity.Items[i].Tag = pMarkerSymbol as ISymbol;
                    }
                }
                else
                {
                    pMarkerSymbol=frmSymbolSelectorNew.SelectedSymbol as IMarkerSymbol;
                    pMarkerSymbol.Size = System.Convert.ToDouble(cboDotSymbolSize.Text);
                    slvDotDensity.SelectedItems[0].Tag = pMarkerSymbol as ISymbol;
                }
                this.slvDotDensity.Refresh();
            }
            this.Cursor = Cursors.Default;
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            if (lbxFields.SelectedItems.Count == 0)
                return;
            string strSelectedItem = lbxFields.SelectedItem.ToString();
            lbxFields.Items.Remove(lbxFields.SelectedItem);

            string[] subItems;
            subItems = new string[1];
            subItems[0] = strSelectedItem;
            IMarkerSymbol pMarkerSymbol = new SimpleMarkerSymbolClass();
            pMarkerSymbol.Size = 2;
            pMarkerSymbol.Color = CommonFunction.GetRandomColor();
            slvDotDensity.AddSymbolItem(pMarkerSymbol as ISymbol, subItems);

            //设置Dotvalue的值（Textbox）
            tbxDotValue.Text = GetDotVauleByFields().ToString();
        }

        private void butRemove_Click(object sender, EventArgs e)
        {
            if (slvDotDensity.SelectedItems.Count == 0)
                return;
            string strSelectedItem = slvDotDensity.SelectedItems[0].SubItems[1].Text.ToString();
            slvDotDensity.Items.Remove(slvDotDensity.SelectedItems[0]);

            lbxFields.Items.Add(strSelectedItem);
            //设置Dotvalue的值（Textbox）
            if (slvDotDensity.SelectedItems.Count == 0)
                tbxDotValue.Text = "0";
            else
                tbxDotValue.Text = GetDotVauleByFields().ToString();
        }

        private void butRemoveAll_Click(object sender, EventArgs e)
        {
            if (slvDotDensity.Items.Count == 0)
                return;
            //初始化字段
            lbxFields.Items.Clear();
            IList<IField> fields = CommonFunction.GetNumbleField(m_GeoLayer.FeatureClass.Fields);
            int i;
            for (i = 0; i <= fields.Count - 1; i++)
            {
                lbxFields.Items.Add(new FieldWrapper(fields[i]));
            }

            slvDotDensity.Items.Clear();
            //设置Dotvalue的值（Textbox）
            tbxDotValue.Text = "0";
        }

        private void cboDotSymbolSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //改变符号大小
            if (slvDotDensity.Items.Count == 0)
                return;
            IMarkerSymbol pMarkerSymbol;
            for (int i = 0; i < slvDotDensity.Items.Count; i++)
            {
                pMarkerSymbol = slvDotDensity.Items[i].Tag as IMarkerSymbol;
                pMarkerSymbol.Size = System.Convert.ToDouble(cboDotSymbolSize.Text);
                slvDotDensity.Items[i].Tag = pMarkerSymbol as ISymbol;
            }
            slvDotDensity.Refresh();
        }

        private void butDDUp_Click(object sender, EventArgs e)
        {
            if (slvDotDensity.SelectedItems.Count != 1)
                return;

            if (slvDotDensity.Items[0].Selected)
                return;
            int index = slvDotDensity.SelectedIndices[0];
            ListViewItem preItem = slvDotDensity.Items[index - 1];
            ListViewItem curItem = slvDotDensity.Items[index];
            slvDotDensity.Items.Remove(preItem);
            slvDotDensity.Items.Insert(index, preItem);
            slvDotDensity.Refresh();
        }

        private void butDDDown_Click(object sender, EventArgs e)
        {
            if (slvDotDensity.SelectedItems.Count != 1)
                return;

            if (slvDotDensity.Items[slvDotDensity.Items.Count - 1].Selected)
                return;
            int index = slvDotDensity.SelectedIndices[0];
            ListViewItem nextItem = slvDotDensity.Items[index + 1];
            ListViewItem curItem = slvDotDensity.Items[index];
            slvDotDensity.Items.Remove(curItem);
            slvDotDensity.Items.Insert(index + 1, curItem);
            slvDotDensity.Refresh();
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

        private void lbxFields_DoubleClick(object sender, EventArgs e)
        {
            butAdd_Click(sender, e);
        }

        //获取图层字段的平均值
        private int GetFieldAverage(string FieldName)
        {
            ITable pTable = m_GeoLayer as ITable;
            ICursor pCursor = pTable.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Cursor = pCursor;
            pDataStatistics.Field = FieldName;
            IStatisticsResults pStatisticsResults = pDataStatistics.Statistics;
            double dAverage;
            //所有字段值为空(设为1便于处理)
            if (pStatisticsResults.Sum == 0 && pStatisticsResults.StandardDeviation == 0)
            { dAverage = 0; }
            else
            { dAverage = (pStatisticsResults.Maximum - pStatisticsResults.StandardDeviation) / 2; }
            return System.Convert.ToInt32(Math.Ceiling(System.Convert.ToDecimal(dAverage)));
        }

        //根据添加字段计算默认的Dotvalue值（Textbox）
        private int GetDotVauleByFields()
        {
            int intSum = 0;
            for (int i = 0; i < slvDotDensity.Items.Count; i++)
            {
                intSum = intSum + GetFieldAverage(CommonFunction.GetFieldName(m_GeoLayer.FeatureClass, slvDotDensity.Items[i].SubItems[1].Text));
            }
            return intSum / (slvDotDensity.Items.Count * 10);
        }   

    }
}
