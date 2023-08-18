using System;
using System.Collections.Generic;
using System.Text;
using AG.COM.SDM.Catalog.DataItems;

namespace AG.COM.SDM.Catalog.Filters
{
    /// <summary>
    /// ������������ӿ�
    /// </summary>
    public interface IDataItemFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool CanPass(DataItems.DataItem item);
        /// <summary>
        /// ��ȡ�����������������
        /// </summary>
        string FilterName { get;}
        /// <summary>
        /// �жϴ��������Ƿ���Ϲ���Ҫ��
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Confirm(DataItems.DataItem item);
    }

    /// <summary>
    /// �����������������ࣩ
    /// </summary>
    public abstract class BaseItemFilter:IDataItemFilter
    {
        #region IDataItemFilter ��Ա

        public virtual bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            return false;
        }

        /// <summary>
        /// �жϴ��������Ƿ���Ϲ���Ҫ��
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual bool Confirm(DataItems.DataItem item)
        {
            return false;
        }

        protected string m_FilterName = "";

        /// <summary>
        /// ��ȡ����������
        /// </summary>
        public string FilterName
        {
            get { return m_FilterName; }
        }

        /// <summary>
        /// ����ToString()����
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return m_FilterName;
        }

        /// <summary>
        /// �ж�ָ�����������Ƿ�Ϊ�����ռ�����ݼ�
        /// </summary>
        /// <param name="item">������</param>
        /// <returns>���ָ����������Ϊ�����ռ�����ݼ��򷵻�true�����򷵻�false</returns>
        protected bool IsWorkspaceOrFeatureDataset(DataItems.DataItem item)
        {
            if ((item.Type == DataType.dtFolder) ||
                (item.Type == DataType.dtDisk) ||
                (item.Type == DataType.dtAccess) ||
                (item.Type == DataType.dtFileGdb) ||
                (item.Type == DataType.dtSdeConnection) || 
                (item.Type == DataType.dtFeatureDataset))
                return true;
            else
                return false;
        }      

        #endregion
    }
}
