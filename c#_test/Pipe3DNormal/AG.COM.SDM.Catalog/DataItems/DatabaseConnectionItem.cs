using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using System.Threading;

namespace AG.COM.SDM.Catalog.DataItems
{
    /// <summary>
    /// 数据库工作空间数据项
    /// </summary>
    public class DatabaseWorkspaceItem : DataItem
    {
        private IWorkspace m_Workspace = null;

        /// <summary>
        /// 初始化数据库工作空间实例对象
        /// </summary>
        /// <param name="workspace">工作空间</param>
        public DatabaseWorkspaceItem(IWorkspace workspace)
		{
            m_Workspace = workspace; 
		}

        /// <summary>
        /// 获取数据项的名称
        /// </summary>
        public override string Name
		{
			get
			{
                return m_Workspace.PathName;
			}
		}

        /// <summary>
        /// 获取数据库工作空间对象
        /// </summary>
        /// <returns></returns>
        public override object GetGeoObject()
        {
            return m_Workspace;
        }

        public override IList<DataItem> GetChildren()
        { 
            IList<DataItem> items = GetChildrenGeneral(m_Workspace);
            for (int i=0;i<=items.Count-1;i++)
            {
                items[i].Parent = this;
            }

            return items;
        }

        public static IList<DataItem> GetChildrenGeneral(IWorkspace ws)
        {
            IList<DataItem> items = new List<DataItem>();
            if (ws == null) return items;
            IEnumDatasetName pEnum = ws.get_DatasetNames(esriDatasetType.esriDTAny);
            IDatasetName dsName = pEnum.Next();
            DataItem item;
            while (dsName != null)
            {
                if (dsName.Type == esriDatasetType.esriDTTable)
                {                   
                    item = new TableItem(dsName as ITableName);
                    item.Workspace = ws;
                    items.Add(item);

                }                 
                else if (dsName.Type == esriDatasetType.esriDTFeatureDataset)
                {
                    item = new FeatureDatasetItem(dsName as IFeatureDatasetName);
                    item.Workspace = ws;
                    items.Add(item);
                }
                else if (dsName.Type == esriDatasetType.esriDTTopology)
                {
                    //topology应该不会出现在workspace下面
                    System.Windows.Forms.MessageBox.Show("Topology should not come here");
                }
                else if (dsName.Type == esriDatasetType.esriDTFeatureClass)
                {
                    item = new FeatureClassItem(dsName as IFeatureClassName);
                    item.Workspace = ws;
                    //fclsitem.Parent = this;
                    items.Add(item);
                }
                else if (dsName.Type == esriDatasetType.esriDTRasterCatalog)
                {
                    //System.Windows.Forms.MessageBox.Show("esriDTRasterCatalog");
                    item = new RasterCatalogItem(dsName as IRasterCatalogName);
                    items.Add(item);
                }
                else if (dsName.Type == esriDatasetType.esriDTRasterDataset)
                {
                    //System.Windows.Forms.MessageBox.Show("esriDTRasterDataset");
                    item = new RasterDatasetItem(dsName as IRasterDatasetName);
                    item.Workspace = ws;
                    items.Add(item);
                }
                else if (dsName.Type == esriDatasetType.esriDTRelationshipClass)
                {
                    //IRelationshipClassName rel = dsName as IRelationshipClassName;
                     
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("unknown");
                }


                dsName = pEnum.Next();
            }


            return items;
        }

        public override bool HasChildren
        {
            get
            {
                return true;
            }
        }

        public override bool CanLoad
        {
            get
            {
                return false;
            }
        }
    }


	/// <summary>
	/// 数据库连接项 的摘要说明。
	/// </summary>
    public class DatabaseConnectionItem : DatabaseWorkspaceItem, IFileNameItem
	{
		private string m_FileName = "";
        public DatabaseConnectionItem(string connFileName):base(null)
		{
            m_FileName = connFileName;
			m_DataType = DataType.dtSdeConnection;
		}

        /// <summary>
        /// 获取数据项的名称
        /// </summary>
		public override string Name
		{
			get
			{
                //return System.IO.Path.GetFileNameWithoutExtension(m_FileName);
                return "Database Connections\\" + System.IO.Path.GetFileName(m_FileName);
			}
		}

        /// <summary>
        /// 获取文件名
        /// </summary>
        public string FileName
        {
            get { return m_FileName; }
        }
         
        public void GetGeoObject_NewThread()
        {
            try
            {
                IWorkspaceName pWorspaceName = new ESRI.ArcGIS.Geodatabase.WorkspaceNameClass();
                pWorspaceName.WorkspaceFactoryProgID = "esriDataSourcesGDB.SdeWorkspaceFactory";
                pWorspaceName.PathName = m_FileName;

                m_GeoObject = (pWorspaceName as IName).Open();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + ex.StackTrace);
            }

            //evt.Set();
        }

        private object m_GeoObject = null; 

        /// <summary>
        /// 获取数据库连接对象
        /// </summary>
        /// <returns>返回数据库连接对象</returns>
        public override object GetGeoObject()
        {
            if (m_GeoObject == null)
                GetGeoObject_NewThread();
            return m_GeoObject;
        } 

        public override IList<DataItem> GetChildren()
        {
            object obj = GetGeoObject();
            IList<DataItem> items = GetChildrenGeneral(obj as IWorkspace);
            for (int i=0;i<=items.Count-1;i++)
            {
                items[i].Parent = this;
            }

            return items;
        }          
	}
}
