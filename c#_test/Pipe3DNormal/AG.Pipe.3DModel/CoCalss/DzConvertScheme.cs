using AG.COM.SDM.Utility.Common;
using AG.Pipe.Analyst3DModel.Editor;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AG.Pipe.Analyst3DModel
{
    /// <summary>
    /// 地质方案接口
    /// </summary>
    public interface IDzzkScheme
    {
        IFeatureClass DzzkFeatureClass { get; set; }
        List<string> DzzkFields { get; set; }
    }
    public interface IZkytScheme
    {
        //IFeatureClass ZkytFeatureClass { get; set; }
        ITable ZkytDataTable { get; set; }

        List<string> ZkytFields { get; set; }
    }

    /// <summary>
    /// 地质模型生成方案管理
    /// </summary>
    [Serializable]
    public class DzConvertSchemeManager : ISchemeValueChanged, ISchemeName
    {
        private const string Category1 = "1、常规";

        /// <summary>
        /// 方案名称
        /// </summary>
        [Category("常规"), DisplayName("方案名称"), DefaultValue("")]
        public string Name { get; set; } = "方案名称";

        [Category(Category1), DisplayName("导出路径"), PropertyOrder(2)]
        public string OutPath { get; set; } = $"D:\\";

        /// <summary>
        /// 地质方案
        /// </summary>
        [Browsable(false)]
        public List<DzConvertScheme> Schemes { get; set; } = new List<DzConvertScheme>();

        #region 事件
        public event SchemeValueChangedEventHandler SchemePropertyValueChanged;
        public void OnSchemePropertyValueChanged(object sender, SchemePropertyEventArgs e)
        {
            if (SchemePropertyValueChanged != null)
            {
                SchemePropertyValueChanged(sender, e);
            }
        }
        #endregion
    }

    /// <summary>
    /// 地质模型生成方案
    /// </summary>
    [Serializable]
    public class DzConvertScheme : ISchemeValueChanged, ISchemeName
    {
        /// <summary>
        /// 方案名称
        /// </summary>
        [Category("常规"), DisplayName("方案名称"), DefaultValue("")]
        public string Name { get; set; } = "方案名称";

        /// <summary>
        /// 地质钻孔
        /// </summary>
        [Browsable(false)]
        public DzzkScheme DzzkScheme { get; set; } = new DzzkScheme();

        /// <summary>
        /// 钻孔岩土
        /// </summary>
        [Browsable(false)]
        public ZkytScheme ZkytScheme { get; set; } = new ZkytScheme();


        #region 事件
        public event SchemeValueChangedEventHandler SchemePropertyValueChanged;
        public void OnSchemePropertyValueChanged(object sender, SchemePropertyEventArgs e)
        {
            if (SchemePropertyValueChanged != null)
            {
                SchemePropertyValueChanged(sender, e);
            }
        }
        #endregion
    }

    /// <summary>
    /// 地质钻孔生成方案
    /// </summary>
    [TypeConverter(typeof(PropertySorter))]
    [Serializable]
    public class DzzkScheme : IDzzkScheme, ISchemeValueChanged, ISchemeName
    {
        private const string DzzkCategory1 = "1、常规";
        private const string DzzkCategory2 = "2、地质钻孔属性配置";
        private Color m_LineColor = Color.Gray;

        public DzzkScheme()
        {
            m_LineColor = Color.Gray;
        }

        #region 设置显示属性
        /// <summary>
        /// 方案名称
        /// </summary>
        [Category(DzzkCategory1), DisplayName("方案名称"), DefaultValue(""), PropertyOrder(0)]
        public string Name { get; set; } = "新建钻孔名称";

        /// <summary>
        /// 方案图层名称
        /// </summary>
        [Category(DzzkCategory1), DisplayName("图层名称"), DefaultValue(""), Editor(typeof(DzzkLayerEditor),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(1)]
        public string LayerName { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public IFeatureClass DzzkFeatureClass { get; set; }//绑定的源数据

        /// <summary>
        /// 地质钻孔数据源
        /// </summary>
        [Category(DzzkCategory1), DisplayName("图层数据源"), DefaultValue(""), PropertyOrder(2)]
        public string DataSource { get; set; }

        /// <summary>
        /// 钻孔直径
        /// </summary>
        [Category(DzzkCategory2), DisplayName("钻孔直径字段"), DefaultValue(""), Editor(typeof(DzzkFieldEditorEx),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(3)]
        public string strDN { get; set; }

        /// <summary>
        /// 钻孔标高
        /// </summary>
        [Category(DzzkCategory2), DisplayName("钻孔标高字段"), DefaultValue(""), Editor(typeof(DzzkFieldEditorEx),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(4)]
        public string strHeight { get; set; }

        /// <summary>
        /// 钻孔深度
        /// </summary>
        [Category(DzzkCategory2), DisplayName("钻孔深度字段"), DefaultValue(""), Editor(typeof(DzzkFieldEditorEx),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(5)]
        public string strH_Deep { get; set; }

        #region 颜色配置
        ///// <summary>
        ///// 颜色
        ///// </summary>
        //[Category(DzzkCategory2), DisplayName("颜色"), TypeConverter(typeof(ColorConverterEx)), PropertyOrder(6)]
        //[DefaultValue(typeof(Color), "Gray")]
        //[XmlIgnore()]
        //public Color LineColor
        //{
        //    get { return m_LineColor; }
        //    set { m_LineColor = value; ColorType = SerializeColor(value); }
        //}

        //private string m_ColorType;
        //[Browsable(false)]
        //[XmlElement("LineColor")]
        //public string ColorType
        //{
        //    get { return m_ColorType; }
        //    set { m_ColorType = value; m_LineColor = DeserializeColor(value); }

        //}
        #endregion

        #endregion

        /// <summary>
        /// 地质钻孔字段列表(在配置信息中下拉可选择)
        /// </summary>
        [Browsable(false)]
        public List<string> DzzkFields { get; set; } = new List<string>();

        public List<string> DzzkLayer3D = new List<string>();

        #region 颜色转换
        /// <summary>
        /// 将RGB字符串形式转为Color形式
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public Color DeserializeColor(string color)
        {
            byte a, r, g, b;
            try
            {
                string[] pieces = color.Split(new char[] { ',' });
                r = byte.Parse(pieces[0]);
                g = byte.Parse(pieces[1]);
                b = byte.Parse(pieces[2]);
                return Color.FromArgb(r, g, b);
            }
            catch (Exception ex)
            {

                return Color.Empty;
            }
        }
        public string SerializeColor(Color color)
        {
            return string.Format("{0},{1},{2}", color.R, color.G, color.B);
        }

        /// <summary>
        /// 获取RGB颜色
        /// </summary>
        /// <param name="clrPicker">控件获取的颜色</param>
        /// <returns>RGB颜色</returns>
        private IColor GetRgbColor(Color clrPicker)
        {
            RgbColor clr = new RgbColorClass();
            clr.Red = clrPicker.R;
            clr.Green = clrPicker.G;
            clr.Blue = clrPicker.B;
            clr.Transparency = clrPicker.A;
            return clr;
        }
        #endregion

        /// <summary>
        /// 运行生成模型
        /// </summary>
        /// <param name="pWorkspace"></param>
        /// <param name="trackProgress"></param>
        public void RunTask(IWorkspace pWorkspace, ITrackProgress trackProgress, Dictionary<string, IMarker3DSymbol> dictMarker3DSymbol)
        {
            #region 检查地质钻孔的配置信息是否为空
            //地质钻孔图层名
            if (LayerName.IsNull())
            {
                return;
            }
            //数据源
            if (DataSource.IsNull())
            {
                return;
            }
            //钻孔直径
            if (strDN.IsNull())
            {
                return;
            }
            //钻孔标高
            if (strHeight.IsNull())
            {
                return;
            }
            //钻孔深度
            if (strH_Deep.IsNull())
            {
                return;
            }
            //绑定的源数据
            if (DzzkFeatureClass == null)
            {
                return;
            }
            #endregion

            trackProgress.SubMessage = $"正在创建{LayerName} 3D模型";
            Application.DoEvents();

            #region 创建3D图层
            string Layer3dName = LayerName + "_3D";
            string LayerAliasName = DzzkFeatureClass.AliasName;

            ITable pTable = DzzkFeatureClass as ITable;
            //原要素字段
            IFields pFields = (DzzkFeatureClass.Fields as IClone).Clone() as IFields;
            //3d属性要素
            IGeoDataset pGeoDataSet = DzzkFeatureClass as IGeoDataset;
            ISpatialReference sr = pGeoDataSet.SpatialReference;
            //创建三维多面体字段
            IFields pNewFields = FeatureModelHelper.CreateFields3D(pFields, sr, esriGeometryType.esriGeometryMultiPatch);

            IFeatureClass pToFeatureClass;
            if (AnalystHelper.IsExistFeatureClass(LayerName, pWorkspace))
            {
                pToFeatureClass = (pWorkspace as IFeatureWorkspace).OpenFeatureClass(LayerName);
            }
            else
            {
                pToFeatureClass = (pWorkspace as IFeatureWorkspace).CreateFeatureClass(Layer3dName, pNewFields, null, null,
                    DzzkFeatureClass.FeatureType, "SHAPE", "");
            }
            #endregion

            if (dictMarker3DSymbol.ContainsKey(LayerName))
            {
                IMarker3DSymbol marker3DSymbol = dictMarker3DSymbol[LayerName];
                DzzkFeatureClass.CopyToDzzk3DByMarker(pToFeatureClass, marker3DSymbol, null, strDN, strHeight, strH_Deep, trackProgress);
            }
            else if (LayerAliasName != " " && dictMarker3DSymbol.ContainsKey(LayerAliasName))
            {
                IMarker3DSymbol marker3DSymbol = dictMarker3DSymbol[LayerAliasName];
                DzzkFeatureClass.CopyToDzzk3DByMarker(pToFeatureClass, marker3DSymbol, null, strDN, strHeight, strH_Deep, trackProgress);
            }
            else
            {
                //IColor pColor = GetRgbColor(LineColor);
                //复制要素 到三维中
                DzzkFeatureClass.CopyToDzzk3D(pToFeatureClass, null, strDN, strHeight, strH_Deep, trackProgress);
            }

            //获取三维管线要素个数
            int count = pToFeatureClass.FeatureCount(null);
            if (count > 0)
            {
                #region 在生成3D要素并初始化地理实体后填充符号
                //简单填充符号
                //ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
                ////可以用符号选择器进行
                //simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
                //simpleFillSymbol.Color = GetRgbColor(LineColor);

                //ISimpleRenderer simpleRender = new SimpleRendererClass();
                //simpleRender.Symbol = simpleFillSymbol as ISymbol;

                //IFeatureLayer featureLayer = new FeatureLayerClass();
                //featureLayer.FeatureClass = pToFeatureClass;

                //IGeoFeatureLayer tGeoLayer = featureLayer as IGeoFeatureLayer;
                //tGeoLayer.Renderer = simpleRender as IFeatureRenderer;

                //ISimpleLine3DSymbol line3DSymbol = new SimpleLine3DSymbolClass();
                //line3DSymbol.Style = esriSimple3DLineStyle.esriS3DLSTube;
                //IMarker3DPlacement pMarker3DPlacement = new Marker3DSymbolClass();
                //pMarker3DPlacement.Color = LineColor;
                //ISimpleRenderer simpleRender = new SimpleRendererClass();
                //simpleRender.Symbol = pFillSymbol as ISymbol;
                //IFeatureLayer featureLayer = new FeatureLayerClass();
                //featureLayer.FeatureClass = pToFeatureClass;

                //IGeoFeatureLayer tGeoLayer = featureLayer as IGeoFeatureLayer;
                //tGeoLayer.Renderer = simpleRender as IFeatureRenderer;
                #endregion

                if (!DzzkLayer3D.Exists(m => m == Layer3dName))
                    DzzkLayer3D.Add(Layer3dName);
            }
        }

        #region 事件
        public event SchemeValueChangedEventHandler SchemePropertyValueChanged;
        public void OnSchemePropertyValueChanged(object sender, SchemePropertyEventArgs e)
        {
            if (SchemePropertyValueChanged != null)
            {
                SchemePropertyValueChanged(sender, e);
            }
        }
        #endregion

    }

    /// <summary>
    /// 钻孔岩土生成方案
    /// </summary>
    [TypeConverter(typeof(PropertySorter))]
    [Serializable]
    public class ZkytScheme : IDzzkScheme, IZkytScheme, ISchemeValueChanged, ISchemeName
    {
        private const string ZkytCategory1 = "1、常规";
        private const string ZkytCategory2 = "2、钻孔岩土属性配置";
        private const string ZkytCategory3 = "3、地质钻孔属性配置";
        private Color m_LineColor = Color.Gray;

        public ZkytScheme()
        {
            m_LineColor = Color.Gray;
        }

        #region 设置显示属性
        #region 常规
        /// <summary>
        /// 方案名称
        /// </summary>
        [Category(ZkytCategory1), DisplayName("方案名称"), DefaultValue(""), PropertyOrder(0)]
        public string Name { get; set; } = "新建岩土方案";

        /// <summary>
        /// 方案数据名称
        /// </summary>
        [Category(ZkytCategory1), DisplayName("岩土数据名称"), DefaultValue(""), Editor(typeof(ZkytLayerEditor),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(1)]
        public string ZkytDataName { get; set; }

        //[Browsable(false)]
        //[XmlIgnore]
        //public IFeatureClass ZkytFeatureClass { get; set; }//绑定的源数据

        [Browsable(false)]
        [XmlIgnore]
        public ITable ZkytDataTable { get; set; }//绑定的源数据(mdb表)

        /// <summary>
        /// 钻孔岩土数据源
        /// </summary>
        [Category(ZkytCategory1), DisplayName("岩土数据源"), DefaultValue(""), PropertyOrder(2)]
        public string ZkytDataSource { get; set; }

        /// <summary>
        /// 地质钻孔图层名称
        /// </summary>
        [Category(ZkytCategory1), DisplayName("钻孔图层名称"), DefaultValue(""), Editor(typeof(DzzkLayerEditor),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(3)]
        public string DzzkLayerName { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public IFeatureClass DzzkFeatureClass { get; set; }//绑定的源数据

        /// <summary>
        /// 地质钻孔数据源
        /// </summary>
        [Category(ZkytCategory1), DisplayName("钻孔图层数据源"), DefaultValue(""), PropertyOrder(4)]
        public string DzzkDataSource { get; set; }

        #endregion

        #region 钻孔岩土属性
        /// <summary>
        /// 钻孔岩土工程编号
        /// </summary>
        [Category(ZkytCategory2), DisplayName("岩土工程编号"), DefaultValue(""), Editor(typeof(ZkytFieldEditorEx),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(5)]
        public string strYtPro_No { get; set; }

        /// <summary>
        /// 钻孔编号
        /// </summary>
        [Category(ZkytCategory2), DisplayName("钻孔编号"), DefaultValue(""), Editor(typeof(ZkytFieldEditorEx),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(6)]
        public string strYtId { get; set; }

        /// <summary>
        /// 岩土类别
        /// </summary>
        [Category(ZkytCategory2), DisplayName("岩土类别"), DefaultValue(""), Editor(typeof(ZkytFieldEditorEx),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(7)]
        public string strGeo_Name { get; set; }

        /// <summary>
        /// 最高海拔
        /// </summary>
        [Category(ZkytCategory2), DisplayName("最高海拔"), DefaultValue(""), Editor(typeof(ZkytFieldEditorEx),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(8)]
        public string strTop_H { get; set; }

        /// <summary>
        /// 最低海拔
        /// </summary>
        [Category(ZkytCategory2), DisplayName("最低海拔"), DefaultValue(""), Editor(typeof(ZkytFieldEditorEx),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(9)]
        public string strBot_H { get; set; }

        #endregion

        #region 地质钻孔属性
        /// <summary>
        /// 地质钻孔工程编号
        /// </summary>
        [Category(ZkytCategory3), DisplayName("地质钻孔编号"), DefaultValue(""), Editor(typeof(DzzkFieldEditorEx),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(10)]
        public string strZkPro_No { get; set; }

        /// <summary>
        /// 钻孔编号
        /// </summary>
        [Category(ZkytCategory3), DisplayName("钻孔编号"), DefaultValue(""), Editor(typeof(DzzkFieldEditorEx),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(11)]
        public string strZkId { get; set; }

        /// <summary>
        /// 钻孔直径
        /// </summary>
        [Category(ZkytCategory3), DisplayName("钻孔直径字段"), DefaultValue(""), Editor(typeof(DzzkFieldEditorEx),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(12)]
        public string strDN { get; set; }

        /// <summary>
        /// 钻孔标高
        /// </summary>
        [Category(ZkytCategory3), DisplayName("钻孔标高字段"), DefaultValue(""), Editor(typeof(DzzkFieldEditorEx),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(13)]
        public string strHeight { get; set; }

        /// <summary>
        /// 钻孔深度
        /// </summary>
        [Category(ZkytCategory3), DisplayName("钻孔深度字段"), DefaultValue(""), Editor(typeof(DzzkFieldEditorEx),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(14)]
        public string strH_Deep { get; set; }

        #endregion
        #endregion

        /// <summary>
        /// 钻孔岩土字段列表(在配置信息中下拉可选择)
        /// </summary>
        [Browsable(false)]
        public List<string> ZkytFields { get; set; } = new List<string>();
        /// <summary>
        /// 地质钻孔字段列表(在配置信息中下拉可选择)
        /// </summary>
        [Browsable(false)]
        public List<string> DzzkFields { get; set; } = new List<string>();

        public List<string> ZkytLayer3D = new List<string>();
        public List<string> DzzkLayer3D = new List<string>();

        /// <summary>
        /// 运行生成模型(钻孔岩土)
        /// </summary>
        /// <param name="pWorkspace"></param>
        /// <param name="trackProgress"></param>
        /// <param name="dictMarker3DSymbol"></param>
        public void RunTask(IWorkspace pWorkspace, ITrackProgress trackProgress, Dictionary<string, IMarker3DSymbol> dictMarker3DSymbol)
        {
            #region 检查钻孔岩土的配置信息是否为空
            //钻孔岩土图层名
            if (ZkytDataName.IsNull() || DzzkLayerName.IsNull())
            {
                return;
            }
            //钻孔岩土数据源
            if (ZkytDataSource.IsNull() || DzzkDataSource.IsNull())
            {
                return;
            }
            //岩土钻孔图层工程编号
            if (strYtPro_No.IsNull() || strZkPro_No.IsNull())
            {
                return;
            }
            //岩土钻孔图层钻孔编码
            if (strYtId.IsNull() || strZkId.IsNull())
            {
                return;
            }
            //岩土类别
            if (strGeo_Name.IsNull())
            {
                return;
            }
            //最高最低海拔
            if (strTop_H.IsNull() || strBot_H.IsNull())
            {
                return;
            }
            //钻孔直径
            if (strDN.IsNull())
            {
                return;
            }
            //钻孔标高
            if (strHeight.IsNull())
            {
                return;
            }
            //钻孔深度
            if (strH_Deep.IsNull())
            {
                return;
            }
            //钻孔图层的源数据
            if (DzzkFeatureClass == null)
            {
                return;
            }
            //岩土图层源数据
            if (ZkytDataTable == null)
            {
                return;
            }
            #endregion

            trackProgress.SubMessage = $"正在创建{ZkytDataName} 3D模型";
            Application.DoEvents();

            #region 创建3D图层
            string Layer3dName = ZkytDataName + "_3D";
            string LayerAliasName = DzzkFeatureClass.AliasName;

            ITable pTable = ZkytDataTable as ITable;
            //原要素字段
            IFields pFields = (ZkytDataTable.Fields as IClone).Clone() as IFields;
            //获取二维地质钻孔图层的空间参考
            IGeoDataset pGeoDataSet = DzzkFeatureClass as IGeoDataset;
            ISpatialReference sr = pGeoDataSet.SpatialReference;
            //创建三维多面体字段(获取岩土mdb图层字段)
            //IFields pNewFields = FeatureModelHelper.CreateFields3D(pFields, sr, esriGeometryType.esriGeometryMultiPatch);
            IFields pNewFields = FeatureModelHelper.CreateZkytFields3D(pFields, sr, esriGeometryType.esriGeometryMultiPatch);

            //生成要素
            IFeatureClass pToFeatureClass;
            if (AnalystHelper.IsExistFeatureClass(DzzkLayerName, pWorkspace))
            {
                pToFeatureClass = (pWorkspace as IFeatureWorkspace).OpenFeatureClass(DzzkLayerName);
            }
            else
            {
                pToFeatureClass = (pWorkspace as IFeatureWorkspace).CreateFeatureClass(Layer3dName, pNewFields, null, null,
                    DzzkFeatureClass.FeatureType, "SHAPE", "");
            }
            #endregion

            #region 获取钻孔编号唯一值
            List<UniqueValueInfo> pUniqueValueInfos = new List<UniqueValueInfo>();
            List<string> lstFields = new List<string>();
            //根据地质钻孔编号绘制模型
            if (!string.IsNullOrWhiteSpace(strYtId))
            {
                int ytIdIndex = pTable.FindField(strYtId);
                if (ytIdIndex < 0)
                {
                    MessageBox.Show("岩土数据找不到钻孔编号字段!");
                    return;
                }

                //验证钻孔图层的钻孔编号字段存在
                int zkIdIndex = DzzkFeatureClass.FindField(strZkId);
                if (zkIdIndex < 0)
                {
                    MessageBox.Show("钻孔图层的钻孔编号字段不存在!");
                    return;
                }
                //验证岩土数据中的工程编号字段是否存在
                int ytProIndex = ZkytDataTable.FindField(strYtPro_No);
                if (ytProIndex < 0)
                {
                    MessageBox.Show("岩土数据的工程编号字段不存在!");
                    return;
                }
                //验证钻孔点图层中的工程编号字段是否存在
                int zkProIndex = DzzkFeatureClass.FindField(strZkPro_No);
                if (zkProIndex < 0)
                {
                    MessageBox.Show("钻孔点图层的工程编号字段不存在!");
                    return;
                }
                //验证岩土数据中的岩土名称字段是否存在
                int geoNameIndex = ZkytDataTable.FindField(strGeo_Name);
                if (geoNameIndex < 0)
                {
                    MessageBox.Show("岩土数据的岩土名称字段不存在!");
                    return;
                }
                //验证岩土数据中的高程字段是否完整
                int topHIndex = ZkytDataTable.FindField(strTop_H);
                int botHIndex = ZkytDataTable.FindField(strBot_H);
                if (topHIndex < 0 || botHIndex < 0)
                {
                    MessageBox.Show("岩土数据的高程字段缺少!");
                    return;
                }
                //验证钻孔图层中的孔径字段是否存在
                int zkSizeIndex = DzzkFeatureClass.FindField(strDN);
                if (zkSizeIndex < 0)
                {
                    MessageBox.Show("钻孔图层的孔径字段不存在!");
                    return;
                }

                lstFields.Add(strYtId);

                Hashtable pHashTable = AnalystHelper.GetUniqueValues(pTable, lstFields);//获取各唯一值数量
                IQueryFilter tQueryFilter = null;
                //根据地质钻孔编号来绘制对应柱体的岩土分层模型
                foreach (string key in pHashTable.Keys)
                {
                    tQueryFilter = new QueryFilterClass();
                    tQueryFilter.WhereClause = $"{strYtId}='{key}'";

                    //根据3D配置文件中的3D模型符号来生成岩土分层模型
                    ZkytDataTable.CopyToZkyt3D(DzzkFeatureClass, pToFeatureClass, tQueryFilter, strYtPro_No, strZkPro_No,
                    strYtId, strZkId, key, strGeo_Name, strTop_H, strBot_H, strDN, trackProgress, dictMarker3DSymbol);

                    #region 判断是否有地质钻孔3D模型符号
                    //if (dictMarker3DSymbol.ContainsKey(DzzkLayerName))
                    //{
                    //    IMarker3DSymbol marker3DSymbol = dictMarker3DSymbol[DzzkLayerName];
                    //    ZkytDataTable.CopyToZkyt3D(DzzkFeatureClass,pToFeatureClass,tQueryFilter, strYtPro_No, strZkPro_No,
                    //    strYtId, strZkId, key, strGeo_Name, strTop_H, strBot_H, strDN, trackProgress, marker3DSymbol);

                    //}
                    //else if (LayerAliasName != " " && dictMarker3DSymbol.ContainsKey(LayerAliasName))
                    //{
                    //    IMarker3DSymbol marker3DSymbol = dictMarker3DSymbol[LayerAliasName];
                    //    ZkytDataTable.CopyToZkyt3D(DzzkFeatureClass, pToFeatureClass, tQueryFilter,strYtPro_No, strZkPro_No, 
                    //    strYtId, strZkId, key, strGeo_Name, strTop_H, strBot_H, strDN, trackProgress, marker3DSymbol);
                    //}
                    //else
                    //{
                    //    //每个钻孔编号将mdb的字段拷贝到新图层的字段中并生成模型
                    //    ZkytDataTable.CopyToZkyt3D(DzzkFeatureClass, pToFeatureClass, tQueryFilter, strYtPro_No, 
                    //    strZkPro_No, strYtId, strZkId, key, strGeo_Name, strTop_H, strBot_H, strDN, trackProgress);
                    //}
                    #endregion

                }
            }
            #endregion

            //获取三维岩土模型要素个数
            int count = pToFeatureClass.FeatureCount(null);
            if (count > 0)
            {
                if (!ZkytLayer3D.Exists(m => m == Layer3dName))
                    ZkytLayer3D.Add(Layer3dName);
            }
        }

        #region 事件
        public event SchemeValueChangedEventHandler SchemePropertyValueChanged;
        public void OnSchemePropertyValueChanged(object sender, SchemePropertyEventArgs e)
        {
            if (SchemePropertyValueChanged != null)
            {
                SchemePropertyValueChanged(sender, e);
            }
        }
        #endregion
    }


}
