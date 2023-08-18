using System.Windows.Forms;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// �������Ի���
	/// </summary>
	public class CtrlPropertyBase : UserControl, ICtrlSymbolProperty
	{
		/// <summary>
		/// ��������ҳ���Ӧ�ķ���
		/// </summary>
		protected object m_pCtrlSymbol = null;

		/// <summary>
		/// ��������ҳ���Ӧ����ͼ��
		/// </summary>
		protected ISymbolLayer m_pSymbolLayer = null;
		
        /// <summary>
		/// ����ͼ������
		/// </summary>
		protected int m_iLayerIndex = -1;
		
        /// <summary>
		/// ��ʼ�����
		/// </summary>
		protected bool m_bInitComplete = false;

		#region ICtrlSymbolPrpperty ��Ա
		/// <summary>
		/// ��ȡ�����õ�ǰ����
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
        /// ��ȡ�����õ�ǰ����ͼ��
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
        /// ��ȡ�����õ�ǰ����ͼ������
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
