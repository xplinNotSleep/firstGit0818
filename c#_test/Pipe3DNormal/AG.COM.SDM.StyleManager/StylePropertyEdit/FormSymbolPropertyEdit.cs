using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit
{
	/// <summary>
	/// �������Ա༭��
	/// </summary>
	public partial class FormSymbolPropertyEdit : Form
	{
		#region �ֶ�/����
		/// <summary>
		/// ��ǰ�༭�ķ���
		/// </summary>
		private IStyleGalleryItem m_pStyleGalleryItem = null;
		
        /// <summary>
		/// ��ǰ�༭�ķ���
		/// </summary>
		private object m_pSymbol = null;

		/// <summary>
		/// ��ǰ�༭�ķ�������
		/// </summary>
		EnumSymbolType m_SymbolType = EnumSymbolType.SymbolTypeUnknow;

		/// <summary>
		/// ��ǰ�༭�ķ���
		/// </summary>
		public object Symbol
		{
			get { if (m_pSymbol is ITextBackground) return this.ctrlSymbolLayers1.Symbol;else return m_pSymbol; }
		}
		/// <summary>
		/// ͼ���б��Ƿ���ʾ��ѡ��
		/// </summary>
		public bool HasCheckBoxes
		{
			set { if (null != ctrlSymbolLayers1) ctrlSymbolLayers1.HasCheckBoxes = value; }
		}

		#endregion

		#region ��ʼ��
		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="pSGItem">������</param>
		public FormSymbolPropertyEdit(IStyleGalleryItem pSGItem)
		{
			Pretreatment(pSGItem);
			m_SymbolType = GetSymbolType();

			InitializeComponent();

			// ����Ԥ������
			ctrlSymbolPreview1.SymbolType = m_SymbolType;
			// ����ͼ������
			ctrlSymbolLayers1.Symbol = m_pSymbol;
			ctrlSymbolLayers1.SymbolPropertyEdit = ctrlSymbolProperty1;
			ctrlSymbolLayers1.SymbolPreview = ctrlSymbolPreview1;
			// ������������
			ctrlSymbolProperty1.SymbolType = m_SymbolType;
			ctrlSymbolProperty1.SymbolLayer = ctrlSymbolLayers1;

			// ������ʾ
			ctrlSymbolPreview1.UpdateView(m_pSymbol);
			if (m_pSymbol is ITextSymbol) ctrlSymbolProperty1.UpdatePropertyView(m_pSymbol, -1);
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="pSGItem">������</param>
		public FormSymbolPropertyEdit(object pSymbol)
		{
			Pretreatment(pSymbol);
			m_SymbolType = GetSymbolType();

			InitializeComponent();

			// ����Ԥ������
			ctrlSymbolPreview1.SymbolType = m_SymbolType;
			// ����ͼ������
			ctrlSymbolLayers1.Symbol = m_pSymbol;
			ctrlSymbolLayers1.SymbolPropertyEdit = ctrlSymbolProperty1;
			ctrlSymbolLayers1.SymbolPreview = ctrlSymbolPreview1;
			// ������������
			ctrlSymbolProperty1.SymbolType = m_SymbolType;
			ctrlSymbolProperty1.SymbolLayer = ctrlSymbolLayers1;

			// ������ʾ
			ctrlSymbolPreview1.UpdateView(m_pSymbol);
			if (m_pSymbol is ITextSymbol || m_pSymbol is ITextBackground) ctrlSymbolProperty1.UpdatePropertyView(m_pSymbol, -1);
		}

		/// <summary>
		/// �������
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

		#region ����
		/// <summary>
		/// Ԥ����
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

			// ��ǰ�༭�ķ���
			m_pSymbol = pSGItem.Item as ISymbol;
		}
	
        /// <summary>
		/// Ԥ����
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
		/// ��ȡ��ǰ��������
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
		/// ���Ĵ����С
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FormSymbolPropertyEdit_Resize(object sender, EventArgs e)
		{
			// ������ʾ
			ctrlSymbolPreview1.UpdateView();
		}
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {

        }
    }
}