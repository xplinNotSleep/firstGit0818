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
using System.Xml.Serialization;

namespace AG.Pipe.Analyst3DModel
{

    /// <summary>
    /// 管线图形化方案管理
    /// </summary>
    [Serializable]
    public class PipeConvertManager: ISchemeValueChanged, ISchemeName
    {
        /// <summary>
        /// 一般构造函数
        /// </summary>
        public PipeConvertManager()
        {
            this.Name = "管线小类名";
        }


        #region 空间参考坐标系相关变量
        private string m_CoordinateSystem = string.Empty;
        private string m_XYDomain = string.Empty;
        private string m_ZDomain = string.Empty;
        private string m_MDomain = string.Empty;
        [Browsable(false)]
        public string CoordinateSystem
        {
            get => m_CoordinateSystem;
            set => m_CoordinateSystem = value;
        }
        [Browsable(false)]
        public string XYDomain
        {
            get => m_XYDomain;
            set => m_XYDomain = value;
        }
        [Browsable(false)]
        public string ZDomain
        {
            get => m_ZDomain;
            set => m_ZDomain = value;
        }
        [Browsable(false)]
        public string MDomain
        {
            get => m_MDomain;
            set => m_MDomain = value;
        }
        #endregion

        #region 界面配置内容

        private const string Category1 = "常规";

        /// <summary>
        /// 方案名称
        /// </summary>
        [Category(Category1), DisplayName("方案名称"), DefaultValue(""), PropertyOrder(1)]
        public string Name { get; set; } = "方案名称";

        #region 工作空间
        //private IWorkspace m_WorkSpace;
        //[Category("保存路径"), Description("保存路径"), DefaultValue("保存路径"), Browsable(false),
        //    TypeConverter(typeof(PipeConvertSchemeConverter)), EditorAttribute(typeof(SavePathEditor),
        //    typeof(System.Drawing.Design.UITypeEditor))]
        //public IWorkspace WorkSpace
        //{
        //    get
        //    {
        //        return this.m_WorkSpace;
        //    }
        //    set
        //    {
        //        m_WorkSpace = value;
        //    }
        //}
        #endregion

        private string saveDic { get; set; } = $"D:\\";
        /// <summary>
        /// 保存路径
        /// </summary>
        [Category(Category1), DisplayName("保存路径"), DefaultValue("保存路径"), Browsable(true)
            , EditorAttribute(typeof(SavePathEditor),typeof(System.Drawing.Design.UITypeEditor))]
            
        
        public string SaveDic
        {
            get => saveDic;
            set => saveDic = value;
        }

        private ISpatialReference m_SpatialReference = new UnknownCoordinateSystemClass();

        [Category("空间参考"), Description("空间参考"), DefaultValue("Unknown"), Browsable(true)]
        public string SpatialRefenceName
        {
            get
            {
                return this.m_SpatialReference.Name;

                
            }
        }

