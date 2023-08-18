using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
    /// 栅格数据集数据项 的摘要说明。
	/// </summary>
	public class RasterDatasetItem:DataItem
	{
        private IRasterDatasetName m_RasterDs = null;
        public RasterDatasetItem(IRasterDatasetName rasterDs)
		{
            m_RasterDs = rasterDs;
            m_DataType = DataType.dtRasterDataset;
		}

        /// <summary>
        /// 获取数据项的名称
        /// </summary>
		public override string Name
		{
			get
			{
                return (m_RasterDs as IDatasetName).Name;
			}
		}

        /// <summary>
        /// 获取数据项的别名
        /// </summary>
        public override string AliasName
        {
            get
            {
                return Name; //m_RasterDs as IObjectClass).AliasName;
            }
        }

        /// <summary>
        /// 获取栅格数据集对象
        /// </summary>
        /// <returns>返回栅格数据集对象</returns>
        public override object GetGeoObject()
        {
            return (m_RasterDs as IName).Open();
        }
        
	}


    /// <summary>
    /// 栅格数据目录数据项 描述说明
    /// </summary>
    public class RasterCatalogItem : DataItem
    {
        private IRasterCatalogName m_RasterCat = null;

        /// <summary>
        /// 初始化栅格数据目录数据项实例对象
        /// </summary>
        /// <param name="rasterCatalog">栅格数据目录名称</param>
        public RasterCatalogItem(IRasterCatalogName rasterCatalog)
        {
            m_RasterCat = rasterCatalog;
            m_DataType = DataType.dtRasterCatalog;
        }

        /// <summary>
        /// 获取数据项的名称
        /// </summary>
        public override string Name
        {
            get
            {
                return (m_RasterCat as IDatasetName).Name;
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
        /// 获取栅格数据目录对象
        /// </summary>
        /// <returns>返回栅格数据目录对象</returns>
        public override object GetGeoObject()
        {
            return (m_RasterCat as IName).Open();
        }
    }
}
