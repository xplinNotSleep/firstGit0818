using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Catalog.DataItems
{
	/// <summary>
	/// ����������� ��ժҪ˵����
	/// </summary>
	public abstract class CommandItem:DataItem
	{	
        public CommandItem()
		{
             
		}

        /// <summary>
        /// ��ȡ�˶��������
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
