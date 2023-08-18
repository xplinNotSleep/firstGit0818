using System.Windows.Forms;
using AG.COM.SDM.GeoDataBase.DBuilder;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 添加数据库节点 上下文菜单项
    /// </summary>
    internal sealed class AddDataBaseNode:BaseCommand
    {
        private TreeView m_DataView;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AddDataBaseNode()
        {
            this.m_caption = "添加数据库";
            this.m_toolTip = "添加数据库节点";
            this.m_name = "AddDataBaseNode";
        }
        /// <summary>
        /// 获取对象的可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                return (m_DataView == null) ? false : true;
            }
        }

        /// <summary>
        /// 单击处理事件
        /// </summary>
        public override void OnClick()
        {
            //实例化节点属性项
            ItemProperty DBNodeItem = new ItemProperty();
            DBNodeItem.ItemName = "New DataBase";
            DBNodeItem.ItemAliasName = "新数据库";
            DBNodeItem.DataNodeItem = EnumDataNodeItems.DataBaseItem;
            DBNodeItem.ItemPropertyValueChanged += new ItemPropertyValueChangedEventHandler(ItemPropertyValueChanged);

            //实例化树节点
            TreeNode DBTreeNode = new TreeNode();       
            DBTreeNode.Text = string.Format("{0}:{1}", DBInfoHandler.GetDataNodeDescripble(DBNodeItem.DataNodeItem), DBNodeItem.ItemAliasName);
            DBTreeNode.Tag = DBNodeItem;

            this.m_DataView.Nodes.Add(DBTreeNode);            
        }

        /// <summary>
        /// 创建初始化
        /// </summary>
        /// <param name="hook">当前操作树对象</param>
        public override void OnCreate(object hook)
        {
            if (hook == null) return;
            this.m_DataView = hook as TreeView;            
        }

        /// <summary>
        /// ItemProperty将调用此方法来通知DBTreeControl对象其属性值已发生改变
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">ItemPropertyEventArgs事件参数</param>
        private void ItemPropertyValueChanged(object sender, ItemPropertyEventArgs e)
        {
            ItemProperty itemProperty = e.ItemProperty;            
            TreeNode selTreeNode = this.m_DataView.SelectedNode;           
            selTreeNode.ToolTipText = string.Format("{0}:{1}", DBInfoHandler.GetDataNodeDescripble(itemProperty.DataNodeItem), itemProperty.ItemAliasName);
        }  
    }
}
