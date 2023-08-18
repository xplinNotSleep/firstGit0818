using System;
using System.Windows.Forms;
using AG.COM.SDM.StylePropertyEdit.PropertyCtrl;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DisplayUI;

namespace AG.COM.SDM.StylePropertyEdit
{
	/// <summary>
	/// 符号属性控件
	/// </summary>
	public partial class CtrlSymbolProperty : UserControl, ISymbolPropertyEdit
	{
		#region 字段/属性
		/// <summary>
		/// 当前编辑的符号类型
		/// </summary>
		EnumSymbolType m_SymbolType = EnumSymbolType.SymbolTypeUnknow;
	
        /// <summary>
		/// 当前编辑的符号
		/// </summary>
		private object m_pSymbol = null;
	
        /// <summary>
		/// 符号图层接口
		/// </summary>
		private ISymbolLayer m_pSymbolLayer = null;
		
        /// <summary>
		/// 符号图层索引
		/// </summary>
		private int m_iLayerIndex = -1;
	
        /// <summary>
		/// 重新设置属性页
		/// </summary>
		private bool m_bResetPropertyPage = false;

		/// <summary>
        /// 获取或设置当前编辑的符号类型
		/// </summary>
		public EnumSymbolType SymbolType
		{
			set { m_SymbolType = value; }
			get { return m_SymbolType; }
		}
		
        /// <summary>
        /// 获取或设置当前编辑的符号
		/// </summary>
		public object Symbol
		{
			set { m_pSymbol = value; }
			get { return m_pSymbol; }
		}
		
        /// <summary>
        /// 设置符号图层接口
		/// </summary>
		public ISymbolLayer SymbolLayer
		{
			set 
			{
				m_pSymbolLayer = value;
				switch (m_SymbolType)
				{
					case EnumSymbolType.SymbolTypeMarker:
						{
							foreach (string s in Enum.GetNames(typeof(EnumMarkerSymbolType)))
							{
								this.comboBoxSymbolType.Items.Add(s);
							}
						}
						break;
					case EnumSymbolType.SymbolTypeLine:
						{
							foreach (string s in Enum.GetNames(typeof(EnumLineSymbolType)))
							{
								this.comboBoxSymbolType.Items.Add(s);
							}
						}
						break;
					case EnumSymbolType.SymbolTypeFill:
						{
							foreach (string s in Enum.GetNames(typeof(EnumFillSymbolType)))
							{
								this.comboBoxSymbolType.Items.Add(s);
							}
						}
						break;
					case EnumSymbolType.SymbolTypeText:
						{
							foreach (string s in Enum.GetNames(typeof(EnumTextSymbolType)))
							{
								this.comboBoxSymbolType.Items.Add(s);
							}
						}
						break;
					case EnumSymbolType.SymbolTypeLineDecoration:
						{
							foreach (string s in Enum.GetNames(typeof(EnumLineDecorationType)))
							{
								this.comboBoxSymbolType.Items.Add(s);
							}
						}
						break;
					case EnumSymbolType.SymbolTypeTextBackground:
						{
							foreach (string s in Enum.GetNames(typeof(EnumTextBackgroundType)))
							{
								this.comboBoxSymbolType.Items.Add(s);
							}
						}
						break;
					default:
						break;
				}

			}
		}
		#endregion

		#region 初始化
		public CtrlSymbolProperty()
		{
			InitializeComponent();

		}

		private void CtrlSymbolProperty_Load(object sender, EventArgs e)
		{
			this.comboBoxSymbolUnit.SelectedIndex = 0;
		}
		#endregion

		#region ISymbolPropertyEdit 成员
		/// <summary>
		/// 更新属性视图
		/// </summary>
		/// <param name="pSymbol"></param>
		public void UpdatePropertyView(object pSymbol)
		{
		}
		
