using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
	/// ���Ա������� ��ժҪ˵����
	/// </summary>
	public class TableItem:DataItem
	{
		private ITableName m_Table = null;

        /// <summary>
        /// ��ʼ�����Ա��������ʵ������
        /// </summary>
        /// <param name="table"></param>
        public TableItem(ITableName table)
		{
            m_Table = table;
             
			m_DataType = DataType.dtTable;
		}

        /// <summary>
        /// ��ȡ�����������
        /// </summary>
		public override string Name
		{
			get
			{
                return (m_Table as IDatasetName).Name;
			}
		}

        /// <summary>
        /// ��ȡ������ı���
        /// </summary>
        public override string AliasName
        {
            get
            {
                 return Name;
            }
        }

        /// <summary>
        /// ��ȡ���Ա����
        /// </summary>
        /// <returns>�������Ա����</returns>
        public override object GetGeoObject()
        {
            return (m_Table as IName).Open();
        }        
	}
}
