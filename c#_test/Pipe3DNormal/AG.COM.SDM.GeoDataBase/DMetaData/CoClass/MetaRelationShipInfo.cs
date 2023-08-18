using System;
using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DMetaData
{
    /// <summary>
    /// 元数据之联系信息
    /// </summary>
    public class MetaRelationShipInfo : MetaDataInfo
    {
        private DateTime m_publishTime;
        private string m_deptName;
        private string m_relationshipName;
        private string m_deptTel;
        private string m_deptFax;
        private string m_zipCode;
        private string m_deptAddress;
        private string m_deptEmail;
        private string m_deptWeb;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MetaRelationShipInfo()
        {
            this.m_metaTableType = EnumMetaTableType.联系信息; 
        }
        /// <summary>
        /// 获取或设置发布日期
        /// </summary>
        [Category("基本信息"), Description("发布日期")] 
        public DateTime PublishTime
        {
            get
            {
                return this.m_publishTime;
            }
            set
            {
                this.m_publishTime = value;
            }
        }

        /// <summary>
        /// 获取或设置发布单位名称
        /// </summary>
        [Category("基本信息"), Description("发布单位名称")] 
        public string DepartmentName
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

        /// <summary>
        /// 获取或设置联系人电话
        /// </summary>
        [Category("基本信息"), Description("联系人电话")] 
        public string DepartmentTel
        {
            get
            {
                return this.m_deptTel;
            }
            set
            {
                this.m_deptTel = value;
            }
        }

        /// <summary>
        /// 获取或设置数据集发布单位通信地址
        /// </summary>
        [Category("基本信息"), Description("通信地址")] 
        public string DepartmentAddress
        {
            get
            {
                return this.m_deptAddress;
            }
            set
            {
                this.m_deptAddress = value;
            }
        }

        /// <summary>
        /// 获取或设置邮政编码
        /// </summary>
        [Category("其它信息"), Description("邮政编码")]
        public string ZipCode
        {
            get
            {
                return this.m_zipCode;
            }
            set
            {
                this.m_zipCode = value;
            }
        }

        /// <summary>
        /// 获取或设置联系人
        /// </summary>
        [Category("其它信息"), Description("联系人姓名")]
        public string RelationShipName
        {
            get
            {
                return this.m_relationshipName;
            }
            set
            {
                this.m_relationshipName = value;
            }
        }


        /// <summary>
        /// 获取或设置数据集发布单位传真
        /// </summary>
        [Category("其它信息"), Description("传真")]
        public string DepartmentFax
        {
            get
            {
                return this.m_deptFax;
            }
            set
            {
                this.m_deptFax = value;
            }
        } 

        /// <summary>
        /// 获取或设置电子信箱地址
        /// </summary>
        [Category("其它信息"), Description("电子信箱地址")]
        public string DepartmentEmail
        {
            get
            {
                return this.m_deptEmail;
            }
            set
            {
                this.m_deptEmail = value;
            }
        }

        /// <summary>
        /// 获取或设置网址
        /// </summary>
        [Category("其它信息"), Description("网址")]
        public string DepartmentWeb
        {
            get
            {
                return this.m_deptWeb;
            }
            set
            {
                this.m_deptWeb = value;
            }
        }
    }
}
