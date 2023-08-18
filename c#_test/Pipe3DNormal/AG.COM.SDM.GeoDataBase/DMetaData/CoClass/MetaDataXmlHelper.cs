using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

namespace AG.COM.SDM.GeoDataBase.DMetaData
{
    /// <summary>
    /// 元数据Xml交换格式帮助类
    /// </summary>
    public class MetaDataXmlHelper
    {
        private string m_filePath;
        private MetaIdentifyInfo m_metaIdentifyInfo;
        private MetaDistributeInfo m_metaDistributeInfo;
        private MetaQualityInfo m_metaQualityInfo;
        private MetaSpatialRefInfo m_metaSpatialRefInfo;
        private MetaRelationShipInfo m_metaRelationShipInfo;

        /// <summary>
        /// 获取或设置文件路径
        /// </summary>
        public string FilePath
        {
            get
            {
                return this.m_filePath;
            }
            set
            {
                this.m_filePath = value;
            }
        }

        /// <summary>
        /// 获取或设置元数据 联系信息
        /// </summary>
        public MetaRelationShipInfo RelationShipInfo
        {
            get
            {
                return this.m_metaRelationShipInfo;
            }
            set
            {
                this.m_metaRelationShipInfo = value;
            }
        }

        /// <summary>
        /// 获取或设置元数据 标识信息
        /// </summary>
        public MetaIdentifyInfo IdentifyInfo
        {
            get
            {
                return this.m_metaIdentifyInfo;
            }
            set
            {
                this.m_metaIdentifyInfo = value;
            }
        }

        /// <summary>
        /// 获取或设置元数据 质量信息
        /// </summary>
        public MetaQualityInfo QualityInfo
        {
            get
            {
                return this.m_metaQualityInfo;
            }
            set
            {
                this.m_metaQualityInfo = value;
            }
        }

        /// <summary>
        /// 获取或设置元数据 空间参考
        /// </summary>
        public MetaSpatialRefInfo SpatialRefInfo
        {
            get
            {
                return this.m_metaSpatialRefInfo;
            }
            set
            {
                this.m_metaSpatialRefInfo = value;
            }
        }

        /// <summary>
        /// 获取或设置元数据 分发信息
        /// </summary>
        public MetaDistributeInfo DistributeInfo
        {
            get
            {
                return this.m_metaDistributeInfo;
            }
            set
            {
                this.m_metaDistributeInfo = value;
            }
        }

        /// <summary>
        /// 写入元数据到Xml文件
        /// </summary>
        public void WriteMetaDataToXmlFile()
        {
            //初始化文件流
            using (FileStream fs = new FileStream(this.m_filePath, FileMode.Create))
            {
                XmlTextWriter tXmlOut = new XmlTextWriter(fs, Encoding.Unicode);
                tXmlOut.Formatting = Formatting.Indented;

                //书写XML声明
                tXmlOut.WriteStartDocument();

                //书写配置文件注释信息
                tXmlOut.WriteComment("标题:元数据标准文件！");
                tXmlOut.WriteComment("版本:");
                tXmlOut.WriteComment("作者:Echo-AG.COM.SDM");

                //开始
                tXmlOut.WriteStartElement("MetaDataConfig");
                tXmlOut.WriteAttributeString("SubjectName",m_metaIdentifyInfo.SubjectName);

                //写标识信息到文件
                WriteMetaIdentifyInfo(tXmlOut);
                //写质量信息到文件
                WriteMetaQulityInfo(tXmlOut);
                //写空间参考信息到文件
                WriteSpatialRefInfo(tXmlOut);
                ////写联系信息到文件
                //WriteRelationShipInfo(tXmlOut);
                ////写分发信息到文件
                //WriteDistributeInfo(tXmlOut);

                //终止一个元素
                tXmlOut.WriteEndElement();
                //关闭任何打开的元素
                tXmlOut.WriteEndDocument();

                //关闭
                tXmlOut.Close();
            }
        }

