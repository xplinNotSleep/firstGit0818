using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.COM.SDM.Utility
{
    public class RemarkAttribute : Attribute
    {
        public RemarkAttribute(string remark)
        {
            this.Remark = remark;
        }
        /// <summary>  
        /// 备注  
        /// </summary>  
        public string Remark { get; set; }
    }



}
