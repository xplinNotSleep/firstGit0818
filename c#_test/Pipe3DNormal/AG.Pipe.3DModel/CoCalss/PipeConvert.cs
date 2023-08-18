using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using AG.Pipe.Analyst3DModel.Editor;
using AG.COM.SDM.Utility.Common;
using System.Xml.Serialization;

namespace AG.Pipe.Analyst3DModel
{
    /// <summary>
    /// 小类图层集转换方案
    /// </summary>
    [Serializable]
    public class ConvertLayerSet : ISchemeValueChanged, ISchemeName
    {
        #region 配置属性的私有变量
        private ITable m_LineSource;
        private ITable m_PointSource;
        private string m_ItemName = "";
        private string m_PointItemName = "";
        private string m_LineItemName = "";
        private string m_PointStandardName="";
        private string m_LineStandardName="";
        private string m_ExpNoFieldName = "";
        private string m_HighFieldName = "";
        private string m_PointLayerType = "点";
        private string m_PointX = "";
        private string m_PointY = "";
        private List<PointInfo> m_Points = new List<PointInfo>();
        private string m_SPointFieldName = "";
        private string m_EPointFieldName = "";
        private string m_SHighFieldName = "";
        private string m_EHighFieldName = "";
        private string m_LineLayerType = "线";
        private List<PointInfo> m_LinePoints = new List<PointInfo>();

        #endregion

        /// <summary>
        /// 获取或设置图层
        /// </summary>
        [Category("常规"), Description("小类名"), DefaultValue("管线小类名")]
        public string Name
        {
            get
            {
                return m_ItemName;
            }
            set
            {
                m_ItemName = value;
                //this.m_LineItemName = m_ItemName + "管线";
                //this.m_PointItemName = m_ItemName + "管点";
            }
        }
        
        #region 管点表配置信息

        /// <summary>
        /// 管点名称
        /// </summary>
        [Category("管点表配置信息"), Description("0、管点名称"), DefaultValue(""), ReadOnly(false)]
        public string PointItemName
        {
            get
            {
                this.m_PointItemName = this.m_ItemName + "管点";
                return m_PointItemName;
            }
        }

        /// <summary>
        /// 管点表标准名称
        /// </summary>
        [Category("管点表配置信息"), Description("1、管点表标准名称"), DefaultValue(""), ReadOnly(false)]
        public string PointStandardName
        {
            get
            {
                return m_PointStandardName;
            }
            set
            {
                this.m_PointStandardName = value;
            }
        }

        /// <summary>
        /// 管点表名称
        /// </summary>
        [Category("管点表配置信息"), Description("2、管点表名称"), DefaultValue("<未设置>"), Browsable(true)]
        public string PointLayerName
        {
            get
            {
                if (this.m_PointSource == null)
                {
                    return "<未设置>";
                }
                else
                {
                    string SourceName = (m_PointSource as IDataset).BrowseName;
                    return SourceName;
                }
            }
            set
            {

            }
        }

        /// <summary>
        /// 获取或设置线源数据图层
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public ITable PointSource
        {
            get
            {
                return this.m_PointSource;
            }
            set
            {
                m_PointSource = value;
            }
        }

