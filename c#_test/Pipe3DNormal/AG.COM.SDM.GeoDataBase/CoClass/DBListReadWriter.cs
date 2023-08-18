using System;
using System.Windows.Forms;
using AG.COM.SDM.GeoDataBase.DBuilder;
using AG.COM.SDM.Utility;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// AM_SYSTEM_DataBaseList 表读写操作类
    /// </summary>
    public class DBListReadWriter
    {
        /// <summary>
        /// 保存配置信息到数据库
        /// </summary>
        /// <param name="pTreeView">树对象</param>
        /// <param name="pFeatureWorkspace">工作空间</param>
        public static void SaveLayerListInfoToDB(TreeView pTreeView,IFeatureWorkspace pFeatureWorkspace)
        {
            try
            {
                if (pFeatureWorkspace == null) return ;

                string strListTableName = "AM_System_databaselist";

                //从指定工作空间中打开指定名称的属性表
                ITable listTable = GeoTableHandler.OpenTable(strListTableName, pFeatureWorkspace);
                //删除所有行
                listTable.DeleteSearchedRows(null);

                //设置游标类型
                ICursor tCursor = listTable.Insert(true);
                //设置行缓冲
                IRowBuffer tRowBuffer = listTable.CreateRowBuffer();

                for (int i = 0; i < pTreeView.Nodes.Count; i++)
                {
                    TreeNode firstNode = pTreeView.Nodes[i];
                    //设置当前节点编号
                    string strItemID = "0" + Convert.ToString(i + 1).PadLeft(2, '0');
                    //对象类型转换
                    ItemProperty tItemProperty = firstNode.Tag as ItemProperty;
                    if (tItemProperty != null)
                    {
                        tRowBuffer.set_Value(tRowBuffer.Fields.FindField("ItemName"), tItemProperty.ItemName);
                        tRowBuffer.set_Value(tRowBuffer.Fields.FindField("ItemAliasName"), tItemProperty.ItemAliasName);
                        tRowBuffer.set_Value(tRowBuffer.Fields.FindField("NodeType"), (int)tItemProperty.DataNodeItem);
                        tRowBuffer.set_Value(tRowBuffer.Fields.FindField("ParentID"), "0");
                        tRowBuffer.set_Value(tRowBuffer.Fields.FindField("ItemID"), strItemID);
                    }

                    //插入当前行
                    tCursor.InsertRow(tRowBuffer);

                    //递归保存当前节点的子节点信息到数据库
                    SaveChildInfotoDB(strItemID, firstNode, tCursor, tRowBuffer);
                }

                //释放游标资源
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tCursor);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 从数据库中读取图层列表信息并以树形层级状态显示
        /// </summary>
        /// <param name="pTreeView">树对象</param>
        /// <param name="pFeatureWorkspace">工作空间</param>
        public static void ReadLayerListInfoFromDB(TreeView pTreeView, IFeatureWorkspace pFeatureWorkspace)
        {
            try
            {
                if (pFeatureWorkspace == null) return ;

                string strListTableName = "AM_System_databaselist";

                //从指定工作空间中打开指定名称的属性表
                ITable listTable = GeoTableHandler.OpenTable(strListTableName, pFeatureWorkspace);

                if (listTable != null)
                {
                    int itemNameIndex = listTable.Fields.FindField("ItemName");
                    int itemAliasIndex = listTable.Fields.FindField("ItemAliasName");
                    int itemNodeTypeIndex = listTable.Fields.FindField("NodeType");
                    int itemIDIndex = listTable.Fields.FindField("ItemID");

                    //设置读取一级节点查询过滤器
                    IQueryFilter tQueryFilter = new QueryFilterClass();
                    tQueryFilter.WhereClause = "ParentID='0'";

                    //获取查询结果游标
                    ICursor tCursor = listTable.Search(tQueryFilter, false);                   

                    for (IRow tRow = tCursor.NextRow(); tRow != null; tRow = tCursor.NextRow())
                    {
                        TreeNode firstNode = new TreeNode();
                        //实例化节点属性项
                        ItemProperty itemProperty = new ItemProperty();
                        //设置节点名称
                        if (itemNameIndex > -1) itemProperty.ItemName =Convert.ToString( tRow.get_Value(itemNameIndex));
                        //设置节点别名
                        if (itemAliasIndex > -1) itemProperty.ItemAliasName =Convert.ToString( tRow.get_Value(itemAliasIndex));
                        //设置节点类型
                        if (itemNodeTypeIndex > -1) itemProperty.DataNodeItem = (EnumDataNodeItems)Convert.ToInt32(tRow.get_Value(itemNodeTypeIndex));

                        firstNode.Text = string.Format("{0}:{1}", DBInfoHandler.GetDataNodeDescripble(itemProperty.DataNodeItem),itemProperty.ItemAliasName);
                        firstNode.Tag = itemProperty;                    

                        if (itemIDIndex > -1)
                        {
                            string strItemID = Convert.ToString(tRow.get_Value(itemIDIndex));
                            //递归从图层列表中读取当前节点的子节点
                            ReadChildInfoFromDB(strItemID, firstNode, listTable);
                        }
                        //添加一级节点
                        pTreeView.Nodes.Add(firstNode);
                        //展开树节点
                        firstNode.Expand();
                    }

                    //释放游标资源
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(tCursor);
                }
                else
                {
                    throw (new Exception(string.Format("工作空间中不存在指定名称的表:[{0}]", strListTableName)));
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 递归保存当前节点的子节点信息到数据库
        /// </summary>
        /// <param name="strParentID">父节点编号</param>
        /// <param name="pTreeNode">当前节点</param>
        /// <param name="pCursor">插入游标</param>
        /// <param name="pRowBuffer">插入行缓冲区</param>
        private static void SaveChildInfotoDB(string strParentID,TreeNode pTreeNode,ICursor pCursor,IRowBuffer pRowBuffer)
        {
            for (int i = 0; i < pTreeNode.Nodes.Count; i++)
            {
                TreeNode childNode = pTreeNode.Nodes[i];
                string strItemID = strParentID + Convert.ToString(i + 1).PadLeft(2, '0');
                //对象转换
                ItemProperty tItemProperty = childNode.Tag as ItemProperty;

                if (tItemProperty != null)
                {
                    pRowBuffer.set_Value(pRowBuffer.Fields.FindField("ItemName"), tItemProperty.ItemName);
                    pRowBuffer.set_Value(pRowBuffer.Fields.FindField("ItemAliasName"), tItemProperty.ItemAliasName);
                    pRowBuffer.set_Value(pRowBuffer.Fields.FindField("NodeType"), (int)tItemProperty.DataNodeItem);
                    pRowBuffer.set_Value(pRowBuffer.Fields.FindField("ParentID"), strParentID);
                    pRowBuffer.set_Value(pRowBuffer.Fields.FindField("ItemID"), strItemID);
                }

                if (tItemProperty.DataNodeItem == EnumDataNodeItems.FeatureClassItem)
                {
                    FeatClassNodeItem tFeatClassNodeItem=tItemProperty as FeatClassNodeItem;
                    pRowBuffer.set_Value(pRowBuffer.Fields.FindField("FeatureType"), (int)tFeatClassNodeItem.FeatureType);
                    pRowBuffer.set_Value(pRowBuffer.Fields.FindField("GeometryType"), (int)tFeatClassNodeItem.GeometryType);
                }

                pCursor.InsertRow(pRowBuffer);
                //递归保存当前节点的子节点信息到数据库
                SaveChildInfotoDB(strItemID, childNode, pCursor, pRowBuffer);                
            }
        }

        /// <summary>
        /// 递归从图层列表中读取当前节点的子节点
        /// </summary>
        /// <param name="pItemID">当前结点ID编号</param>
        /// <param name="pTreeNode">当前节点</param>
        /// <param name="pTable">图层列表</param>
        private static void ReadChildInfoFromDB(string pItemID, TreeNode pTreeNode,ITable pTable)
        {
            try
            {
                int itemNameIndex = pTable.Fields.FindField("ItemName");
                int itemAliasIndex = pTable.Fields.FindField("ItemAliasName");
                int itemNodeTypeIndex = pTable.Fields.FindField("NodeType");
                int itemIDIndex = pTable.Fields.FindField("ItemID");
                int itemFeatTypeIndex = pTable.Fields.FindField("FeatureType");
                int itemGeoTypeIndex = pTable.Fields.FindField("GeometryType");

                //设置查询过滤器
                IQueryFilter tQueryFilter = new QueryFilterClass();
                tQueryFilter.WhereClause = string.Format("ParentID='{0}'", pItemID);

                //获取查询结果游标
                ICursor tCursor = pTable.Search(tQueryFilter, false);//tablesort.Rows;

                for (IRow tRow = tCursor.NextRow(); tRow != null; tRow = tCursor.NextRow())
                {
                    TreeNode childNode = new TreeNode();
                    //获取节点类型
                    EnumDataNodeItems tDataNodeType = (EnumDataNodeItems)Convert.ToInt32(tRow.get_Value(itemNodeTypeIndex));

                    //实例化节点属性项
                    ItemProperty itemProperty;

                    if (tDataNodeType == EnumDataNodeItems.FeatureClassItem)
                    {
                        //实例化节点属性项
                        FeatClassNodeItem tFeatClassNodeItem = new FeatClassNodeItem();
                        if (itemFeatTypeIndex > -1) tFeatClassNodeItem.FeatureType = (EnumFeatureType)Convert.ToInt32(tRow.get_Value(itemFeatTypeIndex));
                        if (itemGeoTypeIndex > -1) tFeatClassNodeItem.GeometryType = (EnumGeometryItems)Convert.ToInt32(tRow.get_Value(itemGeoTypeIndex));
                        //ItemProperty是其父类
                        itemProperty = tFeatClassNodeItem;
                    }
                    else
                    {
                        //实例化节点属性项
                        itemProperty = new ItemProperty();
                    }

                    //设置节点名称
                    if (itemNameIndex > -1) itemProperty.ItemName = Convert.ToString(tRow.get_Value(itemNameIndex));
                    //设置节点别名
                    if (itemAliasIndex > -1) itemProperty.ItemAliasName = Convert.ToString(tRow.get_Value(itemAliasIndex));
                    //设置节点类型
                    if (itemNodeTypeIndex > -1) itemProperty.DataNodeItem = tDataNodeType;

                    //将节点属性项绑定到树的tag对象
                    childNode.Tag = itemProperty;
                    //childNode.Text = itemProperty.ItemAliasName;
                    childNode.Text = string.Format("{0}:{1}", DBInfoHandler.GetDataNodeDescripble(itemProperty.DataNodeItem), itemProperty.ItemAliasName);

                    if (itemIDIndex > -1)
                    {
                        string strItemID = Convert.ToString(tRow.get_Value(itemIDIndex));

                        //递归从图层列表中读取当前节点的子节点
                        ReadChildInfoFromDB(strItemID, childNode, pTable);
                    }

                    pTreeNode.Nodes.Add(childNode);
                }

                //释放游标资源
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tCursor);
            }
            catch
            { }
        }
    }
}
