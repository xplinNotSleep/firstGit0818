using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Catalog
{
    public class ListViewItemSorterBinder
    {
        /// <summary>
        /// ʹһ��ListView���е������Ĺ���
        /// </summary>
        /// <param name="listview"></param>
        public static void BindListView(ListView listview)
        {
            ListViewColumnSorter sorter = new ListViewColumnSorter(listview);
        }
    }
 
    
    /// <summary>
    /// �̳���IComparer
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        private ListView m_ListView = null;
        public ListViewColumnSorter(ListView listview)
        {
            // Ĭ�ϰ���һ������
            ColumnToSort = 0;

            // ����ʽΪ������
            OrderOfSort = SortOrder.None;

            // ��ʼ��CaseInsensitiveComparer�����
            ObjectCompare = new CaseInsensitiveComparer();

            m_ListView = listview;
            listview.ListViewItemSorter = this;
            listview.ColumnClick += new ColumnClickEventHandler(listview_ColumnClick);
        }
         
        /// <summary>
        /// ָ�������ĸ�������
        /// </summary>
        private int ColumnToSort;
        /// <summary>
        /// ָ������ķ�ʽ
        /// </summary>
        private SortOrder OrderOfSort;
        /// <summary>
        /// ����CaseInsensitiveComparer�����
        /// �μ�ms-help://MS.VSCC.2003/MS.MSDNQTR.2003FEB.2052/cpref/html/frlrfSystemCollectionsCaseInsensitiveComparerClassTopic.htm
        /// </summary>
        private CaseInsensitiveComparer ObjectCompare;
     

        /// <summary>
        /// ��дIComparer�ӿ�.
        /// </summary>
        /// <param name="x">Ҫ�Ƚϵĵ�һ������</param>
        /// <param name="y">Ҫ�Ƚϵĵڶ�������</param>
        /// <returns>�ȽϵĽ��.�����ȷ���0�����x����y����1�����xС��y����-1</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            // ���Ƚ϶���ת��ΪListViewItem����
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            // �Ƚ�
            compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

            // ��������ıȽϽ��������ȷ�ıȽϽ��
            if (OrderOfSort == SortOrder.Ascending)
            {
                // ��Ϊ��������������ֱ�ӷ��ؽ��
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // ����Ƿ�����������Ҫȡ��ֵ�ٷ���
                return (-compareResult);
            }
            else
            {
                // �����ȷ���0
                return 0;
            }
        }

        /// <summary>
        /// ��ȡ�����ð�����һ������.
        /// </summary>
        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }

        /// <summary>
        /// ��ȡ����������ʽ.
        /// </summary>
        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }
          
        private ListViewColumnSorter lvwColumnSorter;
        private void listview_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // ����������ǲ������ڵ�������.
            if (e.Column == this.SortColumn)
            {
                // �������ô��е����򷽷�.
                if (this.Order == SortOrder.Ascending)
                {
                    this.Order = SortOrder.Descending;
                }
                else
                {
                    this.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // ���������У�Ĭ��Ϊ��������
                this.SortColumn = e.Column;
                this.Order = SortOrder.Ascending;
            }

            // ���µ����򷽷���ListView����
            m_ListView.Sort();
        }
    }

}