        /// <summary>
        /// 从Xml文件中读取元数据
        /// </summary>
        public void ReadMetaDataFromXmlFile()
        {
            try
            {
                DataSet tDataset = new DataSet();
                tDataset.ReadXml(this.m_filePath);

                int tableIndex = tDataset.Tables.IndexOf("MetaDataConfig");
                if (tableIndex < 0)
                {
                    throw new Exception("该Xml文件不符合要求");
                }

                tableIndex = tDataset.Tables.IndexOf("标识信息");
                if (tableIndex > -1)
                {
                    DataTable tDataTable = tDataset.Tables[tableIndex];
                    //从标识信息表中读取标识信息元数据内容
                    ReadMetaIdentifyInfo(tDataTable);
                }

                tableIndex = tDataset.Tables.IndexOf("质量信息");
                if (tableIndex > -1)
                {
                    DataTable tDataTable = tDataset.Tables[tableIndex];
                    //从质量信息表中读取质量信息元数据内容
                    ReadMetaQulityInfo(tDataTable);
                }

                tableIndex = tDataset.Tables.IndexOf("空间参考信息");
                if (tableIndex > -1)
                {
                    DataTable tDataTable = tDataset.Tables[tableIndex];
                    //从空间参考信息表中读取空间参考信息元数据内容
                    ReadMetaSpatialRefInfo(tDataTable);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 写标识信息到Xml文件
        /// </summary>
        /// <param name="pXmlOut">Xml文本读写器</param>
        private void WriteMetaIdentifyInfo(XmlTextWriter pXmlOut)
        {
            //写入开始元素标识
            pXmlOut.WriteStartElement("标识信息");

            pXmlOut.WriteElementString("MC", m_metaIdentifyInfo.ResTitle);                                  //名称
            pXmlOut.WriteElementString("RQ", string.Format("{0:yyyy/MM/dd}" ,m_metaIdentifyInfo.ResRefDate));//日期
            pXmlOut.WriteElementString("BB", m_metaIdentifyInfo.ResEd);                                     //版本

            pXmlOut.WriteElementString("YZ", m_metaIdentifyInfo.DatLangCode);                               //语种
            pXmlOut.WriteElementString("ZY", m_metaIdentifyInfo.IDAbs);                                     //摘要
            pXmlOut.WriteElementString("XZ", m_metaIdentifyInfo.IDStstCode);                                //现状
            pXmlOut.WriteElementString("FL", Convert.ToString((int)m_metaIdentifyInfo.ClassfiyCode));       //分类

            pXmlOut.WriteElementString("XBJD", m_metaIdentifyInfo.WestBL.ToString());                       //西边经度
            pXmlOut.WriteElementString("DBJD", m_metaIdentifyInfo.EastBL.ToString());                       //东边经度 
            pXmlOut.WriteElementString("NBWD", m_metaIdentifyInfo.SourthBL.ToString());                     //南边纬度
            pXmlOut.WriteElementString("BBWD", m_metaIdentifyInfo.NorthBL.ToString());                      //北边纬度 

            pXmlOut.WriteElementString("DLBSF", m_metaIdentifyInfo.GeoID);                              //地理标识符
            pXmlOut.WriteElementString("ZZSJ", string.Format("{0:yyyy/MM/dd}",m_metaIdentifyInfo.End));  //终止时间 
            pXmlOut.WriteElementString("BSFS", Convert.ToString((int)m_metaIdentifyInfo.RPType));       //表示方式
            pXmlOut.WriteElementString("KJFBL", m_metaIdentifyInfo.SpatRes);                            //空间分辨率            

            pXmlOut.WriteElementString("LB", Convert.ToString((int)m_metaIdentifyInfo.TPCatCode));      //类别
            pXmlOut.WriteElementString("FZDWMC", m_metaIdentifyInfo.DepartName);                        //负责单位名称 
            pXmlOut.WriteElementString("LXR", m_metaIdentifyInfo.RelationName);                         //联系人
            pXmlOut.WriteElementString("DH", m_metaIdentifyInfo.RelationPersionTel);                    //电话         

            pXmlOut.WriteElementString("CZ", m_metaIdentifyInfo.RelationPersionFax);                    //传真
            pXmlOut.WriteElementString("TXDZ", m_metaIdentifyInfo.RelationDepartAddress);               //通信地址 
            pXmlOut.WriteElementString("YZBM", m_metaIdentifyInfo.RelationDepartZip);                   //邮政编码
            pXmlOut.WriteElementString("DZXXDZ", m_metaIdentifyInfo.RelationDepartEmail);               //电子信箱地址  

            pXmlOut.WriteElementString("WZ", m_metaIdentifyInfo.RelationDepartLinkWeb);                 //网址
            pXmlOut.WriteElementString("WJMC", m_metaIdentifyInfo.BgFileName);                          //文件名称 
            pXmlOut.WriteElementString("SYXZDM", Convert.ToString((int)m_metaIdentifyInfo.UseRestrict));//使用限制代码
            pXmlOut.WriteElementString("AQDJDM", Convert.ToString((int)m_metaIdentifyInfo.ClassCode));  //安全等级代码            

            pXmlOut.WriteElementString("GSMC", m_metaIdentifyInfo.FormatName);                          //格式名称 
            pXmlOut.WriteElementString("GSBB", m_metaIdentifyInfo.FormatVersion);                       //格式版本
            pXmlOut.WriteElementString("GLRJMC", m_metaIdentifyInfo.SoftwareName);                      //管理软件名称 
       
            //结尾标识符
            pXmlOut.WriteFullEndElement();
        }

        /// <summary>
        /// 写质量信息到Xml文件
        /// </summary>
        /// <param name="pXmlOut">Xml文本读写器</param>
        private void WriteMetaQulityInfo(XmlTextWriter pXmlOut)
        {
            //写开始元素
            pXmlOut.WriteStartElement("质量信息");

            //数据质量概述
            pXmlOut.WriteElementString("SJZLMS", m_metaQualityInfo.QualityComment);
            //数据志
            pXmlOut.WriteElementString("SJZ", m_metaQualityInfo.QualityLineage);            

            //结束标识
            pXmlOut.WriteFullEndElement();
        }

        /// <summary>
        /// 写空间参考信息到Xml文件
        /// </summary>
        /// <param name="pXmlOut">Xml文本读写器</param>
        private void WriteSpatialRefInfo(XmlTextWriter pXmlOut)
        {
            pXmlOut.WriteStartElement("空间参考信息");

            pXmlOut.WriteElementString("DDZBCZXTMC", Convert.ToString((int)m_metaSpatialRefInfo.CoordsRefName));  //大地坐标参照系统名称
            pXmlOut.WriteElementString("ZBXTLX", Convert.ToString((int)m_metaSpatialRefInfo.CoordsType));   //坐标系统类型
            pXmlOut.WriteElementString("ZBXTMC", m_metaSpatialRefInfo.CoordsName);                          //坐标系统名称
            pXmlOut.WriteElementString("TYZBXTCS", m_metaSpatialRefInfo.ProjectParameter);                  //投影坐标系统参数
                            
            pXmlOut.WriteFullEndElement();
        }

        /// <summary>
        /// 写联系信息到Xml文件
        /// </summary>
        /// <param name="pXmlOut">Xml文本读写器</param>
        private void WriteRelationShipInfo(XmlTextWriter pXmlOut)
        {
            //开始标识
            pXmlOut.WriteStartElement("空间参考信息");

            pXmlOut.WriteElementString("FBRQ", string.Format("{0:yyyy/MM/dd", m_metaRelationShipInfo.PublishTime));   //发布日期
            pXmlOut.WriteElementString("FBDWMC", m_metaRelationShipInfo.DepartmentName);                              //发布单位名称

            pXmlOut.WriteElementString("LXR", m_metaRelationShipInfo.RelationShipName);                             //联系人
            pXmlOut.WriteElementString("DH", m_metaRelationShipInfo.DepartmentTel);                                 //电话
            pXmlOut.WriteElementString("CZ", m_metaRelationShipInfo.DepartmentFax);                                 //传真

            pXmlOut.WriteElementString("TXDZ", m_metaRelationShipInfo.DepartmentAddress);                           //通信地址
            pXmlOut.WriteElementString("YZBM", m_metaRelationShipInfo.ZipCode);                                     //邮政编码
            pXmlOut.WriteElementString("WZ", m_metaRelationShipInfo.DepartmentWeb);                                 //网址  

            //结束标识
            pXmlOut.WriteFullEndElement();
        }

        /// <summary>
        /// 写分发信息到Xml文件
        /// </summary>
        /// <param name="pXmlOut">Xml文本读写器</param>
        private void WriteDistributeInfo(XmlTextWriter pXmlOut)
        {
            //开始标识
            pXmlOut.WriteStartElement("分发信息");

            pXmlOut.WriteElementString("ZXLJ", m_metaDistributeInfo.OnLine);                        //在线连接
            pXmlOut.WriteElementString("FFDWMC", m_metaDistributeInfo.DistributeDepartName);        //分发单位名称
            pXmlOut.WriteElementString("LXR", m_metaDistributeInfo.RelationPersonName);             //联系人
            pXmlOut.WriteElementString("DH", m_metaDistributeInfo.RelationPersonTel);               //电话 

            pXmlOut.WriteElementString("CZ", m_metaDistributeInfo.RelationPersonFax);               //传真
            pXmlOut.WriteElementString("TXDZ", m_metaDistributeInfo.RelationDepartAddress);         //通信地址
            pXmlOut.WriteElementString("YZBM", m_metaDistributeInfo.RelationDepartZip);             //邮政编码
            pXmlOut.WriteElementString("DZXXDZ", m_metaDistributeInfo.RelationDepartEmail);         //电子信箱地址 

            pXmlOut.WriteElementString("WZ", m_metaDistributeInfo.RelationDepartLinkWeb);           //网址 

            //结束标识
            pXmlOut.WriteFullEndElement();
        }

        /// <summary>
        /// 从标识信息表中读取标识信息元数据内容
        /// </summary>
        /// <param name="pIdentifyTable">标识信息表</param>
        private void ReadMetaIdentifyInfo(DataTable pIdentifyTable)
        {
            DataRow tDataRow = pIdentifyTable.Rows[0];

            m_metaIdentifyInfo = new MetaIdentifyInfo();
            m_metaIdentifyInfo.ResTitle = Convert.ToString(tDataRow["MC"]);                                 //名称
            m_metaIdentifyInfo.ResRefDate = Convert.ToDateTime(tDataRow["RQ"]);                             //日期
            m_metaIdentifyInfo.ResEd = Convert.ToString(tDataRow["BB"]);                                    //版本
            m_metaIdentifyInfo.DatLangCode = Convert.ToString(tDataRow["YZ"]);                            //语种

            m_metaIdentifyInfo.IDAbs = Convert.ToString(tDataRow["ZY"]);                                    //摘要
            m_metaIdentifyInfo.IDStstCode = Convert.ToString(tDataRow["XZ"]);                               //现状
            m_metaIdentifyInfo.ClassfiyCode = (EnumMetaDataClassifyType)Convert.ToInt32(tDataRow["FL"]);    //分类

            m_metaIdentifyInfo.WestBL = Convert.ToDouble(tDataRow["XBJD"]);                                 //西边经度  
            m_metaIdentifyInfo.EastBL = Convert.ToDouble(tDataRow["DBJD"]);                                 //东边经度 
            m_metaIdentifyInfo.SourthBL = Convert.ToDouble(tDataRow["NBWD"]);                               //南边经度 
            m_metaIdentifyInfo.NorthBL = Convert.ToDouble(tDataRow["BBWD"]);                                //北边经度 

            m_metaIdentifyInfo.GeoID = Convert.ToString(tDataRow["DLBSF"]);                                 //地理标识符
            m_metaIdentifyInfo.RPType = (EnumMetaDisplayType)Convert.ToInt32(tDataRow["BSFS"]);             //表示方式
            m_metaIdentifyInfo.End = Convert.ToDateTime(tDataRow["ZZSJ"]);                                  //终止时间
            m_metaIdentifyInfo.SpatRes = Convert.ToString(tDataRow["KJFBL"]);                               //空间分辨率

            m_metaIdentifyInfo.TPCatCode = (EnumMetaDataCategoryType)Convert.ToInt32(tDataRow["LB"]);       //类别
            m_metaIdentifyInfo.DepartName = Convert.ToString(tDataRow["FZDWMC"]);                           //负责单位名称
            m_metaIdentifyInfo.RelationName = Convert.ToString(tDataRow["LXR"]);                            //联系人
            m_metaIdentifyInfo.RelationPersionTel = Convert.ToString(tDataRow["DH"]);                       //电话                

            m_metaIdentifyInfo.RelationPersionFax = Convert.ToString(tDataRow["CZ"]);                       //传真
            m_metaIdentifyInfo.RelationDepartAddress = Convert.ToString(tDataRow["TXDZ"]);                  //通信地址
            m_metaIdentifyInfo.RelationDepartZip = Convert.ToString(tDataRow["YZBM"]);                    //邮政编码
            m_metaIdentifyInfo.RelationDepartEmail = Convert.ToString(tDataRow["DZXXDZ"]);                  //电子信箱地址

            m_metaIdentifyInfo.RelationDepartLinkWeb = Convert.ToString(tDataRow["WZ"]);                    //网址
            m_metaIdentifyInfo.BgFileName = Convert.ToString(tDataRow["WJMC"]);                             //文件名称
            m_metaIdentifyInfo.UseRestrict = (EnumMetaUseRestrictType)Convert.ToInt32(tDataRow["SYXZDM"]);  //使用限制代码
            m_metaIdentifyInfo.ClassCode = (EnumMetaSafeCodeType)Convert.ToInt32(tDataRow["AQDJDM"]);       //安全等级代码

            m_metaIdentifyInfo.FormatName = Convert.ToString(tDataRow["GSMC"]);                             //格式名称
            m_metaIdentifyInfo.FormatVersion = Convert.ToString(tDataRow["GSBB"]);                        //格式版本
            m_metaIdentifyInfo.SoftwareName = Convert.ToString(tDataRow["GLRJMC"]);                         //管理软件名称
        }

        /// <summary>
        /// 从质量信息表中读取质量信息元数据内容
        /// </summary>
        /// <param name="pIdentifyTable">质量信息表</param>
        private void ReadMetaQulityInfo(DataTable pQulityTable)
        {
            //获取第一行数据
            DataRow tDataRow = pQulityTable.Rows[0];
            //实例化新对象
            m_metaQualityInfo = new MetaQualityInfo(); 
            //数据质量概述
            m_metaQualityInfo.QualityComment = Convert.ToString(tDataRow["SJZLMS"]);
            //数据志
            m_metaQualityInfo.QualityLineage = Convert.ToString(tDataRow["SJZ"]);
        }

        /// <summary>
        /// 从空间参考信息表中读取空间参考信息元数据内容
        /// </summary>
        /// <param name="pIdentifyTable">空间参考信息表</param>
        private void ReadMetaSpatialRefInfo(DataTable pSpatialRefTable)
        {
            //获取第一行数据
            DataRow tDataRow = pSpatialRefTable.Rows[0];
            //实例化新对象
            m_metaSpatialRefInfo = new MetaSpatialRefInfo();

            m_metaSpatialRefInfo.CoordsRefName = (EnumMetaCoordsRefName)Convert.ToInt32(tDataRow["DDZBCZXTMC"]);     //大地坐标参照系统名称 
            m_metaSpatialRefInfo.CoordsType = (EnumMetaCoordsType)Convert.ToInt32(tDataRow["ZBXTLX"]);              //坐标系统类型
            m_metaSpatialRefInfo.CoordsName = Convert.ToString(tDataRow["ZBXTMC"]);                                 //坐标系统名称 
            m_metaSpatialRefInfo.ProjectParameter = Convert.ToString(tDataRow["TYZBXTCS"]);                         //投影坐标系统参数
        }

        /// <summary>
        /// 从联系信息表中读取联系信息元数据内容
        /// </summary>
        /// <param name="pIdentifyTable">联系信息表</param>
        private void ReadMetaRelationShipInfo(DataTable pRelationShipTable)
        {
            //获取第一行数据
            DataRow tDataRow = pRelationShipTable.Rows[0];
            //实例化新对象
            m_metaRelationShipInfo = new MetaRelationShipInfo();

            m_metaRelationShipInfo.PublishTime = Convert.ToDateTime(tDataRow["FBRQ"]);            //发布日期
            m_metaRelationShipInfo.DepartmentName = Convert.ToString(tDataRow["FBDWMC"]);         //发布单位名称
            m_metaRelationShipInfo.RelationShipName = Convert.ToString(tDataRow["LXR"]);          //联系人
            m_metaRelationShipInfo.DepartmentTel = Convert.ToString(tDataRow["DH"]);              //电话

            m_metaRelationShipInfo.DepartmentFax = Convert.ToString(tDataRow["CZ"]);              //传真
            m_metaRelationShipInfo.DepartmentAddress = Convert.ToString(tDataRow["TXDZ"]);        //通信地址
            m_metaRelationShipInfo.ZipCode = Convert.ToString(tDataRow["YZBM"]);                  //邮政编码
            m_metaRelationShipInfo.DepartmentWeb = Convert.ToString(tDataRow["WZ"]);              //网址
        }

        /// <summary>
        /// 从分发信息表中读取分发信息元数据内容
        /// </summary>
        /// <param name="pIdentifyTable">分发信息表</param>
        private void ReadMetaDistributeInfo(DataTable pDistributeTable)
        {
            //获取第一行数据
            DataRow tDataRow = pDistributeTable.Rows[0];

            //实例化新对象
            m_metaDistributeInfo = new MetaDistributeInfo();

            m_metaDistributeInfo.OnLine = Convert.ToString(tDataRow["ZXLJ"]);                       //在线连接
            m_metaDistributeInfo.DistributeDepartName = Convert.ToString(tDataRow["FFDWMC"]);       //分发单位名称           
            m_metaDistributeInfo.RelationPersonName = Convert.ToString(tDataRow["LXR"]);            //联系人
            m_metaDistributeInfo.RelationPersonTel = Convert.ToString(tDataRow["DH"]);              //电话

            m_metaDistributeInfo.RelationPersonFax = Convert.ToString(tDataRow["CZ"]);              //传真
            m_metaDistributeInfo.RelationDepartAddress = Convert.ToString(tDataRow["TXDZ"]);        //通信地址           
            m_metaDistributeInfo.RelationDepartZip = Convert.ToString(tDataRow["YZBM"]);            //邮政编码
            m_metaDistributeInfo.RelationDepartEmail = Convert.ToString(tDataRow["DZXXDZ"]);        //电子信箱地址

            m_metaDistributeInfo.RelationDepartLinkWeb = Convert.ToString(tDataRow["WZ"]);          //网址     
        }
    }
}
