using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace AG.Pipe.Analyst3DModel
{
    /// <summary>
    /// 通用帮助类
    /// </summary>
    public class CommonHelper
    {
        
        public static string ConvertPointsInfo2Str(List<PointInfo> pPointsInfo)
        {
            if (pPointsInfo == null) return "";
            string tPointsStr = "";

            for (int i = 0; i < pPointsInfo.Count; i++)
            {
                if (pPointsInfo[i] == null) continue;
                tPointsStr += "@点" + i.ToString() + "(" + pPointsInfo[i].x + "," + pPointsInfo[i].y + "," + pPointsInfo[i].z + ")";
            }

            return tPointsStr;


        }

        public static List<PointInfo> ConvertStr2PointsInfo(string pPointsInfo)
        {
            if (pPointsInfo == string.Empty) return null;
            List<PointInfo> tPointsStr = new List<PointInfo>();
            string[] tt = pPointsInfo.Split('@');
            for (int i = 1; i < tt.Length; i++)
            {
                string tTempStr = tt[i].Substring(tt[i].IndexOf('(')+1, tt[i].IndexOf(")") - tt[i].IndexOf('(')-1);
               string[] tPointstr =tTempStr.Split(',');
                PointInfo tPointInfo = new PointInfo();
                tPointInfo.x = tPointstr[0].ToString();
                tPointInfo.y = tPointstr[1].ToString();
                tPointInfo.z = tPointstr[2].ToString();

                tPointsStr.Add(tPointInfo);

               
            }

                return tPointsStr;


        }

    }
}
