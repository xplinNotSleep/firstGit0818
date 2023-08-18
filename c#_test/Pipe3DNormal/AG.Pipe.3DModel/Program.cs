using AG.COM.MapSoft.LicenseManager;
using AG.COM.MapSoft.Register;
using AG.COM.MapSoft.Tool;
using AG.COM.SDM.Utility.Logger;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;
using System;
using System.Windows.Forms;

namespace AG.Pipe.Analyst3DModel
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                #region
                //string ProcessMessage = "";
                //bool DogUse = true;
                //string softName = LimitDateHelper.SoftwareName;
                ////检查软件狗
                //DogUse = CheckSoftDog.CheckDog(softName, ref ProcessMessage, true);

                //if(DogUse)
                //{
                //    SplashScreen.Show(typeof(SplashForm));
                //    Application.EnableVisualStyles();
                //    Application.SetCompatibleTextRenderingDefault(false);

                //    if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine))
                //    {
                //        if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop))
                //        {
                //            MessageBox.Show("未检测到ArcGIS运行环境（Engine/Desktop）");
                //            return;
                //        }
                //    }
                //    InitializeEngineLicense();
                //    SplashScreen.Close();
                //    //LimitDateHelper.GetLicenseTimeFromKey(softName);
                //    Application.Run(new Create3DForm());
                //}
                //else
                //{
                //    System.Windows.Forms.MessageBox.Show(ProcessMessage,
                //                                       "Exception",
                //                                        System.Windows.Forms.MessageBoxButtons.OK);
                //}
                #endregion

                SplashScreen.Show(typeof(SplashForm));
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine))
                {
                    if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop))
                    {
                        MessageBox.Show("未检测到ArcGIS运行环境（Engine/Desktop）");
                        return;
                    }
                }
                InitializeEngineLicense();
                SplashScreen.Close();
                //LimitDateHelper.GetLicenseTimeFromKey(softName);
                Application.Run(new Create3DForm());

            }
            catch (Exception ex)
            {
                MapSoftLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }

        }
        private static void InitializeEngineLicense()
        {
            RuntimeManager.Bind(ProductCode.EngineOrDesktop);
            AoInitialize aoi = new AoInitializeClass();

            esriLicenseExtensionCode extensionCodes = esriLicenseExtensionCode.esriLicenseExtensionCodeSpatialAnalyst; //这是解决的办法
            esriLicenseExtensionCode extensionCode = esriLicenseExtensionCode.esriLicenseExtensionCode3DAnalyst;
            esriLicenseProductCode pro = esriLicenseProductCode.esriLicenseProductCodeEngine;
            if (aoi.IsProductCodeAvailable(pro) == esriLicenseStatus.esriLicenseAvailable &&
                aoi.IsExtensionCodeAvailable(pro, extensionCode) == esriLicenseStatus.esriLicenseAvailable &&
                aoi.IsExtensionCodeAvailable(pro, extensionCodes) == esriLicenseStatus.esriLicenseAvailable
                 )
            {
                aoi.Initialize(pro);
                aoi.CheckOutExtension(extensionCode);
                aoi.CheckOutExtension(extensionCodes);
            }
        }

    }

    /// <summary>
    /// 软件狗版本检测
    /// </summary>
    public class CheckSoftDog
    {
        /// <summary>
        /// 检查注册或者狗
        /// </summary>
        /// <param name="softName">软件名称 如“AGSPIPE”</param>
        /// <param name="ProcessMessage"></param>
        /// <returns></returns>
        public static bool CheckDog(string regName, ref string ProcessMessage, bool IsNoDog)
        {
            if(IsNoDog)
            {
                return true; //这样写暂时运行起来
            }
            else
            {
                LimitDateHelper.SoftwareName = regName;//软件注册表名称就是许可名称
                if (LimitDateHelper.IsSoftwareValid() == false)
                {
                    ProcessMessage = "获得许可失败!";
                    return false;
                }
                else
                {
                    MessageBox.Show("许可注册成功!");
                    return true;
                }
                

            }


        }
    }

    #region
    //public static class StopwatchHelper
    //{
    //    public static void ExeuteEx(this Action action, string actionname)
    //    {
    //        LogHelper.Info($"{actionname}方法开始");
    //        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
    //        sw.Start();
    //        action.Invoke();
    //        sw.Stop();
    //        LogHelper.Info($"{actionname}方法耗时:{sw.ElapsedMilliseconds}");
    //    }
    //}
    #endregion

}
