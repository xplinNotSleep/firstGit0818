using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public partial class ClassBreaksRendererControl : UserControl,IRendererControl
    {
        public ClassBreaksRendererControl()
        {
            InitializeComponent();
            InitialControls();
        }

        #region IRendererControl 成员

        private IGeoFeatureLayer m_GeoLayer;
        private IClassBreaksRenderer m_Renderer = null;
        private Boolean m_InitialFlag = false;
        private double m_MinimumBreak;
        public void SetGeoLayer(ESRI.ArcGIS.Carto.IGeoFeatureLayer layer)
        {
            m_InitialFlag = true;
            this.cboValue.Items.Clear();
            this.cboNormalization.Items.Clear();
            this.cboClasses.Items.Clear();
            this.scbColorRamp.Items.Clear();
            this.slvGCRenderer.Items.Clear();
            m_GeoLayer = layer;
            if (layer.Renderer is IClassBreaksRenderer)
                m_Renderer = layer.Renderer as IClassBreaksRenderer;
            else
                m_Renderer = new ClassBreaksRendererClass();

            //初始化字段
            IList<IField> fields = CommonFunction.GetNumbleField(layer.FeatureClass.Fields);
            cboValue.Items.Add("none");
            cboNormalization.Items.Add("none");
            cboNormalization.Items.Add("百分比");
            cboNormalization.Items.Add("求对数");
            StyleHelper.SetComboBoxValue(cboValue, "none");
            StyleHelper.SetComboBoxValue(cboNormalization, "none");
            int i;
            for (i = 0; i <= fields.Count - 1; i++)
            {
                cboValue.Items.Add(new FieldWrapper(fields[i]));
                cboNormalization.Items.Add(new FieldWrapper(fields[i]));
            }

            //获取图层记录数目
            ITable pTable=m_GeoLayer as ITable;
            int pFeatureCount= pTable.RowCount(null); 

            //初始化分级数目控件
            for (i = 1; i <= 32; i++)
            {
                cboClasses.Items.Add(i.ToString());
            }
            cboClasses.SelectedIndex = 0;

            //初始化默认值
            if (m_Renderer.BreakCount != 0)
            {
                //计算Total值，用于处理"百分比"
                IDataStatistics pDataStatistics = new DataStatisticsClass();
                pDataStatistics.Cursor = (m_GeoLayer as ITable).Search(null, true);
                pDataStatistics.Field = m_Renderer.Field;
                m_ValueTotal = pDataStatistics.Statistics.Sum;

                m_MinimumBreak = m_Renderer.MinimumBreak;
                string[] subItems=new string[2]; 
                this.cboValue.Text =CommonFunction.GetFieldAliasName(m_GeoLayer.FeatureClass, m_Renderer.Field);
                this.cboClasses.Text = m_Renderer.BreakCount.ToString();
                for (i = 0; i < m_Renderer.BreakCount; i++)
                {
                    if (m_Renderer.NormField == "百分比")
                    {
                        double rangeB = m_Renderer.get_Break(i) / m_ValueTotal * 100;
                        if (i == 0)
                        {
                            double rangeMini = m_MinimumBreak * 100;
                            subItems[0] = rangeMini.ToString("0.000000") + "-" + rangeB.ToString("0.000000");
                        }
                        else
                        {
                            double rangeA = m_Renderer.get_Break(i - 1) / m_ValueTotal * 100;
                            subItems[0] = rangeA.ToString("0.000000") + "-" + rangeB.ToString("0.000000");
                        }
                        subItems[1] = m_Renderer.get_Label(i);
                        slvGCRenderer.AddSymbolItem(m_Renderer.get_Symbol(i), subItems);
                    }
                    else if (m_Renderer.NormField == "求对数")
                    {
                        double rangeB =Math.Log10(m_Renderer.get_Break(i));
                        if (i == 0)
                        {
                            double rangeMini = m_MinimumBreak;
                            subItems[0] = rangeMini.ToString("0.000000") + "-" + rangeB.ToString("0.000000");
                        }
                        else
                        {
                            double rangeA =Math.Log10( m_Renderer.get_Break(i - 1));
                            subItems[0] = rangeA.ToString("0.000000") + "-" + rangeB.ToString("0.000000");
                        }
                        subItems[1] = m_Renderer.get_Label(i);
                        slvGCRenderer.AddSymbolItem(m_Renderer.get_Symbol(i), subItems);
                    }
                    else
                    {
                        double rangeB = m_Renderer.get_Break(i);
                        if (i == 0)
                        {
                            double rangeMini = m_MinimumBreak;
                            subItems[0] = rangeMini.ToString("0.000000") + "-" + rangeB.ToString("0.000000");
                        }
                        else
                        {
                            double rangeA = m_Renderer.get_Break(i - 1);
                            subItems[0] = rangeA.ToString("0.000000") + "-" + rangeB.ToString("0.000000");
                        }
                        subItems[1] = m_Renderer.get_Label(i);
                        slvGCRenderer.AddSymbolItem(m_Renderer.get_Symbol(i), subItems);
                    }
                }
            }
            else
            {
                this.cboValue.SelectedIndex = 0;
                this.cboClasses.Text =System.Convert.ToString(pFeatureCount/2);
            }

            //初始化ColorRamp
            StyleHelper.InitialColorCombox(scbColorRamp);
            scbColorRamp.SelectedIndex = 0;
            //初始化Normalizationkongjian
            if (m_Renderer.NormField =="")
                cboNormalization.SelectedIndex = 0;
            else if (m_Renderer.NormField == "百分比" || m_Renderer.NormField == "求对数")
                cboNormalization.Text = m_Renderer.NormField;
            else
                cboNormalization.Text =CommonFunction.GetFieldAliasName(m_GeoLayer.FeatureClass, m_Renderer.NormField);
            m_InitialFlag = false;
        }

        private double m_ValueTotal=0;
        public ESRI.ArcGIS.Carto.IFeatureRenderer Renderer
        {
            get
            {
                //返回设置的Renderer
                m_Renderer = new ClassBreaksRendererClass();
                //设置Renderer的值
                m_Renderer.Field =CommonFunction.GetFieldName(m_GeoLayer.FeatureClass, this.cboValue.Text.Trim());
                m_Renderer.BreakCount =System.Convert.ToInt32(this.cboClasses.Text);
                m_Renderer.SortClassesAscending = true;
                int i = 0;
                foreach (ListViewItem Item in slvGCRenderer.Items)
                {
                    m_Renderer.set_Symbol(i, Item.Tag as ISymbol);
                    if (cboNormalization.Text == "百分比")
                    { m_Renderer.set_Break(i,GetLaterBreakNumble(Item.SubItems[1].Text)* m_ValueTotal/100);}
                    else if (cboNormalization.Text == "求对数")
                    { m_Renderer.set_Break(i,Math.Pow(10,GetLaterBreakNumble(Item.SubItems[1].Text))); }
                    else
                        m_Renderer.set_Break(i, GetLaterBreakNumble(Item.SubItems[1].Text));
                    m_Renderer.set_Label(i, Item.SubItems[2].Text);
                    i++;
                }
                m_Renderer.MinimumBreak = m_MinimumBreak;
                if (cboNormalization.Text!="none")
                {
                    if (cboNormalization.Text == "百分比" || cboNormalization.Text == "求对数")
                        m_Renderer.NormField = cboNormalization.Text;
                    else
                        m_Renderer.NormField =CommonFunction.GetFieldName(m_GeoLayer.FeatureClass, cboNormalization.Text);
                }
                ILegendInfo pLegendInfo = m_Renderer as ILegendInfo;
                pLegendInfo.get_LegendGroup(0).Heading = this.cboValue.Text.Trim();
                return m_Renderer as IFeatureRenderer;
            }
        }

        public string RendererName
        {
            get { return "分级图"; }
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
            get 
            {
                return pictureBox1.Image;
            }
        }

        public bool CheckRenderer(IFeatureRenderer renderer)
        {
            if (renderer is IClassBreaksRenderer)
                return true;
            else
                return false;
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            slvGCRendererRefresh();
        }

        private void cboValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_InitialFlag == true)
                return;
            if (cboValue.Text == "none")
                return;
            slvGCRendererRefresh();
        }

        private void cboClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_InitialFlag == true)
                return;
            slvGCRendererRefresh();
        }

        private void scbColorRamp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_InitialFlag == true)
                return;
            slvGCRendererRefresh();
        }

        private void cboClasses_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (m_InitialFlag == true)
                return;
            slvGCRendererRefresh();
        }

        private void slvGCRenderer_OnCellDblClick(ListViewItem item, ListViewItem.ListViewSubItem subItem)
        {
            if (item == null)
                return;
            if (subItem == item.SubItems[0])
            {
                //鼠标等待
                this.Cursor = Cursors.WaitCursor;
                frmSymbolSelector frmSymbolSelectorNew = new frmSymbolSelector();
                frmSymbolSelectorNew.InitialSymbol = slvGCRenderer.SelectedItems[0].Tag as ISymbol;
                frmSymbolSelectorNew.SymbolType = StyleHelper.GetSymbolType(m_GeoLayer);
                if (frmSymbolSelectorNew.ShowDialog() == DialogResult.OK)
                {
                    slvGCRenderer.SelectedItems[0].Tag = frmSymbolSelectorNew.SelectedSymbol;
                    this.slvGCRenderer.Refresh();
                }
                this.Cursor = Cursors.Default;
            }
            else if (subItem == item.SubItems[1])
            {
                frmGraduatedBreak frmGraduatedBreakNew = new frmGraduatedBreak();
                frmGraduatedBreakNew.BreakValue = GetLaterBreakNumble(item.SubItems[1].Text);
                frmGraduatedBreakNew.ShowDialog();
                if (frmGraduatedBreakNew.DialogResult == DialogResult.OK)
                {
                    if (cboNormalization.Text == "百分比")
                    {
                        ListViewItem pNextItem = GetNextItem(item);
                        double PreBreakNumble = GetPreBreakNumble(item.SubItems[1].Text) / 100;
                        double BreakValue = frmGraduatedBreakNew.BreakValue / 100;
                        item.SubItems[1].Text = PreBreakNumble.ToString("0.000000%") + '-' + BreakValue.ToString("0.000000%");
                        if (pNextItem != null)
                        {
                            double LaterBreakNumble = GetLaterBreakNumble(pNextItem.SubItems[1].Text) / 100;
                            pNextItem.SubItems[1].Text = BreakValue.ToString("0.000000%") + '-' + LaterBreakNumble.ToString("0.000000%");
                        }
                    }
                    else
                    {
                        ListViewItem pNextItem = GetNextItem(item);
                        item.SubItems[1].Text = GetPreBreakNumble(item.SubItems[1].Text).ToString("0.000000") + '-' + frmGraduatedBreakNew.BreakValue.ToString("0.000000");
                        if (pNextItem != null)
                        {
                            pNextItem.SubItems[1].Text = frmGraduatedBreakNew.BreakValue.ToString("0.000000") + '-' + GetLaterBreakNumble(pNextItem.SubItems[1].Text).ToString("0.000000");
                        }
                    }
                }
            }
            else if (subItem == item.SubItems[2])
            {
                frmGraduatedLabel frmGraduatedLabelNew = new frmGraduatedLabel();
                frmGraduatedLabelNew.RangeLabel = item.SubItems[2].Text;
                frmGraduatedLabelNew.ShowDialog();
                if (frmGraduatedLabelNew.DialogResult == DialogResult.OK)
                    item.SubItems[2].Text = frmGraduatedLabelNew.RangeLabel;
            }
        }

        private void cboNormalization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_InitialFlag == true)
                return;
            slvGCRendererRefresh();
        }

        //得到后一个Item
        private ListViewItem GetNextItem(ListViewItem item)
        {
            ListViewItem pItem=new ListViewItem();
            for (int i = 0; i < slvGCRenderer.Items.Count; i++)
            {
                pItem =slvGCRenderer.Items[i];
                if (pItem == item)
                {
                    if (i != slvGCRenderer.Items.Count - 1)
                        pItem = slvGCRenderer.Items[i + 1];
                    else
                        pItem = null;
                    break;
                }
            }
            return pItem;
        }

        private void InitialControls()
        {
            //初始化UniqueValue的StyleListView
            this.slvGCRenderer.View = View.Details;
            this.slvGCRenderer.Columns.Clear();
            this.slvGCRenderer.Columns.Add("符号", 100, HorizontalAlignment.Left);
            this.slvGCRenderer.Columns.Add("范围", 180, HorizontalAlignment.Left);
            this.slvGCRenderer.Columns.Add("图例标注", 180, HorizontalAlignment.Left);
            //用于设置每列的高度
            ImageList pImageList = new ImageList();
            pImageList.ImageSize = new Size(16, 18);
            this.slvGCRenderer.SmallImageList = pImageList;
        }

        //根据字段选择项刷新slvGCRenderer
        private void slvGCRendererRefresh()
        {
            slvGCRenderer.Items.Clear();
            if (cboValue.Text == "none")
                return;
            string strField;
            strField = CommonFunction.GetFieldName(m_GeoLayer.FeatureClass, cboValue.Text);

            //得到字段所有值和值的频率
            ITable pTable;
            pTable = this.m_GeoLayer as ITable;
            StringCollection fields = new StringCollection();
            fields.Add(strField);
            if (cboNormalization.Text != "none")
            {
                if (cboNormalization.Text == "百分比" || cboNormalization.Text == "求对数")
                { fields.Add(cboNormalization.Text); }
                else
                    fields.Add(CommonFunction.GetFieldName(m_GeoLayer.FeatureClass, cboNormalization.Text));
            }
            Hashtable pHashtable;
            pHashtable = CommonFunction.GetUniqueValues(pTable, fields);
            int UniqueValueCount = pHashtable.Count;
            int[] dataFrequency = new int[UniqueValueCount];
            double[] dataValues = new double[UniqueValueCount];
            //取Hashtable的值到数组中
            pHashtable.Keys.CopyTo(dataValues, 0);
            pHashtable.Values.CopyTo(dataFrequency, 0);
            //排序数组
            Quicksort(dataValues, dataFrequency, 0, UniqueValueCount - 1);

            //计算Total值，用于处理"百分比"
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Cursor = pTable.Search(null, true);
            pDataStatistics.Field = strField;
            m_ValueTotal = pDataStatistics.Statistics.Sum;

            //根据条件计算出Classes和ClassesCount
            IClassifyGEN pClassifyGen;
            pClassifyGen = new EqualIntervalClass();
            double[] Classes;
            long ClassesCount;
            int p = System.Convert.ToInt32(this.cboClasses.Text.Trim());
            pClassifyGen.Classify(dataValues as object, dataFrequency as object, ref p);
            Classes = pClassifyGen.ClassBreaks as double[];
            if (Classes.Length == 0)
                return;
            ClassesCount = Classes.GetUpperBound(0);
            int ValueCount = System.Convert.ToInt32(ClassesCount + 1);

            //读取ComboxColor中的颜色
            IStyleGalleryItem pStyleGalleryItem;
            pStyleGalleryItem = this.scbColorRamp.SelectedItem as IStyleGalleryItem;
            IColorRamp pColorRamp;
            IColor pNextUniqueColor;
            IEnumColors pEnumRampColors;
            pColorRamp = pStyleGalleryItem.Item as IColorRamp;
            pEnumRampColors = pColorRamp.Colors;
            pEnumRampColors.Reset();
            pNextUniqueColor = null;

            long breakIndex;
            ISymbol pSymbol;
            int i = 0, j = 0;
            //历遍所有要素
            for (breakIndex = 0; breakIndex < ClassesCount; breakIndex++)
            {
                //均匀获取颜色值到符号中(如果ValueCount>Size重复取，如果<则间隔赋值)
                if (ValueCount <= pColorRamp.Size)
                {
                    if (i == 0)
                    { pNextUniqueColor = StyleHelper.GetColor(pColorRamp, 0); }
                    else if (i == ValueCount - 1)
                    { pNextUniqueColor = StyleHelper.GetColor(pColorRamp, pColorRamp.Size); }
                    else
                    { pNextUniqueColor = StyleHelper.GetColor(pColorRamp, (pColorRamp.Size / (ValueCount - 2)) * i); }
                }
                else if (ValueCount > pColorRamp.Size)
                {
                    //获取颜色值的重复次数
                    int intRepeat;
                    intRepeat = System.Convert.ToInt32(Math.Ceiling(System.Convert.ToDecimal(ValueCount - 2) / System.Convert.ToDecimal(pColorRamp.Size)));
                    if (i == ValueCount - 1)
                    { pNextUniqueColor = StyleHelper.GetColor(pColorRamp, pColorRamp.Size); }
                    else       //包括i=0
                    {
                        if (i % intRepeat == 0)
                        {
                            j++;
                        }
                        pNextUniqueColor = StyleHelper.GetColor(pColorRamp, j - 1);
                    }
                }

                //创建一个新的符号对象
                pSymbol = null;
                switch (StyleHelper.GetSymbolType(m_GeoLayer))
                {
                    case SymbolType.stMarkerSymbol:
                        pSymbol = new SimpleMarkerSymbolClass();
                        (pSymbol as ISimpleMarkerSymbol).Color = pNextUniqueColor;
                        (pSymbol as ISimpleMarkerSymbol).Size = 4;
                        break;
                    case SymbolType.stLineSymbol:
                        pSymbol = new SimpleLineSymbolClass();
                        (pSymbol as ISimpleLineSymbol).Color = pNextUniqueColor;
                        (pSymbol as ISimpleLineSymbol).Width = 1;
                        break;
                    case SymbolType.stFillSymbol:
                        pSymbol = new SimpleFillSymbolClass();
                        (pSymbol as ISimpleFillSymbol).Color = pNextUniqueColor;
                        (pSymbol as ISimpleFillSymbol).Outline.Width = 0.4;
                        (pSymbol as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSSolid;
                        break;
                    default:
                        break;
                }
                string strLabel = "";
                string strRange = "";
                if (cboNormalization.Text == "百分比")
                {

                    strLabel = Classes[breakIndex].ToString("0.000000%") + "-" + Classes[breakIndex + 1].ToString("0.000000%");
                    double rangeA = Classes[breakIndex] * 100;
                    double rangeB = Classes[breakIndex + 1] * 100;
                    strRange = rangeA.ToString("0.000000") + "-" + rangeB.ToString("0.000000");
                }
                else
                {
                    strLabel = Classes[breakIndex].ToString("0.000000") + "-" + Classes[breakIndex + 1].ToString("0.000000");
                    strRange = strLabel;
                }
                //将符号和字段值显示在ListView中
                string[] strSubItems = new string[2];
                strSubItems[0] = strRange;
                strSubItems[1] = strLabel;
                this.slvGCRenderer.AddSymbolItem(pSymbol, strSubItems);
                //要素计数器，用于均匀分布颜色
                i++;
            }
            this.m_MinimumBreak = Classes[0];
        }

        //从（"0.000000-0.000000"）中取出后一个值
        private double GetLaterBreakNumble(string strRange)
        {
            string[] SP;
            SP = strRange.Split('-');
            if (SP[SP.Length - 1].Substring(SP[SP.Length - 1].Length - 1, 1) == "%")
            { return System.Convert.ToDouble(SP[SP.Length - 1].Substring(0, SP[SP.Length - 1].Length - 1)); }
            else
            { return System.Convert.ToDouble(SP[SP.Length - 1]); }
        }

        //从（"0.000000-0.000000"）中取出前一个值
        private double GetPreBreakNumble(string strRange)
        {
            string[] SP;
            SP = strRange.Split('-');
            if (SP[0].Substring(SP[0].Length - 1, 1) == "%")
            { return System.Convert.ToDouble(SP[0].Substring(0, SP[0].Length - 1)); }
            else
            { return System.Convert.ToDouble(SP[0]); }
        }

        //从小到大排列数组
        private void Quicksort(double[] data, int[] dataFrequency, int low, int high) /*用快速排序方法数组元素data[low..high]作排序*/
        {
            int i, j;
            double pivot;
            if (low < high)/*以数组的第一个元素为基准进行划分*/
            {
                pivot = data[low];
                i = low;
                j = high;

                while (i < j)/*从数组的两端交替地向中间扫描*/
                {
                    while (i < j && data[j] >= pivot)
                        j--;
                    if (i < j)
                    {
                        dataFrequency[i] = dataFrequency[j];
                        data[i++] = data[j];/*比枢轴元素小者移到低下标端*/
                    }
                    while (i < j && data[i] <= pivot)
                        i++;
                    if (i < j)
                    {
                        dataFrequency[j] = dataFrequency[i];
                        data[j--] = data[i];/*比枢轴元素大者移到高下标端*/
                    }
                }

                data[i] = pivot;/*枢轴元素移到正确的位置*/
                Quicksort(data, dataFrequency, low, i - 1);/*对前半个子表递归排序*/
                Quicksort(data, dataFrequency, i + 1, high);/*对后半个子表递归排序*/
            }
        } 

    }
}
