using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.Catalog.Filters
{
    /// <summary>
    /// �ļ��й�����
    /// </summary>
    public class FolderFilter:BaseItemFilter
    {
        public FolderFilter()
        {
            m_FilterName = "�ļ���"; 
        }
        #region IDataItemFilter ��Ա

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
    /// �����ռ������
    /// </summary>
    public class WorkspaceFilter:BaseItemFilter
    {
        public WorkspaceFilter()
        {
            m_FilterName = "�����ռ�"; 
        }
        #region IDataItemFilter ��Ա

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
    /// �ļ�GeoDatabase�����ռ������
    /// </summary>
    public class FileGeoDatabaseFilter : BaseItemFilter
    {
        public FileGeoDatabaseFilter()
        {
            m_FilterName = "�ļ�GeoDatabase�����ռ�";
        }
        #region IDataItemFilter ��Ա

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
    /// ����GeoDatabase�����ռ������
    /// </summary>
    public class PersonalGeoDatabaseFilter : BaseItemFilter
    {
        public PersonalGeoDatabaseFilter()
        {
            m_FilterName = "����GeoDatabase�����ռ�";
        }
        #region IDataItemFilter ��Ա

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
    /// SDE GeoDatabase�����ռ������
    /// </summary>
    public class SDEWorkspaceFilter : BaseItemFilter
    {
        public SDEWorkspaceFilter()
        {
            m_FilterName = "SDE GeoDatabase�����ռ�";
        }
        #region IDataItemFilter ��Ա

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
