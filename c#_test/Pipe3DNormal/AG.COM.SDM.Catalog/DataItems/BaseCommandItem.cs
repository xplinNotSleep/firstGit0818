using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
	/// 命令项抽象类 的摘要说明。
	/// </summary>
	public abstract class CommandItem:DataItem
	{	
        public CommandItem()
		{
             
		}

        /// <summary>
        /// 获取此对象的名称
        /// </summary>
		public override string Name
		{
			get
			{
                return "";
			}
		}
         
        public virtual bool OnClick()
        {
            return false;
        }
	}
}
