using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
	/// �ļ���GDB������ ��ժҪ˵����
	/// </summary>
	public class FileGdbItem:DataItem,IFileNameItem
	{
		private string m_FileName = "";
        public FileGdbItem(string folder)
		{
            m_FileName = folder;
			m_DataType = DataType.dtFileGdb;
		}

		public override string Name
		{
			get
			{
                //ԭ����GetFileNameWithoutExtension������ArcMap GP�ĸ�ʽ�Ǵ�.gdb�ģ���˸�Ϊ��GetFileName
                //return System.IO.Path.GetFileNameWithoutExtension(m_FileName);
                return System.IO.Path.GetFileName(m_FileName);
			}
		}

        public string FileName
        {
            get { return m_FileName; }
        }

        public void GetGeoObject_NewThread()
        {
            
            IWorkspaceName pWorspaceName = new ESRI.ArcGIS.Geodatabase.WorkspaceNameClass();
            pWorspaceName.WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory";
            pWorspaceName.PathName = m_FileName;

            if (System.IO.Directory.Exists(m_FileName) == false)
            {
                throw new Exception("�Ҳ����ļ����ݿ� " + m_FileName);
            }

            m_GeoObject = (pWorspaceName as IName).Open(); 
        }

        private object m_GeoObject = null;
        private AutoResetEvent evt;
        public override object GetGeoObject()
        {
            if (m_GeoObject == null)
                GetGeoObject_NewThread();            

            return m_GeoObject;

        }

        public override IList<DataItem> GetChildren()
        {
            object obj = GetGeoObject();
            IList<DataItem> items = DatabaseConnectionItem.GetChildrenGeneral(obj as IWorkspace);
            for (int i = 0; i <= items.Count - 1; i++)
            {
                items[i].Workspace=obj as IWorkspace;
                items[i].Parent = this;
            }

            return items; 
        }

        public override bool HasChildren
        {
            get
            {
                return true;
            }
        }

        public override bool CanLoad
        {
            get
            {
                return false;
            }
        }
	}
}
