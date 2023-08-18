namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// 符号属性控件
	/// </summary>
	public interface ICtrlSymbolProperty
	{
		/// <summary>
		/// 获取或设置当前符号
		/// </summary>
		object CtrlSymbol
		{
			get;
			set;
		}
		/// <summary>
        /// 获取或设置当前符号图层
		/// </summary>
		ISymbolLayer LayerSymbol
		{
			get;
			set;
		}
		/// <summary>
        /// 获取或设置当前符号图层索引
		/// </summary>
		int LayerIndex
		{
			get;
			set;
		}
	}
}
