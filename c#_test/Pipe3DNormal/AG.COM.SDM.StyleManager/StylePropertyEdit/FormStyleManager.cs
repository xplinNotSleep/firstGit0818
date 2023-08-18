using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AG.COM.SDM.Utility;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DisplayUI;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.StylePropertyEdit
{
    /// <summary>
    /// ���Ź�����
    /// </summary>
    public partial class FormStyleManager : SkinForm
	{
		#region �ֶ�
		/// <summary>
		/// ��ǰ���ſ�
		/// </summary>
		private IStyleGalleryStorage m_pStyleGalleryStorage = null;
		
        /// <summary>
		/// ��ǰ���ſ�
		/// </summary>
		private IStyleGallery m_pStyleGallery = null;
		
        /// <summary>
		/// ��ǰ�������
		/// </summary>
		private string m_CurrentCategoryName = "";
		
        /// <summary>
		/// ��ǰ�����ļ�
		/// </summary>
		private string m_CurrentStyleFile = "";
		
        /// <summary>
		/// ��ǰ���
		/// </summary>
		private string m_CurrentStyleGalleryClass = "";
		
        /// <summary>
		/// ��ǰ�������
		/// </summary>
		private int m_CurrentStyleGalleryClassIndex = -1;
		
        /// <summary>
		/// �Ƿ�Ϊ��Ͽ��¼�
		/// </summary>
		private bool m_bControlEvent = false;
		
        /// <summary>
		/// �Ƿ����¼����ļ�
		/// </summary>
		private bool m_bReAddFile = true;
       
        /// <summary>
        /// ��ʽ�ļ�
        /// </summary>
        private string[] m_styleFiles;
		#endregion

		#region ��ʼ��

        /// <summary>
        /// Ĭ�Ϲ�������
        /// </summary>
		public FormStyleManager()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ���� ��ʼ��������ʽ������ʵ��
        /// </summary>
        /// <param name="stylefiles">��ʽ�ļ�����</param>
        public FormStyleManager(string[] stylefiles)
        {
            InitializeComponent();

            this.m_styleFiles = stylefiles;
        }

		/// <summary>
		/// �������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void FormStyleManager_Load(object sender, EventArgs e)
        {
			this.m_pStyleGallery = new ServerStyleGalleryClass();

			treeViewStyle_AfterSelect(null, null);
			listViewSymbol_SelectedIndexChanged(null, null);

            //�����ʽ�ļ�
            if (this.m_styleFiles != null)
            {
                foreach (string strStyle in this.m_styleFiles)
                {
                    if (File.Exists(strStyle)) this.AddStyleFile(strStyle);
                }
            }
        }

		/// <summary>
		/// ����ر�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FormStyleManager_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				if(m_pStyleGalleryStorage!=null)
                {
					System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pStyleGalleryStorage);
				}
				if(m_pStyleGallery!=null)
                {
					System.Runtime.InteropServices.Marshal.ReleaseComObject(m_pStyleGallery);
				}
			}
			catch
			{ 
            }	
		}
		#endregion

		#region ��������ť��Ӧ
		/// <summary>
        /// ѡ����ſ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSytle_Click(object sender, EventArgs e)
        {
            OpenFileDialog oDlg = new OpenFileDialog();
            oDlg.Filter = "ESRI ServerStyle(*.ServerStyle)|*.ServerStyle";
            if (oDlg.ShowDialog() == DialogResult.OK)
            {
                AddStyleFile(oDlg.FileName);
            }

        }
	
        /// <summary>
		/// �����ʽ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void tsbAdd_Click(object sender, EventArgs e)
        {
			TreeNode tnode = this.treeViewStyle.SelectedNode;
			if (tnode == null) return;
			if (tnode.Parent == null) return;
			else
			{
				try
				{
					m_CurrentStyleGalleryClassIndex = int.Parse(tnode.Tag.ToString());
					IStyleGalleryItem pSGItem = null;
					// ��Ƿ���
					if (tnode.Text == (string)StyleCommon.StyleGallery["Marker Symbols"])
					{
						IMultiLayerMarkerSymbol pMultiLayer = new MultiLayerMarkerSymbolClass();
						pMultiLayer.AddLayer(new SimpleMarkerSymbolClass() as IMarkerSymbol);

						pSGItem = new ServerStyleGalleryItemClass();
						pSGItem.Name = "New Marker Symbols";
						pSGItem.Item = pMultiLayer;
						pSGItem.Category = "Default";
					}
					// ��״����
					else if (tnode.Text == StyleCommon.StyleGallery["Line Symbols"].ToString())
					{
						IMultiLayerLineSymbol pMultiLayer = new MultiLayerLineSymbolClass();
						pMultiLayer.AddLayer(new SimpleLineSymbolClass() as ILineSymbol);

						pSGItem = new ServerStyleGalleryItemClass();
						pSGItem.Name = "New Line Symbols";
						pSGItem.Item = pMultiLayer;
						pSGItem.Category = "Default";
					}
					// ������
					else if (tnode.Text == StyleCommon.StyleGallery["Fill Symbols"].ToString())
					{
						IMultiLayerFillSymbol pMultiLayer = new MultiLayerFillSymbolClass();
						pMultiLayer.AddLayer(new SimpleFillSymbolClass() as IFillSymbol);

						pSGItem = new ServerStyleGalleryItemClass();
						pSGItem.Name = "New Fill Symbols";
						pSGItem.Item = pMultiLayer;
						pSGItem.Category = "Default";
					}
					// �ı�����
					else if (tnode.Text == StyleCommon.StyleGallery["Text Symbols"].ToString())
					{
						ITextSymbol pTextSymbol = new TextSymbolClass();

						pSGItem = new ServerStyleGalleryItemClass();
						pSGItem.Name = "New Text Symbols";
						pSGItem.Item = pTextSymbol;
						pSGItem.Category = "Default";
					}
					// ��ɫ����
					else if (tnode.Text == StyleCommon.StyleGallery["Colors"].ToString())
					{
						IRgbColor pRgbColor = new RgbColorClass();
						pRgbColor.Red = 0;
						pRgbColor.Blue = 0;
						pRgbColor.Green = 255;

						pSGItem = new ServerStyleGalleryItemClass();
						pSGItem.Name = "New RGB";
						pSGItem.Item = pRgbColor;
						pSGItem.Category = "Default";
					}
					// �˴�����������ͷ��Ŵ���
					else
					{
					}

					EditStyleGalleryItem(pSGItem, true);
				}
				catch (System.Exception ex)
				{
					MessageBox.Show(string.Format("����·���ʱ�������󣡴�����Ϣ��{0}",ex.Message), "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}
	
        /// <summary>
		/// ɾ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void tsbDel_Click(object sender, EventArgs e)
        {
			if (this.listViewSymbol.SelectedItems.Count < 1) return;
			if (MessageBox.Show(string.Format("ȷʵҪɾ����ǰѡ��ķ����𣿣���{0}����",this.listViewSymbol.SelectedItems.Count),
				this.Text,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.No) return;
			// ѭ��ɾ��
			for (int i = listViewSymbol.SelectedItems.Count-1; i > -1; i--)
			{
				DelSelStyleItem(listViewSymbol.SelectedItems[i].Tag as IStyleGalleryItem);
				listViewSymbol.Items.Remove(listViewSymbol.SelectedItems[i]);
			}
        }
	
        /// <summary>
		/// �����޸�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void tsbProperty_Click(object sender, EventArgs e)
        {
			if (this.listViewSymbol.SelectedItems.Count < 1) return;
			IStyleGalleryItem pSGItem = listViewSymbol.SelectedItems[0].Tag as IStyleGalleryItem;
			if (null == pSGItem) return;
			// ����һ��
			IClone pClone = pSGItem as IClone;
			if (null == pClone) return;
			EditStyleGalleryItem1((IStyleGalleryItem)pClone.Clone(), false);

			#region
			////IStyleDialog pStyleDialog = new StyleManagerDialogClass();
			////pStyleDialog.DoModal(m_pStyleGallery, 0);
			////ISymbolEditor styleItemEditor = new SymbolEditorClass();
			//ISymbol symbol = pSGItem.Item as ISymbol;
			////styleItemEditor.EditSymbol(ref symbol, 0);
			//ISymbolSelector selector = new SymbolSelectorClass();
			//selector.AddSymbol(symbol);
			//selector.SelectSymbol(0);
			#endregion
		}
	
        /// <summary>
		/// �������ƺͷ���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsbSetNameAndGroup_Click(object sender, EventArgs e)
		{
			if (this.listViewSymbol.SelectedItems.Count < 1) return;
			IStyleGalleryItem pSGItem = listViewSymbol.SelectedItems[0].Tag as IStyleGalleryItem;
			if (null == pSGItem) return;
			IList<string> listCategory = new List<string>();
			for (int i = 0; i < comboBoxCategory.Items.Count;i++ )
			{
				string strCategory = comboBoxCategory.Items[i].ToString();
				if (strCategory != "<ȫ��>" && !string.IsNullOrEmpty(strCategory)) listCategory.Add(strCategory);
			}
			if (!listCategory.Contains("Default")) listCategory.Insert(0,"Default");
			FormSetNameAndCategory frm = new FormSetNameAndCategory(listCategory);
			frm.StyleName = pSGItem.Name;
			frm.StyleCategory = pSGItem.Category;
			if (frm.ShowDialog(this) != DialogResult.OK) return;
			pSGItem.Name = frm.StyleName;
			pSGItem.Category = frm.StyleCategory;
			if (!this.comboBoxCategory.Items.Contains(frm.StyleCategory)) this.comboBoxCategory.Items.Add(frm.StyleCategory);
			UpdateStyleItem(pSGItem);
			UpdateListViewItem(listViewSymbol.SelectedItems[0], pSGItem);
		}
	
        /// <summary>
		/// �༭����
		/// </summary>
		/// <param name="pSGItem">����</param>
		private void EditStyleGalleryItem(IStyleGalleryItem pSGItem,bool bNew)
		{
			if (null == pSGItem) return;
			// ���ݷ������ͽ��б༭
			// ��ǰ�༭���ǵ�״����״����״���ı�����
			if(pSGItem.Item is ISymbol)
			{
				FormSymbolPropertyEdit frm = null;
				frm = new FormSymbolPropertyEdit(pSGItem);
				if (frm.ShowDialog(this) == DialogResult.OK)
				{
					pSGItem.Item = frm.Symbol;
					if(bNew)
					{
						AddNewStyleItem(pSGItem);
						AddListViewItem(pSGItem, m_CurrentStyleGalleryClass);
					}
					else
					{
						UpdateStyleItem(pSGItem);
						UpdateListViewItem(listViewSymbol.SelectedItems[0], pSGItem);
					}
				}
			}
			// ��ǰ�༭������ɫ����
			else if(pSGItem.Item is IRgbColor)
			{
				IRgbColor pRgbColor = pSGItem.Item as IRgbColor;
				if (null != pRgbColor)
				{
					ColorDialog dlgColor = new ColorDialog();
					dlgColor.AllowFullOpen = false;
					dlgColor.ShowHelp = true;
					dlgColor.Color = Color.FromArgb(pRgbColor.Transparency, pRgbColor.Red, pRgbColor.Green, pRgbColor.Blue);

					if (dlgColor.ShowDialog() != DialogResult.OK) return;

					pRgbColor.Red = dlgColor.Color.R;
					pRgbColor.Blue = dlgColor.Color.B;
					pRgbColor.Green = dlgColor.Color.G;

					pSGItem.Item = pRgbColor;
					if (bNew)
					{
						AddNewStyleItem(pSGItem);
						AddListViewItem(pSGItem, m_CurrentStyleGalleryClass);
					}
					else
					{
						UpdateStyleItem(pSGItem);
						UpdateListViewItem(listViewSymbol.SelectedItems[0], pSGItem);
					}
					return;
				}
			}
			// �˴�����������͵ķ��ű༭
			else 
			{

			}
		}

		/// <summary>
		/// ˫��������༭����
		/// </summary>
		/// <param name="pSGItem"></param>
		/// <param name="bNew"></param>
		private void EditStyleGalleryItem1(IStyleGalleryItem pSGItem, bool bNew=false)
        {
			if (null == pSGItem) return;
			// ���ݷ������ͽ��б༭
			// ��ǰ�༭���ǵ�״����״����״���ı�����
			if (pSGItem.Item is ISymbol)
			{
                ISymbolEditor styleItemEditor = new SymbolEditorClass();
                ISymbol symbol = pSGItem.Item as ISymbol;
               // styleItemEditor.EditSymbol(ref symbol, 0);
               // FormSymbolPropertyEdit frm = null;
				//frm = new FormSymbolPropertyEdit(pSGItem);
				if (styleItemEditor.EditSymbol(ref symbol, 0))
				{
					pSGItem.Item = symbol;
					if (bNew)
					{
						AddNewStyleItem(pSGItem);
						AddListViewItem(pSGItem, m_CurrentStyleGalleryClass);
					}
					else
					{
						UpdateStyleItem(pSGItem);
						UpdateListViewItem(listViewSymbol.SelectedItems[0], pSGItem);
					}
				}
			}
			// ��ǰ�༭������ɫ����
			else if (pSGItem.Item is IRgbColor)
			{
				IRgbColor pRgbColor = pSGItem.Item as IRgbColor;
				if (null != pRgbColor)
				{
					ColorDialog dlgColor = new ColorDialog();
					dlgColor.AllowFullOpen = false;
					dlgColor.ShowHelp = true;
					dlgColor.Color = Color.FromArgb(pRgbColor.Transparency, pRgbColor.Red, pRgbColor.Green, pRgbColor.Blue);

					if (dlgColor.ShowDialog() != DialogResult.OK) return;

					pRgbColor.Red = dlgColor.Color.R;
					pRgbColor.Blue = dlgColor.Color.B;
					pRgbColor.Green = dlgColor.Color.G;

					pSGItem.Item = pRgbColor;
					if (bNew)
					{
						AddNewStyleItem(pSGItem);
						AddListViewItem(pSGItem, m_CurrentStyleGalleryClass);
					}
					else
					{
						UpdateStyleItem(pSGItem);
						UpdateListViewItem(listViewSymbol.SelectedItems[0], pSGItem);
					}
					return;
				}
			}
			// �˴�����������͵ķ��ű༭
			else
			{

			}
		}

		/// <summary>
		/// �ı��б���ʾ��ʽ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsbView_Click(object sender, EventArgs e)
        {
            this.tsbLarge.Checked = false;
            this.tsbSmall.Checked = false;
            this.tsbDetail.Checked = false;
            if (sender == tsbLarge)
            {
                this.listViewSymbol.View = System.Windows.Forms.View.LargeIcon;
                this.tsbLarge.Checked = true;
            }
            else if (sender == tsbSmall)
            {
                this.listViewSymbol.View = System.Windows.Forms.View.SmallIcon;
                this.tsbSmall.Checked = true;
            }
            else if (sender == tsbDetail)
            {
                this.listViewSymbol.View = System.Windows.Forms.View.Details;
                this.tsbDetail.Checked = true;
            }
            else
            {
                this.listViewSymbol.View = System.Windows.Forms.View.LargeIcon;
                this.tsbLarge.Checked = true;
            }

        }
        #endregion

        #region ����
		/// <summary>
		/// ��ӷ��ſ��ļ�
		/// </summary>
		/// <param name="filename"></param>
        private void AddStyleFile(string filename)
        {
            if (!System.IO.File.Exists(filename))
            {
                return;
            }
            bool IsExist = false;
            foreach (TreeNode tnode in this.treeViewStyle.Nodes)
            {
                if (tnode.Tag.ToString() == filename)
                {
                    IsExist = true;
                }
            }
            if (!IsExist)
            {
                System.IO.FileInfo finfo = new System.IO.FileInfo(filename);
                TreeNode RootNode = new TreeNode(finfo.Name);
                RootNode.Tag = finfo.FullName;
                this.treeViewStyle.Nodes.Add(RootNode);

				for (int i = 0; i < this.m_pStyleGallery.ClassCount; i++)
                {
					string strStyleGallery = this.m_pStyleGallery.get_Class(i).Name;
					if (!StyleCommon.StyleGallery.ContainsKey(strStyleGallery)) continue;
					TreeNode tnode = new TreeNode(StyleCommon.StyleGallery[strStyleGallery].ToString());
                    tnode.Tag = i;
                    RootNode.Nodes.Add(tnode);
                }
            }
        }
	
        /// <summary>
		/// ��ȡ����
		/// </summary>
		/// <param name="StyleFile"></param>
		/// <param name="StyleGalleryClass"></param>
		private void GetSymbols(string StyleFile, string StyleGalleryClass)
		{
			IEnumStyleGalleryItem mEnumStyleItem;
			IStyleGalleryItem mStyleItem;
			// ��ȡ���ŷ���
            try
            {
                if (m_bReAddFile)
                {
                    m_CurrentStyleFile = StyleFile;
                    m_CurrentStyleGalleryClass = StyleGalleryClass;
                    m_pStyleGalleryStorage = m_pStyleGallery as IStyleGalleryStorage;
                    m_pStyleGalleryStorage.AddFile(m_CurrentStyleFile);
                    mEnumStyleItem = m_pStyleGallery.get_Items(m_CurrentStyleGalleryClass, m_CurrentStyleFile, "");
                    this.comboBoxCategory.Items.Clear();
                    this.comboBoxCategory.Items.Add("<ȫ��>");
                    ESRI.ArcGIS.esriSystem.IEnumBSTR pEnumBSTR = m_pStyleGallery.get_Categories(m_CurrentStyleGalleryClass);
                    pEnumBSTR.Reset();
                    string Category = "";
                    Category = pEnumBSTR.Next();
                    while (Category != null)
                    {
                        if (!string.IsNullOrEmpty(Category)) this.comboBoxCategory.Items.Add(Category);
                        Category = pEnumBSTR.Next();
                    }
                    m_bControlEvent = true;
                    this.comboBoxCategory.SelectedIndex = 0;
                    m_bControlEvent = false;
                    m_bReAddFile = false;
                }
                else
                {
                    if (m_CurrentStyleGalleryClass != StyleGalleryClass)
                    {
                        this.comboBoxCategory.Items.Clear();
                        this.comboBoxCategory.Items.Add("<ȫ��>");
                        ESRI.ArcGIS.esriSystem.IEnumBSTR pEnumBSTR = m_pStyleGallery.get_Categories(StyleGalleryClass);
                        pEnumBSTR.Reset();
                        string Category = "";
                        Category = pEnumBSTR.Next();
                        while (Category != null)
                        {
                            if (!string.IsNullOrEmpty(Category)) this.comboBoxCategory.Items.Add(Category);
                            Category = pEnumBSTR.Next();
                        }
                        m_bControlEvent = true;
                        this.comboBoxCategory.SelectedIndex = 0;
                        m_bControlEvent = false;
                    }
                    m_CurrentStyleGalleryClass = StyleGalleryClass;
                    mEnumStyleItem = m_pStyleGallery.get_Items(m_CurrentStyleGalleryClass, m_CurrentStyleFile, m_CurrentCategoryName);
                }
                IStyleGalleryClass mStyleClass = m_pStyleGallery.get_Class(m_CurrentStyleGalleryClassIndex);
                // ���÷�������ͼ��С
                bool bLabelTextStyle = StyleGalleryClass.Contains("Text") | StyleGalleryClass.Contains("Label") | StyleGalleryClass.Contains("Scale") | StyleGalleryClass.Contains("Ramps");
                System.Windows.Forms.ImageList Largeimage = new ImageList();
                System.Windows.Forms.ImageList Smallimage = new ImageList();
                Largeimage.ImageSize = new Size(bLabelTextStyle ? 128 : 32, 32);
                Smallimage.ImageSize = new Size(bLabelTextStyle ? 64 : 16, 16);
                System.Drawing.Bitmap bmpB, bmpS;
                // ��ӷ���
                ListViewItem lvItem;
                this.listViewSymbol.Items.Clear();
                this.listViewSymbol.Columns.Clear();
                this.listViewSymbol.LargeImageList = Largeimage;
                this.listViewSymbol.SmallImageList = Smallimage;
                this.listViewSymbol.Columns.Add("����", 150, System.Windows.Forms.HorizontalAlignment.Left);
                this.listViewSymbol.Columns.Add("���", 50, System.Windows.Forms.HorizontalAlignment.Left);
                this.listViewSymbol.Columns.Add("���", 200, System.Windows.Forms.HorizontalAlignment.Left);

                // ѭ����ȡ������ӵ��б�
                mEnumStyleItem.Reset();
                mStyleItem = mEnumStyleItem.Next();
                int ImageIndex = 0;
                while (mStyleItem != null)
                {
                    bmpB = StyleCommon.StyleGalleryItemToBmp(bLabelTextStyle ? 128 : 32, 32, mStyleClass, mStyleItem);
                    bmpS = StyleCommon.StyleGalleryItemToBmp(bLabelTextStyle ? 64 : 16, 16, mStyleClass, mStyleItem);
                    Largeimage.Images.Add(bmpB);
                    Smallimage.Images.Add(bmpS);
                    lvItem = new ListViewItem(new string[] { mStyleItem.Name, mStyleItem.ID.ToString(), mStyleItem.Category }, ImageIndex);
                    lvItem.Tag = mStyleItem;
                    this.listViewSymbol.Items.Add(lvItem);
                    mStyleItem = mEnumStyleItem.Next();
                    ImageIndex++;
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(mEnumStyleItem);
            }
            catch
            {
 
            }
		}
	
        /// <summary>
		/// �����б���ʾ
		/// </summary>
		/// <param name="lvItem">�б���</param>
		/// <param name="pItemUpdate">���µķ���</param>
		private void UpdateListViewItem(ListViewItem lvItem, IStyleGalleryItem pItemUpdate)
		{
			lvItem.Tag = pItemUpdate;
			lvItem.Text = pItemUpdate.Name;
			lvItem.SubItems[2].Text = pItemUpdate.Category;

			string strStyleGalleryClass = m_CurrentStyleGalleryClass;
			bool bLabelTextStyle = strStyleGalleryClass.Contains("Text") | strStyleGalleryClass.Contains("Label") | strStyleGalleryClass.Contains("Scale") | strStyleGalleryClass.Contains("Ramps");
			System.Drawing.Bitmap bmpB, bmpS;
			IStyleGalleryClass mStyleClass = m_pStyleGallery.get_Class(m_CurrentStyleGalleryClassIndex);
			bmpB = StyleCommon.StyleGalleryItemToBmp(bLabelTextStyle ? 128 : 32, 32, mStyleClass, pItemUpdate);
			bmpS = StyleCommon.StyleGalleryItemToBmp(bLabelTextStyle ? 64 : 16, 16, mStyleClass, pItemUpdate);

			this.listViewSymbol.LargeImageList.Images[lvItem.ImageIndex] = bmpB;
			this.listViewSymbol.SmallImageList.Images[lvItem.ImageIndex] = bmpS;
			this.listViewSymbol.SelectedItems.Clear();
		}
	
        /// <summary>
		/// ��ӷ����б���
		/// </summary>
		/// <param name="pItemNew"></param>
		private void AddListViewItem(IStyleGalleryItem pItemNew, string StyleGalleryClass)
		{
			IStyleGalleryClass mStyleClass = m_pStyleGallery.get_Class(m_CurrentStyleGalleryClassIndex);
			// ���÷�������ͼ��С
			bool bLabelTextStyle = StyleGalleryClass.Contains("Text") | StyleGalleryClass.Contains("Label") | StyleGalleryClass.Contains("Scale") | StyleGalleryClass.Contains("Ramps");
			System.Drawing.Bitmap bmpB, bmpS;

			bmpB = StyleCommon.StyleGalleryItemToBmp(bLabelTextStyle ? 128 : 32, 32, mStyleClass, pItemNew);
			bmpS = StyleCommon.StyleGalleryItemToBmp(bLabelTextStyle ? 64 : 16, 16, mStyleClass, pItemNew);
			listViewSymbol.LargeImageList.Images.Add(bmpB);
			listViewSymbol.SmallImageList.Images.Add(bmpS);

			ListViewItem lvItem = new ListViewItem(new string[] { pItemNew.Name, pItemNew.ID.ToString(), pItemNew.Category }, listViewSymbol.LargeImageList.Images.Count-1);
			lvItem.Tag = pItemNew;
			this.listViewSymbol.Items.Add(lvItem);

		}
	
        /// <summary>
		/// ����·���
		/// </summary>
		/// <param name="pNewItem">����</param>
		private void AddNewStyleItem(IStyleGalleryItem pNewItem)
		{
			try
			{
				if (null == pNewItem) return;
				IStyleGallery _iServerStyleGallery = new ServerStyleGalleryClass();
				IStyleGalleryStorage iServerStyleGalleryStorage = (IStyleGalleryStorage)_iServerStyleGallery;
				_iServerStyleGallery.Clear();

				// ����Ŀ���ļ�
				iServerStyleGalleryStorage.TargetFile = m_CurrentStyleFile;
				// ��ӷ���
				_iServerStyleGallery.AddItem(pNewItem);

				// �ر�Ŀ���ļ�
				iServerStyleGalleryStorage.RemoveFile(m_CurrentStyleFile);
				System.Runtime.InteropServices.Marshal.ReleaseComObject(_iServerStyleGallery);
				m_bReAddFile = true;
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(string.Format("�򿪷����ļ�����·���ʱ�������󣡴�����Ϣ��{0}", ex.Message), "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	
        /// <summary>
		/// ɾ������
		/// </summary>
		/// <param name="pItemSel">����</param>
		private void DelSelStyleItem(IStyleGalleryItem pItemSel)
		{
			try
			{
				if (null == pItemSel) return;
				IStyleGallery _iServerStyleGallery = new ServerStyleGalleryClass();
				IStyleGalleryStorage iServerStyleGalleryStorage = (IStyleGalleryStorage)_iServerStyleGallery;
				_iServerStyleGallery.Clear();

				// ����Ŀ���ļ�
				iServerStyleGalleryStorage.TargetFile = m_CurrentStyleFile;
				// ɾ������
				_iServerStyleGallery.RemoveItem(pItemSel);

				// �ر�Ŀ���ļ�
				iServerStyleGalleryStorage.RemoveFile(m_CurrentStyleFile);
				System.Runtime.InteropServices.Marshal.ReleaseComObject(_iServerStyleGallery);
				m_bReAddFile = true;
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(string.Format("�򿪷����ļ�ɾ������ʱ�������󣡴�����Ϣ��{0}", ex.Message), "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	
        /// <summary>
		/// ���·���
		/// </summary>
		/// <param name="pItemUpdate">����</param>
		private void UpdateStyleItem(IStyleGalleryItem pItemUpdate)
		{
			try
			{
				if (null == pItemUpdate) return;
				IStyleGallery _iServerStyleGallery = new ServerStyleGalleryClass();
				IStyleGalleryStorage iServerStyleGalleryStorage = (IStyleGalleryStorage)_iServerStyleGallery;
				_iServerStyleGallery.Clear();

				// ����Ŀ���ļ�
				iServerStyleGalleryStorage.TargetFile = m_CurrentStyleFile;
				// ��ӷ���
				_iServerStyleGallery.UpdateItem(pItemUpdate);

				// �ر�Ŀ���ļ�
				iServerStyleGalleryStorage.RemoveFile(m_CurrentStyleFile);
				System.Runtime.InteropServices.Marshal.ReleaseComObject(_iServerStyleGallery);
				m_bReAddFile = true;
			}
			catch (System.Exception ex)
			{
				AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
				MessageBox.Show(string.Format("�򿪷����ļ����·���ʱ�������󣡴�����Ϣ��{0}", ex.Message), "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
        #endregion

		#region �û�ѡ�����
		/// <summary>
		/// ���ķ���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!m_bControlEvent)
			{
				if (this.comboBoxCategory.SelectedItem.ToString() == "<ȫ��>")
				{
					m_CurrentCategoryName = "";
					this.GetSymbols(m_CurrentStyleFile, m_CurrentStyleGalleryClass);
				}
				else
				{
					m_CurrentCategoryName = this.comboBoxCategory.SelectedItem.ToString();
					this.GetSymbols(m_CurrentStyleFile, m_CurrentStyleGalleryClass);
				}
			}
		}
	
        /// <summary>
		/// ѡ��������ڵ�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeViewStyle_AfterSelect(object sender, TreeViewEventArgs e)
		{
			this.tsbAdd.Enabled = false;
			TreeNode tnode = this.treeViewStyle.SelectedNode;
			if (tnode == null) return;
			if(tnode.Parent == null)
			{
				m_CurrentStyleGalleryClassIndex = -1;
				m_CurrentCategoryName = "";
				m_CurrentStyleGalleryClass = "";
				this.listViewSymbol.Items.Clear();
				this.comboBoxCategory.Items.Clear();
			}
			else 
			{
				m_bReAddFile = true;
				this.tsbAdd.Enabled = true;
				m_CurrentStyleGalleryClassIndex = int.Parse(tnode.Tag.ToString());
				string strStyleFile = tnode.Parent.Tag.ToString();
				if (strStyleFile != this.m_CurrentStyleFile) m_bReAddFile = true;
				GetSymbols(strStyleFile, StyleCommon.GetStyleGalleryName(tnode.Text));
			}
		}
	
        /// <summary>
		/// ���ķ����б�ѡ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewSymbol_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.tsbDel.Enabled = false;
			this.tsbProperty.Enabled = false;
			this.tsbSetNameAndCategory.Enabled = false;

			if (this.listViewSymbol.SelectedItems.Count == 0) return;
			if(this.listViewSymbol.SelectedItems.Count == 1) 
			{
				this.tsbProperty.Enabled = true;
				this.tsbSetNameAndCategory.Enabled = true;
			}
			this.tsbDel.Enabled = true;
		}
	
        /// <summary>
		/// ˫���б�༭ѡ��ķ���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewSymbol_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			tsbProperty_Click(sender, e);
		}
		#endregion
	}
}