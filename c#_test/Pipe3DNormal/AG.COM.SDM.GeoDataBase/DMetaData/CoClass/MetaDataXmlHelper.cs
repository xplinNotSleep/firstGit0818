using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

namespace AG.COM.SDM.GeoDataBase.DMetaData
{
    /// <summary>
    /// Ԫ����Xml������ʽ������
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
        /// ��ȡ�������ļ�·��
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
        /// ��ȡ������Ԫ���� ��ϵ��Ϣ
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
        /// ��ȡ������Ԫ���� ��ʶ��Ϣ
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
        /// ��ȡ������Ԫ���� ������Ϣ
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
        /// ��ȡ������Ԫ���� �ռ�ο�
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
        /// ��ȡ������Ԫ���� �ַ���Ϣ
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
        /// д��Ԫ���ݵ�Xml�ļ�
        /// </summary>
        public void WriteMetaDataToXmlFile()
        {
            //��ʼ���ļ���
            using (FileStream fs = new FileStream(this.m_filePath, FileMode.Create))
            {
                XmlTextWriter tXmlOut = new XmlTextWriter(fs, Encoding.Unicode);
                tXmlOut.Formatting = Formatting.Indented;

                //��дXML����
                tXmlOut.WriteStartDocument();

                //��д�����ļ�ע����Ϣ
                tXmlOut.WriteComment("����:Ԫ���ݱ�׼�ļ���");
                tXmlOut.WriteComment("�汾:");
                tXmlOut.WriteComment("����:Echo-AG.COM.SDM");

                //��ʼ
                tXmlOut.WriteStartElement("MetaDataConfig");
                tXmlOut.WriteAttributeString("SubjectName",m_metaIdentifyInfo.SubjectName);

                //д��ʶ��Ϣ���ļ�
                WriteMetaIdentifyInfo(tXmlOut);
                //д������Ϣ���ļ�
                WriteMetaQulityInfo(tXmlOut);
                //д�ռ�ο���Ϣ���ļ�
                WriteSpatialRefInfo(tXmlOut);
                ////д��ϵ��Ϣ���ļ�
                //WriteRelationShipInfo(tXmlOut);
                ////д�ַ���Ϣ���ļ�
                //WriteDistributeInfo(tXmlOut);

                //��ֹһ��Ԫ��
                tXmlOut.WriteEndElement();
                //�ر��κδ򿪵�Ԫ��
                tXmlOut.WriteEndDocument();

                //�ر�
                tXmlOut.Close();
            }
        }

