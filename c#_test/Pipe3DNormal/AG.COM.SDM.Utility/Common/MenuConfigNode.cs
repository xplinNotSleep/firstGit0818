using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.COM.SDM.Framework.Utility;

namespace AG.COM.SDM.Utility.Common
{
    public class MenuConfigNode
    {
        #region 字段
        private string m_Caption = "";
        private string m_PlugAssembly = "";
        private string m_PlugType = "";
        #endregion

        #region 属性

        /// <summary>
        /// GUID
        /// </summary>
        public string GUID
        {
            get;
            set;
        }

        /// <summary>
        /// 显示文字
        /// </summary>
        public string Caption
        {
            get
            {
                return m_Caption;
            }
            set
            {
                m_Caption = value;
            }
        }

        /// <summary>
        /// dll文件名
        /// </summary>
        public string PlugAssembly
        {
            get
            {
                return m_PlugAssembly;
            }
            set
            {
                m_PlugAssembly = value;
            }
        }

        /// <summary>
        /// 功能全名
        /// </summary>
        public string PlugType
        {
            get
            {
                return m_PlugType;
            }
            set
            {
                m_PlugType = value;
            }
        }

        /// <summary>
        /// 菜单编码
        /// </summary>
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// 排序号
        /// </summary>
        public decimal Sort
        {
            get;
            set;
        }

        /// <summary>
        /// 父编码
        /// </summary>
        public string ParentCode
        {
            get;
            set;
        }

        /// <summary>
        /// 菜单类型
        /// 可能的值：Page，Panel，Group，Item
        /// </summary>
        public MenuType MenuType
        {
            get;
            set;
        }

        /// <summary>
        /// 扩展属性1（是否新分组）
        /// </summary>
        public string ExtValue1
        {
            get;
            set;
        }

        /// <summary>
        /// 扩展属性2（true=大图标，false=小图标）
        /// </summary>
        public string ExtValue2
        {
            get;
            set;
        }

        /// <summary>
        /// 扩展属性3
        /// </summary>
        public string ExtValue3
        {
            get;
            set;
        }

        /// <summary>
        /// 扩展属性4
        /// </summary>
        public string ExtValue4
        {
            get;
            set;
        }

        /// <summary>
        /// 扩展属性5
        /// </summary>
        public string ExtValue5
        {
            get;
            set;
        }

        /// <summary>
        /// 扩展属性6
        /// </summary>
        public string ExtValue6
        {
            get;
            set;
        }

        /// <summary>
        /// 扩展属性7
        /// </summary>
        public string ExtValue7
        {
            get;
            set;
        }

        /// <summary>
        /// 扩展属性8
        /// </summary>
        public string ExtValue8
        {
            get;
            set;
        }

        /// <summary>
        /// 扩展属性9
        /// </summary>
        public string ExtValue9
        {
            get;
            set;
        }

        /// <summary>
        /// 扩展属性10
        /// </summary>
        public string ExtValue10
        {
            get;
            set;
        }

        #endregion

        public MenuConfigNode()
        {
            GUID = "";
            Code = "";
            ParentCode = "";
            MenuType = MenuType.None;
            Sort = 0;
            ExtValue1 = "";
            ExtValue2 = "";
            ExtValue3 = "";
            ExtValue4 = "";
            ExtValue5 = "";
        }
    }
}
