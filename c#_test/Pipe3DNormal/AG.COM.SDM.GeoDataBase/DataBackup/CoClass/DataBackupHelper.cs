using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AG.COM.SDM.Model;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase.DataBackup
{
    /// <summary>
    /// 数据备份帮助类
    /// </summary>
    public class DataBackupHelper
    {
        /// <summary>
        /// 获取选择的数据项
        /// </summary>
        /// <param name="tTree"></param>
        /// <returns></returns>
        public static List<DBDataItem> GetSelectData(TreeView tTree)
        {
            List<DBDataItem> result = new List<DBDataItem>();

            AddChildData(tTree.Nodes, result);

            return result;
        }

        /// <summary>
        /// 添加选择的数据项
        /// </summary>
        /// <param name="tNodeColl"></param>
        /// <param name="tDBDataItems"></param>
        private static void AddChildData(TreeNodeCollection tNodeColl, List<DBDataItem> tDBDataItems)
        {
            foreach (TreeNode tNode in tNodeColl)
            {
                if (tNode.Checked == true)
                {
                    if (tNode.Tag is AGSDM_DATASET)
                    {
                        DBDataItem tDBDataItem = new DBDataItem();
                        tDBDataItem.Name = (tNode.Tag as AGSDM_DATASET).DATASET_NAME;
                        tDBDataItem.Type = DataType.DataSet;
                        tDBDataItems.Add(tDBDataItem);
                        //继续添加子节点
                        AddChildData(tNode.Nodes, tDBDataItem.Childs);
                    }
                    else if (tNode.Tag is AGSDM_LAYER)
                    {
                        DBDataItem tDBDataItem = new DBDataItem();
                        tDBDataItem.Name = (tNode.Tag as AGSDM_LAYER).LAYER_TABLE;
                        tDBDataItem.Type = DataType.FeatureClass;
                        tDBDataItems.Add(tDBDataItem);
                    }
                }
            }
        }

        /// <summary>
        /// 连接数据源，获取数据源Workspace
        /// </summary>
        /// <param name="tDataSource"></param>
        /// <returns></returns>
        public static IWorkspace2 ConnectDataSource(AGSDM_DATASOURCE tDataSource)
        {
            IPropertySet tPropertySet = new PropertySetClass();
            tPropertySet.SetProperty("Server", tDataSource.IP);
            tPropertySet.SetProperty("Instance", tDataSource.SERVICE_NAME);
            tPropertySet.SetProperty("User", tDataSource.USER_NAME);
            tPropertySet.SetProperty("Password", tDataSource.PASSWORD);
            tPropertySet.SetProperty("Version", "SDE.DEFAULT");

            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory");
            IWorkspaceFactory tWorkspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
            return tWorkspaceFactory.Open(tPropertySet, 0) as IWorkspace2;
        }

        /// <summary>
        /// 创建FileGDB库
        /// </summary>
        /// <returns></returns>
        public static IFeatureWorkspace CreateFileGDB(string Path, string Name)
        {
            IWorkspaceFactory tWorkspaceFactory = new FileGDBWorkspaceFactoryClass();
            IName pName = (IName)tWorkspaceFactory.Create(Path + "\\", Name, null, 0);
            return (IFeatureWorkspace)pName.Open();
        }

        /// <summary>
        /// 计算DBDataItem集合的总数（因为DBDataItem.Child属性还有其子集合，这部分也计算到）
        /// </summary>
        /// <param name="tDBDataItems"></param>
        /// <returns></returns>
        public static int GetDataItemCount(List<DBDataItem> tDBDataItems)
        {
            int result = 0;

            AddDBDataItemCount(tDBDataItems, ref result);

            return result;
        }

        /// <summary>
        /// 计算DBDataItem集合的总数
        /// </summary>
        /// <param name="tDBDataItems"></param>
        /// <param name="count"></param>
        private static void AddDBDataItemCount(List<DBDataItem> tDBDataItems, ref int count)
        {
            foreach (DBDataItem tDBDataItem in tDBDataItems)
            {
                count++;

                AddDBDataItemCount(tDBDataItem.Childs, ref count);
            }
        }
    }
}
