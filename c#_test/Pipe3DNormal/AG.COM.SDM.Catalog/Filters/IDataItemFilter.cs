using System;
using System.Collections.Generic;
using System.Text;
using AG.COM.SDM.Catalog.DataItems;

namespace AG.COM.SDM.Catalog.Filters
{
    /// <summary>
    /// 数据项过滤器接口
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
        /// 获取此数据项过滤器名称
        /// </summary>
        string FilterName { get;}
        /// <summary>
        /// 判断此数据项是否符合过滤要求
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Confirm(DataItems.DataItem item);
    }

    /// <summary>
    /// 基础过滤器（抽象类）
    /// </summary>
    public abstract class BaseItemFilter:IDataItemFilter
    {
        #region IDataItemFilter 成员

        public virtual bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            return false;
        }

        /// <summary>
        /// 判断此数据项是否符合过滤要求
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual bool Confirm(DataItems.DataItem item)
        {
            return false;
        }

        protected string m_FilterName = "";

        /// <summary>
        /// 获取过滤器名称
        /// </summary>
        public string FilterName
        {
            get { return m_FilterName; }
        }

        /// <summary>
        /// 重载ToString()方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return m_FilterName;
        }

        /// <summary>
        /// 判断指定的数据项是否为工作空间或数据集
        /// </summary>
        /// <param name="item">数据项</param>
        /// <returns>如果指定的数据项为工作空间或数据集则返回true，否则返回false</returns>
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
