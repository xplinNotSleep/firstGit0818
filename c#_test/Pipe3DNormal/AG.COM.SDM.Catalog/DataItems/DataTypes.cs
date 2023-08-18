using System;

namespace AG.COM.SDM.Catalog.DataItems
{
    /// <summary>
    /// 数据类型 的摘要说明。
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 磁盘
        /// </summary>
        dtDisk,
        /// <summary>
        /// 文件夹
        /// </summary>
        dtFolder,
        /// <summary>
        /// Shape文件
        /// </summary>
        dtShapeFile,
        /// <summary>
        /// 错误Shape文件
        /// </summary>
        dtErrorShapeFile,
        /// <summary>
        /// CAD整图
        /// </summary>
        dtCadDrawing,
        /// <summary>
        /// CAD数据集
        /// </summary>
        dtCadDrawingDataset,
        /// <summary>
        /// LayerFile
        /// </summary>
        dtLayerFile,
        /// <summary>
        /// CAD点状数据集
        /// </summary>
        dtCadPoint,
        /// <summary>
        /// CAD面状数据集
        /// </summary>
        dtCadPolygon,
        /// <summary>
        /// CAD多线数据集
        /// </summary>
        dtCadPolyline,
        /// <summary>
        /// CAD切片数据集
        /// </summary>
        dtCadMultiPatch,
        /// <summary>
        /// CAD注记数据集
        /// </summary>
        dtCadAnno,
        /// <summary>
        /// 个人GDB
        /// </summary>
        dtAccess,
        /// <summary>
        /// 文件型GDB
        /// </summary>
        dtFileGdb,
        /// <summary>
        /// SDE连接
        /// </summary>
        dtSdeConnection,
        /// <summary>
        /// 矢量数据集
        /// </summary>
        dtFeatureDataset,
        /// <summary>
        /// 矢量要素类
        /// </summary>
        dtFeatureClass,
        /// <summary>
        /// 栅格数据集
        /// </summary>
        dtRasterDataset,
        /// <summary>
        /// 栅格数据目录
        /// </summary>
        dtRasterCatalog,
        /// <summary>
        /// 拓扑
        /// </summary>
        dtTopology,
        /// <summary>
        /// 网络
        /// </summary>
        dtNetwork,
        /// <summary>
        /// 属性表
        /// </summary>
        dtTable,
        /// <summary>
        /// Excel文件
        /// </summary>
        dtExcel,
        /// <summary>
        /// Converage文件
        /// </summary>
        dtConverage,
        /// <summary>
        /// Image文件
        /// </summary>
        dtImageFile,
        /// <summary>
        /// Tin文件
        /// </summary>
        dtTin,
        /// <summary>
        /// 点要素
        /// </summary>
        dtPointFeatureClass,
        /// <summary>
        /// 线要素
        /// </summary>
        dtLineFeatureClass,
        /// <summary>
        /// 面要素
        /// </summary>
        dtAreaFeatureClass,
        /// <summary>
        /// 注记要素
        /// </summary>
        dtAnnoFeatureClass,
        /// <summary>
        /// IMS服务
        /// </summary>
        dtImsService,
        /// <summary>
        /// 添加DataBase连接
        /// </summary>
        dtAddDatabaseConnection,
        /// <summary>
        /// 添加IMS
        /// </summary>
        dtAddIms,
        /// <summary>
        /// 历史位置
        /// </summary>
        dtHisLocation,
        /// <summary>
        /// 添加AGS连接
        /// </summary>
        dtAddAgsConnection,
        /// <summary>
        /// AGS连接
        /// </summary>
        dtAgsConnection,
        /// <summary>
        /// AGS服务
        /// </summary>
        dtAgsService,
        /// <summary>
        /// 未知数据类型
        /// </summary>
        dtUnknown
    }

    /// <summary>
    /// 数据类型转换类
    /// </summary>
    public class DataTypeParser
    {
        private static System.Collections.Hashtable m_Dict;
        /// <summary>
        /// 获取枚举数据类型的字典表
        /// </summary>
        /// <returns>返回枚举数据类型的字典表</returns>
        private static System.Collections.Hashtable GetDict()
        {
            if (m_Dict == null)
            {
                m_Dict = new System.Collections.Hashtable();
                m_Dict.Add(DataType.dtAccess, "个人空间数据库");
                m_Dict.Add(DataType.dtFileGdb, "文件数据库");
                m_Dict.Add(DataType.dtCadDrawing, "CAD整图");
                m_Dict.Add(DataType.dtCadDrawingDataset, "CAD数据集");
                m_Dict.Add(DataType.dtLayerFile, "图层文件");
                m_Dict.Add(DataType.dtCadAnno, "CAD注记");
                m_Dict.Add(DataType.dtCadPoint, "CAD点");
                m_Dict.Add(DataType.dtCadPolygon, "CAD多边形");
                m_Dict.Add(DataType.dtCadPolyline, "CAD多线");
                m_Dict.Add(DataType.dtCadMultiPatch, "CAD切片");
                m_Dict.Add(DataType.dtConverage, "Coverage文件");
                m_Dict.Add(DataType.dtFeatureDataset, "数据集");
                m_Dict.Add(DataType.dtFeatureClass, "要素类");
                m_Dict.Add(DataType.dtPointFeatureClass, "点层");
                m_Dict.Add(DataType.dtLineFeatureClass, "线层");
                m_Dict.Add(DataType.dtAreaFeatureClass, "面层");
                m_Dict.Add(DataType.dtAnnoFeatureClass, "注记层");
                m_Dict.Add(DataType.dtFolder, "文件夹");
                m_Dict.Add(DataType.dtImageFile, "栅格文件");
                m_Dict.Add(DataType.dtNetwork, "网络");
                m_Dict.Add(DataType.dtRasterCatalog, "栅格Catalog");
                m_Dict.Add(DataType.dtRasterDataset, "栅格数据集");
                m_Dict.Add(DataType.dtShapeFile, "Shape文件");
                m_Dict.Add(DataType.dtErrorShapeFile, "错误的Shape文件");
                m_Dict.Add(DataType.dtTable, "数据表");
                m_Dict.Add(DataType.dtExcel, "excel文件");
                m_Dict.Add(DataType.dtTin, "Tin");
                m_Dict.Add(DataType.dtTopology, "拓扑关系");
                m_Dict.Add(DataType.dtSdeConnection, "数据库连接");
                m_Dict.Add(DataType.dtDisk, "逻辑磁盘");
                m_Dict.Add(DataType.dtImsService, "ArcIMS地图服务");
                m_Dict.Add(DataType.dtAddDatabaseConnection, "添加空间数据库连接");
                m_Dict.Add(DataType.dtAddIms, "添加ArcIMS地图服务");
                m_Dict.Add(DataType.dtHisLocation, "浏览历史位置");
                m_Dict.Add(DataType.dtAddAgsConnection, "添加ArcGIS Server地图服务");
                m_Dict.Add(DataType.dtAgsConnection, "ArcGIS Server连接");
                m_Dict.Add(DataType.dtAgsService, "ArcGIS Server地图服务");


                m_Dict.Add(DataType.dtUnknown, "未知");
            }

            return m_Dict;
        }

        /// <summary>
        /// 获取指定枚举的数据类型的名称
        /// </summary>
        /// <param name="type">数据类型枚举</param>
        /// <returns>返回数据类型名称</returns>
        public static string GetDataTypeName(DataType type)
        {
            return GetDict()[type].ToString();
        }
    }
}