namespace AG.COM.SDM.StylePropertyEdit
{
	/// <summary>
	/// ����Ԥ���ӿ�
	/// </summary>
	public interface ISymbolPreview
	{
		/// <summary>
		/// ������ͼ
		/// </summary>
		/// <param name="pSymbol">����</param>
		void UpdateView(object pSymbol);
	}
	
    /// <summary>
	/// �������Ա༭�ӿ�
	/// </summary>
	public interface ISymbolPropertyEdit
	{
		/// <summary>
		/// ����������ͼ
		/// </summary>
		/// <param name="pSymbol">����</param>
		void UpdatePropertyView(object pSymbol);
		
        /// <summary>
		/// ����������ͼ
		/// </summary>
		/// <param name="pSymbol">����</param>
		/// <param name="iLayerIndex">ͼ������</param>
		void UpdatePropertyView(object pSymbol, int iLayerIndex);
		
        /// <summary>
		/// ��ȡ��ǰѡ��ķ�������
		/// </summary>
		/// <returns>������������</returns>
		string GetSelectSymbolType();
	}

    /// <summary>
	/// ����ͼ��ӿ�
	/// </summary>
	public interface ISymbolLayer
	{
		/// <summary>
		/// ����ͼ����ͼ
		/// </summary>
		void UpdateLayerView(object pSymbol);
		
        /// <summary>
		/// ����ͼ����ͼ
		/// </summary>
		/// <param name="pSymbol">����</param>
		/// <param name="iLayerIndex">ͼ������</param>
		void UpdateLayerView(object pSymbol, int iLayerIndex);
	}
}
