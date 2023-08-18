namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 属性域对话框状态枚举
    /// </summary>
    public enum EnumDomainDlgState
    {
        /// <summary>
        /// 正常状态
        /// 指不能修改属性域名称及描述信息的编辑状态
        /// </summary>
        Normal,
        /// <summary>
        /// 编辑状态
        /// </summary>
        Editor,
        /// <summary>
        /// 浏览状态
        /// </summary>
        Browse,
        /// <summary>
        /// 添加状态
        /// </summary>
        New
    }
}
