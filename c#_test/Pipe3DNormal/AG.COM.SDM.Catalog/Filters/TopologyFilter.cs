using System;
using System.Collections.Generic;
using System.Text;
using AG.COM.SDM.Catalog.DataItems;

namespace AG.COM.SDM.Catalog.Filters
{
    /// <summary>
    /// 拓扑关系过滤器
    /// </summary>
    public class TopologyFilter : BaseItemFilter
    {
        public TopologyFilter()
        {
            m_FilterName = "拓扑关系";
        }
        #region IDataItemFilter 成员

        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (Confirm(item))
                return true;
            if (IsWorkspaceOrFeatureDataset(item))
                return true;
            return false;
        }

        public override bool Confirm(DataItem item)
        {
            if (item.Type == DataType.dtTopology)
                return true;
            else
                return false;
        }

        #endregion
    }
}
