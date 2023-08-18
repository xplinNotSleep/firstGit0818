using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
	/// Image文件数据项 的摘要说明。
	/// </summary>
	public class ImageFileItem:DataItem
	{
		private string m_FileName = "";

        /// <summary>
        /// 初始化Image文件数据项实例
        /// </summary>
        /// <param name="fileName">文件路径</param>
        public ImageFileItem(string fileName)
		{
            m_FileName = fileName;
			m_DataType = DataType.dtImageFile;
		}

        /// <summary>
        /// 获取数据项名称
        /// </summary>
		public override string Name
		{
			get
			{
                return System.IO.Path.GetFileName(m_FileName);
			}
		}

        public override object GetGeoObject()
        {
            try
            {
                IDatasetName pDatasetName = new RasterDatasetNameClass();
                IWorkspaceName pWorspaceName = new ESRI.ArcGIS.Geodatabase.WorkspaceNameClass();
                pWorspaceName.WorkspaceFactoryProgID = "esriDataSourcesRaster.RasterWorkspaceFactory";
                pWorspaceName.PathName = System.IO.Path.GetDirectoryName(m_FileName);               
                pDatasetName.Name = System.IO.Path.GetFileName(m_FileName);
                if (string.IsNullOrEmpty( pDatasetName.Name))//是切片栅格时，没有后缀
                {
                    string[] dirName = m_FileName.Split('\\');
                    pDatasetName.Name = dirName[dirName.Length - 1];
                       
                }
                pDatasetName.WorkspaceName = pWorspaceName;

                return (pDatasetName as IName).Open();
            }
            catch //(Exception ex)
            {
                //这里写日志
                return null;
            }
        }
        
	}
}
