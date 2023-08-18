using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.Catalog.DataItems
{
    /// <summary>
    /// LayerFile图层
    /// </summary>
    public class LayerFileItem : DataItem
    {
        private string m_FileName = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">文件路径</param>
        public LayerFileItem(string fileName)
        {
            m_FileName = fileName;
            m_DataType = DataType.dtLayerFile;
        }

        /// <summary>
        /// 获取数据项的名称
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
                ILayerFile tLayerFile = new LayerFileClass();
                tLayerFile.Open(m_FileName);

                return tLayerFile;
            }
            catch
            {
                return null;
            }
        }
    }
}
