using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AG.Pipe.Analyst3DModel
{
    /// <summary>
    /// 可编辑的ListView控件
    /// </summary>
    public class ListViewEdit : ListView
    {
        private ListViewItem m_currentLVItem;
        private int m_nX = 0;
        private int m_nY = 0;
        private string m_strSubItemText;
        private int m_nSubItemSelected = 0;
        private ComboBox[] m_arrComboBoxes = new ComboBox[20];
        private System.Windows.Forms.TextBox editBox;
        private Font m_fontComboBox;
        private Font m_fontEdit;
        private Color m_bgcolorComboBox;
        private Color m_bgcolorEdit;
        private ComboBox m_CurrentComboBox = null;

        /// <summary>
        /// 默认构造函数,实例化新对象
        /// </summary>
        public ListViewEdit()
        {
            editBox = new System.Windows.Forms.TextBox();
            this.m_fontComboBox = this.Font;
            this.EditFont = this.Font;

            this.EditBgColor = Color.LightBlue;
            this.m_bgcolorComboBox = Color.LightBlue;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Custom_MouseDown);
            this.DoubleClick += new System.EventHandler(this.Custom_DoubleClick);
            this.GridLines = true;

            editBox.Size = new System.Drawing.Size(0, 0);
            editBox.Location = new System.Drawing.Point(0, 0);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.editBox });
            editBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EditOver);
            editBox.LostFocus += new System.EventHandler(this.FocusOver);
            editBox.AutoSize = true;
            editBox.Font = this.EditFont;
            editBox.BackColor = this.EditBgColor;
            editBox.BorderStyle = BorderStyle.FixedSingle;
            editBox.Hide();
            editBox.Text = "";
            this.FullRowSelect = true;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            //水平滚动消息276 ，垂直滚动条消息277
            //滚动时隐藏输入框
            if ((m.Msg == 277) || (m.Msg == 276))
            {
                if (m_CurrentComboBox != null)
                    m_CurrentComboBox.Hide();

                editBox.Hide();
            }
        }

        /// <summary>
        /// 获取或设置下拉列表框的字体
        /// </summary>
        public Font ComboBoxFont
        {
            get { return this.m_fontComboBox; }
            set { this.m_fontComboBox = value; }
        }

        /// <summary>
        /// 获取或设置下拉列表框的各项的背景颜色
        /// </summary>
        public Color ComboBoxBgColor
        {
            get
            {
                return this.m_bgcolorComboBox;
            }
            set
            {
                this.m_bgcolorComboBox = value;
                for (int i = 0; i < this.m_arrComboBoxes.Length; i++)
                {
                    if (m_arrComboBoxes[i] != null)
                        m_arrComboBoxes[i].BackColor = this.m_bgcolorComboBox;
                }
            }
        }

        /// <summary>
        /// 获取或设置编辑文本框的字体
        /// </summary>
        public Font EditFont
        {
            get
            {
                return this.m_fontEdit;
            }
            set
            {
                this.m_fontEdit = value;
                this.editBox.Font = this.m_fontEdit;
            }
        }

        /// <summary>
        /// 获取或设置编辑文本框的背景颜色
        /// </summary>
        public Color EditBgColor
        {
            get { return this.m_bgcolorEdit; }
            set
            {
                this.m_bgcolorEdit = value;
                this.editBox.BackColor = this.m_bgcolorEdit;
            }
        }

        /// <summary>
        /// 设置指定列下拉列表框的样式
        /// </summary>
        /// <param name="columnIndex">指定列索引</param>
        /// <param name="cs">列样式</param>
        public void SetColumnStyle(int columnIndex, ALAN_ListViewColumnStyle cs)
        {
            if (columnIndex < 0 || columnIndex > this.Columns.Count)
                throw new Exception("Column index is out of range");
            ((ALAN_ColumnHeader)Columns[columnIndex]).ColumnStyle = cs;
        }

        /// <summary>
        /// 设置指定列下拉列表框的的数据源
        /// </summary>
        /// <param name="columnIndex">指定列索引</param>
        /// <param name="items">绑定的集合项</param>
        public void BoundListToColumn(int columnIndex, string[] items)
        {
            if (columnIndex < 0 || columnIndex > this.Columns.Count)
                throw new Exception("Column index is out of range");

            if (((ALAN_ColumnHeader)Columns[columnIndex]).ColumnStyle != ALAN_ListViewColumnStyle.ComboBox)
                throw new Exception("Column should be ComboBox style");

            ComboBox newbox = new ComboBox();
            newbox.DataSource = items;
            //for (int i = 0; i < items.Length; i++)
            //    newbox.Items.Add(items[i]);

            newbox.Size = new System.Drawing.Size(0, 0);
            newbox.Location = new System.Drawing.Point(0, 0);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { newbox });
            newbox.SelectedIndexChanged += new System.EventHandler(this.CmbSelected);
            newbox.LostFocus += new System.EventHandler(this.CmbFocusOver);
            newbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbKeyPress);

            newbox.Font = this.ComboBoxFont;
            newbox.BackColor = this.ComboBoxBgColor;
            newbox.DropDownStyle = ComboBoxStyle.DropDownList;
            newbox.Hide();

            this.m_arrComboBoxes[columnIndex] = newbox;
        }

        /// <summary>
        /// 在下拉列表框具有焦点的情况按键处理方法
        /// </summary>
        /// <param name="sender">源对象</param>
        /// <param name="e">事件参数</param>
        private void CmbKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            ComboBox cmbBox = (ComboBox)sender;

            if (e.KeyChar == 13 || e.KeyChar == 27) //CR or ESC press
            {
                cmbBox.Hide();
            }
        }

        private void CmbSelected(object sender, System.EventArgs e)
        {
            ComboBox cmbBox = (ComboBox)sender;
            int sel = cmbBox.SelectedIndex;
            if (sel >= 0)
            {
                string itemSel = cmbBox.Items[sel].ToString();
                if (m_currentLVItem.SubItems[m_nSubItemSelected].Text != itemSel)
                {
                    m_currentLVItem.SubItems[m_nSubItemSelected].Text = itemSel;

                    if (AfterEdit != null)
                        AfterEdit(m_currentLVItem, itemSel);
                }
            }
        }

        private void CmbFocusOver(object sender, System.EventArgs e)
        {
            ComboBox cmbBox = (ComboBox)sender;
            cmbBox.Hide();
        }

        /// <summary>
        /// 在编辑文本框具有焦点的情况下按键处理方法
        /// </summary>
        /// <param name="sender">源对象</param>
        /// <param name="e">事件参数</param>
        private void EditOver(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (m_currentLVItem.SubItems[m_nSubItemSelected].Text != editBox.Text)
                {
                    m_currentLVItem.SubItems[m_nSubItemSelected].Text = editBox.Text;

                    if (AfterEdit != null)
                        AfterEdit(m_currentLVItem, editBox.Text);
                }

                editBox.Hide();
            }

            //ESC键
            if (e.KeyChar == 27)
                editBox.Hide();
        }

        private void FocusOver(object sender, System.EventArgs e)
        {
            if (m_currentLVItem.SubItems[m_nSubItemSelected].Text != editBox.Text)
            {
                m_currentLVItem.SubItems[m_nSubItemSelected].Text = editBox.Text;
                if (AfterEdit != null)
                    AfterEdit(m_currentLVItem, editBox.Text);
            }
            editBox.Hide();
        }

        /// <summary>
        /// 取得当前鼠标在控件上的位置
        /// </summary>
        /// <param name="hwnd">控件句柄</param>
        /// <returns></returns>
        private System.Drawing.Point GetMousePosInControl(int hwnd)
        {
            POINTAPI pt = new POINTAPI();
            RECT rect = new RECT();
            WinAPI.GetCursorPos(ref pt);
            WinAPI.GetWindowRect(hwnd, ref rect);

            System.Drawing.Point pt2 = new System.Drawing.Point();
            pt2.X = pt.x - rect.Left;
            pt2.Y = pt.y - rect.Top;

            return pt2;
        }

        public void Custom_DoubleClick(object sender, System.EventArgs e)
        {
            int nStart = m_nX; //current mouse down X position 
            int spos = 0;
            int epos = this.Columns[0].Width;

            for (int i = 0; i < this.Columns.Count; i++)
            {
                if (nStart > spos && nStart < epos)
                {
                    m_nSubItemSelected = i;
                    break;
                }

                spos = epos;
                epos += this.Columns[i].Width;
            }

            Point point = this.GetMousePosInControl((int)this.Handle);
            ListViewHitTestInfo hit = this.HitTest(point);
            if (hit.Item == null) return;
            if (hit.SubItem == null) return;

            m_strSubItemText = m_currentLVItem.SubItems[m_nSubItemSelected].Text;
            ALAN_ColumnHeader column = (ALAN_ColumnHeader)Columns[m_nSubItemSelected];

            if (column.ColumnStyle == ALAN_ListViewColumnStyle.ComboBox)
            {
                ComboBox cmbBox = this.m_arrComboBoxes[m_nSubItemSelected];
                if (cmbBox == null) return;

                Rectangle r = hit.SubItem.Bounds;

                cmbBox.Size = editBox.Size = new Size(r.Width, r.Height);
                cmbBox.Location = new Point(r.Left, r.Top);
                cmbBox.Show();
                cmbBox.Text = m_strSubItemText;
                cmbBox.SelectAll();
                cmbBox.Focus();
                cmbBox.DroppedDown = true;

                m_CurrentComboBox = cmbBox;
            }

            if (column.ColumnStyle == ALAN_ListViewColumnStyle.EditBox)
            {
                if (hit.SubItem != null)
                {
                    Rectangle r = hit.SubItem.Bounds;
                    editBox.Size = new Size(r.Width, r.Height);
                    editBox.Location = new Point(r.Left, r.Top);
                    editBox.Show();
                    editBox.Text = m_strSubItemText;
                    editBox.SelectAll();
                    editBox.Focus();
                }
            }
        }

        public void Custom_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            m_currentLVItem = this.GetItemAt(e.X, e.Y);
            m_nX = e.X;
            m_nY = e.Y;
        }

        public event AfterEditDelegate AfterEdit = null;
    }

    public delegate void AfterEditDelegate(ListViewItem item, string value);

    /// <summary>
    /// 列风格枚举
    /// </summary>
    public enum ALAN_ListViewColumnStyle
    {
        /// <summary>
        /// 只读
        /// </summary>
        ReadOnly,
        /// <summary>
        /// 编辑状态下显示为文本框
        /// </summary>
        EditBox,
        /// <summary>
        /// 编辑状态下显示为组合框
        /// </summary>
        ComboBox
    }

    /// <summary>
    /// 列描述 类
    /// </summary>
    public class ALAN_ColumnHeader : ColumnHeader
    {
        private ALAN_ListViewColumnStyle cs; //本列的风格
        public ALAN_ColumnHeader()
            : base()
        {
            cs = ALAN_ListViewColumnStyle.ReadOnly;
        }

        /// <summary>
        /// 实例化列描述新对象
        /// </summary>
        /// <param name="_cs">指定的列风格</param>
        public ALAN_ColumnHeader(ALAN_ListViewColumnStyle _cs)
        {
            cs = _cs;
        }

        /// <summary>
        /// 获取或设置ListView列样式
        /// </summary>
        public ALAN_ListViewColumnStyle ColumnStyle
        {
            get { return cs; }
            set { cs = value; }
        }
    }

    /// <summary>
    /// 矩形
    /// </summary>
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    /// <summary>
    /// 点
    /// </summary>
    public struct POINTAPI
    {
        public int x;
        public int y;
    }
    /// <summary>
    /// WinAPI引用
    /// </summary>
    public static class WinAPI
    {
        /// <summary>
        /// 获取光标当前位置
        /// </summary>
        /// <param name="lpPoint"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        public static extern int GetCursorPos(
            ref POINTAPI lpPoint
        );


        [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        public static extern int GetWindowRect(
            int hwnd,
            ref RECT lpRect
        );

        /// <summary>
        /// 获取窗体句柄
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowDC")]
        public static extern int GetWindowDC(
            int hwnd
        );

        /// <summary>
        /// 释放设备资源
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="hdc"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern int ReleaseDC(
            int hwnd,
            int hdc
        );
    }
}
