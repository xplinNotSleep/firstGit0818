using System.Windows.Forms;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// 符号属性基类
	/// </summary>
	public class CtrlPropertyBase : UserControl, ICtrlSymbolProperty
	{
		/// <summary>
		/// 符号属性页面对应的符号
		/// </summary>
		protected object m_pCtrlSymbol = null;

		/// <summary>
		/// 符号属性页面对应符号图层
		/// </summary>
		protected ISymbolLayer m_pSymbolLayer = null;
		
        /// <summary>
		/// 符号图层索引
		/// </summary>
		protected int m_iLayerIndex = -1;
		
        /// <summary>
		/// 初始化完毕
		/// </summary>
		protected bool m_bInitComplete = false;

		#region ICtrlSymbolPrpperty 成员
		/// <summary>
		/// 获取或设置当前符号
		/// </summary>
		public object CtrlSymbol
		{
			get
			{
				return m_pCtrlSymbol;
			}
			set
			{
				m_pCtrlSymbol = value;
			}
		}

		/// <summary>
        /// 获取或设置当前符号图层
		/// </summary>
		public ISymbolLayer LayerSymbol
		{
			get
			{
				return m_pSymbolLayer;
			}
			set
			{
				m_pSymbolLayer = value;
			}
		}
		
        /// <summary>
        /// 获取或设置当前符号图层索引
		/// </summary>
		public int LayerIndex
		{
			get
			{
				return m_iLayerIndex;
			}
			set
			{
				m_iLayerIndex = value;
			}
		}

		#endregion
	}
}
