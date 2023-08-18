using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.Catalog.DataItems
{
    /// <summary>
    /// ��ʷλ��������
    /// </summary>
    public class HisLocationItem:DataItem
    {
        private string m_HisString="";

        /// <summary>
        /// ��ʼ����ʷλ��������ʵ��
        /// </summary>
        /// <param name="hisStr">��ʷλ���ı���Ϣ</param>
        public HisLocationItem(string hisStr)
        {
            m_HisString = hisStr;
            m_DataType = DataType.dtHisLocation;
        }

        /// <summary>
        /// ��ȡ����������
        /// </summary>
        public override string Name
        {
            get
            {
                string[] str = m_HisString.Split(',');
                if (str.Length == 1)
                    return str[0];
                else if (str.Length == 2)
                {
                    if (str[0].Trim().Length > 0)
                        return str[0] + "\\" + str[1];
                    else
                        return str[1];
                }
                else
                {
                    if (str[0].Trim().Length > 0)
                        return str[0] + "\\" + str[1] + "\\" + str[2];
                    else
                        return str[1] + "\\" + str[2];
                }

            }
        }
        public override bool HasChildren
        {
            get
            {
                return true;
            }
        }

        public override object GetGeoObject()
        {
            IList<DataItem> items = new List<DataItem>();
            DataItem item = LocationSerializer.LoadFromString(m_HisString);
            return item;
        }

        public override bool CanLoad
        {
            get
            {
                return false;
            }
        }

        public override IList<DataItem> GetChildren()
        {
            DataItem item = GetGeoObject() as DataItem;
            return item.GetChildren();
        }
    }
}
