using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
    /// 拓扑数据项 的摘要说明。
	/// </summary>
	public class TopologyItem:DataItem
	{
        private ITopologyName m_Topology = null;

        /// <summary>
        /// 初始化拓扑数据项的实例对象
        /// </summary>
        /// <param name="topology">拓扑数据项</param>
        public TopologyItem(ITopologyName topology)
		{
            m_Topology = topology;
            m_DataType = DataType.dtTopology;
		}

        /// <summary>
        /// 获取数据项的名称
        /// </summary>
		public override string Name
		{
			get
			{
                return (m_Topology as IDatasetName).Name;
			}
		}

        /// <summary>
        /// 获取拓扑对象
        /// </summary>
        /// <returns>返回拓扑对象</returns>
        public override object GetGeoObject()
        {
            return (m_Topology as IName).Open();
        }
        
	}
}
