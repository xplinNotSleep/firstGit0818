using System;
using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DMetaData
{
    /// <summary>
    /// 标识信息元数据
    /// </summary>
    public class MetaIdentifyInfo:MetaDataInfo
    {       
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MetaIdentifyInfo()
        {
            this.m_metaTableType = EnumMetaTableType.标识信息;
        }

        private string m_resTitle;
        /// <summary>
        /// 获取或设置数据集名称
        /// </summary>
        [Category("基本信息"), Description("数据集名称")]
        public string ResTitle
        {
            get
            {
                return this.m_resTitle;
            }
            set
            {
                this.m_resTitle = value;
            }
        }

        // 数据集发布或最近更新日期
        private DateTime m_resRefDate=DateTime.Now;
        /// <summary>
        /// 获取或设置数据集发布或最近更新日期
        /// </summary>
        [Category("基本信息"), Description("日期")]
        public DateTime ResRefDate
        {
            get
            {
                return this.m_resRefDate;
            }
            set
            {
                this.m_resRefDate = value;
            }
        }

        //数据集版本
        private string m_resEdition;
        /// <summary>
        /// 获取或设置数据集版本
        /// </summary>
        [Category("基本信息"), Description("数据集版本")]
        public string ResEd
        {
            get
            {
                return this.m_resEdition;
            }
            set
            {
                this.m_resEdition = value;
            }
        }

        private string m_datLangcode="zh";
        /// <summary>
        /// 获取或设置语种
        /// </summary>
        [Category("基本信息"), Description("语种")]
        public string DatLangCode
        {
            get
            {
                return this.m_datLangcode;
            }
            set
            {
                this.m_datLangcode = value;
            }
        }

        private string m_idAbstract;
        /// <summary>
        /// 获取或设置摘要
        /// </summary>
        [Category("基本信息"), Description("摘要")]
        public string IDAbs
        {
            get
            {
                return this.m_idAbstract;
            }
            set
            {
                this.m_idAbstract = value;
            }
        }

        private string m_idStstCode;
        /// <summary>
        /// 获取或设置现状
        /// </summary>
        [Category("基本信息"), Description("现状")]
        public string IDStstCode
        {
            get
            {
                return this.m_idStstCode;
            }
            set
            {
                this.m_idStstCode = value;
            }
        }

        private EnumMetaDataClassifyType m_classifyCode;
        /// <summary>
        /// 获取或设置土地利用分类
        /// </summary>
        [Category("基本信息"), Description("土地利用分类")]
        public EnumMetaDataClassifyType ClassfiyCode
        {
            get
            {
                return this.m_classifyCode;
            }
            set
            {
                this.m_classifyCode = value;
            }
        }

        private double m_westBL;
        /// <summary>
        /// 获取或设置西边经度
        /// </summary>
        [Category("地理范围"),Description("西边经度")]
        public double WestBL
        {
            get
            {
                return this.m_westBL;
            }
            set
            {
                if (value > 180 || value < -180)
                    throw new Exception("值不在有效范围内,请输入有效的经度范围");
                else 
                    this.m_westBL = value;
            }
        }

        private double m_eastBL;
        /// <summary>
        /// 获取或设置东边经度
        /// </summary>
        [Category("地理范围"),Description("东边经度")]
        public double EastBL
        {
            get
            {
                return this.m_eastBL;
            }
            set
            {
                if (value > 180 || value < -180)
                    throw new Exception("值不在有效范围内,请输入有效的经度范围");
                else
                    this.m_eastBL = value;
            }
        }

        private double m_northBL;
        /// <summary>
        /// 获取或设置北边纬度
        /// </summary>
        [Category("地理范围"),Description("北边纬度")]
        public double NorthBL
        {
            get
            {
                return this.m_northBL;
            }
            set
            {
                if (value > 90 || value < -90)
                    throw new Exception("值不在有效范围内,请输入有效的经度范围");
                else
                    this.m_northBL = value;
            }
        }

        private double m_southBL;
        /// <summary>
        /// 获取或设置南边纬度
        /// </summary>
        [Category("地理范围"),Description("南边纬度")]
        public double SourthBL
        {
            get
            {
                return this.m_southBL;
            }
            set
            {
                if (value > 90 || value < -90)
                    throw new Exception("值不在有效范围内,请输入有效的经度范围");
                else
                    this.m_southBL = value;
            }
        }

        private string m_geoID;
        /// <summary>
        /// 获取或设置地理标识符
        /// </summary>
        [Description("地理标识符")]
        public string GeoID
        {
            get
            {
                return this.m_geoID;
            }
            set
            {
                this.m_geoID = value;
            }
        }

        private DateTime m_endTime=DateTime.Now;
        /// <summary>
        /// 获取或设置终止时间
        /// </summary>
        [Category("数据集"), Description("终止时间")]
        public DateTime End
        {
            get
            {
                return this.m_endTime;
            }
            set
            {
                this.m_endTime = value;
            }
        }

        private EnumMetaDisplayType m_rpType;
        /// <summary>
        /// 获取或设置表示方式
        /// </summary>
        [Category("数据集"), Description("表示方式")]
        public EnumMetaDisplayType RPType
        {
            get
            {
                return this.m_rpType;
            }
            set
            {
                this.m_rpType = value;
            }
        }

        private string m_spatRes;
        /// <summary>
        /// 获取或设置空间分辨率
        /// </summary>
        [Category("数据集"), Description("空间分辨率")]
        public string SpatRes
        {
            get
            {
                return this.m_spatRes;
            }
            set
            {
                this.m_spatRes = value;
            }
        }

        private EnumMetaDataCategoryType m_tpCatCode;
        /// <summary>
        /// 获取或设置数据集专业或专题内容类别
        /// </summary>
        [Category("数据集"), Description("内容类别")]
        public EnumMetaDataCategoryType TPCatCode
        {
            get
            {
                return this.m_tpCatCode;
            }
            set
            {
                this.m_tpCatCode = value;
            }
        }

        private string m_deptName;
        /// <summary>
        /// 获取或设置数据集负责单位名称
        /// </summary>
        [Category("联系信息"),Description("单位名称")]
        public string DepartName
        {
            get
            {
                return this.m_deptName;
            }
            set
            {
                this.m_deptName = value;
            }
        }

        //联系人名称
        private string m_relPersionName;
        /// <summary>
        /// 获取或设置联系人名称
        /// </summary>
        [Category("联系信息"), Description("联系人名称")]
        public string RelationName
        {
            get 
            { 
                return this.m_relPersionName; 
            }
            set 
            { 
                this.m_relPersionName = value; 
            }
        }

        //联系人电话
        private string m_relPersionTel;
        /// <summary>
        ///  获取或设置数据集负责单位或联系人电话
        /// </summary>
        [Category("联系信息"), Description("联系人电话")]
        public string RelationPersionTel
        {
            get 
            { 
                return this.m_relPersionTel; 
            }
            set 
            { 
                this.m_relPersionTel = value;
            }            
        }

        private string m_relPersionFax;
        /// <summary>
        /// 获取或设置数据集负责单位或联系人的传真号码
        /// </summary>
        [Category("联系信息"), Description("传真号码")]
        public string RelationPersionFax
        {
            get
            {
                return this.m_relPersionFax;
            }
            set
            {
                this.m_relPersionFax = value;
            }
        }

        private string m_relDepartAddress;
        /// <summary>
        /// 获取或设置数据集负责单位或联系人的通信地址
        /// </summary>
        [Category("联系信息"), Description("通信地址")]
        public string RelationDepartAddress
        {
            get 
            { 
                return this.m_relDepartAddress; 
            }
            set 
            { 
                this.m_relDepartAddress = value; 
            }
        }

        private string m_relDepartZip;
        /// <summary>
        /// 获取或设置数据集负责单位邮政编码
        /// </summary>
        [Category("联系信息"), Description("邮政编码")]
        public string RelationDepartZip
        {
            get
            {
                return this.m_relDepartZip;
            }
            set 
            { 
                this.m_relDepartZip = value;
            }
        }

        private string m_relDepartEmail;
        /// <summary>
        /// 获取或设置数据集负责单位或联系人电子信箱
        /// </summary>
        [Category("联系信息"), Description("电子信箱")]
        public string RelationDepartEmail
        {
            get 
            { 
                return this.m_relDepartEmail;
            }
            set 
            { 
                this.m_relDepartEmail = value;
            }
        }

        private string m_relDepartLinkWeb;
        /// <summary>
        /// 获取或设置数据集负责单位的网络地址
        /// </summary>
        [Category("联系信息"), Description("网络地址")]
        public string RelationDepartLinkWeb
        {
            get 
            { 
                return this.m_relDepartLinkWeb; 
            }
            set 
            { 
                this.m_relDepartLinkWeb = value; 
            }
        }

        private string m_bgFileName;
        /// <summary>
        /// 获取或设置静态浏览图的文件名称
        /// </summary>
        [Category("基本信息"), Description("静态浏览图的文件名称")]
        public string BgFileName
        {
            get
            {
                return m_bgFileName;
            }
            set
            {
                this.m_bgFileName = value;
            }
        }

        private EnumMetaUseRestrictType m_useRestrict=EnumMetaUseRestrictType.版权;
        /// <summary>
        /// 获取或设置使用限制代码
        /// </summary>
        [Category("数据集限制"), Description("使用限制代码"),DefaultValue(EnumMetaUseRestrictType.版权)]
        public EnumMetaUseRestrictType UseRestrict
        {
            get
            {
                return this.m_useRestrict;
            }
            set
            {
                this.m_useRestrict = value;
            }
        }

        private EnumMetaSafeCodeType m_safeCode=EnumMetaSafeCodeType.内部;
        /// <summary>
        /// 获取或设置安全等级代码
        /// </summary>
        [Category("数据集限制"), Description("安全等级代码"),DefaultValue(EnumMetaSafeCodeType.内部)]
        public EnumMetaSafeCodeType ClassCode
        {
            get
            {
                return this.m_safeCode;
            }
            set
            {
                this.m_safeCode = value;
            }
        }

        private string m_formatName;
        /// <summary>
        /// 获取或设置数据集分发者提供的数据交换格式名称
        /// </summary>
        [Category("数据集限制"), Description("格式名称")]
        public string FormatName
        {
            get
            {
                return this.m_formatName;
            }
            set
            {
                this.m_formatName = value;
            }
        }

        private string m_formatVersion="1.0.0";
        /// <summary>
        /// 获取或设置数据集分发者提供的数据交换格式版本
        /// </summary>
        [Category("管理软件"), Description("版本")]
        public string FormatVersion
        {
            get
            {
                return this.m_formatVersion;
            }
            set
            {
                this.m_formatVersion = value;
            }
        }

        private string m_softwareName;
        /// <summary>
        /// 获取或设置对数据集进行维护和管理的软件名称
        /// </summary>
        [Category("管理软件"), Description("名称")]
        public string SoftwareName
        {
            get
            {
                return this.m_softwareName;
            }
            set
            {
                this.m_softwareName = value;
            }
        }
    }
}
