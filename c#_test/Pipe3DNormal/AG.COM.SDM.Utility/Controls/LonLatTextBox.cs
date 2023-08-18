using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility.Controls
{
    /// <summary>
    /// 用于表现经纬度的TextBox
    /// </summary>
    public partial class LonLatTextBox : UserControl
    {
        #region 变量

        /// <summary>
        /// 当前经纬度显示风格
        /// </summary>
        private LonLatTextStyle m_LonLatTextStyle = LonLatTextStyle.DegreesMinutesSeconds;

        #endregion

        #region  初始化

        public LonLatTextBox()
        {
            InitializeComponent();
        }

        #endregion

        #region 外部函数

        /// <summary>
        /// 赋值到控件（单位：小数度）
        /// </summary>
        /// <param name="value"></param>
        public void SetValueByDecimalDegrees(double value)
        {
            RefreshTextBox(value);
        }

        /// <summary>
        /// 改变TextBox的显示风格
        /// </summary>
        /// <param name="tLonLatTextStyle"></param>
        public void ChangeTextStyle(LonLatTextStyle tLonLatTextStyle)
        {
            double value = GetTextBoxValue();

            m_LonLatTextStyle = tLonLatTextStyle;

            RefreshTextBox(value);
        }

        /// <summary>
        /// 获取当前值（单位：小数度）
        /// </summary>
        /// <returns></returns>
        public double GetCurrentValueByDecimalDegrees()
        {
            return GetTextBoxValue();
        }

        #endregion

        #region 内部函数

        /// <summary>
        /// 刷新TextBox内容到某个值（单位：小数度）
        /// </summary>
        /// <param name="value"></param>
        private void RefreshTextBox(double value)
        {
            if (m_LonLatTextStyle == LonLatTextStyle.DegreesMinutesSeconds)
            {
                nud1.DecimalPlaces = 0;
                nud2.DecimalPlaces = 0;

                nud2.Enabled = true;
                nud3.Enabled = true;

                decimal lon = Convert.ToDecimal(value);
                nud1.Value = (int)lon;
                decimal tem1 = (lon - (int)lon) * 60;
                nud2.Value = (int)tem1;
                tem1 = (tem1 - (int)tem1) * 60;
                nud3.Value = tem1;
            }
            else if (m_LonLatTextStyle == LonLatTextStyle.DecimalDegrees)
            {
                nud1.DecimalPlaces = 6;
                nud2.DecimalPlaces = 0;

                nud2.Enabled = false;
                nud3.Enabled = false;

                decimal lon = Convert.ToDecimal(value);
                nud1.Value = (int)lon;
                nud2.Value = 0;
                nud3.Value = 0;
            }
            else if (m_LonLatTextStyle == LonLatTextStyle.DegreesAndDecimalMinutes)
            {
                nud1.DecimalPlaces = 0;
                nud2.DecimalPlaces = 6;

                nud2.Enabled = true;
                nud3.Enabled = false;

                decimal lon = Convert.ToDecimal(value);
                nud1.Value = (int)lon;
                decimal tem1 = (lon - (int)lon) * 60;
                nud2.Value = tem1;
                nud3.Value = 0;
            }
        }

        /// <summary>
        /// 获取当前TextBox的值（单位：小数度）
        /// </summary>
        /// <returns></returns>
        private double GetTextBoxValue()
        {
            //当前单位是度分秒
            if (m_LonLatTextStyle == LonLatTextStyle.DegreesMinutesSeconds)
            {
                return Convert.ToDouble(nud1.Value) + Convert.ToDouble(nud2.Value) / 60
                    + Convert.ToDouble(nud3.Value) / 3600;
            }
            //当前单位是小数度
            else if (m_LonLatTextStyle == LonLatTextStyle.DecimalDegrees)
            {
                return Convert.ToDouble(nud1.Value);
            }
            //当前单位是度和小数分
            else if (m_LonLatTextStyle == LonLatTextStyle.DegreesAndDecimalMinutes)
            {
                return Convert.ToDouble(nud1.Value) + Convert.ToDouble(nud2.Value) / 60;
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }

    /// <summary>
    /// 经纬度显示风格
    /// </summary>
    public enum LonLatTextStyle
    {
        /// <summary>
        /// 度分秒
        /// </summary>
        DegreesMinutesSeconds,
        /// <summary>
        /// 小数度
        /// </summary>
        DecimalDegrees,
        /// <summary>
        /// 度和小数分
        /// </summary>
        DegreesAndDecimalMinutes
    }
}
