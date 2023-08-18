using AG.COM.SDM.Catalog;
using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.Catalog.Filters;
using AG.COM.SDM.Utility;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AG.Pipe.Analyst3DModel
{
    /// <summary>
    /// 生成三维数据设置的二维数据源
    /// </summary>
    public partial class FormSetDataSource : SkinForm
    {
        private Dictionary<string, IFeatureClass> m_DictFeatClass = new Dictionary<string, IFeatureClass>();//要素数据集名称和要素图层映射
        private Dictionary<string, ITable> m_DictTable = new Dictionary<string, ITable>();//数据表名称和映射的数据表
        private Dictionary<string, string> m_DictDataName = new Dictionary<string, string>();//核查图层名称和数据源名称映射
        public static string MDBpath = "";


        /// <summary>
        /// 储存的矢量要素类集合
        /// </summary>
        public Dictionary<string, IFeatureClass> DictFeatClass
        {
            get { return m_DictFeatClass; }
            set { m_DictFeatClass = value; }
        }

        /// <summary>
        /// 储存的数据表集合
        /// </summary>
        public Dictionary<string, ITable> DicTable
        {
            get { return m_DictTable; }
            set { m_DictTable = value; }
        }

        /// <summary>
        /// 储存的规则图层映射的数据源名称集合
        /// </summary>
        public Dictionary<string, string> DicDataName
        {
            get { return m_DictDataName; }
            set { m_DictDataName = value; }
        }

        private List<string> m_DictDataCheckLayer;
        /// <summary>
        /// 获取或设置要核查规划图层集
        /// </summary>
        public List<string> DicDataCheckLayer
        {
            get
            {
                return this.m_DictDataCheckLayer;
            }
            set
            {
                this.m_DictDataCheckLayer = value;
            }

        }

        /// <summary>
        /// 数据集和对应数据源路径
        /// </summary>
        private Dictionary<IDataset, string> m_dicDatasetPath = new Dictionary<IDataset, string>();

        public FormSetDataSource()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选择数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            AG.COM.SDM.Catalog.IDataBrowser tDataBrowser = new FormDataBrowser();
            tDataBrowser.CategoriesType = EnumCategoriesType.DiskAndDatabase;//数据源浏览展示方式
            tDataBrowser.AddFilter(new FeatureClassFilter());
            tDataBrowser.AddFilter(new TableFilter());
            tDataBrowser.AddFilter(new WorkspaceFilter());
            IDictionary<string, IFeatureClass> m_DictTempFeatClass = new Dictionary<string, IFeatureClass>();
            IDictionary<string, ITable> m_DictTempTable = new Dictionary<string, ITable>();

            if (tDataBrowser.ShowDialog() == DialogResult.OK)
            {
                #region 之前的代码,根据选中项设置数据源
                //txtDataSource.Text = tDataBrowser.SelectedItems[0].Parent.FullPath;//数据源路径
                //IList<DataItem> tListDataItem = tDataBrowser.SelectedItems;

                //IDataset tDataset = tListDataItem[0].GetGeoObject() as IDataset;//获取设置的数据源的数据集
                //string dsPath = txtDataSource.Text;
                //if (!m_dicDatasetPath.ContainsKey(tDataset))
                //{
                //    m_dicDatasetPath.Add(tDataset, dsPath);
                //}
                //if (tDataset != null)
                //{
                //    //获取数据源名称(之前去掉前缀，后面保留完整名称)
                //    string layername = tDataset.BrowseName.ToUpper();

                //    //如果数据集为矢量要素类
                //    if (tDataset is IFeatureClass)
                //    {
                //        if (m_DictTempFeatClass.Keys.Contains(layername) == false)
                //            m_DictTempFeatClass.Add(layername, tDataset as IFeatureClass);
                //    }

                //    //如果数据集为数据表类
                //    else if (tDataset is ITable)
                //    {
                //        if (m_DictTempTable.Keys.Contains(layername) == false)
                //        {
                //            m_DictTempTable.Add(layername, tDataset as ITable);
                //        }
                //    }

                //}


                //string[] strKeys = new string[m_DictTempFeatClass.Count];//矢量要素类的键值集
                //string[] strTableKeys = new string[m_DictTempTable.Count];//数据表类的键值集
                //m_DictTempFeatClass.Keys.CopyTo(strKeys, 0);
                //m_DictTempTable.Keys.CopyTo(strTableKeys, 0);

                //if (strKeys.Length > 0)
                //{
                //    string feaLayerName = strKeys[0].ToUpper();//选择的数据源文件名
                //    listDataSource.SelectedItems[0].SubItems[1].Text = feaLayerName;
                //    if (!m_DictFeatClass.ContainsKey(feaLayerName))
                //    {
                //        m_DictFeatClass.Add(feaLayerName, m_DictTempFeatClass[feaLayerName]);
                //    }
                //    string ruleName = listDataSource.SelectedItems[0].Text;
                //    if (!m_DictDataName.ContainsKey(ruleName))
                //    {
                //        m_DictDataName.Add(ruleName, feaLayerName);
                //    }
                //    else
                //    {
                //        m_DictDataName[ruleName] = feaLayerName;
                //    }
                //}

                //if (strTableKeys.Length > 0)
                //{
                //    string tableName = strTableKeys[0].ToUpper();
                //    listDataSource.SelectedItems[0].SubItems[1].Text = tableName;
                //    if (!m_DictTable.ContainsKey(tableName))
                //    {
                //        m_DictTable.Add(tableName, m_DictTempTable[tableName]);
                //    }
                //    string ruleName = listDataSource.SelectedItems[0].Text;
                //    if (!m_DictDataName.ContainsKey(ruleName))
                //    {
                //        m_DictDataName.Add(ruleName, tableName);
                //    }
                //    else
                //    {
                //        m_DictDataName[ruleName] = tableName;
                //    }
                //}

                //if (tDataset is IFeatureClass)
                //{
                //    listDataSource.SelectedItems[0].SubItems[2].Text = "矢量要素类";
                //    listDataSource.SelectedItems[0].SubItems[1].Tag = tDataset;
                //}
                //else if (tDataset is ITable)
                //{
                //    listDataSource.SelectedItems[0].SubItems[2].Text = "数据表类";
                //    listDataSource.SelectedItems[0].SubItems[1].Tag = tDataset;
                //}
                #endregion

                #region 有四种情况
                IList<DataItem> tListDataItem = tDataBrowser.SelectedItems;

                //如果在数据源浏览界面中只选择一项
                if (tListDataItem.Count == 1)
                {
                    string dsPath = tDataBrowser.SelectedItems[0].Parent.FullPath;//数据源路径
                    IDataset tDataset = tListDataItem[0].GetGeoObject() as IDataset;//获取设置的数据源的数据集

                    if (!m_dicDatasetPath.ContainsKey(tDataset))
                    {
                        m_dicDatasetPath.Add(tDataset, dsPath);
                    }
                    if (tDataset != null)
                    {
                        //获取数据源名称(之前去掉前缀，后面保留完整名称)
                        //string layername = tDataset.BrowseName.ToUpper();
                        string layername = tDataset.BrowseName;

                        //如果数据集为矢量要素类
                        if (tDataset is IFeatureClass)
                        {
                            if (m_DictTempFeatClass.Keys.Contains(layername) == false)
                                m_DictTempFeatClass.Add(layername, tDataset as IFeatureClass);
                        }

                        //如果数据集为数据表类
                        else if (tDataset is ITable)
                        {
                            if (m_DictTempTable.Keys.Contains(layername) == false)
                            {
                                m_DictTempTable.Add(layername, tDataset as ITable);
                            }
                        }

                    }

                    //将临时字典中的键值拷贝到新建的字典中
                    string[] strKeys = new string[m_DictTempFeatClass.Count];//矢量要素类的键值集
                    string[] strTableKeys = new string[m_DictTempTable.Count];//数据表类的键值集
                    m_DictTempFeatClass.Keys.CopyTo(strKeys, 0);
                    m_DictTempTable.Keys.CopyTo(strTableKeys, 0);

                    //若数据源设置界面中没有选中某一项,则需要查找数据源设置界面中是否存在等于引用数据源名称的规则图层名
                    if (listDataSource.SelectedItems.Count == 0)
                    {
                        if (strKeys.Length > 0)
                        {
                            //获取引用的要素类数据源名称
                            //string feaLayerName = strKeys[0].ToUpper();
                            string feaLayerName = strKeys[0];
                            //去掉前缀再进行比对
                            string[] LayerNames = feaLayerName.Split(',');
                            string LayerName = LayerNames[LayerNames.Length - 1];
                            //遍历数据源设置界面中的规则图层名
                            for (int i = 0; i < listDataSource.Items.Count; i++)
                            {
                                string ruleName = listDataSource.Items[i].Text;
                                //若规则图层名与去前缀的数据源名称一致
                                if (ruleName == LayerName)
                                {
                                    listDataSource.Items[i].SubItems[1].Text = feaLayerName;

                                    if (!m_DictFeatClass.ContainsKey(feaLayerName))
                                    {
                                        m_DictFeatClass.Add(feaLayerName, m_DictTempFeatClass[feaLayerName]);
                                    }

                                    if (!m_DictDataName.ContainsKey(ruleName))
                                    {
                                        m_DictDataName.Add(ruleName, feaLayerName);
                                    }
                                    else
                                    {
                                        m_DictDataName[ruleName] = feaLayerName;
                                    }

                                    if (tDataset is IFeatureClass)
                                    {
                                        listDataSource.Items[i].SubItems[2].Text = "矢量要素类";
                                        listDataSource.Items[i].SubItems[1].Tag = tDataset;
                                    }
                                }

                            }

                        }

                        if (strTableKeys.Length > 0)
                        {
                            //string tableName = strTableKeys[0].ToUpper();
                            string tableName = strTableKeys[0].ToUpper();
                            //去掉前缀再进行比对
                            string[] tableNames = tableName.Split(',');
                            string tName = tableNames[tableNames.Length - 1];
                            //遍历数据源设置界面中的规则图层名
                            for (int i = 0; i < listDataSource.Items.Count; i++)
                            {
                                string ruleName = listDataSource.Items[i].Text;
                                //若规则图层名与去前缀的数据源名称一致
                                if (ruleName == tName)
                                {
                                    listDataSource.Items[i].SubItems[1].Text = tableName;

                                    if (!m_DictTable.ContainsKey(tableName))
                                    {
                                        m_DictTable.Add(tableName, m_DictTempTable[tableName]);
                                    }

                                    if (!m_DictDataName.ContainsKey(ruleName))
                                    {
                                        m_DictDataName.Add(ruleName, tableName);
                                    }
                                    else
                                    {
                                        m_DictDataName[ruleName] = tableName;
                                    }

                                    if (tDataset is ITable)
                                    {
                                        listDataSource.Items[i].SubItems[2].Text = "数据表类";
                                        listDataSource.Items[i].SubItems[1].Tag = tDataset;
                                    }
                                }

                            }
                        }

                    }

                    //若数据源设置界面中选中了某一项,则直接引用数据源
                    else
                    {
                        if (strKeys.Length > 0)
                        {
                            //string feaLayerName = strKeys[0].ToUpper();//选择的数据源文件名
                            string feaLayerName = strKeys[0];
                            listDataSource.SelectedItems[0].SubItems[1].Text = feaLayerName;
                            if (!m_DictFeatClass.ContainsKey(feaLayerName))
                            {
                                m_DictFeatClass.Add(feaLayerName, m_DictTempFeatClass[feaLayerName]);
                            }
                            string ruleName = listDataSource.SelectedItems[0].Text;
                            if (!m_DictDataName.ContainsKey(ruleName))
                            {
                                m_DictDataName.Add(ruleName, feaLayerName);
                            }
                            else
                            {
                                m_DictDataName[ruleName] = feaLayerName;
                            }
                        }

                        if (strTableKeys.Length > 0)
                        {
                            //string tableName = strTableKeys[0].ToUpper();
                            string tableName = strTableKeys[0].ToUpper();
                            listDataSource.SelectedItems[0].SubItems[1].Text = tableName;
                            if (!m_DictTable.ContainsKey(tableName))
                            {
                                m_DictTable.Add(tableName, m_DictTempTable[tableName]);
                            }
                            string ruleName = listDataSource.SelectedItems[0].Text;
                            if (!m_DictDataName.ContainsKey(ruleName))
                            {
                                m_DictDataName.Add(ruleName, tableName);
                            }
                            else
                            {
                                m_DictDataName[ruleName] = tableName;
                            }
                        }

                        if (tDataset is IFeatureClass)
                        {
                            listDataSource.SelectedItems[0].SubItems[2].Text = "矢量要素类";
                            listDataSource.SelectedItems[0].SubItems[1].Tag = tDataset;
                        }
                        else if (tDataset is ITable)
                        {
                            listDataSource.SelectedItems[0].SubItems[2].Text = "数据表类";
                            listDataSource.SelectedItems[0].SubItems[1].Tag = tDataset;
                        }
                    }

                }

                //如果在数据源浏览界面中选择多项
                else
                {
                    //先遍历选择的所有项，将选择的数据源名称和对象储存在字典中
                    for (int i = 0; i < tListDataItem.Count; i++)
                    {
                        string dsPath = tDataBrowser.SelectedItems[i].Parent.FullPath;//数据源路径
                        IDataset tDataset = tListDataItem[i].GetGeoObject() as IDataset;//获取设置的数据源的数据集

                        if (!m_dicDatasetPath.ContainsKey(tDataset))
                        {
                            m_dicDatasetPath.Add(tDataset, dsPath);
                        }
                        if (tDataset != null)
                        {
                            //获取数据源名称(之前去掉前缀，后面保留完整名称)
                            //string layername = tDataset.BrowseName.ToUpper();
                            string layername = tDataset.BrowseName;

                            //如果数据集为矢量要素类
                            if (tDataset is IFeatureClass)
                            {
                                if (m_DictTempFeatClass.Keys.Contains(layername) == false)
                                    m_DictTempFeatClass.Add(layername, tDataset as IFeatureClass);
                            }

                            //如果数据集为数据表类
                            else if (tDataset is ITable)
                            {
                                if (m_DictTempTable.Keys.Contains(layername) == false)
                                {
                                    m_DictTempTable.Add(layername, tDataset as ITable);
                                }
                            }

                        }
                    }

                    //从字典中获取数据源名称并存放在名称集合中
                    string[] strKeys = new string[m_DictTempFeatClass.Count];//矢量要素类的键值集
                    string[] strTableKeys = new string[m_DictTempTable.Count];//数据表类的键值集
                    m_DictTempFeatClass.Keys.CopyTo(strKeys, 0);
                    m_DictTempTable.Keys.CopyTo(strTableKeys, 0);

                    //如果数据源设置界面中没有选中任一项
                    if (listDataSource.SelectedItems.Count == 0)
                    {
                        #region 判定规则核查图层名与名称集合中的数据源名称是否一致
                        for (int i = 0; i < this.listDataSource.Items.Count; i++)
                        {
                            //遍历矢量要素类的键值
                            for (int j = 0; j < strKeys.Length; j++)
                            {
                                string standardName = listDataSource.Items[i].Text;//标准图层名称
                                string destLayerName = strKeys[j];//获取数据源名称

                                string[] LayerNames = destLayerName.Split(',');
                                string LayerName = LayerNames[LayerNames.Length - 1];

                                if (standardName.ToUpper() == LayerName.ToUpper())
                                {
                                    if (listDataSource.Items[i].SubItems[1].Text != destLayerName)
                                    {
                                        listDataSource.Items[i].SubItems[1].Text = destLayerName;
                                    }
                                    if (!m_DictFeatClass.ContainsKey(destLayerName))
                                    {
                                        m_DictFeatClass.Add(destLayerName, m_DictTempFeatClass[destLayerName]);
                                    }
                                    if (!m_DictDataName.ContainsKey(standardName))
                                    {
                                        m_DictDataName.Add(standardName, destLayerName);
                                    }
                                    else
                                    {
                                        m_DictDataName[standardName] = destLayerName;
                                    }


                                    listDataSource.Items[i].SubItems[2].Text = "矢量要素类";
                                    listDataSource.Items[i].SubItems[1].Tag = m_DictFeatClass[destLayerName];

                                }
                            }
                            //遍历数据表类的键值
                            for (int k = 0; k < strTableKeys.Length; k++)
                            {
                                string standardName = listDataSource.Items[i].Text;
                                string destTableName = strTableKeys[k];
                                string[] tableNames = destTableName.Split(',');
                                string tName = tableNames[tableNames.Length - 1];

                                if (standardName.ToUpper() == tName.ToUpper())
                                {
                                    if (listDataSource.Items[i].SubItems[1].Text != destTableName)
                                    {
                                        listDataSource.Items[i].SubItems[1].Text = destTableName;
                                    }
                                    if (!m_DictTable.ContainsKey(destTableName))
                                    {
                                        m_DictTable.Add(destTableName, m_DictTempTable[standardName]);
                                    }
                                    if (!m_DictDataName.ContainsKey(standardName))
                                    {
                                        m_DictDataName.Add(standardName, destTableName);
                                    }
                                    else
                                    {
                                        m_DictDataName[standardName] = destTableName;
                                    }


                                    listDataSource.Items[i].SubItems[2].Text = "数据表类";
                                    listDataSource.Items[i].SubItems[1].Tag = m_DictTable[destTableName];

                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        if (strKeys.Length > 0)
                        {
                            string ruleName = listDataSource.SelectedItems[0].Text;
                            bool IsSame = false;
                            for (int i = 0; i < strKeys.Length; i++)
                            {
                                string feaLayerName = strKeys[i];
                                string[] LayerNames = feaLayerName.Split(',');
                                string LayerName = LayerNames[LayerNames.Length - 1];
                                if (ruleName.ToUpper() == LayerName.ToUpper())
                                {
                                    listDataSource.SelectedItems[0].SubItems[1].Text = feaLayerName;
                                    if (!m_DictFeatClass.ContainsKey(feaLayerName))
                                    {
                                        m_DictFeatClass.Add(feaLayerName, m_DictTempFeatClass[feaLayerName]);
                                    }

                                    if (!m_DictDataName.ContainsKey(ruleName))
                                    {
                                        m_DictDataName.Add(ruleName, feaLayerName);
                                    }
                                    else
                                    {
                                        m_DictDataName[ruleName] = feaLayerName;
                                    }

                                    listDataSource.SelectedItems[0].SubItems[2].Text = "矢量要素类";
                                    listDataSource.SelectedItems[0].SubItems[1].Tag = m_DictFeatClass[feaLayerName];

                                    IsSame = true;
                                }
                            }
                            if (!IsSame)
                            {
                                string feaLayerName = strKeys[0];//选择的数据源文件名
                                listDataSource.SelectedItems[0].SubItems[1].Text = feaLayerName;
                                if (!m_DictFeatClass.ContainsKey(feaLayerName))
                                {
                                    m_DictFeatClass.Add(feaLayerName, m_DictTempFeatClass[feaLayerName]);
                                }

                                if (!m_DictDataName.ContainsKey(ruleName))
                                {
                                    m_DictDataName.Add(ruleName, feaLayerName);
                                }
                                else
                                {
                                    m_DictDataName[ruleName] = feaLayerName;
                                }

                                listDataSource.SelectedItems[0].SubItems[2].Text = "矢量要素类";
                                listDataSource.SelectedItems[0].SubItems[1].Tag = m_DictFeatClass[feaLayerName];
                            }

                        }

                        if (strTableKeys.Length > 0)
                        {
                            string tableName = strTableKeys[0];
                            listDataSource.SelectedItems[0].SubItems[1].Text = tableName;
                            if (!m_DictTable.ContainsKey(tableName))
                            {
                                m_DictTable.Add(tableName, m_DictTempTable[tableName]);
                            }
                            string ruleName = listDataSource.SelectedItems[0].Text;
                            if (!m_DictDataName.ContainsKey(ruleName))
                            {
                                m_DictDataName.Add(ruleName, tableName);
                            }
                            else
                            {
                                m_DictDataName[ruleName] = tableName;
                            }

                            listDataSource.SelectedItems[0].SubItems[2].Text = "数据表类";
                            listDataSource.SelectedItems[0].SubItems[1].Tag = m_DictTable[tableName];
                        }

                    }

                }

                #endregion
            }
        }

        private void FormSetDataSource_Load(object sender, EventArgs e)
        {
            this.listDataSource.SetColumnStyle(0, ALAN_ListViewColumnStyle.ReadOnly);
            this.listDataSource.SetColumnStyle(1, ALAN_ListViewColumnStyle.ComboBox);
            foreach (string keyPair in this.m_DictDataCheckLayer)
            {
                ListViewItem tListViewItem = new ListViewItem();
                tListViewItem.Text = keyPair;
                tListViewItem.SubItems.Add("");
                tListViewItem.SubItems.Add("");
                //添加核查图层项
                this.listDataSource.Items.Add(tListViewItem);
            }
        }

        /// <summary>
        /// 选中项更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listDataSource_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listDataSource.SelectedItems != null && listDataSource.SelectedItems.Count > 0)
            {
                if (listDataSource.SelectedItems[0].Text != "")
                {
                    if (listDataSource.SelectedItems[0].SubItems[1].Text != "" &&
                        listDataSource.SelectedItems[0].SubItems[1].Tag != null)
                    {
                        IDataset tDataset = listDataSource.SelectedItems[0].SubItems[1].Tag as IDataset;
                        string dsPath = m_dicDatasetPath[tDataset];
                        txtDataSource.Text = dsPath;
                    }
                }

            }
        }
    }
}
