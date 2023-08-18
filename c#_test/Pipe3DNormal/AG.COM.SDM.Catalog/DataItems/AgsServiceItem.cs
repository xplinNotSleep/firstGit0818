using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
    /// AgsServiceItem ��ժҪ˵����
	/// </summary>
	public class AgsServiceItem:DataItem
	{
        private IAGSServerObjectName m_AgsName = null; 

        /// <summary>
        /// ��ʼ��ʵ������
        /// </summary>
        /// <param name="agsname">IAGSServerObjectName����</param>
        public AgsServiceItem(IAGSServerObjectName agsname)
		{
            m_AgsName = agsname;
			m_DataType = DataType.dtAgsService;
		}

        /// <summary>
        /// ��ȡAGSServerObjectName������
        /// </summary>
		public override string Name
		{
			get
			{
                return m_AgsName.Name;
			}
		} 
        
        /// <summary>
        /// ��ȡAGSServerObjectName����
        /// </summary>
        /// <returns></returns>
        public override object GetGeoObject()
        {
            return m_AgsName;
        }  

        /// <summary>
        /// ��ȡ���Ƿ�����ӽڵ�
        /// </summary>
        public override bool HasChildren
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// ��ȡ���Ƿ���Լ��ص���ͼ������������д򿪵�״ֵ̬
        /// </summary>
        public override bool CanLoad
        {
            get
            {
                return true;
            }
        }
	}
}
