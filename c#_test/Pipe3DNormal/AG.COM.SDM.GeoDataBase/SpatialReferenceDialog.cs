using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Catalog;
using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.Catalog.Filters;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 空间参考 窗体类
    /// </summary>
    public partial class SpatialReferenceDialog:Form 
    {
        private ISpatialReference m_SpatialReference;
        private EnumDataSRType m_DataSRType = EnumDataSRType.DatasetSR;  

        /// <summary>
        /// 获取或设置当前操作数据的类型
        /// </summary>
        public EnumDataSRType DataSRType
        {
            get
            {
                return this.m_DataSRType;
            }
            set
            {
                this.m_DataSRType = value;
            }
        }

        /// <summary>
        /// 获取或设置空间参考关系
        /// </summary>
        public ISpatialReference SpatialReference
        {
            get
            {
                return this.m_SpatialReference;
            }
            set
            {
                this.m_SpatialReference = value;
            }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SpatialReferenceDialog()
        {
            //初始化界面组件
            InitializeComponent();
        }

        #region 页面控件事件
        private void SpatialReferenceDialog_Load(object sender, EventArgs e)
        {
            //设置属性页的显示情况
            if (this.m_DataSRType == EnumDataSRType.FeatureClassSR)
            {
                this.tabControl1.TabPages.Clear();
                this.tabControl1.TabPages.Add(this.tabPage1);
                this.tabControl1.TabPages.Add(this.tabPage2);
            }
            else if (this.m_DataSRType == EnumDataSRType.ShapeSR)
            {
                this.tabControl1.TabPages.Clear();
                this.tabControl1.TabPages.Add(this.tabPage1);
            }

            if (this.m_SpatialReference == null)
            {
                this.m_SpatialReference = new UnknownCoordinateSystemClass();
                //设置默认分辨率
                SetDefaultResolution();
            }

            if (m_SpatialReference.Name == "Unknown")
            {
                this.btnClear.Enabled = false;
            }
            
            //初始化界面组件的Text
            InitialControlText();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.m_DataSRType != EnumDataSRType.ShapeSR)
                {
                    //查询ISpatialReferenceResolution接口
                    ISpatialReferenceResolution tSRResolution = this.m_SpatialReference as ISpatialReferenceResolution;
                    //获取XY域各个分量值
                    double xMin, xMax, yMin, yMax,xyResolution;
                    xMin = Convert.ToDouble(this.txtXMin.Text);
                    xMax = Convert.ToDouble(this.txtXMax.Text);
                    yMin = Convert.ToDouble(this.txtYMin.Text);
                    yMax = Convert.ToDouble(this.txtYMax.Text);
                    xyResolution = Convert.ToDouble(this.txtXPrecision.Text);
                    //设置XY域
                    this.m_SpatialReference.SetDomain(xMin, xMax, yMin, yMax);
                    //设置XY域分辨率
                    tSRResolution.set_XYResolution(false, xyResolution);

                    if (this.m_DataSRType == EnumDataSRType.DatasetSR)
                    {
                        //获取M域的值
                        double mMin, mMax, mResolution;
                        mMin = Convert.ToDouble(this.txtMMin.Text);
                        mMax = Convert.ToDouble(this.txtMMax.Text);
                        mResolution = Convert.ToDouble(this.txtMPrecision.Text);
                        //设置M域
                        this.m_SpatialReference.SetMDomain(mMin, mMax);
                        //设置M域分辨率
                        tSRResolution.MResolution = mResolution;                 

                        //设置Z域
                        double zMin, zMax, zResolution;
                        zMin = Convert.ToDouble(this.txtZMin.Text);
                        zMax = Convert.ToDouble(this.txtZMax.Text);
                        zResolution = Convert.ToDouble(this.txtZPrecision.Text);
                        //设置Z域
                        this.m_SpatialReference.SetZDomain(zMin, zMax);
                        //设置Z域分辨率
                        tSRResolution.set_ZResolution(false,zResolution);
                    }
                }

                this.btnApply.Enabled = false;               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }      
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnApply_Click(sender, e);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.m_SpatialReference = new UnknownCoordinateSystemClass();
            this.btnClear.Enabled = false;
            this.btnSaveAs.Enabled = false;

            //设置默认分辨率
            SetDefaultResolution();
            //初始化界面组件的Text
            InitialControlText();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog tOpenFileDlg = new OpenFileDialog();
            tOpenFileDlg.Title = "选择坐标文件";
            tOpenFileDlg.Filter = "坐标文件(*.prj)|*.prj";
            tOpenFileDlg.InitialDirectory =AG.COM.SDM.Utility.CommonConstString.STR_PreAppPath;
            tOpenFileDlg.RestoreDirectory = true;

            if (tOpenFileDlg.ShowDialog() == DialogResult.OK)
            {
                ISpatialReferenceFactory tSpatialFactory = new SpatialReferenceEnvironmentClass();
                m_SpatialReference = tSpatialFactory.CreateESRISpatialReferenceFromPRJFile(tOpenFileDlg.FileName);
                this.btnSaveAs.Enabled = true;
            }
            //设置默认分辨率
            SetDefaultResolution();
            //初始化界面组件的Text
            InitialControlText();
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog tSaveFileDlg = new SaveFileDialog();
            tSaveFileDlg.Title = "保存坐标信息";
            tSaveFileDlg.Filter = "坐标文件(*.prj)|*.prj";
            tSaveFileDlg.RestoreDirectory = true;

            if (tSaveFileDlg.ShowDialog() == DialogResult.OK)
            {
                ISpatialReferenceFactory tSpatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                tSpatialReferenceFactory.ExportESRISpatialReferenceToPRJFile(tSaveFileDlg.FileName, this.m_SpatialReference);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                //实例化数据浏览窗体实例
                AG.COM.SDM.Catalog.IDataBrowser tIDataBrowser = new FormDataBrowser();
                //不可多选
                tIDataBrowser.MultiSelect = false;
                //添加过滤器
                tIDataBrowser.AddFilter(new FeatureClassFilter());   

                if (tIDataBrowser.ShowDialog() == DialogResult.OK)
                {
                    //设置鼠标为忙碌状态
                    this.Cursor = Cursors.WaitCursor;

                    IList<DataItem> items = tIDataBrowser.SelectedItems;
                    if (items.Count > 0)
                    {
                        //查询接口引用
                        IGeoDataset tGeoDataset = items[0].GetGeoObject() as IGeoDataset;
                        if (tGeoDataset != null)
                        {
                            this.m_SpatialReference = tGeoDataset.SpatialReference;
                            //初始化界面组件
                            InitialControlText();
                        }
                    }

                    //鼠标恢复正常状态
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 自定义私有方法

        /// <summary>
        /// 初始化界面组件的Text
        /// </summary>
        private void InitialControlText()
        {
            //查询ISpatialReferenceResolution接口
            ISpatialReferenceResolution tSRResolution = this.m_SpatialReference as ISpatialReferenceResolution;

            #region 初始化XY域
            if (this.m_SpatialReference.HasXYPrecision() == true)
            {
                double xMin, xMax, yMin, yMax;
                this.m_SpatialReference.GetDomain(out xMin, out xMax, out yMin, out yMax);
                this.txtXMin.Text = Convert.ToString(xMin);
                this.txtXMax.Text = Convert.ToString(xMax);
                this.txtYMin.Text = Convert.ToString(yMin);
                this.txtYMax.Text = Convert.ToString(yMax);
                this.txtXPrecision.Text = Convert.ToString(tSRResolution.get_XYResolution(false));
            }
            #endregion

            #region 初始M域
            if (this.m_SpatialReference.HasMPrecision() == true)
            {
                double MMin, MMax;
                this.m_SpatialReference.GetMDomain(out MMin, out MMax);
                this.txtMMin.Text = Convert.ToString(MMin);
                this.txtMMax.Text = Convert.ToString(MMax);
                this.txtMPrecision.Text = Convert.ToString(tSRResolution.MResolution);
            }
            #endregion

            #region 初始化Z域
            if (this.m_SpatialReference.HasZPrecision() == true)
            {
                double zMin, zMax;
                this.m_SpatialReference.GetZDomain(out zMin, out zMax);
                this.txtZMin.Text = Convert.ToString(zMin);
                this.txtZMax.Text = Convert.ToString(zMax);
                this.txtZPrecision.Text = Convert.ToString(tSRResolution.get_ZResolution(false));
            }
            #endregion

            this.txtName.Text = m_SpatialReference.Name;
            this.txtDetailInfo.Text = GetSpatialRefrenceDetail();
        }

        /// <summary>
        /// 设置默认分辨率
        /// </summary>
        private void SetDefaultResolution()
        {
            //查询IControlPrecision2接口
            IControlPrecision2 tControlPrecision = this.m_SpatialReference as IControlPrecision2;
            //设置其精度为低精度
            tControlPrecision.IsHighPrecision = true;// false;

            //查询ISpatialReferenceResolution接口
            ISpatialReferenceResolution tSpatialReferenceResolution = this.m_SpatialReference as ISpatialReferenceResolution;
            tSpatialReferenceResolution.ConstructFromHorizon();
            //设置XY分辨率
            tSpatialReferenceResolution.SetDefaultXYResolution();
            //设置M分辨率
            tSpatialReferenceResolution.SetDefaultMResolution();
            //设置Z分辨率
            tSpatialReferenceResolution.SetDefaultZResolution();
        }

        /// <summary>
        /// 获取空间参考的详细信息
        /// </summary>
        /// <param name="pSpatialReference">空间参考</param>
        /// <returns>返回空间参考详细信息</returns>
        private string GetSpatialRefrenceDetail()
        {
            StringBuilder strSRDetail = new StringBuilder();
            IGeographicCoordinateSystem tGeoCoordSystem = m_SpatialReference as IGeographicCoordinateSystem;

            try
            {
                if (m_SpatialReference is IUnknownCoordinateSystem)
                {
                    //未知坐标系的情况
                    strSRDetail.Append("未知坐标系统:");
                }
                else if (m_SpatialReference is IProjectedCoordinateSystem)
                {
                    //查询IProjectedCoordinateSystem接口
                    IProjectedCoordinateSystem tProjectCoordSystem = this.m_SpatialReference as IProjectedCoordinateSystem;

                    //返回相关信息
                    strSRDetail.Append("投影坐标系统:\r\n");
                    strSRDetail.Append(string.Format("名称:{0}\r\n", tProjectCoordSystem.Name));
                    strSRDetail.Append(string.Format("别名:{0}\r\n", tProjectCoordSystem.Alias));
                    strSRDetail.Append(string.Format("简称:{0}\r\n", tProjectCoordSystem.Abbreviation));
                    strSRDetail.Append(string.Format("投影:{0}\r\n", tProjectCoordSystem.Projection.Name));
                    strSRDetail.Append(string.Format("注释:{0}\r\n", tProjectCoordSystem.Remarks));
                    strSRDetail.Append("参数\r\n");


                    //得到投影坐标系统的参数信息
                    //如果传入null会报错，必须实例化数组
                    //数组大小通过esriSRLimitsEnum.esriSR_MaxParameterCount获取
                    IParameter[] tParameters = new ParameterClass[(int)esriSRLimitsEnum.esriSR_MaxParameterCount];
                    //IParameter[] tParameters = null;
                    //得到它的泛型接口
                    IProjectedCoordinateSystem4GEN tProjectSystemGen = tProjectCoordSystem as IProjectedCoordinateSystem4GEN;
                    tProjectSystemGen.GetParameters(ref tParameters);
                    
                    //追回每一参数信息
                    foreach (IParameter tParam in tParameters)
                    {
                        if (tParam == null) break;
                        strSRDetail.Append(string.Format("   {0}: {1}\r\n", tParam.Name, tParam.Value));
                    }

                    strSRDetail.Append(string.Format("线性单位:{0}({1})\r\n\r\n", tProjectCoordSystem.CoordinateUnit.Name, tProjectCoordSystem.CoordinateUnit.MetersPerUnit));

                    //得到地理坐标信息
                    tGeoCoordSystem = tProjectCoordSystem.GeographicCoordinateSystem;
                }

                if (tGeoCoordSystem != null)
                {
                    //追加地理信息参数
                    strSRDetail.Append("地理坐标系统:\r\n");
                    strSRDetail.Append(string.Format("名称:{0}\r\n", tGeoCoordSystem.Name));
                    strSRDetail.Append(string.Format("别名:{0}\r\n", tGeoCoordSystem.Alias));
                    strSRDetail.Append(string.Format("简称:{0}\r\n", tGeoCoordSystem.Abbreviation));
                    strSRDetail.Append(string.Format("注释:{0}\r\n", tGeoCoordSystem.Remarks));
                    strSRDetail.Append(string.Format("角单位:{0}({1})\r\n", tGeoCoordSystem.CoordinateUnit.Name, tGeoCoordSystem.CoordinateUnit.RadiansPerUnit));
                    strSRDetail.Append(string.Format("本初子午线:{0}({1})\r\n", tGeoCoordSystem.PrimeMeridian.Name, tGeoCoordSystem.PrimeMeridian.Longitude));
                    strSRDetail.Append(string.Format("基准面:{0}\r\n", tGeoCoordSystem.Datum.Name));
                    strSRDetail.Append(string.Format("  椭球体:{0}\r\n", tGeoCoordSystem.Datum.Spheroid.Name));
                    strSRDetail.Append(string.Format("\t长半轴:{0}\r\n", tGeoCoordSystem.Datum.Spheroid.SemiMajorAxis));
                    strSRDetail.Append(string.Format("\t短半轴:{0}\r\n", tGeoCoordSystem.Datum.Spheroid.SemiMinorAxis));
                    strSRDetail.Append(string.Format("\t偏心率:{0}\r\n", tGeoCoordSystem.Datum.Spheroid.Flattening));
                }

                return strSRDetail.ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 检测输入的字符是否符合要求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidateNumberChars(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            try
            {
                double AAA = Convert.ToDouble(textBox.Text);
                textBox.Tag = textBox.Text;
            }
            catch
            {
                MessageBox.Show("请输入一个有效的数字");
                textBox.Text = (string)textBox.Tag;
            }
        }

        private void BtnApplyChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (m_SpatialReference.Name != "Unknown")
            {
                this.btnClear.Enabled = true;
            }
        }       
        #endregion 

    }
}