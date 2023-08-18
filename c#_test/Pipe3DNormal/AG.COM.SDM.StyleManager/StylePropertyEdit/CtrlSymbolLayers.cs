using System;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit
{
	/// <summary>
	/// 符号图层控件
	/// </summary>
	public partial class CtrlSymbolLayers : UserControl, ISymbolLayer
	{
		#region 字段/属性
		/// <summary>
		/// 当前编辑的符号
		/// </summary>
		private object m_pSymbol = null;
		
        /// <summary>
		/// 符号预览接口
		/// </summary>
		private ISymbolPreview m_pSymbolPreview = null;
		
        /// <summary>
		/// 符号属性编辑接口
		/// </summary>
		private ISymbolPropertyEdit m_pSymbolPropertyEdit = null;
		
        /// <summary>
		/// 初始化完毕
		/// </summary>
		protected bool m_bInitComplete = false;

		/// <summary>
        /// 获取或设置当前编辑的符号
		/// </summary>
		public object Symbol
		{
			set { m_pSymbol = value; }
			get { return m_pSymbol; }
		}
		/// <summary>
        /// 设置符号预览接口
		/// </summary>
		public ISymbolPreview SymbolPreview
		{
			set { m_pSymbolPreview = value; }
		}
		
        /// <summary>
        /// 设置符号属性编辑接口
		/// </summary>
		public ISymbolPropertyEdit SymbolPropertyEdit
		{
			set { m_pSymbolPropertyEdit = value; }
		}
		
        /// <summary>
        /// 设置图层列表是否显示复选框
		/// </summary>
		public bool HasCheckBoxes
		{
			set { this.listViewSymbolLayer.CheckBoxes = value; }
		}
		#endregion

		#region 初始化
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
		/// 获取符号图层
		/// </summary>
		private void GetSymbolLayers()
		{
			System.Windows.Forms.ImageList Smallimage = new ImageList();
			Smallimage.ImageSize = new Size(128, 32);
			ListViewItem lvItem;

			this.listViewSymbolLayer.Items.Clear();
			this.listViewSymbolLayer.Columns.Clear();

			this.listViewSymbolLayer.SmallImageList = Smallimage;
			this.listViewSymbolLayer.Columns.Add("符号", listViewSymbolLayer.Size.Width - 5, System.Windows.Forms.HorizontalAlignment.Center);

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

		#region ISymbolLayer 成员
		/// <summary>
		/// 更新符号图层显示
		/// </summary>
		/// <param name="pSymbol">符号</param>
		public void UpdateLayerView(object pSymbol)
		{
			if (pSymbol is ITextBackground) m_pSymbol = pSymbol;
			listViewSymbolLayer_ItemChecked(null, null);
		}
		
        /// <summary>
		/// 更新符号图层显示
		/// </summary>
		/// <param name="pSymbol">符号</param>
		/// <param name="iLayerIndex">图层索引</param>
		public void UpdateLayerView(object pSymbol, int iLayerIndex)
		{
			listViewSymbolLayer_ItemChecked(null, null);
			// 更新图层符号图片
			Image img = StyleCommon.CreatePictureFromSymbol(pSymbol, 128, 32, 1);;
			if(null != img) this.listViewSymbolLayer.SmallImageList.Images[iLayerIndex] = img;
			this.listViewSymbolLayer.Invalidate();
		}
		#endregion

		#region 控件响应事件
		/// <summary>
		/// 符号图层选择改变
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
		/// 图层项复选框更改
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
		/// 设置掩膜
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

		#region 符号图层操作
		/// <summary>
		/// 添加图层
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAdd_Click(object sender, EventArgs e)
		{
			string strSymbolType = "";
			if (null != m_pSymbolPropertyEdit) strSymbolType = this.m_pSymbolPropertyEdit.GetSelectSymbolType();
			object pLayerSymbol = null;
			// 标记符号图层
			if (m_pSymbol is IMultiLayerMarkerSymbol)
			{
				#region 标记符号图层
				IMultiLayerMarkerSymbol pMultLayer = m_pSymbol as IMultiLayerMarkerSymbol;
				if (strSymbolType == EnumMarkerSymbolType.简单点状符号.ToString())
				{
					pLayerSymbol = new SimpleMarkerSymbolClass();
				}
				else if (strSymbolType == EnumMarkerSymbolType.字体点状符号.ToString())
				{
					pLayerSymbol = new CharacterMarkerSymbolClass();
				}
				else if (strSymbolType == EnumMarkerSymbolType.箭头点状符号.ToString())
				{
					pLayerSymbol = new ArrowMarkerSymbolClass();
				}
				else if (strSymbolType == EnumMarkerSymbolType.图片点状符号.ToString())
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
			// 线状符号图层
			else if (m_pSymbol is IMultiLayerLineSymbol)
			{
				#region 线状符号图层
				IMultiLayerLineSymbol pMultLayer = m_pSymbol as IMultiLayerLineSymbol;
				if (strSymbolType == EnumLineSymbolType.简单线状符号.ToString())
				{
					pLayerSymbol = new SimpleLineSymbolClass();
				}
				else if (strSymbolType == EnumLineSymbolType.图片线状符号.ToString())
				{
					pLayerSymbol = new PictureLineSymbolClass();
				}
				else if (strSymbolType == EnumLineSymbolType.哈希线状符号.ToString())
				{
					pLayerSymbol = new HashLineSymbolClass();
				}
				else if (strSymbolType == EnumLineSymbolType.标记线状符号.ToString())
				{
					pLayerSymbol = new MarkerLineSymbolClass();
				}
				else if (strSymbolType == EnumLineSymbolType.地图线状符号.ToString())
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
			// 填充符号图层
			else if (m_pSymbol is IMultiLayerFillSymbol)
			{
				#region 线状符号图层
				IMultiLayerFillSymbol pMultLayer = m_pSymbol as IMultiLayerFillSymbol;
				if (strSymbolType == EnumFillSymbolType.简单填充符号.ToString())
				{
					pLayerSymbol = new SimpleFillSymbolClass();
				}
				else if (strSymbolType == EnumFillSymbolType.图片填充符号.ToString())
				{
					pLayerSymbol = new PictureFillSymbolClass();
				}
				else if (strSymbolType == EnumFillSymbolType.标记填充符号.ToString())
				{
					pLayerSymbol = new MarkerFillSymbolClass();
				}
				else if (strSymbolType == EnumFillSymbolType.线填充符号.ToString())
				{
					pLayerSymbol = new LineFillSymbolClass();
				}
				else if (strSymbolType == EnumFillSymbolType.渐变填充符号.ToString())
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
			// 线装饰
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
			// 获取新添加的符号图层
			object pSymbol = StyleCommon.GetLayerSymbol(m_pSymbol,this.listViewSymbolLayer.Items.Count);
			if (null == pSymbol) return;
			// 添加符号图层到列表中
			listViewSymbolLayer.SmallImageList.Images.Add(StyleCommon.CreatePictureFromSymbol(pSymbol, 128, 32, 1));
			ListViewItem lvItem = new ListViewItem(new string[] { "" }, this.listViewSymbolLayer.Items.Count);
			lvItem.Checked = true;
			lvItem.Tag = pSymbol;
			this.listViewSymbolLayer.Items.Add(lvItem);
			// 选择新添加的符号图层，刷新符号预览视图显示
			this.listViewSymbolLayer.Items[this.listViewSymbolLayer.Items.Count - 1].Selected = true;
			listViewSymbolLayer_ItemChecked(null, null);
		}
	
        /// <summary>
		/// 删除图层
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDel_Click(object sender, EventArgs e)
		{
			if (this.listViewSymbolLayer.SelectedItems.Count < 1 || this.listViewSymbolLayer.Items.Count <= 1) return;
			if (MessageBox.Show("要删除选择的符号图层吗？", "删除图层", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
			object pLayerSymbol = StyleCommon.GetLayerSymbol(m_pSymbol, this.listViewSymbolLayer.SelectedItems[0].Index);
			if (null == pLayerSymbol) return;
			// 标记符号图层
			if (m_pSymbol is IMultiLayerMarkerSymbol)
			{
				IMultiLayerMarkerSymbol pMultLayer = m_pSymbol as IMultiLayerMarkerSymbol;
				pMultLayer.DeleteLayer(pLayerSymbol as IMarkerSymbol);
				this.listViewSymbolLayer.Items.RemoveAt(this.listViewSymbolLayer.SelectedItems[0].Index);
			}
			// 线状符号图层
			else if (m_pSymbol is IMultiLayerLineSymbol)
			{
				IMultiLayerLineSymbol pMultLayer = m_pSymbol as IMultiLayerLineSymbol;
				pMultLayer.DeleteLayer(pLayerSymbol as ILineSymbol);
				this.listViewSymbolLayer.Items.RemoveAt(this.listViewSymbolLayer.SelectedItems[0].Index);
			}
			// 填充符号图层
			else if (m_pSymbol is IMultiLayerFillSymbol)
			{
				IMultiLayerFillSymbol pMultLayer = m_pSymbol as IMultiLayerFillSymbol;
				pMultLayer.DeleteLayer(pLayerSymbol as IFillSymbol);
				this.listViewSymbolLayer.Items.RemoveAt(this.listViewSymbolLayer.SelectedItems[0].Index);
			}
			// 线装饰
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
		/// 上移图层
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
			// 标记符号图层
			if (m_pSymbol is IMultiLayerMarkerSymbol)
			{
				IMultiLayerMarkerSymbol pMultLayer = m_pSymbol as IMultiLayerMarkerSymbol;
				pMultLayer.MoveLayer(pLayerSymbol as IMarkerSymbol, iIndex - 1);
				MoveListViewItem(lvItem, iIndex-1);
			}
			// 线状符号图层
			else if (m_pSymbol is IMultiLayerLineSymbol)
			{
				IMultiLayerLineSymbol pMultLayer = m_pSymbol as IMultiLayerLineSymbol;
				pMultLayer.MoveLayer(pLayerSymbol as ILineSymbol, iIndex - 1);
				MoveListViewItem(lvItem, iIndex-1);
			}
			// 填充符号图层
			else if (m_pSymbol is IMultiLayerFillSymbol)
			{
				IMultiLayerFillSymbol pMultLayer = m_pSymbol as IMultiLayerFillSymbol;
				pMultLayer.MoveLayer(pLayerSymbol as IFillSymbol, iIndex - 1);
				MoveListViewItem(lvItem, iIndex - 1);
			}
			// 线装饰
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
		/// 下移图层
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
			// 标记符号图层
			if (m_pSymbol is IMultiLayerMarkerSymbol)
			{
				IMultiLayerMarkerSymbol pMultLayer = m_pSymbol as IMultiLayerMarkerSymbol;
				pMultLayer.MoveLayer(pLayerSymbol as IMarkerSymbol, iIndex + 1);
				MoveListViewItem(lvItem, iIndex);
			}
			// 线状符号图层
			else if (m_pSymbol is IMultiLayerLineSymbol)
			{
				IMultiLayerLineSymbol pMultLayer = m_pSymbol as IMultiLayerLineSymbol;
				pMultLayer.MoveLayer(pLayerSymbol as ILineSymbol, iIndex + 1);
				MoveListViewItem(lvItem, iIndex);
			}
			// 填充符号图层
			else if (m_pSymbol is IMultiLayerFillSymbol)
			{
				IMultiLayerFillSymbol pMultLayer = m_pSymbol as IMultiLayerFillSymbol;
				pMultLayer.MoveLayer(pLayerSymbol as IFillSymbol, iIndex + 1);
				MoveListViewItem(lvItem, iIndex);
			}
			// 线装饰
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
		/// 移动列表项
		/// </summary>
		/// <param name="lvItem">列表项</param>
		/// <param name="iIndex">新位置</param>
		private void MoveListViewItem(ListViewItem lvItem,int iIndex)
		{
			this.listViewSymbolLayer.BeginUpdate();
			this.listViewSymbolLayer.Items.Remove(lvItem);
			this.listViewSymbolLayer.Items.Insert(iIndex, lvItem);
			// 更新符号图层图像列表
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
