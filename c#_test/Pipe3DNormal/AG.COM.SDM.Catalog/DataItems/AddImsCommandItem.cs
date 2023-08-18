using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
	/// 添加ArcIMS地图服务命令项 的摘要说明。
	/// </summary>
	public class AddImsCommandItem:CommandItem
	{
        /// <summary>
        /// 初始化添加ArcIMS地图服务实例对象
        /// </summary>
        public AddImsCommandItem()
        {
            m_DataType = DataType.dtAddIms;
        } 

		public override string Name
		{
			get
			{
                return "添加ArcIMS地图服务";
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
