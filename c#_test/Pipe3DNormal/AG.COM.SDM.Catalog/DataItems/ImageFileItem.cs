using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
	/// Image�ļ������� ��ժҪ˵����
	/// </summary>
	public class ImageFileItem:DataItem
	{
		private string m_FileName = "";

        /// <summary>
        /// ��ʼ��Image�ļ�������ʵ��
        /// </summary>
        /// <param name="fileName">�ļ�·��</param>
        public ImageFileItem(string fileName)
		{
            m_FileName = fileName;
			m_DataType = DataType.dtImageFile;
		}

        /// <summary>
        /// ��ȡ����������
        /// </summary>
		public override string Name
		{
			get
			{
                return System.IO.Path.GetFileName(m_FileName);
			}
		}

        public override object GetGeoObject()
        {
            try
            {
                IDatasetName pDatasetName = new RasterDatasetNameClass();
                IWorkspaceName pWorspaceName = new ESRI.ArcGIS.Geodatabase.WorkspaceNameClass();
                pWorspaceName.WorkspaceFactoryProgID = "esriDataSourcesRaster.RasterWorkspaceFactory";
                pWorspaceName.PathName = System.IO.Path.GetDirectoryName(m_FileName);               
                pDatasetName.Name = System.IO.Path.GetFileName(m_FileName);
                if (string.IsNullOrEmpty( pDatasetName.Name))//����Ƭդ��ʱ��û�к�׺
                {
                    string[] dirName = m_FileName.Split('\\');
                    pDatasetName.Name = dirName[dirName.Length - 1];
                       
                }
                pDatasetName.WorkspaceName = pWorspaceName;

                return (pDatasetName as IName).Open();
            }
            catch //(Exception ex)
            {
                //����д��־
                return null;
            }
        }
        
	}
}
