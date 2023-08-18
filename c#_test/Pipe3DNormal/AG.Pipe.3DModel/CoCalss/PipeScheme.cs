using AG.COM.SDM.Utility;
using AG.Pipe.Analyst3DModel.Editor;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using AG.COM.SDM.Utility.Common;
using AG.COM.SDM.Utility.Logger;

namespace AG.Pipe.Analyst3DModel
{
    [TypeConverter(typeof(PropertySorter))]
    [Serializable]
    public class SchemePropertyEventArgs : EventArgs
    {
        private object m_ItemProperty;
        private object m_Value;

        /// <summary>
        /// 实例化新的节点属性参数类
        /// </summary>
        /// <param name="pComponent">节点属性项</param>
        /// <param name="value">Object值对象</param>
        public SchemePropertyEventArgs(object pComponent, object value)
        {
            this.m_ItemProperty = pComponent;
            this.m_Value = value;
        }

        /// <summary>
        /// 属性值
        /// </summary>
        public object Value
        {
            get
            {
                return this.m_Value;
            }
        }

        /// <summary>
        /// 属性项
        /// </summary>
        public object ItemProperty
        {
            get
            {
                return this.m_ItemProperty;
            }
        }
    }
    [Serializable]
    public delegate void SchemeValueChangedEventHandler(object sender, SchemePropertyEventArgs e);
    public interface ILineScheme
    {
        IFeatureClass LineFeatureClass { get; set; }
        List<string> LineFields { get; set; }
    }
    public interface IPointScheme
    {
        IFeatureClass PointFeatureClass { get; set; }
        List<string> PointFields { get; set; }
    }
    public interface ISchemeValueChanged
    {
        event SchemeValueChangedEventHandler SchemePropertyValueChanged;
        void OnSchemePropertyValueChanged(object sender, SchemePropertyEventArgs e);
    }
    public interface ISchemeName
    {
        string Name { get; set; }

    }



    /// <summary>
    /// 管线模型生成方案管理
    /// </summary>
    [Serializable]
    public class PipeSchemeManager : ISchemeValueChanged, ISchemeName
    {
        private const string Category1 = "1、常规";
        /// <summary>
        /// 方案名称
        /// </summary>
        [Category("常规"), DisplayName("方案名称"), DefaultValue("")]
        public string Name { get; set; } = "方案名称";

