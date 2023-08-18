using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//用于存放公共的枚举
namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// 管理功能中信息Form的使用情况，一般指增删改查
    /// </summary>
    public enum InfoFormUseState
    {
        Add,
        Edit,
        Delete,
        View
    }

    /// <summary>
    /// 管理功能中管理Form的使用情况
    /// </summary>
    public enum ManagerFormUseState
    {
        Default,
        Select
    }
}