        /// <summary>
		/// 更新属性视图
		/// </summary>
		/// <param name="pSymbol"></param>
		public void UpdatePropertyView(object pSymbol, int iLayerIndex)
		{
			m_pSymbol = pSymbol;
			m_iLayerIndex = iLayerIndex;
			this.tabControlProperty.Controls.Clear();
			object pLayerSymbol = StyleCommon.GetLayerSymbol(m_pSymbol, iLayerIndex);
			// 根据图层符号添加属性页
			AddPropertyPage(pLayerSymbol);
		}
		#endregion

		#region 更新显示
		/// <summary>
		/// 获取当前选择的符号类型
		/// </summary>
		/// <returns>符号类型名称</returns>
		public string GetSelectSymbolType()
		{
			return this.comboBoxSymbolType.SelectedItem.ToString();
		}

		/// <summary>
		/// 根据图层符号添加属性页
		/// </summary>
		/// <param name="pLayerSymbol">图层符号</param>
		private void AddPropertyPage(object pLayerSymbol)
		{
			m_bResetPropertyPage = false;
			// 标记(点状)符号
			if (m_pSymbol is IMultiLayerMarkerSymbol)
			{
				CtrlPropertyBase ctrlmarker = null;
				if (pLayerSymbol is IMarker3DSymbol)
                {
                    ctrlmarker = new Ctrl3DMarker();
                    SetCBSymbolType(EnumMarkerSymbolType.简单3D标志符号.ToString());
                    //               ISymbolEditor styleItemEditor = new SymbolEditorClass();
                    //               ISymbol symbol = pLayerSymbol as ISymbol;
                    //               styleItemEditor.EditSymbol(ref symbol, 0);
                    //pLayerSymbol = symbol;

                }
                else if (pLayerSymbol is ISimpleMarkerSymbol)
				{
					ctrlmarker = new CtrlSimpleMarker();
					SetCBSymbolType(EnumMarkerSymbolType.简单点状符号.ToString());
				}
				else if (pLayerSymbol is ICharacterMarkerSymbol)
				{
					ctrlmarker = new CtrlCharacterMarker();
					SetCBSymbolType(EnumMarkerSymbolType.字体点状符号.ToString());
				}
				else if (pLayerSymbol is IArrowMarkerSymbol)
				{
					ctrlmarker = new CtrlArrowMarker();
					SetCBSymbolType(EnumMarkerSymbolType.箭头点状符号.ToString());
				}
				else if (pLayerSymbol is IPictureMarkerSymbol)
				{
					ctrlmarker = new CtrlPicture(EnumSymbolType.SymbolTypeMarker);
					SetCBSymbolType(EnumMarkerSymbolType.图片点状符号.ToString());
				}

				if (pLayerSymbol is IMarker3DSymbol)
                {
					AddSymbolPropertyPage(ctrlmarker, pLayerSymbol);
				}
				else
				{
					if (null != ctrlmarker)
						AddSymbolPropertyPage(ctrlmarker, pLayerSymbol);
					AddSymbolPropertyPage(new CtrlMask(), m_pSymbol);
				}

			}
			// 线状符号
			else if (m_pSymbol is IMultiLayerLineSymbol)
			{
				ILineSymbol pLineSymbol = pLayerSymbol as ILineSymbol;
				if (pLineSymbol is ISimpleLineSymbol)
				{
					AddSymbolPropertyPage(new CtrlSimpleLine(), pLayerSymbol);
					SetCBSymbolType(EnumLineSymbolType.简单线状符号.ToString());
				}
				else if (pLineSymbol is IPictureLineSymbol)
				{
					AddSymbolPropertyPage(new CtrlPicture(EnumSymbolType.SymbolTypeLine), pLayerSymbol);
					SetCBSymbolType(EnumLineSymbolType.图片线状符号.ToString());
				}
				else
				{
					if (pLineSymbol is IHashLineSymbol)
					{
						AddSymbolPropertyPage(new CtrlHashLine(), pLayerSymbol);
						SetCBSymbolType(EnumLineSymbolType.哈希线状符号.ToString());
					}
					else if (pLineSymbol is IMarkerLineSymbol)
					{
						AddSymbolPropertyPage(new CtrlMarkerLine(), pLayerSymbol);
						AddSymbolPropertyPage(new CtrlCartographicLine(), pLayerSymbol);
						SetCBSymbolType(EnumLineSymbolType.标记线状符号.ToString());
					}
					else if (pLineSymbol is CartographicLineSymbol)
					{
						AddSymbolPropertyPage(new CtrlCartographicLine(), pLayerSymbol);
						SetCBSymbolType(EnumLineSymbolType.地图线状符号.ToString());
					}
					AddSymbolPropertyPage(new CtrlTemplate(), pLayerSymbol);
					AddSymbolPropertyPage(new CtrlLineProperties(), pLayerSymbol);
				}
			}
			// 填充(面状)符号
			else if (m_pSymbol is IMultiLayerFillSymbol)
			{
				if (pLayerSymbol is ISimpleFillSymbol)
				{
					AddSymbolPropertyPage(new CtrlSimpleFill(), pLayerSymbol);
					SetCBSymbolType(EnumFillSymbolType.简单填充符号.ToString());
				}
				else if (pLayerSymbol is IPictureFillSymbol)
				{
					AddSymbolPropertyPage(new CtrlPicture(EnumSymbolType.SymbolTypeFill), pLayerSymbol);
					AddSymbolPropertyPage(new CtrlFillProperties(), pLayerSymbol);
					SetCBSymbolType(EnumFillSymbolType.图片填充符号.ToString());
				}
				else if (pLayerSymbol is IMarkerFillSymbol)
				{
					AddSymbolPropertyPage(new CtrlMarkerFill(), pLayerSymbol);
					AddSymbolPropertyPage(new CtrlFillProperties(), pLayerSymbol);
					SetCBSymbolType(EnumFillSymbolType.标记填充符号.ToString());
				}
				else if (pLayerSymbol is ILineFillSymbol)
				{
					AddSymbolPropertyPage(new CtrlLineFill(), pLayerSymbol);
					SetCBSymbolType(EnumFillSymbolType.线填充符号.ToString());
				}
				else if (pLayerSymbol is IGradientFillSymbol)
				{
					AddSymbolPropertyPage(new CtrlGradientFill(), pLayerSymbol);
					SetCBSymbolType(EnumFillSymbolType.渐变填充符号.ToString());
				}
			}
			// 文本符号
			else if (m_pSymbol is ITextSymbol)
			{
				AddSymbolPropertyPage(new CtrlGeneralText(), m_pSymbol);
				AddSymbolPropertyPage(new CtrlFormattedText(), m_pSymbol);
				AddSymbolPropertyPage(new CtrlAdvancedText(), m_pSymbol);
				AddSymbolPropertyPage(new CtrlMask(), m_pSymbol);
				SetCBSymbolType(EnumTextSymbolType.文本符号.ToString());
			}
			// 文本背景
			else if (m_pSymbol is ITextBackground)
			{
				if (pLayerSymbol is BalloonCallout)
				{
					AddSymbolPropertyPage(new CtrlBalloonCallout(), m_pSymbol);
					SetCBSymbolType(EnumTextBackgroundType.气球提示符号.ToString());
				}
				else if (pLayerSymbol is LineCallout)
				{
					AddSymbolPropertyPage(new CtrlLineCallout(), m_pSymbol);
					SetCBSymbolType(EnumTextBackgroundType.线形提示符号.ToString());
				}
				else if (pLayerSymbol is MarkerTextBackground)
				{
                    AddSymbolPropertyPage(new CtrlMarkerTextBackground(), m_pSymbol);
					SetCBSymbolType(EnumTextBackgroundType.标记文本背景.ToString());
				}
				else if (pLayerSymbol is SimpleLineCallout)
				{
					AddSymbolPropertyPage(new CtrlSimpleLineCallout(), m_pSymbol);
					SetCBSymbolType(EnumTextBackgroundType.简单线形提示符号.ToString());
				}
			}
			// 线装饰
			else if (m_pSymbol is ILineDecoration)
			{
				AddSymbolPropertyPage(new CtrlSimpleLineDecoration(), pLayerSymbol);
				SetCBSymbolType(EnumLineDecorationType.简单线形装饰.ToString());
			}
			m_bResetPropertyPage = true;
		}
	
