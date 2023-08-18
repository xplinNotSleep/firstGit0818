using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DMetaData
{
    /// <summary>
    /// 分发信息
    /// </summary>
    public class MetaDistributeInfo:MetaDataInfo 
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MetaDistributeInfo()
        {
            this.m_metaTableType = EnumMetaTableType.分发信息;
        }
        private string m_onLine;
        /// <summary>
        /// 获取或设置在线连接
        /// </summary>
        [Category("基本信息"), Description("在线连接")]
        public string OnLine
        {
            get
            {
                return this.m_onLine;
            }
            set
            {
                this.m_onLine = value;
            }
        }

        private string m_distributeDepartName;
        /// <summary>
        /// 获取或设置分发单位名称
        /// </summary>
        [Category("基本信息"), Description("分发单位名称")]
        public string DistributeDepartName
        {
            get
            {
                return this.m_distributeDepartName;
            }
            set
            {
                this.m_distributeDepartName = value;
            }
        }

        private string m_relPersonName;
        /// <summary>
        /// 获取或设置联系人名称
        /// </summary>
        [Category("联系信息"), Description("联系人名称")]
        public string RelationPersonName
        {
            get
            {
                return this.m_relPersonName;
            }
            set 
            {
                this.m_relPersonName=value;
            }
        }

        private string m_relPersonTel;

        /// <summary>
        /// 获取或设置联系人电话
        /// </summary>
        [Category("联系信息"),Description("联系人电话")]
        public string RelationPersonTel
        {
            get
            {
                return this.m_relPersonTel;
            }
            set
            {
                this.m_relPersonTel = value;
            }
        }

        private string m_relPersonFax;
        /// <summary>
        /// 获取或设置数据集分发单位的传真
        /// </summary>
        public string RelationPersonFax
        {
            get
            {
                return this.m_relPersonFax;
            }
            set
            {
                this.m_relPersonFax = value;
            }
        }

        private string m_relDepartAddress;
        /// <summary>
        /// 获取或设置数据集分发单位的通信地址
        /// </summary>
        [Category("联系信息"),Description("通信地址")]
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
        /// 获取或设置数据集分发单位邮政编码
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
        /// 获取或设置数据集分发单位电子信箱地址
        /// </summary>
        [Category("联系信息"), Description("电子信箱")]
        public string RelationDepartEmail
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

        private string m_relDepartLinkWeb;

        /// <summary>
        /// 获取或设置数据集分发单位的网址
        /// </summary>
        [Category("联系信息"), Description("网址")]
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
    }
}
