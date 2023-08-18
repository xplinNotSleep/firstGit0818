using System;
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;

namespace AG.COM.SDM.Catalog.DataItems
{
    /// <summary>
    /// CAD要素类对象项
    /// </summary>
    public class CadFeatureClassItem:DataItem 
    {
        private string m_filePath;
        private string m_cadType;
        
        /// <summary>
        /// 初始化CAD要素类对象项
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="dtType">数据类型</param>
        public CadFeatureClassItem(string fileName,DataType dtType)
        {
            this.m_filePath = fileName;
            if (dtType == DataType.dtCadAnno)
                this.m_cadType = "Annotation";
            else if (dtType == DataType.dtCadPoint)
                this.m_cadType = "Point";
            else if (dtType == DataType.dtCadPolyline)
                this.m_cadType = "Polyline";
            else if (dtType == DataType.dtCadPolygon)
                this.m_cadType = "Polygon";
            else
                this.m_cadType = "MultiPatch";           

            this.m_DataType = dtType;
        }

        /// <summary>
        /// 返回CAD要素类型名称
        /// </summary>
        public override string Name
        {
            get
            {
                return m_cadType;
            }
        }

        /// <summary>
        /// 获取CAD要素类项
        /// </summary>
        /// <returns>返回Cad FeatureClass对象</returns>
        public override object GetGeoObject()
        {
            try
            {
                string directoryName = System.IO.Path.GetDirectoryName(m_filePath);
                string fileName = System.IO.Path.GetFileName(m_filePath);

                //实例工作空间工厂类
                IWorkspaceFactory tWorkspaceFactory = new CadWorkspaceFactoryClass();
                IWorkspace tWorkspace = tWorkspaceFactory.OpenFromFile(directoryName, 0);

                IFeatureWorkspace tFeatureWorkspace = tWorkspace as IFeatureWorkspace;
                IFeatureClass tFeatureClass = tFeatureWorkspace.OpenFeatureClass(fileName + ":" + m_cadType);

                IFeatureLayer tFeaturelayer = new FeatureLayerClass();
                tFeaturelayer.FeatureClass = tFeatureClass;
                tFeaturelayer.Name = fileName + " " + m_cadType;

                return tFeaturelayer;
            }
            catch
            {
                return null;
            }

        }
    }
}
