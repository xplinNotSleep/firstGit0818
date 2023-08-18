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
    /// ExcelFile图层
    /// </summary>
    public class ExcelFileItem : DataItem
    {
        string m_FileName = "";
        IDataset m_Dataset = null;
        object m_GeoObject = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">文件路径</param>
        public ExcelFileItem(string fileName)
        {
            m_FileName = fileName;
            m_DataType = DataType.dtExcel;
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

        /// <summary>
        /// 获取其是否还有子节点
        /// </summary>
        public override bool HasChildren
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 获取其是否可以加载到地图或在软件界面中打开的状态值
        /// </summary>
        public override bool CanLoad
        {
            get
            {
                return false;
            }
        }

        public void GetGeoObject_NewThread()
        {
            try
            {
                if (System.IO.File.Exists(m_FileName) == false)
                {
                    throw new Exception("找不到文件数据 " + m_FileName);
                }

                ESRI.ArcGIS.esriSystem.IPropertySet proset = new ESRI.ArcGIS.esriSystem.PropertySetClass();
                string strcon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + m_FileName + @";Extended Properties=Excel 8.0";
                proset.SetProperty("CONNECTSTRING", strcon);
                IWorkspaceFactory workspf = new ESRI.ArcGIS.DataSourcesOleDB.OLEDBWorkspaceFactoryClass();
                IFeatureWorkspace fworkfac = workspf.Open(proset, 0) as IFeatureWorkspace;

                IDataset ds = fworkfac as IDataset;
                m_GeoObject = ds;
            }
            catch (Exception ex)
            {
                
            }
            
        }

        public override object GetGeoObject()
        {
            if (m_GeoObject == null)
                GetGeoObject_NewThread();

            return m_GeoObject;

        }

        public override IList<DataItem> GetChildren()
        {
            object obj = GetGeoObject();
            IList<DataItem> items = DatabaseConnectionItem.GetChildrenGeneral(obj as IWorkspace);
            for (int i = 0; i <= items.Count - 1; i++)
            {
                items[i].Parent = this;
            }

            return items;
        }
    }
}
