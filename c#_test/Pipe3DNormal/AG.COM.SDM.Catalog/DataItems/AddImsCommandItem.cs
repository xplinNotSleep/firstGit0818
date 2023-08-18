using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
	/// ���ArcIMS��ͼ���������� ��ժҪ˵����
	/// </summary>
	public class AddImsCommandItem:CommandItem
	{
        /// <summary>
        /// ��ʼ�����ArcIMS��ͼ����ʵ������
        /// </summary>
        public AddImsCommandItem()
        {
            m_DataType = DataType.dtAddIms;
        } 

		public override string Name
		{
			get
			{
                return "���ArcIMS��ͼ����";
			}
		}
         
        public override  bool OnClick()
        {
            UI.FormAddImsService frm = new AG.COM.SDM.Catalog.UI.FormAddImsService();
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