        [XmlIgnore]
        [Category("空间参考"), Description("空间参考"), DefaultValue("Unknown"), Browsable(true),
        TypeConverter(typeof(SpatialReferenceConverter)), EditorAttribute(typeof(SpatialReferenceEditor), 
            typeof(System.Drawing.Design.UITypeEditor))]
        public ISpatialReference SpatialReference
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(CoordinateSystem) && !string.IsNullOrWhiteSpace(XYDomain) && !string.IsNullOrWhiteSpace(ZDomain) && !string.IsNullOrWhiteSpace(MDomain))
                {
                    CreateESRISpatialReference(CoordinateSystem, out this.m_SpatialReference);
                    SetXYDomain(XYDomain, ref this.m_SpatialReference);
                    SetZDomain(ZDomain, ref this.m_SpatialReference);
                    SetMDomain(MDomain, ref this.m_SpatialReference);
                }
                return this.m_SpatialReference;
            }
            set
            {
                m_SpatialReference = value;
                if (m_SpatialReference != null)
                {
                    this.CoordinateSystem = CoordinateSystemToString(m_SpatialReference);
                    this.XYDomain = XYDomainToString(m_SpatialReference);
                    this.ZDomain = ZDomainToString(m_SpatialReference);
                    this.MDomain = MDomainToString(m_SpatialReference);
                }
            }
        }

        [Browsable(false)]
        public List<PipeConvertGroup> PipeConvertGroups { get; set; } = new List<PipeConvertGroup>();

        #endregion

        #region 读写空间参考 私有处理方法
        /// <summary>
        /// 将指定的空间参考坐标系转换为字符串
        /// </summary>
        /// <param name="pSpatialReferenc">指定的空间参考</param>
        private string CoordinateSystemToString(ISpatialReference pSpatialReference)
        {
            string strSR = "";
            int cBytesWrote;

            //此处一定要引用IESRISpatialReferenceGEN接口,
            //否则引用IESRISpatialReference此接口将产生不可预测错误
            //查询引用接口
            IESRISpatialReferenceGEN tESRISpatialReference = pSpatialReference as IESRISpatialReferenceGEN;
            //输出空间参考信息
            tESRISpatialReference.ExportToESRISpatialReference(out strSR, out cBytesWrote);

            return strSR;
        }

        /// <summary>
        /// 将指定的空间参考XY域转换为字符串
        /// </summary>
        /// <param name="pSpatialReferenc">指定的空间参考</param>
        private string XYDomainToString(ISpatialReference pSpatialReference)
        {
            if (pSpatialReference.HasXYPrecision() == true)
            {
                double xMin, xMax, yMin, yMax, xyResolution;
                //得到XY方向的取值范围
                pSpatialReference.GetDomain(out xMin, out xMax, out yMin, out yMax);
                //查询ISpatialReferenceResolution接口
                ISpatialReferenceResolution tSRResolution = pSpatialReference as ISpatialReferenceResolution;
                //得到Z轴方向的分辨率
                xyResolution = tSRResolution.get_XYResolution(false);

                return string.Format("{0},{1},{2},{3},{4}", xMin, xMax, yMin, yMax, xyResolution);
            }
            else
                return "";
        }

        /// <summary>
        /// 将指定的空间参考Z域转换为字符串
        /// </summary>
        /// <param name="pSpatialReferenc">指定的空间参考</param>
        private string ZDomainToString(ISpatialReference pSpatialReference)
        {
            if (pSpatialReference.HasZPrecision() == true)
            {
                double zMin, zMax, zResolution;
                //得到Z值范围
                pSpatialReference.GetZDomain(out zMin, out zMax);
                //查询ISpatialReferenceResolution接口
                ISpatialReferenceResolution tSRResolution = pSpatialReference as ISpatialReferenceResolution;
                //得到Z轴方向的分辨率
                zResolution = tSRResolution.get_ZResolution(false);

                return string.Format("{0},{1},{2}", zMin, zMax, zResolution);
            }
            else
                return "";
        }

        /// <summary>
        /// 将指定的空间参考M域转换为字符串
        /// </summary>
        /// <param name="pSpatialReferenc">指定的空间参考</param>
        private string MDomainToString(ISpatialReference pSpatialReference)
        {
            if (pSpatialReference.HasMPrecision() == true)
            {
                double MMin, MMax, MResolution;
                //得取M值范围
                pSpatialReference.GetMDomain(out MMin, out MMax);
                //查询ISpatialReferenceResolution接口
                ISpatialReferenceResolution tSRResolution = pSpatialReference as ISpatialReferenceResolution;
                //得到M方向的分辨率
                MResolution = tSRResolution.MResolution;

                return string.Format("{0},{1},{2}", MMin, MMax, MResolution);
            }
            else
                return "";
        }

        /// <summary>
        /// 从指定坐标信息创建空间参考
        /// </summary>
        /// <param name="pSpaBuffer">坐标信息</param>
        /// <param name="pSpatialReference">空间参考</param>
        private void CreateESRISpatialReference(string pSpaBuffer, out ISpatialReference pSpatialReference)
        {
            if (pSpaBuffer.Trim().Length == 0)
            {
                pSpatialReference = new UnknownCoordinateSystemClass();
            }
            else
            {
                int cBytesRead;
                //实例空间参考环境类
                ISpatialReferenceFactory2 spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                spatialReferenceFactory.CreateESRISpatialReference(pSpaBuffer, out pSpatialReference, out cBytesRead);
            }
        }

        /// <summary>
        /// 从指定XY域信息设置XY域
        /// </summary>
        /// <param name="pXYDomain">XY域信息</param>
        /// <param name="pSpatialReference">空间参考</param>
        private void SetXYDomain(string pXYDomain, ref ISpatialReference pSpatialReference)
        {
            if (pXYDomain.Trim().Length > 0 && (pXYDomain.Split(',').Length == 5))
            {
                double xMin, xMax, yMin, yMax, xyResolution;

                string[] strXYDomain = pXYDomain.Split(',');
                xMin = Convert.ToDouble(strXYDomain[0]);
                xMax = Convert.ToDouble(strXYDomain[1]);
                yMin = Convert.ToDouble(strXYDomain[2]);
                yMax = Convert.ToDouble(strXYDomain[3]);
                xyResolution = Convert.ToDouble(strXYDomain[4]);

                pSpatialReference.SetDomain(xMin, xMax, yMin, yMax);
                //查询ISpatialReferenceResolution接口
                ISpatialReferenceResolution tSRResolution = pSpatialReference as ISpatialReferenceResolution;
                //设置分辨率
                tSRResolution.set_XYResolution(false, xyResolution);
            }
        }

        /// <summary>
        /// 从指定M域信息设置空间参考M域
        /// </summary>
        /// <param name="pMDomain">M域信息</param>
        /// <param name="pSpatialReference">空间参考</param>
        private void SetMDomain(string pMDomain, ref ISpatialReference pSpatialReference)
        {
            if (pMDomain.Trim().Length > 0 && (pMDomain.Split(',').Length == 3))
            {
                double MMin, MMax, MResolution;
                string[] strMDomain = pMDomain.Split(',');
                MMin = Convert.ToDouble(strMDomain[0]);
                MMax = Convert.ToDouble(strMDomain[1]);
                MResolution = Convert.ToDouble(strMDomain[2]);

                //设置M域最大、最小值
                pSpatialReference.SetMDomain(MMin, MMax);
                //查询ISpatialReferenceResolution接口
                ISpatialReferenceResolution tSRResolution = pSpatialReference as ISpatialReferenceResolution;
                //设置分辨率
                tSRResolution.MResolution = MResolution;
            }
        }

        /// <summary>
        /// 从指定Z域信息设置空间参考Z域
        /// </summary>
        /// <param name="pZDomain">Z域信息</param>
        /// <param name="pSpatialReference">空间参考</param>
        private void SetZDomain(string pZDomain, ref ISpatialReference pSpatialReference)
        {
            if (pZDomain.Trim().Length > 0 && (pZDomain.Split(',').Length == 3))
            {
                double zMin, zMax, zResolution;

                string[] strZDomain = pZDomain.Split(',');
                zMin = Convert.ToDouble(strZDomain[0]);
                zMax = Convert.ToDouble(strZDomain[1]);
                zResolution = Convert.ToDouble(strZDomain[2]);

                pSpatialReference.SetZDomain(zMin, zMax);
                //查询ISpatialReferenceResolution接口
                ISpatialReferenceResolution tSRResolution = pSpatialReference as ISpatialReferenceResolution;
                //设置分辨率
                tSRResolution.set_ZResolution(false, zResolution);
            }
        }
        #endregion

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
