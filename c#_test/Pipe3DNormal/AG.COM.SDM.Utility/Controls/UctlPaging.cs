using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace AG.COM.SDM.Utility.Controls
{
    /// <summary>
    /// 自定义分页控件
    /// </summary>
    public partial class UctlPaging : UserControl
    {
        #region 变量
        /// <summary>
        /// 重新分页刷新数据
        /// </summary>
        public event PagingEventHandler RePagingEvent;

        /// <summary>
        /// 记录总数
        /// </summary>
        public int RowCount { get; set; }
        /// <summary>
        /// 当前页面
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int RowPerPage { get; set; }
        /// <summary>
        /// 每页最大行数最大值
        /// </summary>
        public int MaxRowPerPage { get; set; }

        #endregion

        /// <summary>
        /// 默认构造方法
        /// </summary>
        public UctlPaging()
        {
            ///初始化默认值
            CurrentPage = 1;
            RowPerPage = 20;
            MaxRowPerPage = 100;

            InitializeComponent();

            RefreshUIPaging();
        }

        /// <summary>
        /// 获取当前分页信息，一般用在查询数据前
        /// </summary>
        /// <returns></returns>
        public PagingInfo GetCurrentPagingInfo()
        {
            GetPagingFromUI();

            return new PagingInfo(RowCount, CurrentPage, RowPerPage);
        }

        /// <summary>
        /// 输入分页信息并刷新界面，一般用在查询数据后从外部刷新本控件的分页信息
        /// </summary>
        /// <param name="tPagingInfo"></param>
        public void RefreshUIPagingInfo(PagingInfo tPagingInfo)
        {
            this.RowCount = tPagingInfo.RowCount;
            this.CurrentPage = tPagingInfo.CurrentPage;
            this.RowPerPage = tPagingInfo.RowPerPage;

            RefreshUIPaging();
        }

        /// <summary>
        /// 控件界面重新分页事件
        /// </summary>
        private void RePaging()
        {
            if (null != RePagingEvent)
            {
                RePagingEvent(new PagingInfo(this.RowCount, this.CurrentPage, this.RowPerPage));
            }
        }

        /// <summary>
        /// 刷新分页信息到控件界面
        /// </summary>
        private void RefreshUIPaging()
        {
            ///计算并验证分页信息

            ///记录总数
            if (RowCount < 0) RowCount = 0;
            ///每页记录数
            if (RowPerPage < 1) RowPerPage = 1;
            ///当前页数
            if (CurrentPage < 1) CurrentPage = 1;
            ///页面总数
            int pageCount = 0;
            if (RowCount % RowPerPage > 0)
            {
                pageCount = RowCount / RowPerPage + 1;
            }
            else
            {
                pageCount = RowCount / RowPerPage;
            }
            if (CurrentPage > pageCount)
            {
                CurrentPage = pageCount;
            }
            ///当前页记录总数
            int currentPageRowCount = 0;
            ///当前页是最后一页
            if (CurrentPage == pageCount)
            {
                currentPageRowCount = RowCount - ((CurrentPage - 1) * RowPerPage);
            }
            else
            {
                currentPageRowCount = RowPerPage;
            }

            ///把分页信息写到界面
            tslAllRecordCount.Text = RowCount.ToString();
            tslCurrentPageRecordCount.Text = currentPageRowCount.ToString();
            tslPageCount.Text = pageCount.ToString();
            tslAllpage.Text = pageCount.ToString();
            txtCurrentPage.Text = CurrentPage.ToString();
            txtRecordPerPage.Text = RowPerPage.ToString();
        }

        #region 界面进行分页操作

        private void txtRecordPerPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    GetPagingFromUI();

                    RePaging();
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
            }
        }

        private void txtCurrentPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    GetPagingFromUI();

                    RePaging();
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
            }
        }

        /// <summary>
        /// 从界面取分页信息的值，一般用在控件进行分页操作时从界面获取分页信息
        /// </summary>
        public void GetPagingFromUI()
        {
            int newRowPerPage = DataConvert.StrToInt(txtRecordPerPage.Text);
            if (newRowPerPage < 1 || newRowPerPage > MaxRowPerPage)
            {
                newRowPerPage = RowPerPage;
            }
            RowPerPage = newRowPerPage;

            int newCurrentPage = DataConvert.StrToInt(txtCurrentPage.Text);
            if (newCurrentPage < 1)
            {
                newCurrentPage = CurrentPage;
            }
            CurrentPage = newCurrentPage;
        }

        private void tsbNextPage_Click(object sender, EventArgs e)
        {
            try
            {
                GetPagingFromUI();

                CurrentPage++;

                RePaging();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
            }
        }

        private void tsbPreviousPage_Click(object sender, EventArgs e)
        {
            try
            {
                GetPagingFromUI();

                CurrentPage--;

                RePaging();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
            }
        }

        private void tsbLastPage_Click(object sender, EventArgs e)
        {
            try
            {
                GetPagingFromUI();

                CurrentPage = DataConvert.StrToInt(tslAllpage.Text);

                RePaging();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
            }
        }

        private void tsbFirstPage_Click(object sender, EventArgs e)
        {
            try
            {
                GetPagingFromUI();

                CurrentPage = 1;

                RePaging();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
            }
        }

        #endregion
    }

    /// <summary>
    /// 分页控件引发的刷新数据事件
    /// </summary>
    /// <param name="tPagingInfo"></param>
    public delegate void PagingEventHandler(PagingInfo tPagingInfo);

    /// <summary>
    /// 分页信息
    /// </summary>
    public class PagingInfo
    {
        #region  变量

        /// <summary>
        /// 记录总数
        /// </summary>
        public int RowCount { get; set; }
        /// <summary>
        /// 当前页面
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int RowPerPage { get; set; }

        #endregion

        public PagingInfo(int RowCount, int CurrentPage, int RowPerPage)
        {
            this.RowCount = RowCount;
            this.CurrentPage = CurrentPage;
            this.RowPerPage = RowPerPage;
        }

        #region 分页操作

        /// <summary>
        /// 根据总记录数和每页记录数计算当前页数
        /// </summary>
        public void CalculateCurrentPage()
        {
            if (RowCount < 0) RowCount = 0;
            if (CurrentPage < 1) CurrentPage = 1;
            if (RowPerPage < 1) RowPerPage = 1;

            ///页面总数
            int pageCount = 0;
            if (RowCount < 1)
            {
                pageCount = 1;
            }
            else if (RowCount % RowPerPage > 0)
            {
                pageCount = RowCount / RowPerPage + 1;
            }
            else
            {
                pageCount = RowCount / RowPerPage;
            }
            ///当前页数
            if (CurrentPage > pageCount)
            {
                CurrentPage = pageCount;
            }
        }

        /// <summary>
        /// 往sql添加分页
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string PagingSql(string sql)
        {
            return "select * from(select * from(select it.*,row_number() over () as rownum from ( " + sql +
                 " )it)tt where tt.rownum<=" + GetPageEndIndex() + ")g where rownum>=" + GetPageStartIndex();
        }

        /// <summary>
        /// 获取当前页的起始行数
        /// </summary>
        /// <returns></returns>
        public int GetPageStartIndex()
        {
            CalculateCurrentPage();

            return (CurrentPage - 1) * RowPerPage + 1;
        }

        /// <summary>
        /// 获取当前页的最后行数
        /// </summary>
        /// <returns></returns>
        public int GetPageEndIndex()
        {
            CalculateCurrentPage();

            return CurrentPage * RowPerPage;
        }

        #endregion
    }
}
