using System;
using System.Windows.Forms;
using AG.COM.SDM.GeoDataBase.DBuilder;
using AG.COM.SDM.Utility;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// AM_SYSTEM_DataBaseList ���д������
    /// </summary>
    public class DBListReadWriter
    {
        /// <summary>
        /// ����������Ϣ�����ݿ�
        /// </summary>
        /// <param name="pTreeView">������</param>
        /// <param name="pFeatureWorkspace">�����ռ�</param>
        public static void SaveLayerListInfoToDB(TreeView pTreeView,IFeatureWorkspace pFeatureWorkspace)
        {
            try
            {
                if (pFeatureWorkspace == null) return ;

                string strListTableName = "AM_System_databaselist";

                //��ָ�������ռ��д�ָ�����Ƶ����Ա�
                ITable listTable = GeoTableHandler.OpenTable(strListTableName, pFeatureWorkspace);
                //ɾ��������
                listTable.DeleteSearchedRows(null);

                //�����α�����
                ICursor tCursor = listTable.Insert(true);
                //�����л���
                IRowBuffer tRowBuffer = listTable.CreateRowBuffer();

                for (int i = 0; i < pTreeView.Nodes.Count; i++)
                {
                    TreeNode firstNode = pTreeView.Nodes[i];
                    //���õ�ǰ�ڵ���
                    string strItemID = "0" + Convert.ToString(i + 1).PadLeft(2, '0');
                    //��������ת��
                    ItemProperty tItemProperty = firstNode.Tag as ItemProperty;
                    if (tItemProperty != null)
                    {
                        tRowBuffer.set_Value(tRowBuffer.Fields.FindField("ItemName"), tItemProperty.ItemName);
                        tRowBuffer.set_Value(tRowBuffer.Fields.FindField("ItemAliasName"), tItemProperty.ItemAliasName);
                        tRowBuffer.set_Value(tRowBuffer.Fields.FindField("NodeType"), (int)tItemProperty.DataNodeItem);
                        tRowBuffer.set_Value(tRowBuffer.Fields.FindField("ParentID"), "0");
                        tRowBuffer.set_Value(tRowBuffer.Fields.FindField("ItemID"), strItemID);
                    }

                    //���뵱ǰ��
                    tCursor.InsertRow(tRowBuffer);

                    //�ݹ鱣�浱ǰ�ڵ���ӽڵ���Ϣ�����ݿ�
                    SaveChildInfotoDB(strItemID, firstNode, tCursor, tRowBuffer);
                }

                //�ͷ��α���Դ
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tCursor);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// �����ݿ��ж�ȡͼ���б���Ϣ�������β㼶״̬��ʾ
        /// </summary>
        /// <param name="pTreeView">������</param>
        /// <param name="pFeatureWorkspace">�����ռ�</param>
        public static void ReadLayerListInfoFromDB(TreeView pTreeView, IFeatureWorkspace pFeatureWorkspace)
        {
            try
            {
                if (pFeatureWorkspace == null) return ;

                string strListTableName = "AM_System_databaselist";

                //��ָ�������ռ��д�ָ�����Ƶ����Ա�
                ITable listTable = GeoTableHandler.OpenTable(strListTableName, pFeatureWorkspace);

                if (listTable != null)
                {
                    int itemNameIndex = listTable.Fields.FindField("ItemName");
                    int itemAliasIndex = listTable.Fields.FindField("ItemAliasName");
                    int itemNodeTypeIndex = listTable.Fields.FindField("NodeType");
                    int itemIDIndex = listTable.Fields.FindField("ItemID");

                    //���ö�ȡһ���ڵ��ѯ������
                    IQueryFilter tQueryFilter = new QueryFilterClass();
                    tQueryFilter.WhereClause = "ParentID='0'";

                    //��ȡ��ѯ����α�
                    ICursor tCursor = listTable.Search(tQueryFilter, false);                   

                    for (IRow tRow = tCursor.NextRow(); tRow != null; tRow = tCursor.NextRow())
                    {
                        TreeNode firstNode = new TreeNode();
                        //ʵ�����ڵ�������
                        ItemProperty itemProperty = new ItemProperty();
                        //���ýڵ�����
                        if (itemNameIndex > -1) itemProperty.ItemName =Convert.ToString( tRow.get_Value(itemNameIndex));
                        //���ýڵ����
                        if (itemAliasIndex > -1) itemProperty.ItemAliasName =Convert.ToString( tRow.get_Value(itemAliasIndex));
                        //���ýڵ�����
                        if (itemNodeTypeIndex > -1) itemProperty.DataNodeItem = (EnumDataNodeItems)Convert.ToInt32(tRow.get_Value(itemNodeTypeIndex));

                        firstNode.Text = string.Format("{0}:{1}", DBInfoHandler.GetDataNodeDescripble(itemProperty.DataNodeItem),itemProperty.ItemAliasName);
                        firstNode.Tag = itemProperty;                    

                        if (itemIDIndex > -1)
                        {
                            string strItemID = Convert.ToString(tRow.get_Value(itemIDIndex));
                            //�ݹ��ͼ���б��ж�ȡ��ǰ�ڵ���ӽڵ�
                            ReadChildInfoFromDB(strItemID, firstNode, listTable);
                        }
                        //���һ���ڵ�
                        pTreeView.Nodes.Add(firstNode);
                        //չ�����ڵ�
                        firstNode.Expand();
                    }

                    //�ͷ��α���Դ
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(tCursor);
                }
                else
                {
                    throw (new Exception(string.Format("�����ռ��в�����ָ�����Ƶı�:[{0}]", strListTableName)));
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// �ݹ鱣�浱ǰ�ڵ���ӽڵ���Ϣ�����ݿ�
        /// </summary>
        /// <param name="strParentID">���ڵ���</param>
        /// <param name="pTreeNode">��ǰ�ڵ�</param>
        /// <param name="pCursor">�����α�</param>
        /// <param name="pRowBuffer">�����л�����</param>
        private static void SaveChildInfotoDB(string strParentID,TreeNode pTreeNode,ICursor pCursor,IRowBuffer pRowBuffer)
        {
            for (int i = 0; i < pTreeNode.Nodes.Count; i++)
            {
                TreeNode childNode = pTreeNode.Nodes[i];
                string strItemID = strParentID + Convert.ToString(i + 1).PadLeft(2, '0');
                //����ת��
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
                //�ݹ鱣�浱ǰ�ڵ���ӽڵ���Ϣ�����ݿ�
                SaveChildInfotoDB(strItemID, childNode, pCursor, pRowBuffer);                
            }
        }

        /// <summary>
        /// �ݹ��ͼ���б��ж�ȡ��ǰ�ڵ���ӽڵ�
        /// </summary>
        /// <param name="pItemID">��ǰ���ID���</param>
        /// <param name="pTreeNode">��ǰ�ڵ�</param>
        /// <param name="pTable">ͼ���б�</param>
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

                //���ò�ѯ������
                IQueryFilter tQueryFilter = new QueryFilterClass();
                tQueryFilter.WhereClause = string.Format("ParentID='{0}'", pItemID);

                //��ȡ��ѯ����α�
                ICursor tCursor = pTable.Search(tQueryFilter, false);//tablesort.Rows;

                for (IRow tRow = tCursor.NextRow(); tRow != null; tRow = tCursor.NextRow())
                {
                    TreeNode childNode = new TreeNode();
                    //��ȡ�ڵ�����
                    EnumDataNodeItems tDataNodeType = (EnumDataNodeItems)Convert.ToInt32(tRow.get_Value(itemNodeTypeIndex));

                    //ʵ�����ڵ�������
                    ItemProperty itemProperty;

                    if (tDataNodeType == EnumDataNodeItems.FeatureClassItem)
                    {
                        //ʵ�����ڵ�������
                        FeatClassNodeItem tFeatClassNodeItem = new FeatClassNodeItem();
                        if (itemFeatTypeIndex > -1) tFeatClassNodeItem.FeatureType = (EnumFeatureType)Convert.ToInt32(tRow.get_Value(itemFeatTypeIndex));
                        if (itemGeoTypeIndex > -1) tFeatClassNodeItem.GeometryType = (EnumGeometryItems)Convert.ToInt32(tRow.get_Value(itemGeoTypeIndex));
                        //ItemProperty���丸��
                        itemProperty = tFeatClassNodeItem;
                    }
                    else
                    {
                        //ʵ�����ڵ�������
                        itemProperty = new ItemProperty();
                    }

                    //���ýڵ�����
                    if (itemNameIndex > -1) itemProperty.ItemName = Convert.ToString(tRow.get_Value(itemNameIndex));
                    //���ýڵ����
                    if (itemAliasIndex > -1) itemProperty.ItemAliasName = Convert.ToString(tRow.get_Value(itemAliasIndex));
                    //���ýڵ�����
                    if (itemNodeTypeIndex > -1) itemProperty.DataNodeItem = tDataNodeType;

                    //���ڵ�������󶨵�����tag����
                    childNode.Tag = itemProperty;
                    //childNode.Text = itemProperty.ItemAliasName;
                    childNode.Text = string.Format("{0}:{1}", DBInfoHandler.GetDataNodeDescripble(itemProperty.DataNodeItem), itemProperty.ItemAliasName);

                    if (itemIDIndex > -1)
                    {
                        string strItemID = Convert.ToString(tRow.get_Value(itemIDIndex));

                        //�ݹ��ͼ���б��ж�ȡ��ǰ�ڵ���ӽڵ�
                        ReadChildInfoFromDB(strItemID, childNode, pTable);
                    }

                    pTreeNode.Nodes.Add(childNode);
                }

                //�ͷ��α���Դ
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tCursor);
            }
            catch
            { }
        }
    }
}
