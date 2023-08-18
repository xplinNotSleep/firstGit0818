using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
	/// AccessGDB ��ժҪ˵����
	/// </summary>
	public class AccessGdbItem:DataItem,IFileNameItem
	{
		private string m_FileName = "";

        /// <summary>
        /// ʵ�����������ݿ���
        /// </summary>
        /// <param name="fileName">�ļ�����</param>
        public AccessGdbItem(string fileName)
		{
            m_FileName = fileName;
			m_DataType = DataType.dtAccess;
		}

        /// <summary>
        /// ��ȡAccessGDB������
        /// </summary>
		public override string Name
		{
			get
			{
                //ԭ����GetFileNameWithoutExtension������ArcMap GP�ĸ�ʽ�Ǵ�.gdb�ģ���˸�Ϊ��GetFileName
                //return System.IO.Path.GetFileNameWithoutExtension(m_FileName);
                return System.IO.Path.GetFileName(m_FileName);
			}
		}

        /// <summary>
        /// ��ȡ�������ݿ����ļ���
        /// </summary>
        public string FileName
        {
            get { return m_FileName; }
        }

        private object m_GeoObject = null;

        /// <summary>
        /// ��ȡGeoObject����
        /// </summary>
        /// <returns>���ظ������ݿ��е���������Ϊ���򷵻�null</returns>
        public override object GetGeoObject()
        {
            if (m_GeoObject == null)
            {
                try
                {
                    IWorkspaceName pWorspaceName = new ESRI.ArcGIS.Geodatabase.WorkspaceNameClass();
                    pWorspaceName.WorkspaceFactoryProgID = "esriDataSourcesGDB.AccessWorkspaceFactory";
                    pWorspaceName.PathName = m_FileName;

                    m_GeoObject = (pWorspaceName as IName).Open();
                }
                catch (Exception ex)
                {

                    return m_GeoObject;
                }
              
            }
            return m_GeoObject;
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <returns>���ظö���������</returns>
        public override IList<DataItem> GetChildren()
        {
            object obj = GetGeoObject();
            IList<DataItem> items = DatabaseConnectionItem.GetChildrenGeneral(obj as IWorkspace);
            for (int i = 0; i <= items.Count - 1; i++)
            {
                items[i].Parent = this;
            }

            return items; 
        }

        /// <summary>
        /// ��ȡ���Ƿ��������״ֵ̬
        /// </summary>
        public override bool HasChildren
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// ��ȡ���Ƿ���Լ��ص���ͼ������������д򿪵�״ֵ̬
        /// </summary>
        public override bool CanLoad
        {
            get
            {
                return false;
            }
        }
	}
}
