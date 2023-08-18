using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
    /// դ�����ݼ������� ��ժҪ˵����
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
        /// ��ȡ�����������
        /// </summary>
		public override string Name
		{
			get
			{
                return (m_RasterDs as IDatasetName).Name;
			}
		}

        /// <summary>
        /// ��ȡ������ı���
        /// </summary>
        public override string AliasName
        {
            get
            {
                return Name; //m_RasterDs as IObjectClass).AliasName;
            }
        }

        /// <summary>
        /// ��ȡդ�����ݼ�����
        /// </summary>
        /// <returns>����դ�����ݼ�����</returns>
        public override object GetGeoObject()
        {
            return (m_RasterDs as IName).Open();
        }
        
	}


    /// <summary>
    /// դ������Ŀ¼������ ����˵��
    /// </summary>
    public class RasterCatalogItem : DataItem
    {
        private IRasterCatalogName m_RasterCat = null;

        /// <summary>
        /// ��ʼ��դ������Ŀ¼������ʵ������
        /// </summary>
        /// <param name="rasterCatalog">դ������Ŀ¼����</param>
        public RasterCatalogItem(IRasterCatalogName rasterCatalog)
        {
            m_RasterCat = rasterCatalog;
            m_DataType = DataType.dtRasterCatalog;
        }

        /// <summary>
        /// ��ȡ�����������
        /// </summary>
        public override string Name
        {
            get
            {
                return (m_RasterCat as IDatasetName).Name;
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
        /// ��ȡդ������Ŀ¼����
        /// </summary>
        /// <returns>����դ������Ŀ¼����</returns>
        public override object GetGeoObject()
        {
            return (m_RasterCat as IName).Open();
        }
    }
}
