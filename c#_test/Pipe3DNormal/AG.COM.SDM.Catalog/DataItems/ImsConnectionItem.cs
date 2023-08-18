using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using System.Threading;
using ESRI.ArcGIS.GISClient;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
	/// ArcIMS���������� ��ժҪ˵����
	/// </summary>
    public class ImsConnectionItem : DataItem, IFileNameItem
	{
		private string m_FileName = "";

        /// <summary>
        /// ��ʼ��ArcIMS����������ʵ������
        /// </summary>
        /// <param name="connFileName"></param>
        public ImsConnectionItem(string connFileName)
		{
            m_FileName = connFileName;
			m_DataType = DataType.dtImsService;
		}

        /// <summary>
        /// ��ȡ����������
        /// </summary>
		public override string Name
		{
			get
			{
                return System.IO.Path.GetFileNameWithoutExtension(m_FileName);
			}
		}
    
        /// <summary>
        /// ��ȡ���������
        /// </summary>
        /// <returns>�����Ϊ���򷵻�IIMSMapLayer����򷵻ؿ�</returns>
        public override object GetGeoObject()
        {
            IMemoryBlobStream stream = new MemoryBlobStreamClass();

            ESRI.ArcGIS.Carto.IIMSMapLayer layer = null;
            try
            {              
                stream.LoadFromFile(m_FileName);
                IIMSServiceDescription service = new IMSServiceNameClass();
                (service as IPersistStream).Load(stream);

                layer = new ESRI.ArcGIS.Carto.IMSMapLayerClass();
                layer.ConnectToService(service);
            }         
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(stream);
            }

            return layer;
        }  

        public override bool HasChildren
        {
            get
            {
                return false;
            }
        }

        public override bool CanLoad
        {
            get
            {
                return true;
            }
        }

        #region IFileNameItem ��Ա

        /// <summary>
        /// ��ȡ�ļ�����
        /// </summary>
        public string FileName
        {
            get { return m_FileName; }
        }

        #endregion
    }
}
