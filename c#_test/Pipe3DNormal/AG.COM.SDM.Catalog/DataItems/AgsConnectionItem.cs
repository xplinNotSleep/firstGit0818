using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using System.Threading;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Carto;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
	/// ArcGIS Server������ ��ժҪ˵����
	/// </summary>
    public class AgsConnectionItem : DataItem, IFileNameItem
	{
		private string m_FileName = "";

        /// <summary>
        /// ��ʼ��ArcGIS Server������ʵ��
        /// </summary>
        /// <param name="connFileName">�����ļ�����</param>
        public AgsConnectionItem(string connFileName)
		{
            m_FileName = connFileName;
			m_DataType = DataType.dtAgsService;
		}

        /// <summary>
        /// ��ȡ�����������
        /// </summary>
		public override string Name
		{
			get
			{
                return System.IO.Path.GetFileNameWithoutExtension(m_FileName);
			}
		}

        /// <summary>
        /// ��ȡArcGIS Server����
        /// </summary>
        /// <returns></returns>
        public override object GetGeoObject()
        {          
            IAGSServerConnectionFactory2 acf = new AGSServerConnectionFactoryClass();
            IPropertySet pset = acf.ReadConnectionPropertiesFromFile(m_FileName);

            IAGSServerConnection con = acf.Open(pset, 0);
            return con;
        }

        public override IList<DataItem> GetChildren()
        {
            IList<DataItem> items = new List<DataItem>();
            IAGSServerConnection con = GetGeoObject() as IAGSServerConnection;
            if (con == null)
                return items;
            IAGSEnumServerObjectName pEnum = con.ServerObjectNames;
            pEnum.Reset();
            IAGSServerObjectName agsname = pEnum.Next();
            while (agsname != null)
            {
                if (agsname.Type == "MapServer")
                {                                     
                    DataItem item = new AgsServiceItem(agsname);
                    item.Parent = this;
                    items.Add(item);
                }
                agsname = pEnum.Next();
            }

            return items;
        }


        public override bool HasChildren
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// ��ȡ���Ƿ���Լ��ص���ͼ������������д򿪵�״ֵ̬
        /// </summary>
        public override bool CanLoad
        {
            get
            {
                return false;
            }
        }

        #region IFileNameItem ��Ա

        public string FileName
        {
            get 
            {
                return m_FileName;
            }
        }

        #endregion
    }
}
