using System;
using System.Collections.Generic;
using System.Text;

namespace AG.Pipe.Analyst3DModel
{
    /// <summary>
    /// 错误记录
    /// </summary>
    [Serializable]
    public class InputRecord
    {
        private string m_LayerName;
        private string m_TargetPath;
        private string m_SourceLayerName;
        private bool m_IsSuceed = false;
        private string m_Detail = "";

        public string Detail
        {
            get { return m_Detail; }
            set { m_Detail = value; }
        }
        /// <summary>
        /// 获取或设置图层名
        /// </summary>
        public string LayerName
        {
            get { return this.m_LayerName; }
            set { this.m_LayerName = value; }
        }

        /// <summary>
        /// 获取或设置错误类型
        /// </summary>
        public string SourceLayerName 
        {
            get { return this.m_SourceLayerName; }
            set { this.m_SourceLayerName = value; }
        }

        /// <summary>
        /// 获取或设置错误类型
        /// </summary>
        public string SLayerName
        {
            get {
                string[] LayerNames = m_SourceLayerName.Split('\\');
                if(LayerNames.Length >1)
                {
                    return LayerNames[LayerNames.Length - 1];
                }
                return LayerNames[0];
            
            }
        }

        /// <summary>
        /// 获取或设置错误描述信息
        /// </summary>
        public string TargetPath
        {
            get { return this.m_TargetPath; }
            set { this.m_TargetPath = value; }
        }

        /// <summary>
        /// 获取或设置修改状态
        /// </summary>
        public bool IsSuceed
        {
            get { return this.m_IsSuceed; }
           
            set
            {
                this.m_IsSuceed = value;
            }
        }
        public string Description
        {
            get
            {
                if (this.m_IsSuceed == false)
                    return "转换成功";
                else
                    return "转换失败";
            }
       
        }

        public override string ToString()
        {
            if (this.m_IsSuceed == false)
            {
                string tStr = this.m_LayerName + "转换失败!源数据表为" + this.SourceLayerName+"错误描述："+m_Detail;
                return tStr;
            }
            else 
            {
                string tStr = this.m_LayerName + "转换成功!" + m_Detail;
                return tStr;
            }
        }
    }
}
