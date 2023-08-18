using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AG.COM.SDM.Catalog.DataItems
{
    /// <summary>
    /// �����������
    /// </summary>
	public abstract class DataItem
	{
		private DataItem m_Parent;

        /// <summary>
        /// ��ȡ�������ϼ����������
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
        /// ��ȡ��ǰ������
        /// </summary>
        /// <returns>���ص�ǰ���������������򷵻�null</returns>
		public virtual object GetGeoObject()
		{
			return null;
		}		 

        /// <summary>
        /// �Ƿ����������������Ϊtrue������Ϊfalse;
        /// </summary>
		public virtual bool HasChildren
		{
			get 
			{
				return false;
			}
		}

        /// <summary>
        /// ��ȡ��ǰ�ڵ�������Ӽ�
        /// </summary>
        /// <returns></returns>
		public virtual IList<DataItem> GetChildren()
		{
            return null;
		}

        /// <summary>
        /// ��ȡ����������
        /// </summary>
		public virtual string Name
		{
			get { return "";}
		}

        /// <summary>
        /// ��ȡ���������
        /// </summary>
		public virtual string AliasName
		{
			get { return "";}
		}
    
        /// <summary>
        /// ��ȡ������ȫ·��
        /// </summary>
        public virtual string FullPath
        {
            get
            {
                return this.GetDataPath(this, this.Name);
            }
        }

        /// <summary>
        /// ��ȡָ��������ȫ·��
        /// </summary>
        /// <param name="item">ָ����������</param>
        /// <param name="curPath">��ǰ�ļ���</param>
        /// <returns>����ָ���������ȫ·��</returns>
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
        /// ��������
        /// </summary>
		protected DataType m_DataType = DataType.dtUnknown;

        /// <summary>
        /// ��ȡ����������
        /// </summary>
		public DataType Type
		{
			get { return m_DataType;}
		}

        /// <summary>
        /// ��ȡ�������������
        /// </summary>
		public string TypeName
		{
			get { return DataTypeParser.GetDataTypeName(m_DataType);}
		}

        /// <summary>
        /// �Ƿ���Լ��ص���ͼ������������д�
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
    /// �ļ�������ӿ�
    /// </summary>
    public interface IFileNameItem
    {
        /// <summary>
        /// ��ȡ�ļ�����
        /// </summary>
        string FileName { get;}
    }

}
