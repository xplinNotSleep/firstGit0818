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
                return "添加空间数据库连接";
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
