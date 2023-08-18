namespace AG.COM.SDM.StylePropertyEdit
{
	/// <summary>
	/// 符号预览接口
	/// </summary>
	public interface ISymbolPreview
	{
		/// <summary>
		/// 更新视图
		/// </summary>
		/// <param name="pSymbol">符号</param>
		void UpdateView(object pSymbol);
	}
	
    /// <summary>
	/// 符号属性编辑接口
	/// </summary>
	public interface ISymbolPropertyEdit
	{
		/// <summary>
		/// 更新属性视图
		/// </summary>
		/// <param name="pSymbol">符号</param>
		void UpdatePropertyView(object pSymbol);
		
        /// <summary>
		/// 更新属性视图
		/// </summary>
		/// <param name="pSymbol">符号</param>
		/// <param name="iLayerIndex">图层索引</param>
		void UpdatePropertyView(object pSymbol, int iLayerIndex);
		
        /// <summary>
		/// 获取当前选择的符号类型
		/// </summary>
		/// <returns>符号类型名称</returns>
		string GetSelectSymbolType();
	}

    /// <summary>
	/// 符号图层接口
	/// </summary>
	public interface ISymbolLayer
	{
		/// <summary>
		/// 更新图层视图
		/// </summary>
		void UpdateLayerView(object pSymbol);
		
        /// <summary>
		/// 更新图层视图
		/// </summary>
		/// <param name="pSymbol">符号</param>
		/// <param name="iLayerIndex">图层索引</param>
		void UpdateLayerView(object pSymbol, int iLayerIndex);
	}
}
