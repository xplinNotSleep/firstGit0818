using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// 地图服务帮助类
    /// </summary>
    public class MapServiceHelper
    {
        /// <summary>
        /// 获取ServerObjectNames
        /// </summary>
        /// <param name="tUrlOrHost"></param>
        /// <param name="tUserName"></param>
        /// <param name="tPassword"></param>
        /// <param name="tIsLAN"></param>
        /// <returns></returns>
        public static IAGSEnumServerObjectName GetServerObjectNames(string tUrlOrHost, string tUserName, string tPassword, bool tIsLAN)
        {
            List<string> result = new List<string>();

            if (!string.IsNullOrEmpty(tUrlOrHost))
            {
                //设置连接属性
                IPropertySet tPropertySet = new PropertySetClass();
                if (tIsLAN)
                    tPropertySet.SetProperty("machine", tUrlOrHost);
                else
                {
                    tPropertySet.SetProperty("url", tUrlOrHost);
                    tPropertySet.SetProperty("user", tUserName == null ? "" : tUserName);
                    tPropertySet.SetProperty("password", tPassword == null ? "" : tPassword);
                }

                IAGSServerConnectionFactory tFactory = new AGSServerConnectionFactory();
                IAGSServerConnection tConnection = tFactory.Open(tPropertySet, 0);

                return tConnection.ServerObjectNames;
            }
            return null;
        }

        /// <summary>
        /// 获取一个GIS服务器的所有MapServer
        /// </summary>
        /// <param name="tUrlOrHost"></param>
        /// <param name="tUserName"></param>
        /// <param name="tPassword"></param>
        /// <param name="tIsLAN"></param>
        /// <returns></returns>
        public static List<string> GetAllMapServerNames(string tUrlOrHost, string tUserName, string tPassword, bool tIsLAN)
        {
            List<string> result = new List<string>();

            IAGSEnumServerObjectName pServerObjectNames = GetServerObjectNames(tUrlOrHost, tUserName, tPassword, tIsLAN);
            if (pServerObjectNames != null)
            {
                pServerObjectNames.Reset();
                IAGSServerObjectName ServerObjectName = pServerObjectNames.Next();
                while (ServerObjectName != null)
                {
                    if (ServerObjectName.Type == "MapServer")
                    {
                        result.Add(ServerObjectName.Name);
                    }
                    ServerObjectName = pServerObjectNames.Next();
                }
            }

            return result;
        }

        /// <summary>
        /// 获取地图服务图层
        /// </summary>
        /// <param name="tMapServiceName"></param>
        /// <param name="tUrlOrHost"></param>
        /// <param name="tUserName"></param>
        /// <param name="tPassword"></param>
        /// <param name="tIsLAN"></param>
        /// <returns></returns>
        public static IMapServerLayer GetMapServerLayer(string tMapServiceName, string tUrlOrHost, string tUserName, string tPassword, bool tIsLAN)
        {
            IAGSEnumServerObjectName pServerObjectNames = GetServerObjectNames(tUrlOrHost, tUserName, tPassword, tIsLAN);
            if (pServerObjectNames != null)
            {
                IAGSServerObjectName tServerObjectName = null;

                pServerObjectNames.Reset();
                tServerObjectName = pServerObjectNames.Next();
                while (tServerObjectName != null)
                {
                    if (tServerObjectName.Type == "MapServer" && tServerObjectName.Name == tMapServiceName)
                    {
                        break;
                    }
                    tServerObjectName = pServerObjectNames.Next();
                }
                if (tServerObjectName != null)
                {
                    IName pName = (IName)tServerObjectName;
                    //访问地图服务
                    IAGSServerObject tServerObject = (IAGSServerObject)pName.Open();
                    IMapServer tMapServer = (IMapServer)tServerObject;

                    IMapServerLayer tMapServerLayer = new MapServerLayerClass();
                    //连接地图服务

                    tMapServerLayer.ServerConnect(tServerObjectName, tMapServer.DefaultMapName);

                    return tMapServerLayer;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取地图服务图层的信息
        /// </summary>
        /// <param name="tMapServerLayer"></param>
        /// <param name="mapName"></param>
        /// <param name="url"></param>
        /// <param name="isLocal"></param>
        public static void GetMapServerLayerInfo(IMapServerLayer tMapServerLayer, out string mapName
            , out string url, out bool isLocal)
        {
            mapName = ""; url = ""; isLocal = false;

            if (tMapServerLayer == null)
            {
                return;
            }

            IAGSServerObjectName tAGSServerObjectName;
            string docLocation;
            tMapServerLayer.GetConnectionInfo(out tAGSServerObjectName, out docLocation, out mapName);
            IPropertySet tPropertySet = tAGSServerObjectName.AGSServerConnectionName.ConnectionProperties;
            object objkeys;
            object objvalues;
            tPropertySet.GetAllProperties(out objkeys, out objvalues);
            string[] keys = objkeys as string[];
            object[] values = objvalues as object[];
            mapName = tAGSServerObjectName.Name;

            //MACHINE                             
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].ToUpper() == "URL" && values[i] != null)
                {
                    isLocal = false;
                    url = values[i].ToString();
                    break;
                }
                else if (keys[i].ToUpper() == "MACHINE" && values[i] != null)
                {
                    isLocal = true;
                    url = values[i].ToString();
                    break;
                }
            }
        }
    }
}
