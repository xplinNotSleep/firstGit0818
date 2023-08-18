using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Security.Permissions;

namespace AG.Pipe.Analyst3DModel
{
    /// <summary>
    /// 数据核查方案属性类
    /// </summary>
    [Serializable]
    public class PipeConvertGroup : ISchemeValueChanged, ISchemeName
    {

        #region 界面配置内容
        /// <summary>
        /// 获取或设置核查数据规则图层
        /// </summary>
        [Category("常规"), Description("管线所在大类"), DefaultValue("管线所在大类")]
        public string Name { get; set; } = "管线大类";

        [Browsable(false)]
        public List<ConvertLayerSet> ConvertLayerSets { get; set; } = new List<ConvertLayerSet>();

        #endregion

        #region 事件
        public event SchemeValueChangedEventHandler SchemePropertyValueChanged;
        public void OnSchemePropertyValueChanged(object sender, SchemePropertyEventArgs e)
        {
            if (SchemePropertyValueChanged != null)
            {
                SchemePropertyValueChanged(sender, e);
            }
        }
        #endregion
    }
}