        /// <summary>
		/// 添加符号属性页
		/// </summary>
		/// <param name="ctrlSP">属性页</param>
		/// <param name="pSymbol">符号</param>
		private void AddSymbolPropertyPage(CtrlPropertyBase ctrlSP,object pSymbol)
		{
			if(ctrlSP is Ctrl3DMarker)
            {
				//tabControlProperty.Visible = false;
				//tabControl3D.Visible = true;
				//axSceneControl1.Visible = true;

				//ctrlSP.CtrlSymbol = pSymbol;
				//ctrlSP.LayerSymbol = m_pSymbolLayer;
				//ctrlSP.LayerIndex = m_iLayerIndex;
				//TabPage pageSP = new TabPage();
				//pageSP.Controls.Add(ctrlSP);
				//tabControlProperty.Controls.Add(pageSP);
			}
			else
            {
				//tabControlProperty.Visible = true;
				//tabControl3D.Visible = false;
				//axSceneControl1.Visible = false;

				ctrlSP.CtrlSymbol = pSymbol;
				ctrlSP.LayerSymbol = m_pSymbolLayer;
				ctrlSP.LayerIndex = m_iLayerIndex;
				TabPage pageSP = new TabPage();
				pageSP.Text = ctrlSP.AccessibleName;
				pageSP.Controls.Add(ctrlSP);
				tabControlProperty.Controls.Add(pageSP);
			}
		}
	
