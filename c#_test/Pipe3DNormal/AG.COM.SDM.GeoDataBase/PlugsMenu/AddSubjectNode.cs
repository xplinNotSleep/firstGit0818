using System.Windows.Forms;
using AG.COM.SDM.GeoDataBase.DBuilder;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 添加专题子库节点 上下文菜单项
    /// </summary>
    internal sealed class AddSubjectNode:BaseCommand
    {
        private TreeNode m_SelTreeNode;             //当前选择节点
        private TreeNode m_SelChildTreeNode;        //当前选择节点要添加的子节点

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AddSubjectNode()
        {
            this.m_caption = "添加专题子库";
            this.m_toolTip = "添加专题子库";
            this.m_name = "AddSubjectNode";
        }

        /// <summary>
        /// 获取对象的可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                return (m_SelTreeNode == null) ? false : true;
            }
        }

        /// <summary>
        /// 单击处理事件
        /// </summary>
        public override void OnClick()
        {
            //实例化节点属性项
            ItemProperty DBNodeItem = new ItemProperty();
            DBNodeItem.ItemName = "New Subject";
            DBNodeItem.ItemAliasName = "新专题子库";
            DBNodeItem.DataNodeItem = EnumDataNodeItems.SubjectChildItem;
            DBNodeItem.ItemPropertyValueChanged += new ItemPropertyValueChangedEventHandler(ItemPropertyValueChanged);

            //实例化树节点
            m_SelChildTreeNode = new TreeNode();
            
            m_SelChildTreeNode.Text = string.Format("{0}:{1}", DBInfoHandler.GetDataNodeDescripble(DBNodeItem.DataNodeItem), DBNodeItem.ItemAliasName);
            m_SelChildTreeNode.Tag = DBNodeItem;
            m_SelChildTreeNode.ImageIndex = 1;
            m_SelChildTreeNode.SelectedImageIndex = 1;

            //添加当前节点
            this.m_SelTreeNode.Nodes.Add(m_SelChildTreeNode);
            this.m_SelTreeNode.Expand();
        }

        /// <summary>
        /// 创建初始化
        /// </summary>
        /// <param name="hook">当前选择节点</param>
        public override void OnCreate(object hook)
        {
            if (hook == null) return;
            this.m_SelTreeNode = hook as TreeNode;
        }

        /// <summary>
        /// ItemProperty将调用此方法来通知DBTreeControl对象其属性值已发生改变
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">ItemPropertyEventArgs事件参数</param>
        private void ItemPropertyValueChanged(object sender, ItemPropertyEventArgs e)
        {
            ItemProperty itemProperty = e.ItemProperty;
            this.m_SelChildTreeNode.Text = string.Format("{0}:{1}({2})", DBInfoHandler.GetDataNodeDescripble(itemProperty.DataNodeItem), itemProperty.ItemAliasName);
        }  
    }
}
