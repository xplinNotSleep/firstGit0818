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
	public class AddAgsCommandItem:CommandItem
	{

        public AddAgsCommandItem()
        {
            m_DataType = DataType.dtAddIms;
        } 

		public override string Name
		{
			get
			{
                return "���ArcGIS Server��ͼ����";
			}
		}
         
        public override  bool OnClick()
        {
            UI.FormAddAgsConnection frm = new AG.COM.SDM.Catalog.UI.FormAddAgsConnection();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return true;
            else
                return false;

            //return false;
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