        /// <summary>
        /// 获取管线点编号字段名称
        /// </summary>
        [Category("管点表配置信息"), Description("3、管点编号字段名称"), DefaultValue("<未设置>"), Browsable(true), 
            EditorAttribute(typeof(ConvertPointFieldEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string ExpNoFieldName
        {
            get
            {
                return m_ExpNoFieldName;
            }
            set
            {
                m_ExpNoFieldName = value;
            }
        }
        
        /// <summary>
        /// 管点地面高程字段名称
        /// </summary>
        [Category("管点表配置信息"), Description("4、管点地面高程字段名称"), Browsable(true), 
            EditorAttribute(typeof(ConvertPointFieldEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string HighFieldName
        {
            get
            {
                return m_HighFieldName;
            }
            set
            {
                m_HighFieldName = value;
            }
        }
        
        /// <summary>
        /// 图层类型
        /// </summary>
        [Category("管点表配置信息"), Description("5、图层类型"), DefaultValue("图层类型"), 
            Browsable(true), EditorAttribute(typeof(LayerTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string PointLayerType
        {
            get
            {
                return m_PointLayerType;
            }
        }

        /// <summary>
        /// X坐标
        /// </summary>
        [Category("管点表配置信息"), Description("6、空间X坐标字段"), DefaultValue("X坐标"),
            Browsable(true), EditorAttribute(typeof(ConvertPointFieldEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
        public string PointX
        {
            get
            {
                return m_PointX;
            }
            set
            {
                this.m_PointX = value;
            }
        }

        /// <summary>
        /// Y坐标
        /// </summary>
        [Category("管点表配置信息"), Description("6、空间Y坐标字段"), DefaultValue("Y坐标"),
            Browsable(true), EditorAttribute(typeof(ConvertPointFieldEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
        public string PointY
        {
            get
            {
                return m_PointY;
            }
            set
            {
                this.m_PointY = value;
            }
        }

        #region
        /// <summary>
        /// 空间信息
        /// </summary>
        //[Category("管点表配置信息"), Description("6、空间信息"), DefaultValue("空间信息"), 
        //    Browsable(true), TypeConverter(typeof(SpatialInfoConverter)), EditorAttribute(typeof(SpatialInfoEditorPoint), 
        //    typeof(System.Drawing.Design.UITypeEditor))]
        //public List<PointInfo> Points
        //{
        //    get
        //    {
        //        return m_Points;
        //    }
        //    set
        //    {
        //        this.m_Points = value;
        //    }
        //}
        #endregion

        #endregion

        #region 管线表配置信息
        /// <summary>
        /// 管点名称
        /// </summary>
        [Category("管线表配置信息"), Description("0、管线名称"), DefaultValue(""), ReadOnly(false)]
        public string LineItemName
        {
            get
            {
                this.m_LineItemName = this.m_ItemName + "管线";
                return m_LineItemName;
            }
        }

        /// <summary>
        /// 获取管线表标准名称
        /// </summary>
        [Category("管线表配置信息"), Description("1、管线表标准名称"), DefaultValue(""), ReadOnly(false)]
        public string LineStandardName
        {
            get
            {
                return m_LineStandardName;
            }
            set
            {
                m_LineStandardName = value;
            }
        }


        /// <summary>
        /// 获取管线表名称
        /// </summary>
        [Category("管线表配置信息"), Description("2、管线表名称"), DefaultValue("<未设置>"), Browsable(true)]
        public string LineLayerName
        {
            get
            {
                if (this.m_LineSource == null)
                {
                    return "<未设置>";
                }
                else
                {
                    return (this.m_LineSource as IDataset).BrowseName;
                }
            }
            set
            {

            }
        }

        /// <summary>
        /// 获取或设置线源数据图层
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public ITable LineSource
        {
            get
            {
                return this.m_LineSource;
            }
            set
            {
                m_LineSource = value;
            }
        }

        /// <summary>
        /// 获取管线表名称
        /// </summary>
        [Category("管线表配置信息"), Description("3、起点编号字段名"), Browsable(true), 
            EditorAttribute(typeof(ConvertLineFieldEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string SPointFieldName
        {
            get
            {
                return m_SPointFieldName;
            }
            set
            {
                m_SPointFieldName = value;
            }
        }

        
        /// <summary>
        /// 获取管线表名称
        /// </summary>
        [Category("管线表配置信息"), Description("4、终点编号字段名"), Browsable(true), 
            EditorAttribute(typeof(ConvertLineFieldEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string EPointFieldName
        {
            get
            {
                return m_EPointFieldName;
            }
            set
            {
                m_EPointFieldName = value;
            }
        }

        
        /// <summary>
        /// 获取管线表名称
        /// </summary>
        [Category("管线表配置信息"), Description("5、起点高程字段名"), Browsable(true), 
            EditorAttribute(typeof(ConvertLineFieldEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string SHighFieldName
        {
            get
            {
                return m_SHighFieldName;
            }
            set
            {
                m_SHighFieldName = value;
            }
        }

        /// <summary>
        /// 获取管线表名称
        /// </summary>
        [Category("管线表配置信息"), Description("6、终点高程字段名"), Browsable(true), 
            EditorAttribute(typeof(ConvertLineFieldEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string EHighFieldName
        {
            get
            {
                return m_EHighFieldName;
            }
            set
            {
                m_EHighFieldName = value;
            }
        }
        
        /// <summary>
        /// 获取图层类型
        /// </summary>
        [Category("管线表配置信息"), Description("7、图层类型"), DefaultValue("图层类型"), Browsable(true), EditorAttribute(typeof(LayerTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string LineLayerType
        {
            get
            {
                return m_LineLayerType;
            }
        }

        #region
        //[Category("管线表配置信息"), Description("8、空间信息"), DefaultValue("空间信息"), Browsable(true), 
        //    TypeConverter(typeof(SpatialInfoConverter)), EditorAttribute(typeof(SpatialInfoEditorLine), 
        //    typeof(System.Drawing.Design.UITypeEditor))]
        //public List<PointInfo> LinePoints
        //{
        //    get
        //    {
        //        return m_LinePoints;
        //    }
        //    set
        //    {
        //        this.m_LinePoints = value;
        //    }
        //}
        #endregion

        #endregion

        #region 管点管线字段集
        [Browsable(false)]
        public List<string> LineFields { get; set; } = new List<string>();

        [Browsable(false)]
        public List<string> PointFields { get; set; } = new List<string>();

        #endregion

        /// <summary>
        /// 开始进行转换
        /// </summary>
        /// <param name="dataCheckScheme"></param>
        /// <returns></returns>
        public List<InputRecord> StartConvert(IWorkspace tWorkspace,PipeConvertManager convertManager,ITrackProgress trackProgress)
        {
            List<InputRecord> inputRecords = new List<InputRecord>();
            GeoDBHelper.ConvertRecord = "";
            try
            {
                //生成并写入管点管线图层的数据
                GeoDBHelper.StartConvert(this, convertManager, trackProgress, tWorkspace);

                //记录导入记录
                InputRecord inputRecord = new InputRecord();
                //inputRecord.TargetPath = convertManager.WorkSpace.PathName;
                inputRecord.TargetPath = convertManager.SaveDic;
                inputRecord.SourceLayerName = (this.m_PointSource as IDataset).Workspace.PathName + "\\" + (this.m_PointSource as IDataset).BrowseName;
                inputRecord.LayerName = this.PointItemName;
                inputRecord.IsSuceed = true;
                inputRecord.Detail = GeoDBHelper.ConvertPointRecord;

                InputRecord inputRecord1 = new InputRecord();
                inputRecord1.TargetPath = convertManager.SaveDic;
                inputRecord1.SourceLayerName = (this.m_LineSource as IDataset).Workspace.PathName + "\\" + (this.m_LineSource as IDataset).BrowseName;
                inputRecord1.LayerName = this.LineItemName;
                inputRecord1.IsSuceed = true;
                inputRecord1.Detail = GeoDBHelper.ConvertLineRecord;

                inputRecords.Add(inputRecord);
                inputRecords.Add(inputRecord1);
                return inputRecords;
            }
            catch (Exception ex)
            {
                InputRecord inputRecord = new InputRecord();
                //InputRecord.TargetPath = convertManager.WorkSpace.PathName;
                inputRecord.TargetPath = convertManager.SaveDic;
                inputRecord.SourceLayerName = (this.m_PointSource as IDataset).Workspace.PathName + "\\" + (this.m_PointSource as IDataset).BrowseName;
                inputRecord.LayerName = this.PointItemName;
                inputRecord.IsSuceed = false;
                inputRecord.Detail = GeoDBHelper.ConvertPointRecord + ex.Message;

                InputRecord errRecord1 = new InputRecord();
                //errRecord1.TargetPath = convertManager.WorkSpace.PathName;
                errRecord1.TargetPath = convertManager.SaveDic;
                errRecord1.SourceLayerName = (this.m_LineSource as IDataset).Workspace.PathName + "\\" + (this.m_LineSource as IDataset).BrowseName;
                errRecord1.LayerName = this.LineItemName;
                errRecord1.IsSuceed = false;
                errRecord1.Detail = GeoDBHelper.ConvertLineRecord + ex.Message;

                inputRecords.Add(inputRecord);
                inputRecords.Add(errRecord1);
                return inputRecords;
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

    public class PointInfo
    {
        public string x = "";
        public string y = "";
        public string z = "";
    }

    public class LinePointInfo
    {
        public string x = "";
        public string y = "";
        public string z = "";
    }
}
