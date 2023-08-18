namespace AG.COM.SDM.StylePropertyEdit
{
	/// <summary>
	/// 符号类型
	/// </summary>
	public enum EnumSymbolType
	{
		/// <summary>
		/// 未知类型
		/// </summary>
		SymbolTypeUnknow,

		/// <summary>
		/// 点状符号
		/// </summary>
		SymbolTypeMarker,

		/// <summary>
		/// 线状符号
		/// </summary>
		SymbolTypeLine,

		/// <summary>
		/// 面状符号
		/// </summary>
		SymbolTypeFill,

		/// <summary>
		/// 文本符号
		/// </summary>
		SymbolTypeText,

		/// <summary>
		/// 线装饰符号
		/// </summary>
		SymbolTypeLineDecoration,

		/// <summary>
		/// 文本符号背景
		/// </summary>
		SymbolTypeTextBackground,
	}

	/// <summary>
	/// 点状符号类型
	/// </summary>
	public enum EnumMarkerSymbolType
	{
		简单3D标志符号,
		/// <summary>
		/// 简单点状符号
		/// </summary>
		简单点状符号,

		/// <summary>
		/// 字体点状符号
		/// </summary>
		字体点状符号,

		/// <summary>
		/// 箭头点状符号
		/// </summary>
		箭头点状符号,

		/// <summary>
		/// 图片点状符号
		/// </summary>
		图片点状符号,
       
	}

	/// <summary>
	/// 线状符号类型
	/// </summary>
	public enum EnumLineSymbolType
	{
		/// <summary>
		/// 简单线状符号
		/// </summary>
		简单线状符号,

		/// <summary>
		/// 制图线状符号
		/// </summary>
		地图线状符号,

		/// <summary>
		/// 线状符号
		/// </summary>
		哈希线状符号,

		/// <summary>
		/// 标记线状符号
		/// </summary>
		标记线状符号,

		/// <summary>
		/// 图片线状符号
		/// </summary>
		图片线状符号,
	}

	/// <summary>
	/// 面状符号类型
	/// </summary>
	public enum EnumFillSymbolType
	{
		/// <summary>
		/// 简单填充符号
		/// </summary>
		简单填充符号,

		/// <summary>
		/// 标记填充符号
		/// </summary>
		标记填充符号,

		/// <summary>
		/// 线填充符号
		/// </summary>
		线填充符号,

		/// <summary>
		/// 渐变填充符号
		/// </summary>
		渐变填充符号,

		/// <summary>
		/// 图片填充符号
		/// </summary>
		图片填充符号,
	}

	/// <summary>
	/// 面状符号类型
	/// </summary>
	public enum EnumTextSymbolType
	{
		/// <summary>
		/// 文本符号
		/// </summary>
		文本符号,
	}

	/// <summary>
	/// 线装饰类型
	/// </summary>
	public enum EnumLineDecorationType
	{
		/// <summary>
		/// 简单线形装饰
		/// </summary>
		简单线形装饰,

	}

	/// <summary>
	/// 文本符号背景类型
	/// </summary>
	public enum EnumTextBackgroundType
	{
		/// <summary>
		/// 气球提示符号
		/// </summary>
		气球提示符号,

		/// <summary>
		/// 线形提示符号
		/// </summary>
		线形提示符号,

		/// <summary>
		/// 标记文本背景
		/// </summary>
		标记文本背景,

		/// <summary>
		/// 简单线形提示符号
		/// </summary>
		简单线形提示符号,
	}
}
