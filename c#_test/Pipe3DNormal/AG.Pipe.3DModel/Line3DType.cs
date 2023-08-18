using System;
using System.ComponentModel;

namespace AG.Pipe.Analyst3DModel
{
    [Serializable]
    public enum Line3DType
    {
        /// <summary>
        /// 圆管
        /// </summary>
        [Description("圆管")]
        Circle = 0,
        /// <summary>
        /// 方管
        /// </summary>
        [Description("方管")]
        Square = 1
    }

    /// <summary>
    /// 井盖类型
    /// </summary>
    public enum Sub3DType
    {
        [Description("圆形")]
        Circle = 0,
        /// <summary>
        /// 方管
        /// </summary>
        [Description("方形")]
        Square = 1
    }
}
