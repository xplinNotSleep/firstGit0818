using System;
using System.Collections.Generic;
using System.Text;
using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.Catalog.Filters;

namespace AG.COM.SDM.Catalog
{
    /// <summary>
    /// 数据浏览对话框接口
    /// </summary>
    public interface IDataBrowser
    {
        /// <summary>
        /// 获取选中的对象列表
        /// </summary>
        IList<DataItem> SelectedItems { get;}
        /// <summary>
        /// 获取输入框中的文字
        /// </summary>
        string NameString { get;}
        /// <summary>
        /// 获取或设置当前位置
        /// </summary>
        DataItem NavigateLocation { get;set;}
        ///// <summary>
        ///// 操作的类型,如果是oaSelecData则不用设置
        ///// </summary>
        //OpenAction OpenAction { get;set;}
        /// <summary>
        /// 是否支持多选， 如果设置了OpenAction,则要在OpenAction之后设置
        /// </summary>
        bool MultiSelect { get;set;}
        /// <summary>
        /// 添加过滤条件
        /// </summary>
        /// <param name="filter">过滤类型</param>
        void AddFilter(IDataItemFilter filter);
        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <returns>获取指定对话框的返回值</returns>
        System.Windows.Forms.DialogResult ShowDialog();
        /// <summary>
        /// 显示界面
        /// </summary>
        void Show();
        /// <summary>
        /// 窗口标题
        /// </summary>
        string Text { get;set;}
        /// <summary>
        /// 设置是否可以自由导航，如果为false，则只能在初始位置上选择
        /// </summary>
        bool FreeNavigation { set;}
        /// <summary>
        /// 设置数据源菜单种类
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
