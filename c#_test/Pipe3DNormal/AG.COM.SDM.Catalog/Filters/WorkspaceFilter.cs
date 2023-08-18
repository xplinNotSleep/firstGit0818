using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.Catalog.Filters
{
    /// <summary>
    /// 文件夹过滤器
    /// </summary>
    public class FolderFilter:BaseItemFilter
    {
        public FolderFilter()
        {
            m_FilterName = "文件夹"; 
        }
        #region IDataItemFilter 成员

        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        { 
            if ((item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtFolder)||                 
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtDisk))
                return true;
            else
                return false;
        }

        public override bool Confirm(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if ((item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtFolder) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtDisk))
                return true;
            else
                return false;
        }
 
        #endregion
    }

    /// <summary>
    /// 工作空间过滤器
    /// </summary>
    public class WorkspaceFilter:BaseItemFilter
    {
        public WorkspaceFilter()
        {
            m_FilterName = "工作空间"; 
        }
        #region IDataItemFilter 成员

        public override bool Confirm(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if ((item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtAccess) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtFileGdb) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtSdeConnection) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtDisk) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtFolder))
                return true;
            else
                return false;
        }

        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (Confirm(item))
                return true;
            if ((item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtDisk) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtFolder))
                return true;
            else
                return false;
        }
 
        #endregion
    }

    /// <summary>
    /// 文件GeoDatabase工作空间过滤器
    /// </summary>
    public class FileGeoDatabaseFilter : BaseItemFilter
    {
        public FileGeoDatabaseFilter()
        {
            m_FilterName = "文件GeoDatabase工作空间";
        }
        #region IDataItemFilter 成员

        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (Confirm(item))
                return true;
            if ((item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtDisk) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtFolder))
                return true;
            else
                return false;
        }

        public override bool Confirm(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtFileGdb)
                return true;
            else
                return false;
        }

        #endregion
    }

    /// <summary>
    /// 个人GeoDatabase工作空间过滤器
    /// </summary>
    public class PersonalGeoDatabaseFilter : BaseItemFilter
    {
        public PersonalGeoDatabaseFilter()
        {
            m_FilterName = "个人GeoDatabase工作空间";
        }
        #region IDataItemFilter 成员

        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (Confirm(item))
                return true;
            if ((item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtDisk) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtFolder))
                return true;
            else
                return false;
        }

        public override bool Confirm(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtAccess)
                return true;
            else
                return false;
        }

        #endregion
    }

    /// <summary>
    /// SDE GeoDatabase工作空间过滤器
    /// </summary>
    public class SDEWorkspaceFilter : BaseItemFilter
    {
        public SDEWorkspaceFilter()
        {
            m_FilterName = "SDE GeoDatabase工作空间";
        }
        #region IDataItemFilter 成员

        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (Confirm(item))
                return true;
            if ((item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtDisk) ||
                (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtFolder))
                return true;
            else
                return false;
        }

        public override bool Confirm(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            if (item.Type == AG.COM.SDM.Catalog.DataItems.DataType.dtSdeConnection)
                return true;
            else
                return false;
        }

        #endregion
    }

}
