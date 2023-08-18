using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
	/// 属性表数据项 的摘要说明。
	/// </summary>
	public class TableItem:DataItem
	{
		private ITableName m_Table = null;

        /// <summary>
        /// 初始化属性表数据项的实例对象
        /// </summary>
        /// <param name="table"></param>
        public TableItem(ITableName table)
		{
            m_Table = table;
             
			m_DataType = DataType.dtTable;
		}

        /// <summary>
        /// 获取数据项的名称
        /// </summary>
		public override string Name
		{
			get
			{
                return (m_Table as IDatasetName).Name;
			}
		}

        /// <summary>
        /// 获取数据项的别名
        /// </summary>
        public override string AliasName
        {
            get
            {
                 return Name;
            }
        }

        /// <summary>
        /// 获取属性表对象
        /// </summary>
        /// <returns>返回属性表对象</returns>
        public override object GetGeoObject()
        {
            return (m_Table as IName).Open();
        }        
	}
}
