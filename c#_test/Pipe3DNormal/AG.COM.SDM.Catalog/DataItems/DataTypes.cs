using System;

namespace AG.COM.SDM.Catalog.DataItems
{
    /// <summary>
    /// �������� ��ժҪ˵����
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// ����
        /// </summary>
        dtDisk,
        /// <summary>
        /// �ļ���
        /// </summary>
        dtFolder,
        /// <summary>
        /// Shape�ļ�
        /// </summary>
        dtShapeFile,
        /// <summary>
        /// ����Shape�ļ�
        /// </summary>
        dtErrorShapeFile,
        /// <summary>
        /// CAD��ͼ
        /// </summary>
        dtCadDrawing,
        /// <summary>
        /// CAD���ݼ�
        /// </summary>
        dtCadDrawingDataset,
        /// <summary>
        /// LayerFile
        /// </summary>
        dtLayerFile,
        /// <summary>
        /// CAD��״���ݼ�
        /// </summary>
        dtCadPoint,
        /// <summary>
        /// CAD��״���ݼ�
        /// </summary>
        dtCadPolygon,
        /// <summary>
        /// CAD�������ݼ�
        /// </summary>
        dtCadPolyline,
        /// <summary>
        /// CAD��Ƭ���ݼ�
        /// </summary>
        dtCadMultiPatch,
        /// <summary>
        /// CADע�����ݼ�
        /// </summary>
        dtCadAnno,
        /// <summary>
        /// ����GDB
        /// </summary>
        dtAccess,
        /// <summary>
        /// �ļ���GDB
        /// </summary>
        dtFileGdb,
        /// <summary>
        /// SDE����
        /// </summary>
        dtSdeConnection,
        /// <summary>
        /// ʸ�����ݼ�
        /// </summary>
        dtFeatureDataset,
        /// <summary>
        /// ʸ��Ҫ����
        /// </summary>
        dtFeatureClass,
        /// <summary>
        /// դ�����ݼ�
        /// </summary>
        dtRasterDataset,
        /// <summary>
        /// դ������Ŀ¼
        /// </summary>
        dtRasterCatalog,
        /// <summary>
        /// ����
        /// </summary>
        dtTopology,
        /// <summary>
        /// ����
        /// </summary>
        dtNetwork,
        /// <summary>
        /// ���Ա�
        /// </summary>
        dtTable,
        /// <summary>
        /// Excel�ļ�
        /// </summary>
        dtExcel,
        /// <summary>
        /// Converage�ļ�
        /// </summary>
        dtConverage,
        /// <summary>
        /// Image�ļ�
        /// </summary>
        dtImageFile,
        /// <summary>
        /// Tin�ļ�
        /// </summary>
        dtTin,
        /// <summary>
        /// ��Ҫ��
        /// </summary>
        dtPointFeatureClass,
        /// <summary>
        /// ��Ҫ��
        /// </summary>
        dtLineFeatureClass,
        /// <summary>
        /// ��Ҫ��
        /// </summary>
        dtAreaFeatureClass,
        /// <summary>
        /// ע��Ҫ��
        /// </summary>
        dtAnnoFeatureClass,
        /// <summary>
        /// IMS����
        /// </summary>
        dtImsService,
        /// <summary>
        /// ���DataBase����
        /// </summary>
        dtAddDatabaseConnection,
        /// <summary>
        /// ���IMS
        /// </summary>
        dtAddIms,
        /// <summary>
        /// ��ʷλ��
        /// </summary>
        dtHisLocation,
        /// <summary>
        /// ���AGS����
        /// </summary>
        dtAddAgsConnection,
        /// <summary>
        /// AGS����
        /// </summary>
        dtAgsConnection,
        /// <summary>
        /// AGS����
        /// </summary>
        dtAgsService,
        /// <summary>
        /// δ֪��������
        /// </summary>
        dtUnknown
    }

    /// <summary>
    /// ��������ת����
    /// </summary>
    public class DataTypeParser
    {
        private static System.Collections.Hashtable m_Dict;
        /// <summary>
        /// ��ȡö���������͵��ֵ��
        /// </summary>
        /// <returns>����ö���������͵��ֵ��</returns>
        private static System.Collections.Hashtable GetDict()
        {
            if (m_Dict == null)
            {
                m_Dict = new System.Collections.Hashtable();
                m_Dict.Add(DataType.dtAccess, "���˿ռ����ݿ�");
                m_Dict.Add(DataType.dtFileGdb, "�ļ����ݿ�");
                m_Dict.Add(DataType.dtCadDrawing, "CAD��ͼ");
                m_Dict.Add(DataType.dtCadDrawingDataset, "CAD���ݼ�");
                m_Dict.Add(DataType.dtLayerFile, "ͼ���ļ�");
                m_Dict.Add(DataType.dtCadAnno, "CADע��");
                m_Dict.Add(DataType.dtCadPoint, "CAD��");
                m_Dict.Add(DataType.dtCadPolygon, "CAD�����");
                m_Dict.Add(DataType.dtCadPolyline, "CAD����");
                m_Dict.Add(DataType.dtCadMultiPatch, "CAD��Ƭ");
                m_Dict.Add(DataType.dtConverage, "Coverage�ļ�");
                m_Dict.Add(DataType.dtFeatureDataset, "���ݼ�");
                m_Dict.Add(DataType.dtFeatureClass, "Ҫ����");
                m_Dict.Add(DataType.dtPointFeatureClass, "���");
                m_Dict.Add(DataType.dtLineFeatureClass, "�߲�");
                m_Dict.Add(DataType.dtAreaFeatureClass, "���");
                m_Dict.Add(DataType.dtAnnoFeatureClass, "ע�ǲ�");
                m_Dict.Add(DataType.dtFolder, "�ļ���");
                m_Dict.Add(DataType.dtImageFile, "դ���ļ�");
                m_Dict.Add(DataType.dtNetwork, "����");
                m_Dict.Add(DataType.dtRasterCatalog, "դ��Catalog");
                m_Dict.Add(DataType.dtRasterDataset, "դ�����ݼ�");
                m_Dict.Add(DataType.dtShapeFile, "Shape�ļ�");
                m_Dict.Add(DataType.dtErrorShapeFile, "�����Shape�ļ�");
                m_Dict.Add(DataType.dtTable, "���ݱ�");
                m_Dict.Add(DataType.dtExcel, "excel�ļ�");
                m_Dict.Add(DataType.dtTin, "Tin");
                m_Dict.Add(DataType.dtTopology, "���˹�ϵ");
                m_Dict.Add(DataType.dtSdeConnection, "���ݿ�����");
                m_Dict.Add(DataType.dtDisk, "�߼�����");
                m_Dict.Add(DataType.dtImsService, "ArcIMS��ͼ����");
                m_Dict.Add(DataType.dtAddDatabaseConnection, "��ӿռ����ݿ�����");
                m_Dict.Add(DataType.dtAddIms, "���ArcIMS��ͼ����");
                m_Dict.Add(DataType.dtHisLocation, "�����ʷλ��");
                m_Dict.Add(DataType.dtAddAgsConnection, "���ArcGIS Server��ͼ����");
                m_Dict.Add(DataType.dtAgsConnection, "ArcGIS Server����");
                m_Dict.Add(DataType.dtAgsService, "ArcGIS Server��ͼ����");


                m_Dict.Add(DataType.dtUnknown, "δ֪");
            }

            return m_Dict;
        }

        /// <summary>
        /// ��ȡָ��ö�ٵ��������͵�����
        /// </summary>
        /// <param name="type">��������ö��</param>
        /// <returns>����������������</returns>
        public static string GetDataTypeName(DataType type)
        {
            return GetDict()[type].ToString();
        }
    }
}