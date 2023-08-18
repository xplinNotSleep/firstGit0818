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
    /// AgsServiceItem 的摘要说明。
	/// </summary>
	public class AgsServiceItem:DataItem
	{
        private IAGSServerObjectName m_AgsName = null; 

        /// <summary>
        /// 初始化实例对象
        /// </summary>
        /// <param name="agsname">IAGSServerObjectName对象</param>
        public AgsServiceItem(IAGSServerObjectName agsname)
		{
            m_AgsName = agsname;
			m_DataType = DataType.dtAgsService;
		}

        /// <summary>
        /// 获取AGSServerObjectName的名称
        /// </summary>
		public override string Name
		{
			get
			{
                return m_AgsName.Name;
			}
		} 
        
        /// <summary>
        /// 获取AGSServerObjectName对象
        /// </summary>
        /// <returns></returns>
        public override object GetGeoObject()
        {
            return m_AgsName;
        }  

        /// <summary>
        /// 获取其是否包含子节点
        /// </summary>
        public override bool HasChildren
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 获取其是否可以加载到地图或在软件界面中打开的状态值
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
