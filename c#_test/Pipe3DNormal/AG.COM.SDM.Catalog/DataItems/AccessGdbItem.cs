using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
	/// AccessGDB 的摘要说明。
	/// </summary>
	public class AccessGdbItem:DataItem,IFileNameItem
	{
		private string m_FileName = "";

        /// <summary>
        /// 实例化个人数据库项
        /// </summary>
        /// <param name="fileName">文件名称</param>
        public AccessGdbItem(string fileName)
		{
            m_FileName = fileName;
			m_DataType = DataType.dtAccess;
		}

        /// <summary>
        /// 获取AccessGDB项名称
        /// </summary>
		public override string Name
		{
			get
			{
                //原来是GetFileNameWithoutExtension，但是ArcMap GP的格式是带.gdb的，因此改为用GetFileName
                //return System.IO.Path.GetFileNameWithoutExtension(m_FileName);
                return System.IO.Path.GetFileName(m_FileName);
			}
		}

        /// <summary>
        /// 获取个人数据库项文件名
        /// </summary>
        public string FileName
        {
            get { return m_FileName; }
        }

        private object m_GeoObject = null;

        /// <summary>
        /// 获取GeoObject对象
        /// </summary>
        /// <returns>返回个人数据库中的数据项，如果为空则返回null</returns>
        public override object GetGeoObject()
        {
            if (m_GeoObject == null)
            {
                try
                {
                    IWorkspaceName pWorspaceName = new ESRI.ArcGIS.Geodatabase.WorkspaceNameClass();
                    pWorspaceName.WorkspaceFactoryProgID = "esriDataSourcesGDB.AccessWorkspaceFactory";
                    pWorspaceName.PathName = m_FileName;

                    m_GeoObject = (pWorspaceName as IName).Open();
                }
                catch (Exception ex)
                {

                    return m_GeoObject;
                }
              
            }
            return m_GeoObject;
        }

        /// <summary>
        /// 获取其子项
        /// </summary>
        /// <returns>返回该对象的子项集合</returns>
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

        /// <summary>
        /// 获取其是否含有子项的状态值
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
	}
}
