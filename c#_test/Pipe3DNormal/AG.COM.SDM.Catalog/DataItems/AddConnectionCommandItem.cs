using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
	/// FolderItem ��ժҪ˵����
	/// </summary>
	public class AddConnectionCommandItem:CommandItem
	{		 
        public AddConnectionCommandItem()
        {
            m_DataType = DataType.dtAddDatabaseConnection;
        } 

		public override string Name
		{
			get
			{
                return "��ӿռ����ݿ�����";
			}
		}
         
        public override  bool OnClick()
        {
            UI.FormAddConnection frm = new AG.COM.SDM.Catalog.UI.FormAddConnection();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return true;
            else
                return false;
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
