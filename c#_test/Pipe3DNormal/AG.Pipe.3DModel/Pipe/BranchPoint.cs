using ESRI.ArcGIS.Geometry;

namespace AG.Pipe.Analyst3DModel
{
    public class BranchPoint
    {
        private double m_h = 0;
        private double m_w = 0;
        private double m_r = 0;
        /// <summary>
        /// 起点
        /// </summary>
        public IPoint FromPnt { get; set; }
        /// <summary>
        /// 终点
        /// </summary>
        public IPoint ToPnt { get; set; }
        /// <summary>
        /// 宽
        /// </summary>
        public double W { get { return m_w; } set { m_w = value; } }
        /// <summary>
        /// 高
        /// </summary>
        public double H { get { return m_h; } set { m_h = value; } }
        /// <summary>
        /// 半径
        /// </summary>
        public double R { get { return m_h / 2; } }
        /// <summary>
        /// 类型
        /// </summary>
        public Line3DType DType { get; set; } = Line3DType.Circle;
    }

    ///// <summary>
    ///// 圆支管起始点
    ///// </summary>
    //public class CirleBranchPoint : BranchPoint
    //{
    //    /// <summary>
    //    /// 管径
    //    /// </summary>
    //    public double R { get; set; }

    //}
    ///// <summary>
    ///// 方管线
    ///// </summary>
    //public class CubeBranchPoint: BranchPoint
    //{
    //    public double W { get; set; }
    //    public double H { get; set; }

    //}
}
