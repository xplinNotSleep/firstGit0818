namespace AG.COM.SDM.Utility.Common
{
    /// <summary>
    /// 进度条显示接口
    /// </summary>
    public interface ITrackProgress
    {
        /// <summary>
        /// 设置是否显示总进度栏 
        /// true 显示 false 不显示
        /// </summary>
        bool DisplayTotal { set; }

        /// <summary>
        /// 是否继续运行
        /// 如果是则返回 true,否则返回 false
        /// </summary>
        bool IsContinue { get; }

        /// <summary>
        /// 标识完成状态
        /// </summary>
        void SetFinish();

        /// <summary>
        /// 获取或设置完成后是否自动关闭窗体
        /// </summary>
        bool AutoFinishClose { get;set;}

        /// <summary>
        /// 设置子进度条显示范围的最大值
        /// </summary>
        int SubMax { get;set; }

        /// <summary>
        /// 设置子进度提示信息
        /// </summary>
        string SubMessage { set; }

        /// <summary>
        /// 设置子进度条显示范围的最小值
        /// </summary>
        int SubMin { get;set; }

        /// <summary>
        /// 获取子进度显示百分比
        /// </summary>
        int SubPercent { get; }

        /// <summary>
        /// 设置子进度条当前显示值
        /// </summary>
        int SubValue { get;set; }

        /// <summary>
        /// 设置标题栏信息
        /// </summary>
        string Title { set; }

        /// <summary>
        /// 设置总进度条显示范围的最大值
        /// </summary>
        int TotalMax { get;set; }

        /// <summary>
        /// 设置总进度提示信息
        /// </summary>
        string TotalMessage { set; }

        /// <summary>
        /// 设置总进度条显示范围的最小值
        /// </summary>
        int TotalMin { get;set; }

        /// <summary>
        /// 获取当前总进度显示百分比
        /// </summary>
        int TotalPercent { get; }

        /// <summary>
        /// 设置总进度条当前显示值
        /// </summary>
        int TotalValue { get;set; }

        /// <summary>
        /// 显示窗体
        /// </summary>
        void Show(); 

        /// <summary>
        /// 关闭
        /// </summary>
        void Close();
    }
}