        /// <summary>
		/// 设置符号类型下拉框
		/// </summary>
		/// <param name="strType">符号类型</param>
		private void SetCBSymbolType(string strType)
		{
			this.comboBoxSymbolType.SelectedIndex = comboBoxSymbolType.FindString(strType, 0);
		}
		#endregion

		#region 控件事件
		/// <summary>
		/// 更改类型
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxSymbolType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!m_bResetPropertyPage) return;
			object pLayerSymbol = null;
			// 标记(点状)符号
			if (m_pSymbol is IMultiLayerMarkerSymbol)
			{
				if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumMarkerSymbolType.简单3D标志符号.ToString())
                {
					pLayerSymbol = new Marker3DSymbolClass();
				}
		        if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumMarkerSymbolType.简单点状符号.ToString()) 
				{
					pLayerSymbol = new SimpleMarkerSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumMarkerSymbolType.字体点状符号.ToString()) 
				{
					pLayerSymbol = new CharacterMarkerSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumMarkerSymbolType.箭头点状符号.ToString())
				{
					pLayerSymbol = new ArrowMarkerSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumMarkerSymbolType.图片点状符号.ToString())
				{
					pLayerSymbol = new PictureMarkerSymbolClass();
				}
			}
			// 线状符号
			else if (m_pSymbol is IMultiLayerLineSymbol)
			{
				if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumLineSymbolType.简单线状符号.ToString())
				{
					pLayerSymbol = new SimpleLineSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumLineSymbolType.图片线状符号.ToString())
				{
					pLayerSymbol = new PictureLineSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumLineSymbolType.哈希线状符号.ToString())
				{
					pLayerSymbol = new HashLineSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumLineSymbolType.标记线状符号.ToString())
				{
					pLayerSymbol = new MarkerLineSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumLineSymbolType.地图线状符号.ToString())
				{
					pLayerSymbol = new CartographicLineSymbolClass();
				}
			}
			// 填充(面状)符号
			else if (m_pSymbol is IMultiLayerFillSymbol)
			{
				if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumFillSymbolType.简单填充符号.ToString())
				{
					pLayerSymbol = new SimpleFillSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumFillSymbolType.图片填充符号.ToString())
				{
					pLayerSymbol = new PictureFillSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumFillSymbolType.标记填充符号.ToString())
				{
					pLayerSymbol = new MarkerFillSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumFillSymbolType.线填充符号.ToString())
				{
					pLayerSymbol = new LineFillSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumFillSymbolType.渐变填充符号.ToString())
				{
					pLayerSymbol = new GradientFillSymbolClass();
				}
			}
			// 文本符号
			else if (m_pSymbol is ITextSymbol)
			{
				pLayerSymbol = new TextSymbolClass();
			}
			// 文本背景
			else if (m_pSymbol is ITextBackground)
			{
				if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumTextBackgroundType.气球提示符号.ToString())
				{
					pLayerSymbol = new BalloonCalloutClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumTextBackgroundType.线形提示符号.ToString())
				{
					pLayerSymbol = new LineCalloutClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumTextBackgroundType.标记文本背景.ToString())
				{
					pLayerSymbol = new MarkerTextBackgroundClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumTextBackgroundType.简单线形提示符号.ToString())
				{
					pLayerSymbol = new SimpleLineCalloutClass();
				}
			}
			// 线装饰
			else if (m_pSymbol is ILineDecoration)
			{
			}
			if (null == pLayerSymbol) return;
			ResetLayerSymbol(pLayerSymbol);

			tabControlProperty.Controls.Clear();
			// 根据图层符号添加属性页
			AddPropertyPage(pLayerSymbol);
			if (null != m_pSymbolLayer)
			{
				if (pLayerSymbol is ITextBackground)
				{
					m_pSymbolLayer.UpdateLayerView(m_pSymbol);
				}
				else
				{
					m_pSymbolLayer.UpdateLayerView(pLayerSymbol, m_iLayerIndex);
				}
			}
		}
	
        /// <summary>
		/// 重新设置图层符号
		/// </summary>
		/// <param name="pLayerSymbol"></param>
		private void ResetLayerSymbol(object pLayerSymbol)
		{
			if (null == pLayerSymbol) return;
			object pOldLayerSymbol = StyleCommon.GetLayerSymbol(m_pSymbol, m_iLayerIndex);
			// 标记(点状)符号
			if (m_pSymbol is IMultiLayerMarkerSymbol)
			{
				IMultiLayerMarkerSymbol pMultLayer = m_pSymbol as IMultiLayerMarkerSymbol;
				pMultLayer.DeleteLayer(pOldLayerSymbol as IMarkerSymbol);
				pMultLayer.AddLayer(pLayerSymbol as IMarkerSymbol);
				pMultLayer.MoveLayer(pLayerSymbol as IMarkerSymbol, m_iLayerIndex);
			}
			// 线状符号
			else if (m_pSymbol is IMultiLayerLineSymbol)
			{
				IMultiLayerLineSymbol pMultLayer = m_pSymbol as IMultiLayerLineSymbol;
				pMultLayer.DeleteLayer(pOldLayerSymbol as ILineSymbol);
				pMultLayer.AddLayer(pLayerSymbol as ILineSymbol);
				pMultLayer.MoveLayer(pLayerSymbol as ILineSymbol, m_iLayerIndex);
			}
			// 填充(面状)符号
			else if (m_pSymbol is IMultiLayerFillSymbol)
			{
				IMultiLayerFillSymbol pMultLayer = m_pSymbol as IMultiLayerFillSymbol;
				pMultLayer.DeleteLayer(pOldLayerSymbol as IFillSymbol);
				pMultLayer.AddLayer(pLayerSymbol as IFillSymbol);
				pMultLayer.MoveLayer(pLayerSymbol as IFillSymbol, m_iLayerIndex);
			}
			// 文本符号
			else if (m_pSymbol is ITextSymbol)
			{
				m_pSymbol = pLayerSymbol;
			}
			// 文本背景
			else if (m_pSymbol is ITextBackground)
			{
				m_pSymbol = pLayerSymbol;
			}
			// 线装饰
			else if (m_pSymbol is ILineDecoration)
			{
			}
		}
		
        /// <summary>
		/// 更改单位
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxSymbolUnit_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
        #endregion

        private void numSize_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numDepth_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numWidth_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSelPicture_Click(object sender, EventArgs e)
        {

        }

        private void txtDZ_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtDY_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtDX_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tbDZ_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tbDY_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tbDX_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btnSelPicture_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void colorPickerForeground_Click(object sender, EventArgs e)
        {

        }
    }
}
