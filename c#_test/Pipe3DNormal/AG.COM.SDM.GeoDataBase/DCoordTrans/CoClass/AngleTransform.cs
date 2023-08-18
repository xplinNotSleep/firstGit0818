using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.COM.SDM.GeoDataBase
{
    public class AngleTransform
    {
        /// <summary>
        /// 角度转换为弧度
        /// </summary>
        /// <param name="dblD">角度</param>
        /// <param name="dfmformat">所传入的角度是否为度分秒(DD.FFMM)格式,如果是则输入true,否则输入false</param>
        /// <returns>返回弧度</returns>
        public static double Degree2Radian(double dblD, bool dfmformat)
        {
            if (dfmformat == true)
            {
                //将度分秒格式转换为百分制角度
                dblD = Ang60ToAng100(dblD);
            }

            double dR = dblD * Math.PI / 180;

            return dR;
        }

        /// <summary>
        /// 把弧度转化为弧度 *180/PI
        /// </summary>
        /// <param name="dblR">弧度</param>
        /// <param name="dfmformat">所返回的角度是否为度分秒(DD.FFMM)格式,如果是则输入true,否则输入false</param>
        /// <returns>返回指定格式的角度</returns>
        public static double Radian2Degree(double dblR, bool dfmformat)
        {
            double dAng;

            dAng = dblR * 180 / Math.PI;
            if (dfmformat == true)
            {
                //将百分制角度转换为度分秒角度值
                dAng = Ang100ToAng60(dAng);
            }

            return dAng;
        }

        /// <summary>
        /// 将百分制角度转为度分秒格式的角度
        /// </summary>
        /// <param name="dblAng">百分制格式角度</param>
        /// <returns>度分秒格式角度</returns>
        public static double Ang100ToAng60(double dblAng)
        {
            //将总秒数转换为含度分秒格式的角度值
            return Ms2Ang(dblAng * 3600);
        }


        /// <summary>
        /// 将度分秒格式角度转为百分制角度
        /// </summary>
        /// <param name="dfmAng">度分秒格式角度</param>
        /// <returns>百分制角度</returns>
        public static double Ang60ToAng100(double dfmAng)
        {
            double allms;

            //将度分秒格式角度转换为秒数
            allms = Ang2Ms(dfmAng);

            return Math.Round(allms / 3600, 8);
        }

        /// <summary>
        /// 将度分秒格式角度转换为秒数
        /// </summary>
        /// <param name="dfmAng">度分秒格式角度</param>
        /// <returns>角度总秒数</returns>
        public static double Ang2Ms(double dfmAng)
        {
            double ds, fs, ms;      //度,分,秒
            double tempAng;

            //在角度值中提取度分秒
            ExtractAng(dfmAng, out ds, out fs, out ms);

            if (ds > 360 || fs > 60 || ms > 60)
                return 0;

            tempAng = ds * 3600 + fs * 60 + ms;
            return tempAng;
        }

        /// <summary>
        /// 将总秒数转为含度分秒的度分秒角度值
        /// </summary>
        /// <param name="dfmAllMs">总秒数</param>
        /// <returns>度分秒角度值</returns>
        public static double Ms2Ang(double dfmAllMs)
        {
            double ds, fs, ms;      //度,分,秒          
            double tempAng;

            if (Math.Round(dfmAllMs, 8) < 0)
            {
                ds = Math.Ceiling(dfmAllMs / 3600);
                fs = Math.Ceiling((dfmAllMs % 3600) / 60);
                ms = Math.Round((dfmAllMs % 3600) % 60, 4);
            }
            else
            {
                ds = Math.Floor(dfmAllMs / 3600);
                fs = Math.Floor((dfmAllMs % 3600) / 60);
                ms = Math.Round((dfmAllMs % 3600) % 60, 4);
            }

            tempAng = ds + (fs / 100) + (ms / 10000);
            return tempAng;
        }

        /// <summary>
        /// 在角度值(度分秒格式)中提取出度分秒
        /// </summary>
        /// <param name="dfmAng">度分秒格式角度</param>
        /// <param name="ds">度数</param>
        /// <param name="fs">分数</param>
        /// <param name="ms">秒数</param>
        public static void ExtractAng(double dfmAng, out double ds, out double fs, out double ms)
        {
            if (Math.Round(dfmAng, 8) < 0)
            {
                ds = Math.Ceiling(dfmAng);
                fs = Math.Ceiling(Math.Round((dfmAng - ds) * 100, 6));
                ms = Math.Round(dfmAng * 10000 % 100, 4);
            }
            else
            {
                ds = Math.Floor(dfmAng);
                fs = Math.Floor(Math.Round((dfmAng - ds) * 100, 6));
                ms = Math.Round(dfmAng * 10000 % 100, 4);
            }
        }
    }
}
