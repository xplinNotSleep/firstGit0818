using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit
{
	/// <summary>
	/// 符号属性编辑器
	/// </summary>
	public partial class FormSymbolPropertyEdit : Form
	{
		#region 字段/属性
		/// <summary>
		/// 当前编辑的符号
		/// </summary>
		private IStyleGalleryItem m_pStyleGalleryItem = null;
		
        /// <summary>
		/// 当前编辑的符号
		/// </summary>
		private object m_pSymbol = null;

		/// <summary>
		/// 当前编辑的符号类型
		/// </summary>
		EnumSymbolType m_SymbolType = EnumSymbolType.SymbolTypeUnknow;

		/// <summary>
		/// 当前编辑的符号
		/// </summary>
		public object Symbol
		{
			get { if (m_pSymbol is ITextBackground) return this.ctrlSymbolLayers1.Symbol;else return m_pSymbol; }
		}
		/// <summary>
		/// 图层列表是否显示复选框
		/// </summary>
		public bool HasCheckBoxes
		{
			set { if (null != ctrlSymbolLayers1) ctrlSymbolLayers1.HasCheckBoxes = value; }
		}

		#endregion

		#region 初始化
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="pSGItem">符号项</param>
		public FormSymbolPropertyEdit(IStyleGalleryItem pSGItem)
		{
			Pretreatment(pSGItem);
			m_SymbolType = GetSymbolType();

			InitializeComponent();

			// 符号预览设置
			ctrlSymbolPreview1.SymbolType = m_SymbolType;
			// 符号图层设置
			ctrlSymbolLayers1.Symbol = m_pSymbol;
			ctrlSymbolLayers1.SymbolPropertyEdit = ctrlSymbolProperty1;
			ctrlSymbolLayers1.SymbolPreview = ctrlSymbolPreview1;
			// 符号属性设置
			ctrlSymbolProperty1.SymbolType = m_SymbolType;
			ctrlSymbolProperty1.SymbolLayer = ctrlSymbolLayers1;

			// 更新显示
			ctrlSymbolPreview1.UpdateView(m_pSymbol);
			if (m_pSymbol is ITextSymbol) ctrlSymbolProperty1.UpdatePropertyView(m_pSymbol, -1);
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="pSGItem">符号项</param>
		public FormSymbolPropertyEdit(object pSymbol)
		{
			Pretreatment(pSymbol);
			m_SymbolType = GetSymbolType();

			InitializeComponent();

			// 符号预览设置
			ctrlSymbolPreview1.SymbolType = m_SymbolType;
			// 符号图层设置
			ctrlSymbolLayers1.Symbol = m_pSymbol;
			ctrlSymbolLayers1.SymbolPropertyEdit = ctrlSymbolProperty1;
			ctrlSymbolLayers1.SymbolPreview = ctrlSymbolPreview1;
			// 符号属性设置
			ctrlSymbolProperty1.SymbolType = m_SymbolType;
			ctrlSymbolProperty1.SymbolLayer = ctrlSymbolLayers1;

			// 更新显示
			ctrlSymbolPreview1.UpdateView(m_pSymbol);
			if (m_pSymbol is ITextSymbol || m_pSymbol is ITextBackground) ctrlSymbolProperty1.UpdatePropertyView(m_pSymbol, -1);
		}

		/// <summary>
		/// 窗体加载
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FormSymbolPropertyEdit_Load(object sender, EventArgs e)
		{
			if (null == m_pSymbol) return;
			if(m_pSymbol is ITextSymbol || m_pSymbol is ITextBackground)
			{
				this.panelStyleLayers.Visible = false;
				this.tableLayoutPanel1.SetRowSpan(this.panelStylePreview, 2);
			}
		}
		#endregion

		#region 其他
		/// <summary>
		/// 预处理
		/// </summary>
		/// <param name="pSGItem"></param>
		private void Pretreatment(IStyleGalleryItem pSGItem)
		{
			if (pSGItem == null) return;
			if (pSGItem.Item is SimpleFillSymbol)
			{
				IMultiLayerFillSymbol pMultiLayerFillSymbol = new MultiLayerFillSymbolClass();
				pMultiLayerFillSymbol.AddLayer(pSGItem.Item as IFillSymbol);
				pSGItem.Item = pMultiLayerFillSymbol;
				m_pStyleGalleryItem = pSGItem;
			}
			else if (pSGItem.Item is SimpleLineSymbol)
			{
				IMultiLayerLineSymbol pMultiLayerLineSymbol = new MultiLayerLineSymbolClass();
				pMultiLayerLineSymbol.AddLayer(pSGItem.Item as ILineSymbol);
				pSGItem.Item = pMultiLayerLineSymbol;
				m_pStyleGalleryItem = pSGItem;
			}
			else if (pSGItem.Item is SimpleMarkerSymbol)
			{
				IMultiLayerMarkerSymbol pMultiLayerMarkerSymbol = new MultiLayerMarkerSymbolClass();
				pMultiLayerMarkerSymbol.AddLayer(pSGItem.Item as IMarkerSymbol);
				pSGItem.Item = pMultiLayerMarkerSymbol;
				m_pStyleGalleryItem = pSGItem;
			}
			else if (pSGItem.Item is IMarker3DSymbol)
			{
				IMultiLayerMarkerSymbol pMultiLayerMarkerSymbol = new MultiLayerMarkerSymbolClass();
				pMultiLayerMarkerSymbol.AddLayer(pSGItem.Item as IMarkerSymbol);
				pSGItem.Item = pMultiLayerMarkerSymbol;
				m_pStyleGalleryItem = pSGItem;
			}
			else m_pStyleGalleryItem = pSGItem;

			// 当前编辑的符号
			m_pSymbol = pSGItem.Item as ISymbol;
		}
	
        /// <summary>
		/// 预处理
		/// </summary>
		/// <param name="pSymbol"></param>
		private void Pretreatment(object pSymbol)
		{
			if (pSymbol == null) return;
			if (pSymbol is SimpleFillSymbol)
			{
				IMultiLayerFillSymbol pMultiLayerFillSymbol = new MultiLayerFillSymbolClass();
				pMultiLayerFillSymbol.AddLayer(pSymbol as IFillSymbol);
				m_pSymbol = pMultiLayerFillSymbol;
			}
			else if (pSymbol is SimpleLineSymbol)
			{
				IMultiLayerLineSymbol pMultiLayerLineSymbol = new MultiLayerLineSymbolClass();
				pMultiLayerLineSymbol.AddLayer(pSymbol as ILineSymbol);
				m_pSymbol = pMultiLayerLineSymbol;
			}
			else if (pSymbol is SimpleMarkerSymbol)
			{
				IMultiLayerMarkerSymbol pMultiLayerMarkerSymbol = new MultiLayerMarkerSymbolClass();
				pMultiLayerMarkerSymbol.AddLayer(pSymbol as IMarkerSymbol);
				m_pSymbol = pMultiLayerMarkerSymbol;
			}
			else
			{
				m_pSymbol = pSymbol;
			}
		}

		/// <summary>
		/// 获取当前符号类型
		/// </summary>
		/// <returns></returns>
		private EnumSymbolType GetSymbolType()
		{
			if (m_pSymbol == null) return EnumSymbolType.SymbolTypeUnknow;

			if (m_pSymbol is IMarkerSymbol) return EnumSymbolType.SymbolTypeMarker;
			else if (m_pSymbol is ILineSymbol) return EnumSymbolType.SymbolTypeLine;
			else if (m_pSymbol is IFillSymbol) return EnumSymbolType.SymbolTypeFill;
			else if (m_pSymbol is ITextSymbol) return EnumSymbolType.SymbolTypeText;
			else if (m_pSymbol is ITextBackground) return EnumSymbolType.SymbolTypeTextBackground;
			else if (m_pSymbol is ILineDecoration) return EnumSymbolType.SymbolTypeLineDecoration;

			return EnumSymbolType.SymbolTypeUnknow;
		}
	
        /// <summary>
		/// 更改窗体大小
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FormSymbolPropertyEdit_Resize(object sender, EventArgs e)
		{
			// 更新显示
			ctrlSymbolPreview1.UpdateView();
		}
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {

        }
    }
}