        [Category(Category1), DisplayName("导出路径"), Editor(typeof(SavePathEditor),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(2)]
        public string OutPath { get; set; } = $"D:\\";

        [Category(Category1), DisplayName("样式符号配置文件"), DefaultValue(""), Editor(typeof(StyleFile3DEditor),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(3)]
        public string StylePath { get; set; }

        #region 适配球面坐标系
        [Category(Category1), DisplayName("投影文件(适用于球面坐标系数据)"), DefaultValue(""), Editor(typeof(ProjectFileEditor),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(3)]
        public string ProjectPath { get; set; }
        #endregion

        [Browsable(false)]
        public List<PipeScheme> Schemes { get; set; } = new List<PipeScheme>();
        [Browsable(false)]
        public List<PipeSchemeGroup> SchemeGroups { get; set; } = new List<PipeSchemeGroup>();
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
    /// 新建图层组
    /// </summary>
    public class PipeSchemeGroup : ISchemeValueChanged, ISchemeName
    {
        /// <summary>
        /// 方案名称
        /// </summary>
        [Category("常规"), DisplayName("图层组名称"), DefaultValue("")]
        public string Name { get; set; } = "新建图层组";
        [Browsable(false)]
        public List<PipeScheme> Schemes { get; set; } = new List<PipeScheme>();

        #region 事件
        public event SchemeValueChangedEventHandler SchemePropertyValueChanged;
        public void OnSchemePropertyValueChanged(object sender, SchemePropertyEventArgs e)
        {
            if (SchemePropertyValueChanged != null)
            {
                SchemePropertyValueChanged(sender, e);
            }
        }
        public PipeSchemeGroup()
        {

        }
        #endregion

    }
    /// <summary>
    /// 管线模型生成方案
    /// </summary>
    [Serializable]
    public class PipeScheme : ISchemeValueChanged, ISchemeName
    {
        /// <summary>
        /// 方案名称
        /// </summary>
        [Category("常规"), DisplayName("小类名称"), DefaultValue("")]
        public string Name { get; set; } = "新建模型方案";
        /// <summary>
        /// 管线
        /// </summary>
        [Browsable(false)]
        public LineScheme LineScheme { get; set; } = new LineScheme();
        /// <summary>
        /// 管点
        /// </summary>
        [Browsable(false)]
        public PointScheme PointScheme { get; set; } = new PointScheme();

        #region 事件
        public event SchemeValueChangedEventHandler SchemePropertyValueChanged;
        public void OnSchemePropertyValueChanged(object sender, SchemePropertyEventArgs e)
        {
            if (SchemePropertyValueChanged != null)
            {
                SchemePropertyValueChanged(sender, e);
            }
        }
        public PipeScheme()
        {
            //LineScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
            //PointScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
        }
        #endregion
    }

    /// <summary>
    /// 管线属性
    /// </summary>
    [TypeConverter(typeof(PropertySorter))]
    [Serializable]
    public class LineScheme : ILineScheme, ISchemeValueChanged, ISchemeName
    {
        public LineScheme()
        {
            m_LineColor = Color.Gray;
        }

        private const string LineCategory1 = "1、常规";
        private const string LineCategory2 = "2、管线属性配置";
        private const string LineCategory3 = "3、管点属性配置";
        private Color m_LineColor = Color.Gray;

        /// <summary>
        /// 方案名称
        /// </summary>
        [Category(LineCategory1), DisplayName("管线名称"), DefaultValue(""), PropertyOrder(0)]
        public string Name { get; set; } = "新建管线";

        /// <summary>
        /// 方案图层
        /// </summary>
        [Category(LineCategory1), DisplayName("管线图层名称"), DefaultValue(""), Editor(typeof(LineLayerEditor),
            typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(1)]
        public string LineLayerName { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public IFeatureClass LineFeatureClass { get; set; }
        [Category(LineCategory1), DisplayName("工作空间路径"), Browsable(false), PropertyOrder(2)]
        public string WorkPath { get; set; }

        /// <summary>
        /// 管线数据源名称
        /// </summary>
        [Category(LineCategory1), DisplayName("管线数据源名称"), DefaultValue(""), PropertyOrder(3)]
        public string LineDataSource { get; set; }

        #region 管线配置
        /// <summary>
        /// 起始编号字段
        /// </summary>
        [Category(LineCategory2), DisplayName("起始编号字段"), DefaultValue(""), Editor(typeof(LineFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(3)]
        public string strS_Point { get; set; }

        #region 埋深字段
        /// <summary>
        /// 起始埋深字段
        /// </summary>
        //[Category(LineCategory2), DisplayName("起始埋深字段"), DefaultValue(""), Editor(typeof(LineFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(4)]
        //public string strS_Deep { get; set; }

        /// <summary>
        /// 终点埋深字段
        /// </summary>
        //[Category(LineCategory2), DisplayName("终点埋深字段"), DefaultValue(""), Editor(typeof(LineFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(7)]
        //public string strE_Deep { get; set; }

        #endregion

        /// <summary>
        /// 起点高程
        /// </summary>
        [Category(LineCategory2), DisplayName("起点高程字段"), DefaultValue(""), Editor(typeof(LineFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(5)]
        public string strS_Hight { get; set; }

        /// <summary>
        /// 终点编号字段
        /// </summary>
        [Category(LineCategory2), DisplayName("终点编号字段"), DefaultValue(""), Editor(typeof(LineFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(6)]
        public string strE_Point { get; set; }

        /// <summary>
        /// 终点高程字段
        /// </summary>
        [Category(LineCategory2), DisplayName("终点高程字段"), DefaultValue(""), Editor(typeof(LineFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(8)]
        public string strE_Hight { get; set; }

        /// <summary>
        /// 管径字段
        /// </summary>
        [Category(LineCategory2), DisplayName("管径字段"), DefaultValue(""), Editor(typeof(LineFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(9)]
        public string strPSize { get; set; }

        /// <summary>
        /// 管径类型
        /// </summary>
        //[Category(LineCategory2), DisplayName("管径类型"), TypeConverter(typeof(EnumConverterEx)), Editor(typeof(EnumTypeEditor), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(10)]
        //public Line3DType LineType { get; set; } = Line3DType.Circle;

        /// <summary>
        /// 管线中心点高程
        /// </summary>
        [Category(LineCategory2), DisplayName("管线中心点高程"), TypeConverter(typeof(EnumConverterEx)), Editor(typeof(EnumTypeEditor), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(11)]
        public CalculationType CalculationType { get; set; } = CalculationType.Type1;

        #region 管线颜色配置
        ///// <summary>
        ///// 颜色
        ///// </summary>
        //[Category(LineCategory2), DisplayName("颜色"), TypeConverter(typeof(ColorConverterEx)), PropertyOrder(12)]
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

        /// <summary>
        /// 管线字段列表
        /// </summary>
        [Browsable(false)]
        public List<string> LineFields { get; set; } = new List<string>();
        public List<string> SuccessLayer = new List<string>();
        public List<string> Layer3D = new List<string>();
        List<string> Msgs = new List<string>();
        #endregion

        #region 管点配置
        ///// <summary>
        ///// 管点图层名称
        ///// </summary>
        //[Category(LineCategory1), DisplayName("管点图层名称"), DefaultValue(""), Editor(typeof(PointLayerEditor), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(3)]
        //public string PointLayerName { get; set; }

        ///// <summary>
        ///// 管点数据源名称
        ///// </summary>
        //[Category(LineCategory1), DisplayName("管点数据源名称"), DefaultValue(""), PropertyOrder(3)]
        //public string PointDataSource { get; set; }

        //[Browsable(false)]
        //[XmlIgnore]
        //public IFeatureClass PointFeatureClass { get; set; }

        ///// <summary>
        ///// 管点编号字段
        ///// </summary>
        //[Category(LineCategory3), DisplayName("管点编号字段"), DefaultValue(""), Editor(typeof(PointFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(3)]
        //public string strPNo { get; set; }

        ///// <summary>
        ///// 管点X坐标字段
        ///// </summary>
        //[Category(LineCategory3), DisplayName("管点X坐标字段"), DefaultValue(""), Editor(typeof(PointFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(4)]
        //public string strX { get; set; }

        ///// <summary>
        ///// 管点Y坐标字段
        ///// </summary>
        //[Category(LineCategory3), DisplayName("管点Y坐标字段"), DefaultValue(""), Editor(typeof(PointFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(5)]
        //public string strY { get; set; }

        ///// <summary>
        ///// 管点字段列表
        ///// </summary>
        //[Browsable(false)]
        //public List<string> PointFields { get; set; } = new List<string>();
        #endregion

        /// <summary>
        /// 运行生成模型
        /// </summary>
        /// <param name="pWorkspace">GDB工作空间</param>
        /// <param name="LineBigType">管线所在大类</param>
        /// <param name="trackProgress">进度条</param>
        /// <param name="dictMarker3DSymbol">管点附属物对应模型</param>
        public bool RunTask(IWorkspace pWorkspace, string LineBigType, ITrackProgress trackProgress, Dictionary<string, IMarker3DSymbol> dictMarker3DSymbol)
        {
            bool IsSuccess = false;

            #region 检查管线类型的配置是否为空
            //管线图层名称
            if (LineLayerName.IsNull())
            {
                MessageBox.Show("管线方案图层名为空!");
                return false;
            }
            //起止编号字段为空
            if (strS_Point.IsNull())
            {
                MessageBox.Show("起点编号字段为空!");
                return false;
            }
            //起止高程字段
            if (strS_Hight.IsNull())
            {
                MessageBox.Show("起点高程字段为空!");
                return false;
            }
            //终点编号字段
            if (strE_Point.IsNull())
            {
                MessageBox.Show("终点编号字段为空!");
                return false;
            }
            //终点高程字段
            if (strE_Hight.IsNull())
            {
                MessageBox.Show("终点高程字段为空!");
                return false;
            }
            //管径字段
            if (strPSize.IsNull())
            {
                MessageBox.Show("管径字段为空!");
                return false;
            }
            //管线类型所绑定的源数据
            if (LineFeatureClass == null)
            {
                MessageBox.Show("管线类型没有数据源");
                return false;
            }
            #endregion

            #region 检查管线类型的配置信息有效值
            //起点编号字段
            if (LineFeatureClass.FindField(strS_Point) < 0)
            {
                MessageBox.Show("管线数据源不存在方案配置的起点编号字段");
                return false;
            }
            //终点编号字段
            if (LineFeatureClass.FindField(strE_Point) < 0)
            {
                MessageBox.Show("管线数据源不存在方案配置的终点编号字段");
                return false;
            }
            //起点高程字段
            if (LineFeatureClass.FindField(strS_Hight) < 0)
            {
                MessageBox.Show("管线数据源不存在方案配置的起点高程字段");
                return false;
            }
            //终点高程字段
            if (LineFeatureClass.FindField(strE_Hight) < 0)
            {
                MessageBox.Show("管线数据源不存在方案配置的终点高程字段");
                return false;
            }
            //管径字段
            if (LineFeatureClass.FindField(strPSize) < 0)
            {
                MessageBox.Show("管线数据源不存在方案配置的管径字段");
                return false;
            }
            #endregion

            #region 检查关联管点的配置信息
            //if(PointLayerName.IsNull())
            //{
            //    return false;
            //}
            //if (strPNo.IsNull())
            //{
            //    return false;
            //}
            //if (strX.IsNull())
            //{
            //    return false;
            //}
            //if (strY.IsNull())
            //{
            //    return false;
            //}
            //if(PointFeatureClass==null)
            //{
            //    return false;
            //}
            #endregion
            try
            {
                SuccessLayer.Clear();
                Layer3D.Clear();
                trackProgress.SubMessage = $"正在创建{LineLayerName} 3D管线";
                Application.DoEvents();
                #region 创建3D管线图层

                string Layer3dName = LineLayerName + "_3D";

                //判断工作空间释放存在 其 三维图层
                if (AnalystHelper.IsExistFeatureClass(Layer3dName, pWorkspace))
                {
                    return false;
                }
                #endregion
                ITable pTable = LineFeatureClass as ITable;
                //原要素字段
                IFields pFields = (LineFeatureClass.Fields as IClone).Clone() as IFields;
                //3d属性要素
                IGeoDataset pGeodataSet = LineFeatureClass as IGeoDataset;
                ISpatialReference sr = pGeodataSet.SpatialReference;
                string sName = sr.Name;

                #region 若数据坐标系为球面坐标，则需要根据投影文件对空间参考进行投影，作为3d要素的空间参考
                //ISpatialReference sr3D = AnalystHelper.GCStoPRJ(sr);
                //string name = sr3D.Name;
                #endregion
                ISpatialReference sr3D = sr;

                //创建三维多面体字段
                IFields pNewFields = FeatureModelHelper.CreateFields3D(pFields, sr3D, esriGeometryType.esriGeometryMultiPatch);
                //创建三维多面体管线
                IFeatureClass pToFeatureClass = (pWorkspace as IFeatureWorkspace).CreateFeatureClass(Layer3dName, pNewFields, null, null, LineFeatureClass.FeatureType, "SHAPE", "");

                #region 获取唯一值
                //根据管线管径绘制模型
                if (!string.IsNullOrWhiteSpace(strPSize) && pTable.FindField(strPSize) >= 0)
                {
                    //从管径字段获取唯一值
                    Hashtable pHashTable = AnalystHelper.GetUniqueValue(pTable, strPSize);

                    //获取管径设置字段名所在字段，在遍历时判定其字段类型
                    int PSizeIndex = pTable.Fields.FindField(strPSize);
                    IField tField = pTable.Fields.Field[PSizeIndex];

                    IQueryFilter tQueryFilter = null;
                    int valueCount = 0;
                    string strkey = "";
                    //根据管径字段值中的每个唯一值绘制管线模型
                    foreach (string key in pHashTable.Keys)
                    {
                        if (!trackProgress.IsContinue) break;

                        if (string.IsNullOrWhiteSpace(key) || key == "空" || key == "0")
                        {
                            strkey = "100";
                        }
                        else
                        {
                            strkey = key;
                        }
                        valueCount++;
                        trackProgress.TotalMessage = $"正在生成{LineLayerName}({valueCount}/{pHashTable.Count})";
                        tQueryFilter = new QueryFilterClass();

                        #region 判定字段类型，是否为字符串还是数字型
                        if (tField.Type != esriFieldType.esriFieldTypeString)
                        {
                            if (string.IsNullOrWhiteSpace(key) || key == "空")
                            {
                                tQueryFilter.WhereClause = $"{strPSize} is null";
                            }
                            else
                            {
                                tQueryFilter.WhereClause = $"{strPSize}={key}";
                            }
                        }
                        #endregion
                        else
                        {
                            if (string.IsNullOrWhiteSpace(key) || key == "空")
                            {
                                tQueryFilter.WhereClause = $"{strPSize}=''OR {strPSize} is null";
                            }
                            else
                            {
                                tQueryFilter.WhereClause = $"{strPSize}='{key}'";
                            }
                        }

                        char[] charSeparators = new char[] { 'X', 'x', '×', '*' };
                        string[] result = strkey.Split(charSeparators, StringSplitOptions.None);
                        Line3DType type = Line3DType.Circle;
                        string str3DSymbol = string.Empty;
                        //根据管径的值来判断绘制哪种管
                        if (result.Length > 1)
                        {
                            type = Line3DType.Square;
                            str3DSymbol = $"{LineBigType}_方管";
                        }
                        else
                        {
                            type = Line3DType.Circle;
                            str3DSymbol = $"{LineBigType}_圆管";
                            double keyValue = 0.0;
                            if (Double.TryParse(result[0], out keyValue) == false) continue;//排除管径为非数字
                        }
                        if (dictMarker3DSymbol.ContainsKey(str3DSymbol))
                        {
                            IMarker3DSymbol marker3DSymbol = dictMarker3DSymbol[str3DSymbol];

                            bool IsBuild = LineFeatureClass.CopyToLine3D(pToFeatureClass, tQueryFilter,
                                strS_Hight, strE_Hight, strPSize, strS_Point, strE_Point,
                                 marker3DSymbol, CalculationType, trackProgress);
                            if (!IsBuild)
                            {
                                break;
                            }
                        }
                    }
                }
                #endregion
                else
                {
                    MessageBox.Show("管径字段未设置或管径字段不存在于数据源中!");
                    return false;
                }
                if (!trackProgress.IsContinue) return false;

                //获取三维管线要素个数
                int count = pToFeatureClass.FeatureCount(null);
                if (count > 0)
                {
                    if (!Layer3D.Exists(m => m == Layer3dName))
                        Layer3D.Add(Layer3dName);
                }
                else
                {
                    return IsSuccess;
                }
                IsSuccess = true;
                return IsSuccess;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ExceptionLog.LogError(ex.Message, ex);
                return false;
            }
        }
        public void Set(LineScheme scheme)
        {
            this.CalculationType = scheme.CalculationType;
            this.LineFields = scheme.LineFields;
            //this.LineLayerName = scheme.LineLayerName;
            //this.LineType = scheme.LineType;
            //this.strE_Deep = scheme.strE_Deep;
            this.strE_Hight = scheme.strE_Hight;
            this.strE_Point = scheme.strE_Point;
            this.strPSize = scheme.strPSize;
            //this.strS_Deep = scheme.strS_Deep;
            this.strS_Hight = scheme.strS_Hight;
            this.strS_Point = scheme.strS_Point;
            //this.LineColor = scheme.LineColor;
            this.WorkPath = this.WorkPath;
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

        #region 颜色
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
        #endregion
    }

    /// <summary>
    /// 管点属性
    /// </summary>
    [TypeConverter(typeof(PropertySorter))]
    [Serializable]
    public class PointScheme : ILineScheme, IPointScheme, ISchemeValueChanged, ISchemeName
    {
        private const string PointCategory1 = "1、常规";
        private const string PointCategory2 = "2、管点属性配置";
        private const string PointCategory3 = "3、关联管线属性配置";
        private const string PointCategory4 = "4、附属物和特征点配置";
        public PointScheme()
        {

        }

        /// <summary>
        /// 方案名称
        /// </summary>
        [Category(PointCategory1), DisplayName("管点名称"), DefaultValue(""), PropertyOrder(0)]
        public string Name { get; set; } = "新建管点";

        /// <summary>
        /// 方案图层
        /// </summary>
        [Category(PointCategory1), DisplayName("管点图层名称"), DefaultValue(""), Editor(typeof(PointLayerEditor), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(1)]
        public string PointLayerName { get; set; }

        [Category(PointCategory1), DisplayName("工作空间路径"), Browsable(false), PropertyOrder(2)]
        public string WorkPath { get; set; }

        /// <summary>
        /// 数据源名称
        /// </summary>
        [Category(PointCategory1), DisplayName("管点数据源名称"), DefaultValue(""), PropertyOrder(9)]
        public string PointDataSource { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public IFeatureClass PointFeatureClass { get; set; }

        /// <summary>
        /// 管点编号字段
        /// </summary>
        [Category(PointCategory2), DisplayName("管点编号字段"), DefaultValue(""), Editor(typeof(PointFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(3)]
        public string strPNo { get; set; }

        #region 管点坐标值字段
        /// <summary>
        /// 管点X坐标字段
        /// </summary>
        //[Category(PointCategory2), DisplayName("管点X坐标字段"), DefaultValue(""), Editor(typeof(PointFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(4)]
        //public string strX { get; set; }

        /// <summary>
        /// 管点Y坐标字段
        /// </summary>
        //[Category(PointCategory2), DisplayName("管点Y坐标字段"), DefaultValue(""), Editor(typeof(PointFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(5)]
        //public string strY { get; set; }
        #endregion

        /// <summary>
        /// 管点高程字段(地面高程)
        /// </summary>
        [Category(PointCategory2), DisplayName("管点地面高程字段"), DefaultValue(""), Editor(typeof(PointFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(6)]
        public string strZ { get; set; }

        /// <summary>
        /// 管点井深字段
        /// </summary>
        //[Category(PointCategory2), DisplayName("管点井深字段"), DefaultValue(""),
        //    Editor(typeof(PointFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(7)]
        //public string strDeep { get; set; }

        #region 附属物模型

        /// <summary>
        ///附属物字段
        /// </summary>
        [Category(PointCategory4), DisplayName("附属物字段"), DefaultValue(""), Editor(typeof(PointFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(7)]
        public string strSubsid { get; set; }

        /// <summary>
        /// 附属物类型字段
        /// </summary>
        [Category(PointCategory4), DisplayName("附属物类型字段"), DefaultValue(""), Editor(typeof(PointFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(8)]
        public string strSubsidType { get; set; }

        /// <summary>
        /// 附属物大小字段
        /// </summary>
        [Category(PointCategory4), DisplayName("附属物大小字段"), DefaultValue(""), Editor(typeof(PointFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(9)]
        public string strSubsidSize { get; set; }

        #endregion

        /// <summary>
        /// 管点字段列表
        /// </summary>
        [Browsable(false)]
        public List<string> PointFields { get; set; } = new List<string>();

        #region 关联管线
        /// <summary>
        /// 方案图层
        /// </summary>
        [Category(PointCategory1), DisplayName("管线图层名称"), DefaultValue(""), Editor(typeof(LineLayerEditor), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(10)]
        public string LineLayerName { get; set; }

        /// <summary>
        /// 管线数据源名称
        /// </summary>
        [Category(PointCategory1), DisplayName("管线数据源名称"), DefaultValue(""), PropertyOrder(11)]
        public string LineDataSource { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public IFeatureClass LineFeatureClass { get; set; }
        /// <summary>
        /// 起点编号字段
        /// </summary>
        [Category(PointCategory3), DisplayName("起点编号字段"), DefaultValue(""), Editor(typeof(LineFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(13)]
        public string strS_Point { get; set; }

        #region 埋深字段(notuse)
        ///// <summary>
        ///// 起始埋深字段
        ///// </summary>
        //[Category(PointCategory3), DisplayName("起始埋深字段(选填)"), DefaultValue(""), Editor(typeof(LineFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(14)]
        //public string strS_Deep { get; set; }
        ///// <summary>
        ///// 终点埋深字段
        ///// </summary>
        //[Category(PointCategory3), DisplayName("终点埋深字段(选填)"), DefaultValue(""), Editor(typeof(LineFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(17)]
        //public string strE_Deep { get; set; }

        #endregion 

        /// <summary>
        /// 起点高程
        /// </summary>
        [Category(PointCategory3), DisplayName("起点高程字段"), DefaultValue(""), Editor(typeof(LineFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(15)]
        public string strS_Hight { get; set; }

        /// <summary>
        /// 终点编号字段
        /// </summary>
        [Category(PointCategory3), DisplayName("终点编号字段"), DefaultValue(""), Editor(typeof(LineFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(16)]
        public string strE_Point { get; set; }

        /// <summary>
        /// 终点高程字段
        /// </summary>
        [Category(PointCategory3), DisplayName("终点高程字段"), DefaultValue(""), Editor(typeof(LineFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(18)]
        public string strE_Hight { get; set; }

        /// <summary>
        /// 管径字段
        /// </summary>
        [Category(PointCategory3), DisplayName("管径字段"), DefaultValue(""), Editor(typeof(LineFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(19)]
        public string strPSize { get; set; }

        #endregion

        #region 管点高程计算、特征值及井深计算
        /// <summary>
        /// 管点高程
        /// </summary>
        [Category(PointCategory3), DisplayName("管点高程"), TypeConverter(typeof(EnumConverterEx)), Editor(typeof(EnumTypeEditor), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(21)]
        public CalculationType CalculationType { get; set; } = CalculationType.Type1;

        //[Category(PointCategory4), DisplayName("特征值字段"), Editor(typeof(PointFieldEditorEx), typeof(System.Drawing.Design.UITypeEditor)), PropertyOrder(22)]
        //public string str_Feature { get; set; }

        #endregion

        #region
        /// <summary>
        /// 管线字段列表
        /// </summary>
        [Browsable(false)]
        public List<string> LineFields { get; set; } = new List<string>();

        #endregion


        public List<string> SuccessLayer = new List<string>();
        public List<string> Layer3D = new List<string>();

        /// <summary>
        /// 运行生成管点模型
        /// </summary>
        /// <param name="pWorkspace"></param>
        /// <param name="trackProgress"></param>
        /// <param name="dictMarker3DSymbol"></param>
        public bool RunTask(IWorkspace pWorkspace, string PointBigType, ITrackProgress trackProgress, Dictionary<string, IMarker3DSymbol> dictMarker3DSymbol)
        {
            bool IsSuccess = false;
            try
            {
                SuccessLayer.Clear();
                trackProgress.SubValue = 0;
                trackProgress.SubMessage = $"正在检查参数有效性";
                Application.DoEvents();

                #region 判定配置的管点字段名称是否为空，以及在数据源里面是否存在该名称字段
                //管点转换方案图层名称
                if (PointLayerName.IsNull())
                {
                    MessageBox.Show("管点方案图层名为空!");
                    return false;
                }
                //管点编号字段
                if (strPNo.IsNull())
                {
                    MessageBox.Show("管点编号字段名为空!");
                    return false;
                }
                else if (PointFeatureClass.FindField(strPNo) < 0)
                {
                    MessageBox.Show("管点数据源不存在配置的点号字段名!");
                    return false;
                }
                //管点地面高程字段(这里也要判断是否存在地面高字段，这个字段也是必须的)
                if (strZ.IsNull())
                {
                    MessageBox.Show("管点地面高程字段名为空!");
                    return false;
                }
                else if (PointFeatureClass.FindField(strZ) < 0)
                {
                    MessageBox.Show("管点数据源不存在配置的地面高程字段名!");
                    return false;
                }
                #region 这里暂时先不用井深字段
                //if (strDeep.IsNull())
                //{
                //    MessageBox.Show("管点井深字段名为空!");
                //    return false;
                //}
                //else if (PointFeatureClass.FindField(strDeep) < 0)
                //{
                //    MessageBox.Show("管点数据源不存在配置的井深字段名!");
                //    return false;
                //}
                #endregion
                #region
                //附属物字段
                //if (strSubsid.IsNull())
                //{
                //    MessageBox.Show("管点附属物字段名为空!");
                //    return false;
                //}
                //特征点字段
                //if (str_Feature.IsNull())
                //{
                //    MessageBox.Show("管点特征点字段名为空!");
                //    return false;
                //}
                #endregion
                //管线转换方案图层名称
                if (LineLayerName.IsNull())
                {
                    MessageBox.Show("管线方案图层名为空!");
                    return false;
                }
                //管线起始编号字段
                if (strS_Point.IsNull())
                {
                    MessageBox.Show("管线起点编号字段名为空!");
                    return false;
                }
                if (LineFeatureClass.FindField(strS_Point) < 0)
                {
                    MessageBox.Show("管点方案的管线数据源起点编号字段配置出错!");
                    return false;
                }
                //管线起点高程字段
                if (strS_Hight.IsNull())
                {
                    MessageBox.Show("管线起点高程字段名为空!");
                    return false;
                }
                if (LineFeatureClass.FindField(strS_Hight) < 0)
                {
                    MessageBox.Show("管点方案的管线数据源起点高程字段配置出错!");
                    return false;
                }
                //管线终点编号字段
                if (strE_Point.IsNull())
                {
                    MessageBox.Show("管线终点编号字段名为空!");
                    return false;
                }
                if (LineFeatureClass.FindField(strE_Point) < 0)
                {
                    MessageBox.Show("管点方案的管线数据源终点编号字段配置出错!");
                    return false;
                }
                //管线终点高程字段
                if (strE_Hight.IsNull())
                {
                    MessageBox.Show("管线终点高程字段名为空!");
                    return false;
                }
                if (LineFeatureClass.FindField(strE_Hight) < 0)
                {
                    MessageBox.Show("管点方案的管线数据源终点高程字段配置出错!");
                    return false;
                }
                //管径
                if (strPSize.IsNull())
                {
                    MessageBox.Show("管线管径字段名为空!");
                    return false;
                }
                if (LineFeatureClass.FindField(strPSize) < 0)
                {
                    MessageBox.Show("管点方案的管线数据源管径字段配置出错!");
                    return false;
                }
                //管点数据源
                if (PointFeatureClass == null)
                {
                    return false;
                }
                //管点数据源名称
                if (PointDataSource.IsNull())
                {
                    return false;
                }
                //管线数据源
                if (LineFeatureClass == null)
                {
                    return false;
                }
                //管线数据源名称
                if (LineDataSource.IsNull())
                {
                    return false;
                }

                #endregion

                Layer3D.Clear();
                trackProgress.SubValue = 0;
                trackProgress.SubMessage = $"正在创建{PointLayerName}3D管点";
                Application.DoEvents();
                string Layer3dName = PointLayerName + "_3D";
                ITable pTable = PointFeatureClass as ITable;

                IFields pFields = (PointFeatureClass.Fields as IClone).Clone() as IFields;
                //3d属性要素
                IGeoDataset pGeodataSet = PointFeatureClass as IGeoDataset;
                ISpatialReference sr = pGeodataSet.SpatialReference;
                #region 适配投影坐标
                //ISpatialReference sr3D = AnalystHelper.GCStoPRJ(sr);
                //string sName = sr3D.Name;
                #endregion
                ISpatialReference sr3D = sr;
                //创建三维多面体字段
                IFields pNewFields = FeatureModelHelper.CreateFields3D(pFields, sr3D, esriGeometryType.esriGeometryMultiPatch);
                IFeatureClass pToFeatureClass;
                if (AnalystHelper.IsExistFeatureClass(Layer3dName, pWorkspace))
                {
                    //pToFeatureClass = (pWorkspace as IFeatureWorkspace).OpenFeatureClass(Layer3dName);
                    MessageBox.Show($"{Layer3dName}图层已存在!");
                    return false;
                }
                else
                {
                    pToFeatureClass = (pWorkspace as IFeatureWorkspace).CreateFeatureClass(Layer3dName,
                        pNewFields, null, null, PointFeatureClass.FeatureType, "SHAPE", "");
                }

                //需要判断管点数据源是否存在附属物模型绘制所需字段
                int indexSubsid = strSubsid != null ? PointFeatureClass.FindField(strSubsid) : -1;//附属物

                //得到当前编辑空间
                IWorkspaceEdit tWorkspaceEdit = pWorkspace as IWorkspaceEdit;
                tWorkspaceEdit.StartEditing(false);
                tWorkspaceEdit.StartEditOperation();

                #region 生成附属物模型
                List<UniqueValueInfo> pUniqueValueInfos = new List<UniqueValueInfo>();

                //根据附属物唯一值来生成
                if (!string.IsNullOrWhiteSpace(strSubsid) && indexSubsid >= 0)
                {
                    trackProgress.SubValue = 0;
                    trackProgress.SubMessage = $"正在创建{Layer3dName}附属物模型";
                    Application.DoEvents();

                    bool IsBuildFsw = true;
                    Hashtable pHashTable = AnalystHelper.GetUniqueValue(pTable, strSubsid);
                    #region 判定附属物是否为空，为空则不生成附属物
                    foreach (string key in pHashTable.Keys)
                    {
                        if (!trackProgress.IsContinue) break;

                        if (String.IsNullOrWhiteSpace(key) || key == "空")
                        {
                            continue;
                        }
                        IQueryFilter queryFilter = new QueryFilterClass();
                        queryFilter.WhereClause = $"{strSubsid}='{key}'";
                        IsBuildFsw = FeatureModelHelper.CreateFSW(pWorkspace, Layer3dName, key, this,
                           pToFeatureClass, queryFilter, dictMarker3DSymbol, trackProgress);

                        if (!IsBuildFsw)
                        {
                            MessageBox.Show("附属物生成失败!");
                            break;
                        }

                    }
                    #endregion
                    if (!IsBuildFsw)
                    {
                        tWorkspaceEdit.StopEditOperation();
                        //结束编辑
                        tWorkspaceEdit.StopEditing(true);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("未设置附属物字段或附属物字段不存在于数据源中");
                    //return false;
                }
                #endregion

                #region 附属物为空的记录自动生成连接键
                if (!trackProgress.IsContinue)
                {
                    tWorkspaceEdit.StopEditOperation();
                    //结束编辑
                    tWorkspaceEdit.StopEditing(true);
                    return false;
                }
                trackProgress.SubValue = 0;
                trackProgress.SubMessage = $"正在自动创建{Layer3dName}特征点";
                Application.DoEvents();

                #region 这里特征点字段不是必须的,根据连接的管段数来生成特征点即可
                //if (!string.IsNullOrWhiteSpace(str_Feature) && indexFeature >= 0)
                //{
                //    //lstFeatureFields.Add(str_Feature);
                //    Hashtable pFeatureHashTable = AnalystHelper.GetUniqueValue(pTable, str_Feature);
                //    int subsidIndex = strSubsid != null?PointFeatureClass.Fields.FindField(strSubsid):-1;
                //    string strSub = "";
                //    if (subsidIndex > -1)
                //    {
                //        strSub = $"And ({strSubsid} is Null OR {strSubsid} ='')";
                //    }
                //    bool IsFail = false;
                //    //特征点
                //    foreach (string key in pFeatureHashTable.Keys)
                //    {
                //        if (!trackProgress.IsContinue) break;

                //        string strValue = "";
                //        string sql = "";
                //        if (String.IsNullOrWhiteSpace(key.Trim()) || key == "空")
                //        {
                //            strValue = "其他连接键";
                //            sql = $"({str_Feature} is Null OR {str_Feature} ='')" +strSub;
                //        }
                //        else
                //        {
                //            strValue = key;
                //            sql = $"{str_Feature}='{strValue.Trim()}'" + strSub;
                //        }

                //        IQueryFilter tQueryFilter = new QueryFilterClass();
                //        tQueryFilter.WhereClause = sql;
                //        if (FeatureModelHelper.FeatureDoubleConnect().Contains(strValue))
                //        {
                //            bool IsBuildWT=FeatureModelHelper.CreateModel_弯头(pWorkspace, Layer3dName, strValue, this, PointBigType,
                //                dictMarker3DSymbol, tQueryFilter, trackProgress);
                //            if (!IsBuildWT)
                //            {
                //                IsFail = true;
                //                break;
                //            }
                //        }
                //        else if (FeatureModelHelper.FeatureMoreConnect().Contains(strValue))
                //        {
                //            bool IsBuildTT = FeatureModelHelper.CreateModel_多通头(pWorkspace, Layer3dName, strValue, this, PointBigType,
                //                dictMarker3DSymbol, tQueryFilter, trackProgress);
                //            if (!IsBuildTT)
                //            {
                //                IsFail = true;
                //                break;
                //            }
                //        }
                //        else if (FeatureModelHelper.FeatureSingleConnect().Contains(strValue))
                //        {
                //            bool IsBuildYLK=FeatureModelHelper.CreateModel_预留口(pWorkspace, Layer3dName, strValue, this, PointBigType,
                //                dictMarker3DSymbol, tQueryFilter, trackProgress);
                //            if(!IsBuildYLK)
                //            {
                //                IsFail = true;
                //                break;
                //            }
                //        }
                //        else
                //        {
                //            bool IsBuildQT = FeatureModelHelper.CreateModel_其他模型(pWorkspace, Layer3dName, strValue, this, PointBigType,
                //                dictMarker3DSymbol, tQueryFilter, trackProgress);
                //            if (!IsBuildQT)
                //            {
                //                IsFail = true;
                //                break;
                //            }
                //        }

                //    }
                //    if(IsFail)
                //    {
                //        MessageBox.Show("特征点模型生成失败!");
                //        return false;
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("未设置特征点字段或特征点字段不存在于数据源中");
                //}
                #endregion
                bool IsFailed = false;
                string strSubEmpty = "";
                if (indexSubsid >= 0)
                {
                    strSubEmpty = $"{strSubsid} is Null OR {strSubsid} =''";
                }
                IQueryFilter tQueryFilter = new QueryFilterClass();
                tQueryFilter.WhereClause = strSubEmpty;

                bool IsBuildFeature = FeatureModelHelper.CreateFeature(pToFeatureClass, pWorkspace, this, PointBigType,
                    dictMarker3DSymbol, tQueryFilter, trackProgress);
                if (!IsBuildFeature)
                {
                    IsFailed = true;
                }

                tWorkspaceEdit.StopEditOperation();
                //结束编辑
                tWorkspaceEdit.StopEditing(true);

                #endregion
                if (!trackProgress.IsContinue) return false;

                if (AnalystHelper.IsExistFeatureClass(Layer3dName, pWorkspace))
                {
                    pToFeatureClass = (pWorkspace as IFeatureWorkspace).OpenFeatureClass(Layer3dName);
                    if (pToFeatureClass.FeatureCount(null) > 0)
                    {
                        Layer3D.Add(Layer3dName);
                    }
                    else
                    {
                        return IsSuccess;
                    }

                }
                IsSuccess = true;
                return IsSuccess;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ExceptionLog.LogError(ex.Message, ex);
                IsSuccess = false;
                return IsSuccess;
            }

        }

        /// <summary>
        /// 将某个小类中管点转换配置方案设置成默认的配置方案
        /// </summary>
        /// <param name="scheme"></param>
        public void Set(PointScheme scheme)
        {
            this.CalculationType = scheme.CalculationType;
            this.LineFields = scheme.LineFields;
            this.strE_Hight = scheme.strE_Hight;
            this.strE_Point = scheme.strE_Point;
            this.strPSize = scheme.strPSize;
            this.strS_Hight = scheme.strS_Hight;
            this.strS_Point = scheme.strS_Point;
            this.PointFields = scheme.PointFields;
            this.strPNo = scheme.strPNo;
            this.strSubsid = scheme.strSubsid;
            this.strSubsidSize = scheme.strSubsidSize != null ? scheme.strSubsidSize : "";
            this.strSubsidType = scheme.strSubsidType != null ? scheme.strSubsidType : "";
            this.strZ = scheme.strZ;
            //this.str_Feature = scheme.str_Feature;
            //this.strDeep = scheme.strDeep;
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
    /// 符号化信息
    /// </summary>
    [Serializable]
    public class UniqueValueInfo
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Sql { get; set; }
        public int Number { get; set; }
        public ISymbol Symbol { get; set; }
    }

    /// <summary>
    /// 方案配置下需要用到的路径
    /// </summary>
    public static class PublicPath
    {
        public static string SavePath
        {
            get;
            set;
        }
        public static string StyleFilePath
        {
            get;
            set;
        }
        public static string ProjectPath
        {
            get;
            set;
        }

    }


    [Serializable]
    public enum SubsidType
    {
        [Description("矩形")]
        矩形,
        [Description("圆形")]
        圆形
    }

}
