using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AG.COM.SDM.Catalog.DataItems
{
    /// <summary>
    /// 数据项抽象类
    /// </summary>
	public abstract class DataItem
	{
		private DataItem m_Parent;

        /// <summary>
        /// 获取数据项上级数据项对象
        /// </summary>
		public DataItem Parent
		{
			get 
			{
				return m_Parent;
			}
			set
			{
				m_Parent = value;
			}
		}		

        /// <summary>
        /// 获取当前数据项
        /// </summary>
        /// <returns>返回当前数据项，如果不存在则返回null</returns>
		public virtual object GetGeoObject()
		{
			return null;
		}		 

        /// <summary>
        /// 是否包含子项，如果包含则为true，否则为false;
        /// </summary>
		public virtual bool HasChildren
		{
			get 
			{
				return false;
			}
		}

        /// <summary>
        /// 获取当前节点的数据子集
        /// </summary>
        /// <returns></returns>
		public virtual IList<DataItem> GetChildren()
		{
            return null;
		}

        /// <summary>
        /// 获取数据项名称
        /// </summary>
		public virtual string Name
		{
			get { return "";}
		}

        /// <summary>
        /// 获取数据项别名
        /// </summary>
		public virtual string AliasName
		{
			get { return "";}
		}
    
        /// <summary>
        /// 获取数据项全路径
        /// </summary>
        public virtual string FullPath
        {
            get
            {
                return this.GetDataPath(this, this.Name);
            }
        }

        /// <summary>
        /// 获取指定数据项全路径
        /// </summary>
        /// <param name="item">指定的数据项</param>
        /// <param name="curPath">当前文件名</param>
        /// <returns>返回指定数据项的全路径</returns>
        private string GetDataPath(DataItem item, string curPath)
        {
            if (item.Parent != null)
            {
                if (item.Parent.Name.Length == 0)
                    return GetDataPath(item.Parent, curPath);

                string parentName=item.Parent .Name.TrimEnd('\\');
                string tempPath = string.Format("{0}\\{1}", parentName, curPath);
                return GetDataPath(item.Parent, tempPath);
            }

            return curPath;
        }

        /// <summary>
        /// 数据类型
        /// </summary>
		protected DataType m_DataType = DataType.dtUnknown;

        /// <summary>
        /// 获取数据项类型
        /// </summary>
		public DataType Type
		{
			get { return m_DataType;}
		}

        /// <summary>
        /// 获取数据项类别名称
        /// </summary>
		public string TypeName
		{
			get { return DataTypeParser.GetDataTypeName(m_DataType);}
		}

        /// <summary>
        /// 是否可以加载到地图或在软件界面中打开
        /// </summary>
        public virtual bool CanLoad
        {
            get { return true; }
        }

        public virtual IWorkspace Workspace
        {
            get;
            set;
        }


    }

    /// <summary>
    /// 文件名称项接口
    /// </summary>
    public interface IFileNameItem
    {
        /// <summary>
        /// 获取文件名称
        /// </summary>
        string FileName { get;}
    }

}
