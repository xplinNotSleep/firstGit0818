using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
	/// FolderItem 的摘要说明。
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
                return "添加ArcGIS Server地图服务";
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
        /// 获取其是否可以加载到地图或在软件界面中打开的状态值
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
