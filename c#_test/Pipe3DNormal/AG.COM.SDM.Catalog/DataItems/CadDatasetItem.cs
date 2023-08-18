using System;
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Carto;

namespace AG.COM.SDM.Catalog.DataItems
{
    /// <summary>
    /// CadDrawingDataset对象项
    /// </summary>
    public class CadDatasetItem:DataItem 
    {
        private string m_cadFileName;

        /// <summary>
        /// 初始化实例对象
        /// </summary>
        /// <param name="fileName">文件路径</param>
        public CadDatasetItem(string fileName)
        {
            this.m_cadFileName = fileName;
            this.m_DataType = DataType.dtCadDrawingDataset;
        }

        /// <summary>
        /// 返回文件名称
        /// </summary>
        public override string Name
        {
            get
            {
                return System.IO.Path.GetFileName(m_cadFileName);
            }
        }

        /// <summary>
        /// 获取CAD数据集项
        /// </summary>
        /// <returns>返回CadDrawingDataset对象</returns>
        public override object GetGeoObject()
        {
            try
            {
                string filePath = System.IO.Path.GetDirectoryName(m_cadFileName);
                string fileName = System.IO.Path.GetFileName(m_cadFileName);

                IWorkspaceFactory tWorkspaceFactory = new CadWorkspaceFactoryClass();
                IWorkspace tWorkspace = tWorkspaceFactory.OpenFromFile(filePath, 0);

                ICadDrawingWorkspace tCadDrawingWorkspace = tWorkspace as ICadDrawingWorkspace;
                ICadDrawingDataset tCadDrawingDataset = tCadDrawingWorkspace.OpenCadDrawingDataset(fileName);

                return tCadDrawingDataset;
            }
            catch
            {
                return null;
            }
        }
    }
}
