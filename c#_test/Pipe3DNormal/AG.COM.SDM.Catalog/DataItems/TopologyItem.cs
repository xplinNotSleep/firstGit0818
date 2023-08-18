using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
    /// ���������� ��ժҪ˵����
	/// </summary>
	public class TopologyItem:DataItem
	{
        private ITopologyName m_Topology = null;

        /// <summary>
        /// ��ʼ�������������ʵ������
        /// </summary>
        /// <param name="topology">����������</param>
        public TopologyItem(ITopologyName topology)
		{
            m_Topology = topology;
            m_DataType = DataType.dtTopology;
		}

        /// <summary>
        /// ��ȡ�����������
        /// </summary>
		public override string Name
		{
			get
			{
                return (m_Topology as IDatasetName).Name;
			}
		}

        /// <summary>
        /// ��ȡ���˶���
        /// </summary>
        /// <returns>�������˶���</returns>
        public override object GetGeoObject()
        {
            return (m_Topology as IName).Open();
        }
        
	}
}