        /// <summary>
        /// ��Xml�ļ��ж�ȡԪ����
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
                    throw new Exception("��Xml�ļ�������Ҫ��");
                }

                tableIndex = tDataset.Tables.IndexOf("��ʶ��Ϣ");
                if (tableIndex > -1)
                {
                    DataTable tDataTable = tDataset.Tables[tableIndex];
                    //�ӱ�ʶ��Ϣ���ж�ȡ��ʶ��ϢԪ��������
                    ReadMetaIdentifyInfo(tDataTable);
                }

                tableIndex = tDataset.Tables.IndexOf("������Ϣ");
                if (tableIndex > -1)
                {
                    DataTable tDataTable = tDataset.Tables[tableIndex];
                    //��������Ϣ���ж�ȡ������ϢԪ��������
                    ReadMetaQulityInfo(tDataTable);
                }

                tableIndex = tDataset.Tables.IndexOf("�ռ�ο���Ϣ");
                if (tableIndex > -1)
                {
                    DataTable tDataTable = tDataset.Tables[tableIndex];
                    //�ӿռ�ο���Ϣ���ж�ȡ�ռ�ο���ϢԪ��������
                    ReadMetaSpatialRefInfo(tDataTable);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// д��ʶ��Ϣ��Xml�ļ�
        /// </summary>
        /// <param name="pXmlOut">Xml�ı���д��</param>
        private void WriteMetaIdentifyInfo(XmlTextWriter pXmlOut)
        {
            //д�뿪ʼԪ�ر�ʶ
            pXmlOut.WriteStartElement("��ʶ��Ϣ");

            pXmlOut.WriteElementString("MC", m_metaIdentifyInfo.ResTitle);                                  //����
            pXmlOut.WriteElementString("RQ", string.Format("{0:yyyy/MM/dd}" ,m_metaIdentifyInfo.ResRefDate));//����
            pXmlOut.WriteElementString("BB", m_metaIdentifyInfo.ResEd);                                     //�汾

            pXmlOut.WriteElementString("YZ", m_metaIdentifyInfo.DatLangCode);                               //����
            pXmlOut.WriteElementString("ZY", m_metaIdentifyInfo.IDAbs);                                     //ժҪ
            pXmlOut.WriteElementString("XZ", m_metaIdentifyInfo.IDStstCode);                                //��״
            pXmlOut.WriteElementString("FL", Convert.ToString((int)m_metaIdentifyInfo.ClassfiyCode));       //����

            pXmlOut.WriteElementString("XBJD", m_metaIdentifyInfo.WestBL.ToString());                       //���߾���
            pXmlOut.WriteElementString("DBJD", m_metaIdentifyInfo.EastBL.ToString());                       //���߾��� 
            pXmlOut.WriteElementString("NBWD", m_metaIdentifyInfo.SourthBL.ToString());                     //�ϱ�γ��
            pXmlOut.WriteElementString("BBWD", m_metaIdentifyInfo.NorthBL.ToString());                      //����γ�� 

            pXmlOut.WriteElementString("DLBSF", m_metaIdentifyInfo.GeoID);                              //�����ʶ��
            pXmlOut.WriteElementString("ZZSJ", string.Format("{0:yyyy/MM/dd}",m_metaIdentifyInfo.End));  //��ֹʱ�� 
            pXmlOut.WriteElementString("BSFS", Convert.ToString((int)m_metaIdentifyInfo.RPType));       //��ʾ��ʽ
            pXmlOut.WriteElementString("KJFBL", m_metaIdentifyInfo.SpatRes);                            //�ռ�ֱ���            

            pXmlOut.WriteElementString("LB", Convert.ToString((int)m_metaIdentifyInfo.TPCatCode));      //���
            pXmlOut.WriteElementString("FZDWMC", m_metaIdentifyInfo.DepartName);                        //����λ���� 
            pXmlOut.WriteElementString("LXR", m_metaIdentifyInfo.RelationName);                         //��ϵ��
            pXmlOut.WriteElementString("DH", m_metaIdentifyInfo.RelationPersionTel);                    //�绰         

            pXmlOut.WriteElementString("CZ", m_metaIdentifyInfo.RelationPersionFax);                    //����
            pXmlOut.WriteElementString("TXDZ", m_metaIdentifyInfo.RelationDepartAddress);               //ͨ�ŵ�ַ 
            pXmlOut.WriteElementString("YZBM", m_metaIdentifyInfo.RelationDepartZip);                   //��������
            pXmlOut.WriteElementString("DZXXDZ", m_metaIdentifyInfo.RelationDepartEmail);               //���������ַ  

            pXmlOut.WriteElementString("WZ", m_metaIdentifyInfo.RelationDepartLinkWeb);                 //��ַ
            pXmlOut.WriteElementString("WJMC", m_metaIdentifyInfo.BgFileName);                          //�ļ����� 
            pXmlOut.WriteElementString("SYXZDM", Convert.ToString((int)m_metaIdentifyInfo.UseRestrict));//ʹ�����ƴ���
            pXmlOut.WriteElementString("AQDJDM", Convert.ToString((int)m_metaIdentifyInfo.ClassCode));  //��ȫ�ȼ�����            

            pXmlOut.WriteElementString("GSMC", m_metaIdentifyInfo.FormatName);                          //��ʽ���� 
            pXmlOut.WriteElementString("GSBB", m_metaIdentifyInfo.FormatVersion);                       //��ʽ�汾
            pXmlOut.WriteElementString("GLRJMC", m_metaIdentifyInfo.SoftwareName);                      //����������� 
       
            //��β��ʶ��
            pXmlOut.WriteFullEndElement();
        }

        /// <summary>
        /// д������Ϣ��Xml�ļ�
        /// </summary>
        /// <param name="pXmlOut">Xml�ı���д��</param>
        private void WriteMetaQulityInfo(XmlTextWriter pXmlOut)
        {
            //д��ʼԪ��
            pXmlOut.WriteStartElement("������Ϣ");

            //������������
            pXmlOut.WriteElementString("SJZLMS", m_metaQualityInfo.QualityComment);
            //����־
            pXmlOut.WriteElementString("SJZ", m_metaQualityInfo.QualityLineage);            

            //������ʶ
            pXmlOut.WriteFullEndElement();
        }

        /// <summary>
        /// д�ռ�ο���Ϣ��Xml�ļ�
        /// </summary>
        /// <param name="pXmlOut">Xml�ı���д��</param>
        private void WriteSpatialRefInfo(XmlTextWriter pXmlOut)
        {
            pXmlOut.WriteStartElement("�ռ�ο���Ϣ");

            pXmlOut.WriteElementString("DDZBCZXTMC", Convert.ToString((int)m_metaSpatialRefInfo.CoordsRefName));  //����������ϵͳ����
            pXmlOut.WriteElementString("ZBXTLX", Convert.ToString((int)m_metaSpatialRefInfo.CoordsType));   //����ϵͳ����
            pXmlOut.WriteElementString("ZBXTMC", m_metaSpatialRefInfo.CoordsName);                          //����ϵͳ����
            pXmlOut.WriteElementString("TYZBXTCS", m_metaSpatialRefInfo.ProjectParameter);                  //ͶӰ����ϵͳ����
                            
            pXmlOut.WriteFullEndElement();
        }

        /// <summary>
        /// д��ϵ��Ϣ��Xml�ļ�
        /// </summary>
        /// <param name="pXmlOut">Xml�ı���д��</param>
        private void WriteRelationShipInfo(XmlTextWriter pXmlOut)
        {
            //��ʼ��ʶ
            pXmlOut.WriteStartElement("�ռ�ο���Ϣ");

            pXmlOut.WriteElementString("FBRQ", string.Format("{0:yyyy/MM/dd", m_metaRelationShipInfo.PublishTime));   //��������
            pXmlOut.WriteElementString("FBDWMC", m_metaRelationShipInfo.DepartmentName);                              //������λ����

            pXmlOut.WriteElementString("LXR", m_metaRelationShipInfo.RelationShipName);                             //��ϵ��
            pXmlOut.WriteElementString("DH", m_metaRelationShipInfo.DepartmentTel);                                 //�绰
            pXmlOut.WriteElementString("CZ", m_metaRelationShipInfo.DepartmentFax);                                 //����

            pXmlOut.WriteElementString("TXDZ", m_metaRelationShipInfo.DepartmentAddress);                           //ͨ�ŵ�ַ
            pXmlOut.WriteElementString("YZBM", m_metaRelationShipInfo.ZipCode);                                     //��������
            pXmlOut.WriteElementString("WZ", m_metaRelationShipInfo.DepartmentWeb);                                 //��ַ  

            //������ʶ
            pXmlOut.WriteFullEndElement();
        }

        /// <summary>
        /// д�ַ���Ϣ��Xml�ļ�
        /// </summary>
        /// <param name="pXmlOut">Xml�ı���д��</param>
        private void WriteDistributeInfo(XmlTextWriter pXmlOut)
        {
            //��ʼ��ʶ
            pXmlOut.WriteStartElement("�ַ���Ϣ");

            pXmlOut.WriteElementString("ZXLJ", m_metaDistributeInfo.OnLine);                        //��������
            pXmlOut.WriteElementString("FFDWMC", m_metaDistributeInfo.DistributeDepartName);        //�ַ���λ����
            pXmlOut.WriteElementString("LXR", m_metaDistributeInfo.RelationPersonName);             //��ϵ��
            pXmlOut.WriteElementString("DH", m_metaDistributeInfo.RelationPersonTel);               //�绰 

            pXmlOut.WriteElementString("CZ", m_metaDistributeInfo.RelationPersonFax);               //����
            pXmlOut.WriteElementString("TXDZ", m_metaDistributeInfo.RelationDepartAddress);         //ͨ�ŵ�ַ
            pXmlOut.WriteElementString("YZBM", m_metaDistributeInfo.RelationDepartZip);             //��������
            pXmlOut.WriteElementString("DZXXDZ", m_metaDistributeInfo.RelationDepartEmail);         //���������ַ 

            pXmlOut.WriteElementString("WZ", m_metaDistributeInfo.RelationDepartLinkWeb);           //��ַ 

            //������ʶ
            pXmlOut.WriteFullEndElement();
        }

        /// <summary>
        /// �ӱ�ʶ��Ϣ���ж�ȡ��ʶ��ϢԪ��������
        /// </summary>
        /// <param name="pIdentifyTable">��ʶ��Ϣ��</param>
        private void ReadMetaIdentifyInfo(DataTable pIdentifyTable)
        {
            DataRow tDataRow = pIdentifyTable.Rows[0];

            m_metaIdentifyInfo = new MetaIdentifyInfo();
            m_metaIdentifyInfo.ResTitle = Convert.ToString(tDataRow["MC"]);                                 //����
            m_metaIdentifyInfo.ResRefDate = Convert.ToDateTime(tDataRow["RQ"]);                             //����
            m_metaIdentifyInfo.ResEd = Convert.ToString(tDataRow["BB"]);                                    //�汾
            m_metaIdentifyInfo.DatLangCode = Convert.ToString(tDataRow["YZ"]);                            //����

            m_metaIdentifyInfo.IDAbs = Convert.ToString(tDataRow["ZY"]);                                    //ժҪ
            m_metaIdentifyInfo.IDStstCode = Convert.ToString(tDataRow["XZ"]);                               //��״
            m_metaIdentifyInfo.ClassfiyCode = (EnumMetaDataClassifyType)Convert.ToInt32(tDataRow["FL"]);    //����

            m_metaIdentifyInfo.WestBL = Convert.ToDouble(tDataRow["XBJD"]);                                 //���߾���  
            m_metaIdentifyInfo.EastBL = Convert.ToDouble(tDataRow["DBJD"]);                                 //���߾��� 
            m_metaIdentifyInfo.SourthBL = Convert.ToDouble(tDataRow["NBWD"]);                               //�ϱ߾��� 
            m_metaIdentifyInfo.NorthBL = Convert.ToDouble(tDataRow["BBWD"]);                                //���߾��� 

            m_metaIdentifyInfo.GeoID = Convert.ToString(tDataRow["DLBSF"]);                                 //�����ʶ��
            m_metaIdentifyInfo.RPType = (EnumMetaDisplayType)Convert.ToInt32(tDataRow["BSFS"]);             //��ʾ��ʽ
            m_metaIdentifyInfo.End = Convert.ToDateTime(tDataRow["ZZSJ"]);                                  //��ֹʱ��
            m_metaIdentifyInfo.SpatRes = Convert.ToString(tDataRow["KJFBL"]);                               //�ռ�ֱ���

            m_metaIdentifyInfo.TPCatCode = (EnumMetaDataCategoryType)Convert.ToInt32(tDataRow["LB"]);       //���
            m_metaIdentifyInfo.DepartName = Convert.ToString(tDataRow["FZDWMC"]);                           //����λ����
            m_metaIdentifyInfo.RelationName = Convert.ToString(tDataRow["LXR"]);                            //��ϵ��
            m_metaIdentifyInfo.RelationPersionTel = Convert.ToString(tDataRow["DH"]);                       //�绰                

            m_metaIdentifyInfo.RelationPersionFax = Convert.ToString(tDataRow["CZ"]);                       //����
            m_metaIdentifyInfo.RelationDepartAddress = Convert.ToString(tDataRow["TXDZ"]);                  //ͨ�ŵ�ַ
            m_metaIdentifyInfo.RelationDepartZip = Convert.ToString(tDataRow["YZBM"]);                    //��������
            m_metaIdentifyInfo.RelationDepartEmail = Convert.ToString(tDataRow["DZXXDZ"]);                  //���������ַ

            m_metaIdentifyInfo.RelationDepartLinkWeb = Convert.ToString(tDataRow["WZ"]);                    //��ַ
            m_metaIdentifyInfo.BgFileName = Convert.ToString(tDataRow["WJMC"]);                             //�ļ�����
            m_metaIdentifyInfo.UseRestrict = (EnumMetaUseRestrictType)Convert.ToInt32(tDataRow["SYXZDM"]);  //ʹ�����ƴ���
            m_metaIdentifyInfo.ClassCode = (EnumMetaSafeCodeType)Convert.ToInt32(tDataRow["AQDJDM"]);       //��ȫ�ȼ�����

            m_metaIdentifyInfo.FormatName = Convert.ToString(tDataRow["GSMC"]);                             //��ʽ����
            m_metaIdentifyInfo.FormatVersion = Convert.ToString(tDataRow["GSBB"]);                        //��ʽ�汾
            m_metaIdentifyInfo.SoftwareName = Convert.ToString(tDataRow["GLRJMC"]);                         //�����������
        }

        /// <summary>
        /// ��������Ϣ���ж�ȡ������ϢԪ��������
        /// </summary>
        /// <param name="pIdentifyTable">������Ϣ��</param>
        private void ReadMetaQulityInfo(DataTable pQulityTable)
        {
            //��ȡ��һ������
            DataRow tDataRow = pQulityTable.Rows[0];
            //ʵ�����¶���
            m_metaQualityInfo = new MetaQualityInfo(); 
            //������������
            m_metaQualityInfo.QualityComment = Convert.ToString(tDataRow["SJZLMS"]);
            //����־
            m_metaQualityInfo.QualityLineage = Convert.ToString(tDataRow["SJZ"]);
        }

        /// <summary>
        /// �ӿռ�ο���Ϣ���ж�ȡ�ռ�ο���ϢԪ��������
        /// </summary>
        /// <param name="pIdentifyTable">�ռ�ο���Ϣ��</param>
        private void ReadMetaSpatialRefInfo(DataTable pSpatialRefTable)
        {
            //��ȡ��һ������
            DataRow tDataRow = pSpatialRefTable.Rows[0];
            //ʵ�����¶���
            m_metaSpatialRefInfo = new MetaSpatialRefInfo();

            m_metaSpatialRefInfo.CoordsRefName = (EnumMetaCoordsRefName)Convert.ToInt32(tDataRow["DDZBCZXTMC"]);     //����������ϵͳ���� 
            m_metaSpatialRefInfo.CoordsType = (EnumMetaCoordsType)Convert.ToInt32(tDataRow["ZBXTLX"]);              //����ϵͳ����
            m_metaSpatialRefInfo.CoordsName = Convert.ToString(tDataRow["ZBXTMC"]);                                 //����ϵͳ���� 
            m_metaSpatialRefInfo.ProjectParameter = Convert.ToString(tDataRow["TYZBXTCS"]);                         //ͶӰ����ϵͳ����
        }

        /// <summary>
        /// ����ϵ��Ϣ���ж�ȡ��ϵ��ϢԪ��������
        /// </summary>
        /// <param name="pIdentifyTable">��ϵ��Ϣ��</param>
        private void ReadMetaRelationShipInfo(DataTable pRelationShipTable)
        {
            //��ȡ��һ������
            DataRow tDataRow = pRelationShipTable.Rows[0];
            //ʵ�����¶���
            m_metaRelationShipInfo = new MetaRelationShipInfo();

            m_metaRelationShipInfo.PublishTime = Convert.ToDateTime(tDataRow["FBRQ"]);            //��������
            m_metaRelationShipInfo.DepartmentName = Convert.ToString(tDataRow["FBDWMC"]);         //������λ����
            m_metaRelationShipInfo.RelationShipName = Convert.ToString(tDataRow["LXR"]);          //��ϵ��
            m_metaRelationShipInfo.DepartmentTel = Convert.ToString(tDataRow["DH"]);              //�绰

            m_metaRelationShipInfo.DepartmentFax = Convert.ToString(tDataRow["CZ"]);              //����
            m_metaRelationShipInfo.DepartmentAddress = Convert.ToString(tDataRow["TXDZ"]);        //ͨ�ŵ�ַ
            m_metaRelationShipInfo.ZipCode = Convert.ToString(tDataRow["YZBM"]);                  //��������
            m_metaRelationShipInfo.DepartmentWeb = Convert.ToString(tDataRow["WZ"]);              //��ַ
        }

        /// <summary>
        /// �ӷַ���Ϣ���ж�ȡ�ַ���ϢԪ��������
        /// </summary>
        /// <param name="pIdentifyTable">�ַ���Ϣ��</param>
        private void ReadMetaDistributeInfo(DataTable pDistributeTable)
        {
            //��ȡ��һ������
            DataRow tDataRow = pDistributeTable.Rows[0];

            //ʵ�����¶���
            m_metaDistributeInfo = new MetaDistributeInfo();

            m_metaDistributeInfo.OnLine = Convert.ToString(tDataRow["ZXLJ"]);                       //��������
            m_metaDistributeInfo.DistributeDepartName = Convert.ToString(tDataRow["FFDWMC"]);       //�ַ���λ����           
            m_metaDistributeInfo.RelationPersonName = Convert.ToString(tDataRow["LXR"]);            //��ϵ��
            m_metaDistributeInfo.RelationPersonTel = Convert.ToString(tDataRow["DH"]);              //�绰

            m_metaDistributeInfo.RelationPersonFax = Convert.ToString(tDataRow["CZ"]);              //����
            m_metaDistributeInfo.RelationDepartAddress = Convert.ToString(tDataRow["TXDZ"]);        //ͨ�ŵ�ַ           
            m_metaDistributeInfo.RelationDepartZip = Convert.ToString(tDataRow["YZBM"]);            //��������
            m_metaDistributeInfo.RelationDepartEmail = Convert.ToString(tDataRow["DZXXDZ"]);        //���������ַ

            m_metaDistributeInfo.RelationDepartLinkWeb = Convert.ToString(tDataRow["WZ"]);          //��ַ     
        }
    }
}
