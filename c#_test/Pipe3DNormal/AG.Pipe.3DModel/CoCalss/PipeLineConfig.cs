using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AG.Pipe.Analyst3DModel
{
    #region
    /// <summary>
    /// 管线 配置信息
    /// </summary>
    //[Serializable]
    //public class PipeLineConfig
    //{
    //    /// <summary>
    //    /// 起始编号字段
    //    /// </summary>
    //    public string strS_Point { get; set; }
    //    /// <summary>
    //    /// 起始埋深字段
    //    /// </summary>
    //    public string strS_Deep { get; set; }
    //    /// <summary>
    //    /// 起始地面高程
    //    /// </summary>
    //    public string strS_Hight { get; set; }

    //    /// <summary>
    //    /// 终点编号字段
    //    /// </summary>
    //    public string strE_Point { get; set; }
    //    /// <summary>
    //    /// 终点埋深字段
    //    /// </summary>
    //    public string strE_Deep { get; set; }
    //    /// <summary>
    //    /// 终点地面高程字段
    //    /// </summary>
    //    public string strE_Hight { get; set; }
    //    /// <summary>
    //    /// 管径字段
    //    /// </summary>
    //    public string strPSize { get; set; }
    //    /// <summary>
    //    /// 管径类型
    //    /// </summary>
    //    public Line3DType LineType { get; set; } = Line3DType.Circle;

    //    /// <summary>
    //    /// 管线中心点高程
    //    /// </summary>
    //    public CalculationType CalculationType { get; set; } = CalculationType.Type1;

    //    /// <summary>
    //    /// 管线字段列表
    //    /// </summary>
    //    public List<string> Fields { get; set; } = new List<string>();

    //}

    #endregion

    [Serializable]
    public enum CalculationType
    {
        #region
        ///// <summary>
        ///// 管线中心点高程 = 管线高程(管底高程) + 管径 / 2 (包括雨水，污水，雨污合流)
        ///// </summary>
        //[Description("管线中心点高程 = 管线高程(管底高程) + 管径 / 2")]
        //Type1 = 0,
        ///// <summary>
        ///// 管线中心点高程 = 管线高程(管顶高程) - 管径 / 2
        ///// </summary>
        //[Description("管线中心点高程=管线高程(管顶高程) - 管径 / 2")]
        //Type2 = 1
        #endregion

        /// <summary>
        /// 管线中心点高程 = 管点地面高程 - 埋深 + 管径 / 2 (包括雨水，污水，雨污合流)
        /// </summary>
        [Description("管线中心点高程=地面高程-埋深+管径/2")]
        Type1 = 0,

        /// <summary>
        /// 管线中心点高程 = 管点地面高程 - 埋深 - 管径 / 2
        /// </summary>
        [Description("管线中心点高程=地面高程-埋深-管径/2")]
        Type2 = 1
    }

    [Serializable]
    public enum SubsidMinHType
    {
        /// <summary>
        /// 附属物最低点高程 = 管点地面高程 - 埋深 (- 管径)-0.1 
        /// </summary>
        [Description("附属物最低点高程=管点地面高程-埋深-0.1")]
        Type1 = 0,

        /// <summary>
        /// 附属物最低点高程 = 管点高程 - (- 管径)-0.1 
        /// </summary>
        [Description("附属物最低点高程=管点高程-0.1")]
        Type2 = 1

    }

}
