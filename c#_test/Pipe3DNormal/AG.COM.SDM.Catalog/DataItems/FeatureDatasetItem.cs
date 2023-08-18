using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
	/// 矢量数据集数据项 的摘要说明。
	/// </summary>
	public class FeatureDatasetItem:DataItem
	{
        private IFeatureDatasetName m_Dataset = null;
        public FeatureDatasetItem(IFeatureDatasetName dataset)
		{
            m_Dataset  = dataset;
            m_DataType = DataType.dtFeatureDataset;
 		}

		public override string Name
		{
			get
			{
                return (m_Dataset as IDatasetName).Name;
			}
		}

        public override object GetGeoObject()
        {
            //IDatasetName dataset = m_Dataset as IDatasetName;
            //if (dataset == null) return null;

            //Workspace = (dataset as IName).Open() as IWorkspace;
            //return Workspace;
            IWorkspaceName workspaceName = (m_Dataset as IDatasetName).WorkspaceName;
            IName pTempName = (IName)workspaceName;
            IWorkspace pTempWorkspace = (IWorkspace)pTempName.Open();
            return pTempWorkspace;
        }

        public override IList<DataItem> GetChildren()
        {
            object obj = GetGeoObject();
            IList<DataItem> items = new List<DataItem>();
            IEnumDatasetName pEnum = (m_Dataset as IDatasetName).SubsetNames;
            IDatasetName dsName = pEnum.Next();
            while (dsName != null)
            {
                DataItem item;
                if (dsName is IFeatureClassName)
                {
                    item = new FeatureClassItem(dsName as IFeatureClassName, obj as IWorkspace);
                    item.Parent = this;
                    items.Add(item);
                }
                else if (dsName is ITableName)
                {
                    //should not get here
                    item = new TableItem(dsName as ITableName);
                    item.Parent = this;
                    items.Add(item);
                }
                else if (dsName is ITopologyName)
                {
                    item = new TopologyItem(dsName as ITopologyName);
                    item.Parent = this;
                    items.Add(item);
                }
                else if (dsName is IGeometricNetworkName)
                {
                    //System.Windows.Forms.MessageBox.Show("network");
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("other");
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
}
