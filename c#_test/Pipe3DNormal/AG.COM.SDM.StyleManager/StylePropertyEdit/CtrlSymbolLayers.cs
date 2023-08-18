using System;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit
{
	/// <summary>
	/// ����ͼ��ؼ�
	/// </summary>
	public partial class CtrlSymbolLayers : UserControl, ISymbolLayer
	{
		#region �ֶ�/����
		/// <summary>
		/// ��ǰ�༭�ķ���
		/// </summary>
		private object m_pSymbol = null;
		
        /// <summary>
		/// ����Ԥ���ӿ�
		/// </summary>
		private ISymbolPreview m_pSymbolPreview = null;
		
        /// <summary>
		/// �������Ա༭�ӿ�
		/// </summary>
		private ISymbolPropertyEdit m_pSymbolPropertyEdit = null;
		
        /// <summary>
		/// ��ʼ�����
		/// </summary>
		protected bool m_bInitComplete = false;

		/// <summary>
        /// ��ȡ�����õ�ǰ�༭�ķ���
		/// </summary>
		public object Symbol
		{
			set { m_pSymbol = value; }
			get { return m_pSymbol; }
		}
		/// <summary>
        /// ���÷���Ԥ���ӿ�
		/// </summary>
		public ISymbolPreview SymbolPreview
		{
			set { m_pSymbolPreview = value; }
		}
		
        /// <summary>
        /// ���÷������Ա༭�ӿ�
		/// </summary>
		public ISymbolPropertyEdit SymbolPropertyEdit
		{
			set { m_pSymbolPropertyEdit = value; }
		}
		
        /// <summary>
        /// ����ͼ���б��Ƿ���ʾ��ѡ��
		/// </summary>
		public bool HasCheckBoxes
		{
			set { this.listViewSymbolLayer.CheckBoxes = value; }
		}
		#endregion

		#region ��ʼ��
		public CtrlSymbolLayers()
		{
			InitializeComponent();
		}

		private void CtrlSymbolLayers_Load(object sender, EventArgs e)
		{
			if (null == m_pSymbol) return;
			GetSymbolLayers();
			if (this.listViewSymbolLayer.Items.Count > 0) this.listViewSymbolLayer.Items[0].Selected = true;
			m_bInitComplete = true;
		}

		/// <summary>
		/// ��ȡ����ͼ��
		/// </summary>
		private void GetSymbolLayers()
		{
			System.Windows.Forms.ImageList Smallimage = new ImageList();
			Smallimage.ImageSize = new Size(128, 32);
			ListViewItem lvItem;

			this.listViewSymbolLayer.Items.Clear();
			this.listViewSymbolLayer.Columns.Clear();

			this.listViewSymbolLayer.SmallImageList = Smallimage;
			this.listViewSymbolLayer.Columns.Add("����", listViewSymbolLayer.Size.Width - 5, System.Windows.Forms.HorizontalAlignment.Center);

			int iLayerCount = StyleCommon.GetLayerCount(m_pSymbol);

			for (int i = 0; i < iLayerCount;i++ )
			{
				object pSymbol = StyleCommon.GetLayerSymbol(m_pSymbol,i);
				if (null == pSymbol) continue;
				Smallimage.Images.Add(StyleCommon.CreatePictureFromSymbol(pSymbol, 128, 32, 1));
				lvItem = new ListViewItem(new string[] { "" }, i);
				lvItem.Checked = true;
				lvItem.Tag = pSymbol;
				this.listViewSymbolLayer.Items.Add(lvItem);
			}

		}
		#endregion

		#region ISymbolLayer ��Ա
		/// <summary>
		/// ���·���ͼ����ʾ
		/// </summary>
		/// <param name="pSymbol">����</param>
		public void UpdateLayerView(object pSymbol)
		{
			if (pSymbol is ITextBackground) m_pSymbol = pSymbol;
			listViewSymbolLayer_ItemChecked(null, null);
		}
		
        /// <summary>
		/// ���·���ͼ����ʾ
		/// </summary>
		/// <param name="pSymbol">����</param>
		/// <param name="iLayerIndex">ͼ������</param>
		public void UpdateLayerView(object pSymbol, int iLayerIndex)
		{
			listViewSymbolLayer_ItemChecked(null, null);
			// ����ͼ�����ͼƬ
			Image img = StyleCommon.CreatePictureFromSymbol(pSymbol, 128, 32, 1);;
			if(null != img) this.listViewSymbolLayer.SmallImageList.Images[iLayerIndex] = img;
			this.listViewSymbolLayer.Invalidate();
		}
		#endregion

		#region �ؼ���Ӧ�¼�
		/// <summary>
		/// ����ͼ��ѡ��ı�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewSymbolLayer_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.listViewSymbolLayer.SelectedItems.Count < 1) return;
			if (null != this.listViewSymbolLayer.SelectedItems[0].Tag && null != m_pSymbolPropertyEdit) 
			{
				m_pSymbolPropertyEdit.UpdatePropertyView(m_pSymbol, this.listViewSymbolLayer.SelectedItems[0].Index);
			}
		}
		
        /// <summary>
		/// ͼ���ѡ�����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewSymbolLayer_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if (!m_bInitComplete) return;

			if (m_pSymbol is IMultiLayerMarkerSymbol)
			{
				IMultiLayerMarkerSymbol pMultLayer = new MultiLayerMarkerSymbolClass();
				for (int i = this.listViewSymbolLayer.CheckedItems.Count-1; i > -1; i--)
				{
					pMultLayer.AddLayer(StyleCommon.GetLayerSymbol(m_pSymbol, this.listViewSymbolLayer.CheckedItems[i].Index) as IMarkerSymbol);
				}
				SetMask(pMultLayer as IMask);
				if (null != m_pSymbolPreview) this.m_pSymbolPreview.UpdateView(pMultLayer);
			}
			else if (m_pSymbol is IMultiLayerLineSymbol)
			{
				IMultiLayerLineSymbol pMultLayer = new MultiLayerLineSymbolClass();
				for (int i = this.listViewSymbolLayer.CheckedItems.Count - 1; i > -1; i--)
				{
					pMultLayer.AddLayer(StyleCommon.GetLayerSymbol(m_pSymbol, this.listViewSymbolLayer.CheckedItems[i].Index) as ILineSymbol);
				}
				SetMask(pMultLayer as IMask);
				if (null != m_pSymbolPreview) this.m_pSymbolPreview.UpdateView(pMultLayer);
			}
			else if (m_pSymbol is IMultiLayerFillSymbol)
			{
				IMultiLayerFillSymbol pMultLayer = new MultiLayerFillSymbolClass();
				for (int i = this.listViewSymbolLayer.CheckedItems.Count - 1; i > -1; i--)
				{
					pMultLayer.AddLayer(StyleCommon.GetLayerSymbol(m_pSymbol, this.listViewSymbolLayer.CheckedItems[i].Index) as IFillSymbol);
				}
				SetMask(pMultLayer as IMask);
				if (null != m_pSymbolPreview) this.m_pSymbolPreview.UpdateView(pMultLayer);
			}
			else if (null != m_pSymbolPreview) this.m_pSymbolPreview.UpdateView(m_pSymbol);
		}
	
        /// <summary>
		/// ������Ĥ
		/// </summary>
		/// <param name="pMultLayeMask"></param>
		private void SetMask(IMask pMultLayeMask)
		{
			if (null == pMultLayeMask) return;
			IMask pMask = m_pSymbol as IMask;
			if (null == pMask) return;
			pMultLayeMask.MaskSize = pMask.MaskSize;
			pMultLayeMask.MaskStyle = pMask.MaskStyle;
			pMultLayeMask.MaskSymbol = pMask.MaskSymbol;
		}
		#endregion

		#region ����ͼ�����
		/// <summary>
		/// ���ͼ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAdd_Click(object sender, EventArgs e)
		{
			string strSymbolType = "";
			if (null != m_pSymbolPropertyEdit) strSymbolType = this.m_pSymbolPropertyEdit.GetSelectSymbolType();
			object pLayerSymbol = null;
			// ��Ƿ���ͼ��
			if (m_pSymbol is IMultiLayerMarkerSymbol)
			{
				#region ��Ƿ���ͼ��
				IMultiLayerMarkerSymbol pMultLayer = m_pSymbol as IMultiLayerMarkerSymbol;
				if (strSymbolType == EnumMarkerSymbolType.�򵥵�״����.ToString())
				{
					pLayerSymbol = new SimpleMarkerSymbolClass();
				}
				else if (strSymbolType == EnumMarkerSymbolType.�����״����.ToString())
				{
					pLayerSymbol = new CharacterMarkerSymbolClass();
				}
				else if (strSymbolType == EnumMarkerSymbolType.��ͷ��״����.ToString())
				{
					pLayerSymbol = new ArrowMarkerSymbolClass();
				}
				else if (strSymbolType == EnumMarkerSymbolType.ͼƬ��״����.ToString())
				{
					pLayerSymbol = new PictureMarkerSymbolClass();
				}
				else pLayerSymbol = new SimpleMarkerSymbolClass();
				if (null != pLayerSymbol)
				{
					pMultLayer.AddLayer(pLayerSymbol as IMarkerSymbol);
					pMultLayer.MoveLayer(pLayerSymbol as IMarkerSymbol,pMultLayer.LayerCount-1);
				}
				#endregion
			}
			// ��״����ͼ��
			else if (m_pSymbol is IMultiLayerLineSymbol)
			{
				#region ��״����ͼ��
				IMultiLayerLineSymbol pMultLayer = m_pSymbol as IMultiLayerLineSymbol;
				if (strSymbolType == EnumLineSymbolType.����״����.ToString())
				{
					pLayerSymbol = new SimpleLineSymbolClass();
				}
				else if (strSymbolType == EnumLineSymbolType.ͼƬ��״����.ToString())
				{
					pLayerSymbol = new PictureLineSymbolClass();
				}
				else if (strSymbolType == EnumLineSymbolType.��ϣ��״����.ToString())
				{
					pLayerSymbol = new HashLineSymbolClass();
				}
				else if (strSymbolType == EnumLineSymbolType.�����״����.ToString())
				{
					pLayerSymbol = new MarkerLineSymbolClass();
				}
				else if (strSymbolType == EnumLineSymbolType.��ͼ��״����.ToString())
				{
					pLayerSymbol = new CartographicLineSymbolClass();
				}
				else pLayerSymbol = new SimpleLineSymbolClass();
				if (null != pLayerSymbol)
				{
					pMultLayer.AddLayer(pLayerSymbol as ILineSymbol);
					pMultLayer.MoveLayer(pLayerSymbol as ILineSymbol, pMultLayer.LayerCount - 1);
				}
				#endregion
			}
			// ������ͼ��
			else if (m_pSymbol is IMultiLayerFillSymbol)
			{
				#region ��״����ͼ��
				IMultiLayerFillSymbol pMultLayer = m_pSymbol as IMultiLayerFillSymbol;
				if (strSymbolType == EnumFillSymbolType.��������.ToString())
				{
					pLayerSymbol = new SimpleFillSymbolClass();
				}
				else if (strSymbolType == EnumFillSymbolType.ͼƬ������.ToString())
				{
					pLayerSymbol = new PictureFillSymbolClass();
				}
				else if (strSymbolType == EnumFillSymbolType.���������.ToString())
				{
					pLayerSymbol = new MarkerFillSymbolClass();
				}
				else if (strSymbolType == EnumFillSymbolType.��������.ToString())
				{
					pLayerSymbol = new LineFillSymbolClass();
				}
				else if (strSymbolType == EnumFillSymbolType.����������.ToString())
				{
					pLayerSymbol = new GradientFillSymbolClass();
				}
				else pLayerSymbol = new SimpleFillSymbolClass();
				if (null != pLayerSymbol)
				{
					pMultLayer.AddLayer(pLayerSymbol as IFillSymbol);
					pMultLayer.MoveLayer(pLayerSymbol as IFillSymbol, pMultLayer.LayerCount - 1);
				}
				#endregion
			}
			// ��װ��
			else if(m_pSymbol is ILineDecoration)
			{
				ILineDecoration pMultLayer = m_pSymbol as ILineDecoration;
				pLayerSymbol = new SimpleLineDecorationElement();
				(pLayerSymbol as ISimpleLineDecorationElement).MarkerSymbol = new ArrowMarkerSymbolClass();
				(pLayerSymbol as ISimpleLineDecorationElement).Rotate = false;
				if(null != pLayerSymbol)
				{
					pMultLayer.AddElement(pLayerSymbol as ISimpleLineDecorationElement);
					pMultLayer.MoveElement(pLayerSymbol as ISimpleLineDecorationElement, pMultLayer.ElementCount - 1);
				}
			}
			// ��ȡ����ӵķ���ͼ��
			object pSymbol = StyleCommon.GetLayerSymbol(m_pSymbol,this.listViewSymbolLayer.Items.Count);
			if (null == pSymbol) return;
			// ��ӷ���ͼ�㵽�б���
			listViewSymbolLayer.SmallImageList.Images.Add(StyleCommon.CreatePictureFromSymbol(pSymbol, 128, 32, 1));
			ListViewItem lvItem = new ListViewItem(new string[] { "" }, this.listViewSymbolLayer.Items.Count);
			lvItem.Checked = true;
			lvItem.Tag = pSymbol;
			this.listViewSymbolLayer.Items.Add(lvItem);
			// ѡ������ӵķ���ͼ�㣬ˢ�·���Ԥ����ͼ��ʾ
			this.listViewSymbolLayer.Items[this.listViewSymbolLayer.Items.Count - 1].Selected = true;
			listViewSymbolLayer_ItemChecked(null, null);
		}
	
        /// <summary>
		/// ɾ��ͼ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDel_Click(object sender, EventArgs e)
		{
			if (this.listViewSymbolLayer.SelectedItems.Count < 1 || this.listViewSymbolLayer.Items.Count <= 1) return;
			if (MessageBox.Show("Ҫɾ��ѡ��ķ���ͼ����", "ɾ��ͼ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
			object pLayerSymbol = StyleCommon.GetLayerSymbol(m_pSymbol, this.listViewSymbolLayer.SelectedItems[0].Index);
			if (null == pLayerSymbol) return;
			// ��Ƿ���ͼ��
			if (m_pSymbol is IMultiLayerMarkerSymbol)
			{
				IMultiLayerMarkerSymbol pMultLayer = m_pSymbol as IMultiLayerMarkerSymbol;
				pMultLayer.DeleteLayer(pLayerSymbol as IMarkerSymbol);
				this.listViewSymbolLayer.Items.RemoveAt(this.listViewSymbolLayer.SelectedItems[0].Index);
			}
			// ��״����ͼ��
			else if (m_pSymbol is IMultiLayerLineSymbol)
			{
				IMultiLayerLineSymbol pMultLayer = m_pSymbol as IMultiLayerLineSymbol;
				pMultLayer.DeleteLayer(pLayerSymbol as ILineSymbol);
				this.listViewSymbolLayer.Items.RemoveAt(this.listViewSymbolLayer.SelectedItems[0].Index);
			}
			// ������ͼ��
			else if (m_pSymbol is IMultiLayerFillSymbol)
			{
				IMultiLayerFillSymbol pMultLayer = m_pSymbol as IMultiLayerFillSymbol;
				pMultLayer.DeleteLayer(pLayerSymbol as IFillSymbol);
				this.listViewSymbolLayer.Items.RemoveAt(this.listViewSymbolLayer.SelectedItems[0].Index);
			}
			// ��װ��
			else if (m_pSymbol is ILineDecoration)
			{
				ILineDecoration pMultLayer = m_pSymbol as ILineDecoration;
				pMultLayer.DeleteElement(this.listViewSymbolLayer.SelectedItems[0].Index);
				this.listViewSymbolLayer.Items.RemoveAt(this.listViewSymbolLayer.SelectedItems[0].Index);
			}
			if (this.listViewSymbolLayer.Items.Count > 0) this.listViewSymbolLayer.Items[0].Selected = true;
			listViewSymbolLayer_ItemChecked(null, null);
		}
	
        /// <summary>
		/// ����ͼ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnUp_Click(object sender, EventArgs e)
		{
			if (this.listViewSymbolLayer.SelectedItems.Count < 1) return;
			if (this.listViewSymbolLayer.SelectedItems[0].Index == 0) return;
			int iIndex = this.listViewSymbolLayer.SelectedItems[0].Index;
			object pLayerSymbol = StyleCommon.GetLayerSymbol(m_pSymbol,iIndex);
			ListViewItem lvItem = this.listViewSymbolLayer.SelectedItems[0];
			if (null == pLayerSymbol) return;
			// ��Ƿ���ͼ��
			if (m_pSymbol is IMultiLayerMarkerSymbol)
			{
				IMultiLayerMarkerSymbol pMultLayer = m_pSymbol as IMultiLayerMarkerSymbol;
				pMultLayer.MoveLayer(pLayerSymbol as IMarkerSymbol, iIndex - 1);
				MoveListViewItem(lvItem, iIndex-1);
			}
			// ��״����ͼ��
			else if (m_pSymbol is IMultiLayerLineSymbol)
			{
				IMultiLayerLineSymbol pMultLayer = m_pSymbol as IMultiLayerLineSymbol;
				pMultLayer.MoveLayer(pLayerSymbol as ILineSymbol, iIndex - 1);
				MoveListViewItem(lvItem, iIndex-1);
			}
			// ������ͼ��
			else if (m_pSymbol is IMultiLayerFillSymbol)
			{
				IMultiLayerFillSymbol pMultLayer = m_pSymbol as IMultiLayerFillSymbol;
				pMultLayer.MoveLayer(pLayerSymbol as IFillSymbol, iIndex - 1);
				MoveListViewItem(lvItem, iIndex - 1);
			}
			// ��װ��
			else if (m_pSymbol is ILineDecoration)
			{
				ILineDecoration pMultLayer = m_pSymbol as ILineDecoration;
				pMultLayer.MoveElement(pLayerSymbol as ISimpleLineDecorationElement, iIndex - 1);
				MoveListViewItem(lvItem, iIndex - 1);
			}
			this.listViewSymbolLayer.Items[iIndex - 1].Selected = true;
			listViewSymbolLayer_ItemChecked(null, null);
		}
	
        /// <summary>
		/// ����ͼ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDown_Click(object sender, EventArgs e)
		{
			if (this.listViewSymbolLayer.SelectedItems.Count < 1) return;
			if (this.listViewSymbolLayer.SelectedItems[0].Index == this.listViewSymbolLayer.Items.Count - 1) return;
			int iIndex = this.listViewSymbolLayer.SelectedItems[0].Index;
			object pLayerSymbol = StyleCommon.GetLayerSymbol(m_pSymbol, iIndex);
			ListViewItem lvItem = this.listViewSymbolLayer.SelectedItems[0];
			if (null == pLayerSymbol) return;
			// ��Ƿ���ͼ��
			if (m_pSymbol is IMultiLayerMarkerSymbol)
			{
				IMultiLayerMarkerSymbol pMultLayer = m_pSymbol as IMultiLayerMarkerSymbol;
				pMultLayer.MoveLayer(pLayerSymbol as IMarkerSymbol, iIndex + 1);
				MoveListViewItem(lvItem, iIndex);
			}
			// ��״����ͼ��
			else if (m_pSymbol is IMultiLayerLineSymbol)
			{
				IMultiLayerLineSymbol pMultLayer = m_pSymbol as IMultiLayerLineSymbol;
				pMultLayer.MoveLayer(pLayerSymbol as ILineSymbol, iIndex + 1);
				MoveListViewItem(lvItem, iIndex);
			}
			// ������ͼ��
			else if (m_pSymbol is IMultiLayerFillSymbol)
			{
				IMultiLayerFillSymbol pMultLayer = m_pSymbol as IMultiLayerFillSymbol;
				pMultLayer.MoveLayer(pLayerSymbol as IFillSymbol, iIndex + 1);
				MoveListViewItem(lvItem, iIndex);
			}
			// ��װ��
			else if (m_pSymbol is ILineDecoration)
			{
				ILineDecoration pMultLayer = m_pSymbol as ILineDecoration;
				pMultLayer.MoveElement(pLayerSymbol as ISimpleLineDecorationElement, iIndex + 1);
				MoveListViewItem(lvItem, iIndex);
			}
			this.listViewSymbolLayer.Items[iIndex + 1].Selected = true;
			listViewSymbolLayer_ItemChecked(null, null);
		}
	
        /// <summary>
		/// �ƶ��б���
		/// </summary>
		/// <param name="lvItem">�б���</param>
		/// <param name="iIndex">��λ��</param>
		private void MoveListViewItem(ListViewItem lvItem,int iIndex)
		{
			this.listViewSymbolLayer.BeginUpdate();
			this.listViewSymbolLayer.Items.Remove(lvItem);
			this.listViewSymbolLayer.Items.Insert(iIndex, lvItem);
			// ���·���ͼ��ͼ���б�
			this.listViewSymbolLayer.SmallImageList = null;
			System.Windows.Forms.ImageList Smallimage = new ImageList();
			Smallimage.ImageSize = new Size(128, 32);
			for (int i = 0; i < listViewSymbolLayer.Items.Count;i++ )
			{
				object pSymbol = StyleCommon.GetLayerSymbol(m_pSymbol, i);
				Smallimage.Images.Add(StyleCommon.CreatePictureFromSymbol(pSymbol, 128, 32, 1));
				listViewSymbolLayer.Items[i].Tag = pSymbol;
				listViewSymbolLayer.Items[i].ImageIndex = i;
			}
			this.listViewSymbolLayer.SmallImageList = Smallimage;
			this.listViewSymbolLayer.EndUpdate();
		}
		#endregion
	}
}
