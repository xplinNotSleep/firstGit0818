using System;
using System.Collections.Generic;
using System.Text;
using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.Catalog.Filters;

namespace AG.COM.SDM.Catalog
{
    /// <summary>
    /// ��������Ի���ӿ�
    /// </summary>
    public interface IDataBrowser
    {
        /// <summary>
        /// ��ȡѡ�еĶ����б�
        /// </summary>
        IList<DataItem> SelectedItems { get;}
        /// <summary>
        /// ��ȡ������е�����
        /// </summary>
        string NameString { get;}
        /// <summary>
        /// ��ȡ�����õ�ǰλ��
        /// </summary>
        DataItem NavigateLocation { get;set;}
        ///// <summary>
        ///// ����������,�����oaSelecData��������
        ///// </summary>
        //OpenAction OpenAction { get;set;}
        /// <summary>
        /// �Ƿ�֧�ֶ�ѡ�� ���������OpenAction,��Ҫ��OpenAction֮������
        /// </summary>
        bool MultiSelect { get;set;}
        /// <summary>
        /// ��ӹ�������
        /// </summary>
        /// <param name="filter">��������</param>
        void AddFilter(IDataItemFilter filter);
        /// <summary>
        /// ��ʾ�Ի���
        /// </summary>
        /// <returns>��ȡָ���Ի���ķ���ֵ</returns>
        System.Windows.Forms.DialogResult ShowDialog();
        /// <summary>
        /// ��ʾ����
        /// </summary>
        void Show();
        /// <summary>
        /// ���ڱ���
        /// </summary>
        string Text { get;set;}
        /// <summary>
        /// �����Ƿ�������ɵ��������Ϊfalse����ֻ���ڳ�ʼλ����ѡ��
        /// </summary>
        bool FreeNavigation { set;}
        /// <summary>
        /// ��������Դ�˵�����
        /// </summary>
        EnumCategoriesType CategoriesType { get; set; }
    }


    //[Flags]
    //public enum OpenAction
    //{
    //    oaSelecData = 0x01,
    //    oaSelectFeatureDataset = 0x02,
    //    oaSelectWorkspace = 0x03
    //}
}
