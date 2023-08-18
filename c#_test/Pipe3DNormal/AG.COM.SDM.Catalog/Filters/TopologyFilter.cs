using System;
using System.Collections.Generic;
using System.Text;
using AG.COM.SDM.Catalog.DataItems;

namespace AG.COM.SDM.Catalog.Filters
{
    /// <summary>
    /// ���˹�ϵ������
    /// </summary>
    public class TopologyFilter : BaseItemFilter
    {
        public TopologyFilter()
        {
            m_FilterName = "���˹�ϵ";
        }
        #region IDataItemFilter ��Ա

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
