using System;
using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DMetaData
{
    /// <summary>
    /// ��ʶ��ϢԪ����
    /// </summary>
    public class MetaIdentifyInfo:MetaDataInfo
    {       
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public MetaIdentifyInfo()
        {
            this.m_metaTableType = EnumMetaTableType.��ʶ��Ϣ;
        }

        private string m_resTitle;
        /// <summary>
        /// ��ȡ���������ݼ�����
        /// </summary>
        [Category("������Ϣ"), Description("���ݼ�����")]
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

        // ���ݼ������������������
        private DateTime m_resRefDate=DateTime.Now;
        /// <summary>
        /// ��ȡ���������ݼ������������������
        /// </summary>
        [Category("������Ϣ"), Description("����")]
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

        //���ݼ��汾
        private string m_resEdition;
        /// <summary>
        /// ��ȡ���������ݼ��汾
        /// </summary>
        [Category("������Ϣ"), Description("���ݼ��汾")]
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
        /// ��ȡ����������
        /// </summary>
        [Category("������Ϣ"), Description("����")]
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
        /// ��ȡ������ժҪ
        /// </summary>
        [Category("������Ϣ"), Description("ժҪ")]
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
        /// ��ȡ��������״
        /// </summary>
        [Category("������Ϣ"), Description("��״")]
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
        /// ��ȡ�������������÷���
        /// </summary>
        [Category("������Ϣ"), Description("�������÷���")]
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
        /// ��ȡ���������߾���
        /// </summary>
        [Category("����Χ"),Description("���߾���")]
        public double WestBL
        {
            get
            {
                return this.m_westBL;
            }
            set
            {
                if (value > 180 || value < -180)
                    throw new Exception("ֵ������Ч��Χ��,��������Ч�ľ��ȷ�Χ");
                else 
                    this.m_westBL = value;
            }
        }

        private double m_eastBL;
        /// <summary>
        /// ��ȡ�����ö��߾���
        /// </summary>
        [Category("����Χ"),Description("���߾���")]
        public double EastBL
        {
            get
            {
                return this.m_eastBL;
            }
            set
            {
                if (value > 180 || value < -180)
                    throw new Exception("ֵ������Ч��Χ��,��������Ч�ľ��ȷ�Χ");
                else
                    this.m_eastBL = value;
            }
        }

        private double m_northBL;
        /// <summary>
        /// ��ȡ�����ñ���γ��
        /// </summary>
        [Category("����Χ"),Description("����γ��")]
        public double NorthBL
        {
            get
            {
                return this.m_northBL;
            }
            set
            {
                if (value > 90 || value < -90)
                    throw new Exception("ֵ������Ч��Χ��,��������Ч�ľ��ȷ�Χ");
                else
                    this.m_northBL = value;
            }
        }

        private double m_southBL;
        /// <summary>
        /// ��ȡ�������ϱ�γ��
        /// </summary>
        [Category("����Χ"),Description("�ϱ�γ��")]
        public double SourthBL
        {
            get
            {
                return this.m_southBL;
            }
            set
            {
                if (value > 90 || value < -90)
                    throw new Exception("ֵ������Ч��Χ��,��������Ч�ľ��ȷ�Χ");
                else
                    this.m_southBL = value;
            }
        }

        private string m_geoID;
        /// <summary>
        /// ��ȡ�����õ����ʶ��
        /// </summary>
        [Description("�����ʶ��")]
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
        /// ��ȡ��������ֹʱ��
        /// </summary>
        [Category("���ݼ�"), Description("��ֹʱ��")]
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
        /// ��ȡ�����ñ�ʾ��ʽ
        /// </summary>
        [Category("���ݼ�"), Description("��ʾ��ʽ")]
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
        /// ��ȡ�����ÿռ�ֱ���
        /// </summary>
        [Category("���ݼ�"), Description("�ռ�ֱ���")]
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
        /// ��ȡ���������ݼ�רҵ��ר���������
        /// </summary>
        [Category("���ݼ�"), Description("�������")]
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
        /// ��ȡ���������ݼ�����λ����
        /// </summary>
        [Category("��ϵ��Ϣ"),Description("��λ����")]
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

        //��ϵ������
        private string m_relPersionName;
        /// <summary>
        /// ��ȡ��������ϵ������
        /// </summary>
        [Category("��ϵ��Ϣ"), Description("��ϵ������")]
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

        //��ϵ�˵绰
        private string m_relPersionTel;
        /// <summary>
        ///  ��ȡ���������ݼ�����λ����ϵ�˵绰
        /// </summary>
        [Category("��ϵ��Ϣ"), Description("��ϵ�˵绰")]
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
        /// ��ȡ���������ݼ�����λ����ϵ�˵Ĵ������
        /// </summary>
        [Category("��ϵ��Ϣ"), Description("�������")]
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
        /// ��ȡ���������ݼ�����λ����ϵ�˵�ͨ�ŵ�ַ
        /// </summary>
        [Category("��ϵ��Ϣ"), Description("ͨ�ŵ�ַ")]
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
        /// ��ȡ���������ݼ�����λ��������
        /// </summary>
        [Category("��ϵ��Ϣ"), Description("��������")]
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
        /// ��ȡ���������ݼ�����λ����ϵ�˵�������
        /// </summary>
        [Category("��ϵ��Ϣ"), Description("��������")]
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
        /// ��ȡ���������ݼ�����λ�������ַ
        /// </summary>
        [Category("��ϵ��Ϣ"), Description("�����ַ")]
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
        /// ��ȡ�����þ�̬���ͼ���ļ�����
        /// </summary>
        [Category("������Ϣ"), Description("��̬���ͼ���ļ�����")]
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

        private EnumMetaUseRestrictType m_useRestrict=EnumMetaUseRestrictType.��Ȩ;
        /// <summary>
        /// ��ȡ������ʹ�����ƴ���
        /// </summary>
        [Category("���ݼ�����"), Description("ʹ�����ƴ���"),DefaultValue(EnumMetaUseRestrictType.��Ȩ)]
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

        private EnumMetaSafeCodeType m_safeCode=EnumMetaSafeCodeType.�ڲ�;
        /// <summary>
        /// ��ȡ�����ð�ȫ�ȼ�����
        /// </summary>
        [Category("���ݼ�����"), Description("��ȫ�ȼ�����"),DefaultValue(EnumMetaSafeCodeType.�ڲ�)]
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
        /// ��ȡ���������ݼ��ַ����ṩ�����ݽ�����ʽ����
        /// </summary>
        [Category("���ݼ�����"), Description("��ʽ����")]
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
        /// ��ȡ���������ݼ��ַ����ṩ�����ݽ�����ʽ�汾
        /// </summary>
        [Category("�������"), Description("�汾")]
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
        /// ��ȡ�����ö����ݼ�����ά���͹�����������
        /// </summary>
        [Category("�������"), Description("����")]
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
