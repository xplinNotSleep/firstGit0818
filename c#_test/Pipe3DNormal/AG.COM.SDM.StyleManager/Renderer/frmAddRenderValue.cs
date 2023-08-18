using System;
using System.Collections;
using System.Windows.Forms;

namespace AG.COM.SDM.StyleManager.Renderer
{
    public partial class frmAddRenderValue : Form
    {
        private Hashtable m_Hashtable;                  //没有用于Render的其它字段值
        private Hashtable m_SelectHashtable =new Hashtable();            //根据选择要添加的Render
        private Hashtable m_RenderHashtable;            //现已使用的Render

        public Hashtable RenderHashtable
        {
            set { m_RenderHashtable = value; }
        }

        public Hashtable SelectHashtable
        {
            get { return m_SelectHashtable; }
        }

        public Hashtable Hashtable
        {
            set { m_Hashtable = value; }
        }

        public frmAddRenderValue()
        {
            InitializeComponent();
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            if (lbxRenderValue.Items.Contains(tbxNewValue.Text))
                return;
            if (this.m_RenderHashtable.ContainsKey(tbxNewValue.Text))
                return;
            if (tbxNewValue.Text.Trim() == "" && HasZeroValue()==true)
                return;
            else
                lbxRenderValue.Items.Add(tbxNewValue.Text);
        }

        private void frmAddRenderValue_Load(object sender, EventArgs e)
        {
            IDictionaryEnumerator pDicEnumerator;
            pDicEnumerator = m_Hashtable.GetEnumerator();
            while (pDicEnumerator.MoveNext())
            {
                this.lbxRenderValue.Items.Add(pDicEnumerator.Key.ToString());
            }
        }

        //得到选中项的哈希表
        private void lbxRenderValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_SelectHashtable = new Hashtable();
            IEnumerator pEnumerator;
            pEnumerator=this.lbxRenderValue.SelectedItems.GetEnumerator();
            pEnumerator.Reset();
            while (pEnumerator.MoveNext())
            {
                if (m_Hashtable.ContainsKey(pEnumerator.Current.ToString()))
                {
                    IDictionaryEnumerator pDicEnumerator = m_Hashtable.GetEnumerator();
                    while (pDicEnumerator.MoveNext())
                    {
                        if (pDicEnumerator.Key.ToString().Trim() == pEnumerator.Current.ToString().Trim())
                        {
                            m_SelectHashtable.Add(pDicEnumerator.Key, pDicEnumerator.Value);
                            break;
                        }
                    }
                }
                else
                {
                    m_SelectHashtable.Add(pEnumerator.Current.ToString(), 0);
                }
            }
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if (m_SelectHashtable.Count == 0)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private Boolean HasZeroValue()
        {
            Boolean flag = false;
            foreach (object Item in this.lbxRenderValue.Items)
            {
                if (Item.ToString().Trim() == "")
                { flag = true; break; }
            }
            return flag;
        }

    }
}