using System;
using System.Windows.Forms;
using AG.COM.SDM.StylePropertyEdit.PropertyCtrl;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DisplayUI;

namespace AG.COM.SDM.StylePropertyEdit
{
	/// <summary>
	/// �������Կؼ�
	/// </summary>
	public partial class CtrlSymbolProperty : UserControl, ISymbolPropertyEdit
	{
		#region �ֶ�/����
		/// <summary>
		/// ��ǰ�༭�ķ�������
		/// </summary>
		EnumSymbolType m_SymbolType = EnumSymbolType.SymbolTypeUnknow;
	
        /// <summary>
		/// ��ǰ�༭�ķ���
		/// </summary>
		private object m_pSymbol = null;
	
        /// <summary>
		/// ����ͼ��ӿ�
		/// </summary>
		private ISymbolLayer m_pSymbolLayer = null;
		
        /// <summary>
		/// ����ͼ������
		/// </summary>
		private int m_iLayerIndex = -1;
	
        /// <summary>
		/// ������������ҳ
		/// </summary>
		private bool m_bResetPropertyPage = false;

		/// <summary>
        /// ��ȡ�����õ�ǰ�༭�ķ�������
		/// </summary>
		public EnumSymbolType SymbolType
		{
			set { m_SymbolType = value; }
			get { return m_SymbolType; }
		}
		
        /// <summary>
        /// ��ȡ�����õ�ǰ�༭�ķ���
		/// </summary>
		public object Symbol
		{
			set { m_pSymbol = value; }
			get { return m_pSymbol; }
		}
		
        /// <summary>
        /// ���÷���ͼ��ӿ�
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

		#region ��ʼ��
		public CtrlSymbolProperty()
		{
			InitializeComponent();

		}

		private void CtrlSymbolProperty_Load(object sender, EventArgs e)
		{
			this.comboBoxSymbolUnit.SelectedIndex = 0;
		}
		#endregion

		#region ISymbolPropertyEdit ��Ա
		/// <summary>
		/// ����������ͼ
		/// </summary>
		/// <param name="pSymbol"></param>
		public void UpdatePropertyView(object pSymbol)
		{
		}
		
        /// <summary>
		/// ����������ͼ
		/// </summary>
		/// <param name="pSymbol"></param>
		public void UpdatePropertyView(object pSymbol, int iLayerIndex)
		{
			m_pSymbol = pSymbol;
			m_iLayerIndex = iLayerIndex;
			this.tabControlProperty.Controls.Clear();
			object pLayerSymbol = StyleCommon.GetLayerSymbol(m_pSymbol, iLayerIndex);
			// ����ͼ������������ҳ
			AddPropertyPage(pLayerSymbol);
		}
		#endregion

		#region ������ʾ
		/// <summary>
		/// ��ȡ��ǰѡ��ķ�������
		/// </summary>
		/// <returns>������������</returns>
		public string GetSelectSymbolType()
		{
			return this.comboBoxSymbolType.SelectedItem.ToString();
		}

