namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// �������Կؼ�
	/// </summary>
	public interface ICtrlSymbolProperty
	{
		/// <summary>
		/// ��ȡ�����õ�ǰ����
		/// </summary>
		object CtrlSymbol
		{
			get;
			set;
		}
		/// <summary>
        /// ��ȡ�����õ�ǰ����ͼ��
		/// </summary>
		ISymbolLayer LayerSymbol
		{
			get;
			set;
		}
		/// <summary>
        /// ��ȡ�����õ�ǰ����ͼ������
		/// </summary>
		int LayerIndex
		{
			get;
			set;
		}
	}
}