		/// <summary>
		/// ����ͼ������������ҳ
		/// </summary>
		/// <param name="pLayerSymbol">ͼ�����</param>
		private void AddPropertyPage(object pLayerSymbol)
		{
			m_bResetPropertyPage = false;
			// ���(��״)����
			if (m_pSymbol is IMultiLayerMarkerSymbol)
			{
				CtrlPropertyBase ctrlmarker = null;
				if (pLayerSymbol is IMarker3DSymbol)
                {
                    ctrlmarker = new Ctrl3DMarker();
                    SetCBSymbolType(EnumMarkerSymbolType.��3D��־����.ToString());
                    //               ISymbolEditor styleItemEditor = new SymbolEditorClass();
                    //               ISymbol symbol = pLayerSymbol as ISymbol;
                    //               styleItemEditor.EditSymbol(ref symbol, 0);
                    //pLayerSymbol = symbol;

                }
                else if (pLayerSymbol is ISimpleMarkerSymbol)
				{
					ctrlmarker = new CtrlSimpleMarker();
					SetCBSymbolType(EnumMarkerSymbolType.�򵥵�״����.ToString());
				}
				else if (pLayerSymbol is ICharacterMarkerSymbol)
				{
					ctrlmarker = new CtrlCharacterMarker();
					SetCBSymbolType(EnumMarkerSymbolType.�����״����.ToString());
				}
				else if (pLayerSymbol is IArrowMarkerSymbol)
				{
					ctrlmarker = new CtrlArrowMarker();
					SetCBSymbolType(EnumMarkerSymbolType.��ͷ��״����.ToString());
				}
				else if (pLayerSymbol is IPictureMarkerSymbol)
				{
					ctrlmarker = new CtrlPicture(EnumSymbolType.SymbolTypeMarker);
					SetCBSymbolType(EnumMarkerSymbolType.ͼƬ��״����.ToString());
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
			// ��״����
			else if (m_pSymbol is IMultiLayerLineSymbol)
			{
				ILineSymbol pLineSymbol = pLayerSymbol as ILineSymbol;
				if (pLineSymbol is ISimpleLineSymbol)
				{
					AddSymbolPropertyPage(new CtrlSimpleLine(), pLayerSymbol);
					SetCBSymbolType(EnumLineSymbolType.����״����.ToString());
				}
				else if (pLineSymbol is IPictureLineSymbol)
				{
					AddSymbolPropertyPage(new CtrlPicture(EnumSymbolType.SymbolTypeLine), pLayerSymbol);
					SetCBSymbolType(EnumLineSymbolType.ͼƬ��״����.ToString());
				}
				else
				{
					if (pLineSymbol is IHashLineSymbol)
					{
						AddSymbolPropertyPage(new CtrlHashLine(), pLayerSymbol);
						SetCBSymbolType(EnumLineSymbolType.��ϣ��״����.ToString());
					}
					else if (pLineSymbol is IMarkerLineSymbol)
					{
						AddSymbolPropertyPage(new CtrlMarkerLine(), pLayerSymbol);
						AddSymbolPropertyPage(new CtrlCartographicLine(), pLayerSymbol);
						SetCBSymbolType(EnumLineSymbolType.�����״����.ToString());
					}
					else if (pLineSymbol is CartographicLineSymbol)
					{
						AddSymbolPropertyPage(new CtrlCartographicLine(), pLayerSymbol);
						SetCBSymbolType(EnumLineSymbolType.��ͼ��״����.ToString());
					}
					AddSymbolPropertyPage(new CtrlTemplate(), pLayerSymbol);
					AddSymbolPropertyPage(new CtrlLineProperties(), pLayerSymbol);
				}
			}
			// ���(��״)����
			else if (m_pSymbol is IMultiLayerFillSymbol)
			{
				if (pLayerSymbol is ISimpleFillSymbol)
				{
					AddSymbolPropertyPage(new CtrlSimpleFill(), pLayerSymbol);
					SetCBSymbolType(EnumFillSymbolType.��������.ToString());
				}
				else if (pLayerSymbol is IPictureFillSymbol)
				{
					AddSymbolPropertyPage(new CtrlPicture(EnumSymbolType.SymbolTypeFill), pLayerSymbol);
					AddSymbolPropertyPage(new CtrlFillProperties(), pLayerSymbol);
					SetCBSymbolType(EnumFillSymbolType.ͼƬ������.ToString());
				}
				else if (pLayerSymbol is IMarkerFillSymbol)
				{
					AddSymbolPropertyPage(new CtrlMarkerFill(), pLayerSymbol);
					AddSymbolPropertyPage(new CtrlFillProperties(), pLayerSymbol);
					SetCBSymbolType(EnumFillSymbolType.���������.ToString());
				}
				else if (pLayerSymbol is ILineFillSymbol)
				{
					AddSymbolPropertyPage(new CtrlLineFill(), pLayerSymbol);
					SetCBSymbolType(EnumFillSymbolType.��������.ToString());
				}
				else if (pLayerSymbol is IGradientFillSymbol)
				{
					AddSymbolPropertyPage(new CtrlGradientFill(), pLayerSymbol);
					SetCBSymbolType(EnumFillSymbolType.����������.ToString());
				}
			}
			// �ı�����
			else if (m_pSymbol is ITextSymbol)
			{
				AddSymbolPropertyPage(new CtrlGeneralText(), m_pSymbol);
				AddSymbolPropertyPage(new CtrlFormattedText(), m_pSymbol);
				AddSymbolPropertyPage(new CtrlAdvancedText(), m_pSymbol);
				AddSymbolPropertyPage(new CtrlMask(), m_pSymbol);
				SetCBSymbolType(EnumTextSymbolType.�ı�����.ToString());
			}
			// �ı�����
			else if (m_pSymbol is ITextBackground)
			{
				if (pLayerSymbol is BalloonCallout)
				{
					AddSymbolPropertyPage(new CtrlBalloonCallout(), m_pSymbol);
					SetCBSymbolType(EnumTextBackgroundType.������ʾ����.ToString());
				}
				else if (pLayerSymbol is LineCallout)
				{
					AddSymbolPropertyPage(new CtrlLineCallout(), m_pSymbol);
					SetCBSymbolType(EnumTextBackgroundType.������ʾ����.ToString());
				}
				else if (pLayerSymbol is MarkerTextBackground)
				{
                    AddSymbolPropertyPage(new CtrlMarkerTextBackground(), m_pSymbol);
					SetCBSymbolType(EnumTextBackgroundType.����ı�����.ToString());
				}
				else if (pLayerSymbol is SimpleLineCallout)
				{
					AddSymbolPropertyPage(new CtrlSimpleLineCallout(), m_pSymbol);
					SetCBSymbolType(EnumTextBackgroundType.��������ʾ����.ToString());
				}
			}
			// ��װ��
			else if (m_pSymbol is ILineDecoration)
			{
				AddSymbolPropertyPage(new CtrlSimpleLineDecoration(), pLayerSymbol);
				SetCBSymbolType(EnumLineDecorationType.������װ��.ToString());
			}
			m_bResetPropertyPage = true;
		}
	
        /// <summary>
		/// ��ӷ�������ҳ
		/// </summary>
		/// <param name="ctrlSP">����ҳ</param>
		/// <param name="pSymbol">����</param>
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
		/// ���÷�������������
		/// </summary>
		/// <param name="strType">��������</param>
		private void SetCBSymbolType(string strType)
		{
			this.comboBoxSymbolType.SelectedIndex = comboBoxSymbolType.FindString(strType, 0);
		}
		#endregion

		#region �ؼ��¼�
		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxSymbolType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!m_bResetPropertyPage) return;
			object pLayerSymbol = null;
			// ���(��״)����
			if (m_pSymbol is IMultiLayerMarkerSymbol)
			{
				if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumMarkerSymbolType.��3D��־����.ToString())
                {
					pLayerSymbol = new Marker3DSymbolClass();
				}
		        if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumMarkerSymbolType.�򵥵�״����.ToString()) 
				{
					pLayerSymbol = new SimpleMarkerSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumMarkerSymbolType.�����״����.ToString()) 
				{
					pLayerSymbol = new CharacterMarkerSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumMarkerSymbolType.��ͷ��״����.ToString())
				{
					pLayerSymbol = new ArrowMarkerSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumMarkerSymbolType.ͼƬ��״����.ToString())
				{
					pLayerSymbol = new PictureMarkerSymbolClass();
				}
			}
			// ��״����
			else if (m_pSymbol is IMultiLayerLineSymbol)
			{
				if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumLineSymbolType.����״����.ToString())
				{
					pLayerSymbol = new SimpleLineSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumLineSymbolType.ͼƬ��״����.ToString())
				{
					pLayerSymbol = new PictureLineSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumLineSymbolType.��ϣ��״����.ToString())
				{
					pLayerSymbol = new HashLineSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumLineSymbolType.�����״����.ToString())
				{
					pLayerSymbol = new MarkerLineSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumLineSymbolType.��ͼ��״����.ToString())
				{
					pLayerSymbol = new CartographicLineSymbolClass();
				}
			}
			// ���(��״)����
			else if (m_pSymbol is IMultiLayerFillSymbol)
			{
				if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumFillSymbolType.��������.ToString())
				{
					pLayerSymbol = new SimpleFillSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumFillSymbolType.ͼƬ������.ToString())
				{
					pLayerSymbol = new PictureFillSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumFillSymbolType.���������.ToString())
				{
					pLayerSymbol = new MarkerFillSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumFillSymbolType.��������.ToString())
				{
					pLayerSymbol = new LineFillSymbolClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumFillSymbolType.����������.ToString())
				{
					pLayerSymbol = new GradientFillSymbolClass();
				}
			}
			// �ı�����
			else if (m_pSymbol is ITextSymbol)
			{
				pLayerSymbol = new TextSymbolClass();
			}
			// �ı�����
			else if (m_pSymbol is ITextBackground)
			{
				if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumTextBackgroundType.������ʾ����.ToString())
				{
					pLayerSymbol = new BalloonCalloutClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumTextBackgroundType.������ʾ����.ToString())
				{
					pLayerSymbol = new LineCalloutClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumTextBackgroundType.����ı�����.ToString())
				{
					pLayerSymbol = new MarkerTextBackgroundClass();
				}
				else if (this.comboBoxSymbolType.SelectedItem.ToString() == EnumTextBackgroundType.��������ʾ����.ToString())
				{
					pLayerSymbol = new SimpleLineCalloutClass();
				}
			}
			// ��װ��
			else if (m_pSymbol is ILineDecoration)
			{
			}
			if (null == pLayerSymbol) return;
			ResetLayerSymbol(pLayerSymbol);

			tabControlProperty.Controls.Clear();
			// ����ͼ������������ҳ
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
		/// ��������ͼ�����
		/// </summary>
		/// <param name="pLayerSymbol"></param>
		private void ResetLayerSymbol(object pLayerSymbol)
		{
			if (null == pLayerSymbol) return;
			object pOldLayerSymbol = StyleCommon.GetLayerSymbol(m_pSymbol, m_iLayerIndex);
			// ���(��״)����
			if (m_pSymbol is IMultiLayerMarkerSymbol)
			{
				IMultiLayerMarkerSymbol pMultLayer = m_pSymbol as IMultiLayerMarkerSymbol;
				pMultLayer.DeleteLayer(pOldLayerSymbol as IMarkerSymbol);
				pMultLayer.AddLayer(pLayerSymbol as IMarkerSymbol);
				pMultLayer.MoveLayer(pLayerSymbol as IMarkerSymbol, m_iLayerIndex);
			}
			// ��״����
			else if (m_pSymbol is IMultiLayerLineSymbol)
			{
				IMultiLayerLineSymbol pMultLayer = m_pSymbol as IMultiLayerLineSymbol;
				pMultLayer.DeleteLayer(pOldLayerSymbol as ILineSymbol);
				pMultLayer.AddLayer(pLayerSymbol as ILineSymbol);
				pMultLayer.MoveLayer(pLayerSymbol as ILineSymbol, m_iLayerIndex);
			}
			// ���(��״)����
			else if (m_pSymbol is IMultiLayerFillSymbol)
			{
				IMultiLayerFillSymbol pMultLayer = m_pSymbol as IMultiLayerFillSymbol;
				pMultLayer.DeleteLayer(pOldLayerSymbol as IFillSymbol);
				pMultLayer.AddLayer(pLayerSymbol as IFillSymbol);
				pMultLayer.MoveLayer(pLayerSymbol as IFillSymbol, m_iLayerIndex);
			}
			// �ı�����
			else if (m_pSymbol is ITextSymbol)
			{
				m_pSymbol = pLayerSymbol;
			}
			// �ı�����
			else if (m_pSymbol is ITextBackground)
			{
				m_pSymbol = pLayerSymbol;
			}
			// ��װ��
			else if (m_pSymbol is ILineDecoration)
			{
			}
		}
		
        /// <summary>
		/// ���ĵ�λ
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
