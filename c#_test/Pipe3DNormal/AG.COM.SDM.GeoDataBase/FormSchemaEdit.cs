using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 表结构修改类
    /// </summary>
    public partial class FormSchemaEdit : Form
    {                                                                           
        private IObjectClass m_ObjectClass;                                     //要素类或要素表
        private IList<ListFieldItem> m_DelFields=new List<ListFieldItem>();     //要移除的字段集合
        private IList<ListFieldItem> m_AddFields = new List<ListFieldItem>();   //要添加的字段集合
        private IEnumDomain m_EnumDomain;                                       //工作空间中的属性域集
        private ListFieldItem m_CurrentFieldItem;                               //当前选择的字段项 

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pFeatureClass">IDataset对象</param>
        public FormSchemaEdit(IObjectClass pObjectClass)
        {
            //初始化界面组件
            InitializeComponent();

            this.m_ObjectClass = pObjectClass;
        }

        private void FormSchemaEdit_Load(object sender, EventArgs e)
        {
            this.txtTableName.Text = (this.m_ObjectClass as IDataset).Name;
            this.txtTableAlias.Text = this.m_ObjectClass.AliasName;

            //绑定表字段
            IFields tFields = this.m_ObjectClass.Fields;
            for (int i = 0; i < tFields.FieldCount; i++)
            {
                this.listFields.Items.Add(new ListFieldItem(tFields.get_Field(i)));
            }

            //获取所有属性域
            IDataset tDataset = this.m_ObjectClass as IDataset;
            IWorkspace tWorkspace = tDataset.Workspace;
            IWorkspaceDomains tWorkspaceDomains = tWorkspace as IWorkspaceDomains;

            if (tWorkspaceDomains != null)
            {
                this.m_EnumDomain = tWorkspaceDomains.Domains;
            }

            //绑定字段类型
            this.combFieldType.DataSource = Enum.GetNames(typeof(EnumFieldItems));
            //选择第一项
            if(this.listFields.Items.Count>0)
                this.listFields.SelectedIndex = 0;
        }      

        private void listFields_SelectedValueChanged(object sender, EventArgs e)
        {           
            ListFieldItem listField = this.listFields.SelectedItem as ListFieldItem;
            if (listField == null) return;

            //更新字段值
            UpdateCurrentField(listField);
            //设置控件状态的可用性
            SetControlEnable(listField);
            //设置面板可见性
            SetPanelVisible(listField.Field.Type);
            //设置控件显示文本
            SetControlText(listField);
        }   

        private void combFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //获取选择的字段类型项
            string strFieldType = this.combFieldType.SelectedItem as string;

            //类型之间进行转换
            EnumFieldItems tEnumFieldItems = (EnumFieldItems)Enum.Parse(typeof(EnumFieldItems), strFieldType);
            this.combFieldType.Tag = (esriFieldType)((int)tEnumFieldItems);       

            //获取选择的字段项
            ListFieldItem tListFieldItem = this.listFields.SelectedItem as ListFieldItem;
            if (tListFieldItem == null) return;

            if (tListFieldItem.IsNew == true && (tEnumFieldItems == EnumFieldItems.OID || tEnumFieldItems == EnumFieldItems.几何对象))
            {
                //新加字段类型不能为OID或几何对象类型,如果选择则自动设置为字符型
                this.combFieldType.SelectedItem = EnumFieldItems.字符型.ToString();
            }

            if (tListFieldItem.IsNew == true)
            {
                if (tEnumFieldItems == EnumFieldItems.双精度 || tEnumFieldItems == EnumFieldItems.单精度)
                {
                    this.lblLength.Text = "字段精度";

                    this.txtLength.Text = "10";
                    this.txtScale.Text = "3";
                    this.txtScale.Enabled = true;
                }
                else
                {
                    this.lblLength.Text = "字段长度";
                    this.txtLength.Text = "50";
                    this.txtScale.Text = "";
                    this.txtScale.Enabled = false;
                }
            }

            #region 设置属性域 
            //清除所有项
            this.combDomains.Items.Clear();

            if (this.m_EnumDomain != null)
            {
                //重设游标至起始位置
                this.m_EnumDomain.Reset();
                IField tField = tListFieldItem.Field;
                for (IDomain tDomin = m_EnumDomain.Next(); tDomin != null; tDomin = m_EnumDomain.Next())
                {
                    if (tDomin.FieldType == tField.Type)
                    {
                        ListDomainItem tlistDomainItem = new ListDomainItem(tDomin);
                        this.combDomains.Items.Add(tlistDomainItem);    
                    }
                }
            }

            #endregion
        }

        private void txtFieldName_Leave(object sender, EventArgs e)
        {
            if (this.m_CurrentFieldItem.IsNew == true)
            {
                if (string.Equals(this.m_CurrentFieldItem.Field.Name, this.txtFieldName.Text) == false)
                {
                    //查询IFieldEdit2接口
                    IFieldEdit2 tFieldEdit = this.m_CurrentFieldItem.Field as IFieldEdit2;
                    tFieldEdit.Name_2 = this.txtFieldName.Text;

                    //返回指定项在集合中的索引
                    int index = this.listFields.Items.IndexOf(this.m_CurrentFieldItem);
                    //如果存在指定索引项，则更新显示
                    if (index > -1) this.listFields.Items[index] = this.m_CurrentFieldItem;
                }
            }
        }

        private void txtDefault_Leave(object sender, EventArgs e)
        {               
            //默认值不为空的情况
            if (this.txtDefault.Text.Length > 0)
            {                
                try
                {
                    object tObjValue = null;
                    IField tField = this.m_CurrentFieldItem.Field;

                    if (tField.Type == esriFieldType.esriFieldTypeString)
                    {
                        tObjValue = Convert.ToString(this.txtDefault.Text);             //转换为字符串对象
                    }
                    else if (tField.Type == esriFieldType.esriFieldTypeDouble ||
                        this.m_CurrentFieldItem.Field.Type == esriFieldType.esriFieldTypeSingle)
                    {
                        tObjValue = Convert.ToDouble(this.txtDefault.Text);             //转换为双精度对象
                    }
                    else if (tField.Type == esriFieldType.esriFieldTypeDate)
                    {
                        tObjValue = Convert.ToDateTime(this.txtDefault.Text);           //转换为日期型对象
                    }
                    else
                    {
                        tObjValue = Convert.ToInt32(this.txtDefault.Text);              //转换为整型对象
                    }

                    this.txtDefault.Tag = tObjValue;
                }
                catch
                {
                    MessageBox.Show("默认值类型转换有误;");
                }
            }        
        }      

        private void txtLength_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox.Text.Length > 0)
            {
                try
                {
                    int i = Convert.ToInt32(txtBox.Text);
                }
                catch
                {
                    MessageBox.Show("请输入有效字段长度值或小数位数值");
                }
            }
        }

        private void txtTableAlias_TextChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
        }

        private void btnAddField_Click(object sender, EventArgs e)
        {
            //实例化ListFieldItem
            ListFieldItem tListField = new ListFieldItem();
            this.listFields.Items.Add(tListField);
            this.listFields.SelectedItem = tListField;

            //添加项到添加字段集合
            this.m_AddFields.Add(tListField);

            //设置应用按钮为可用状态
            this.btnApply.Enabled = true;
        }

        private void btnDelField_Click(object sender, EventArgs e)
        {
            //删除字段
            ListFieldItem tListFieldItem = this.listFields.SelectedItem as ListFieldItem;
            this.listFields.Items.Remove(tListFieldItem);

            if (tListFieldItem.IsNew == true)
            {
                //如果为新增字段则需添加集合项移除掉它
                this.m_AddFields.Remove(tListFieldItem);
            }
            else
            {
                //添加项到删除字段集合,标识为要移除掉的项
                this.m_DelFields.Add(tListFieldItem);
            }

            if (this.listFields.Items.Count > 0)
            {
                this.m_CurrentFieldItem = this.listFields.Items[0] as ListFieldItem;
                this.listFields.SelectedItem = this.m_CurrentFieldItem;
            }

            //设置应用按钮为可用状态
            this.btnApply.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //设置对象类为空
            this.m_ObjectClass = null;
            this.m_EnumDomain = null;
            this.m_AddFields = null;
            this.m_DelFields = null;
            this.m_CurrentFieldItem = null;
            //关闭窗体
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {                   
            if (this.btnApply.Enabled = true)
            {
                //调用[应用]方法
                btnApply_Click(null, null);
            }
            //关闭窗体
            btnClose_Click(null, null);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //如果当前表不能修改则返回
            if (CanEdit() == false)
            {
                MessageBox.Show("当前表已被锁定,不能对其进行修改操作!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;    //返回
            }

            try
            {
                //更新当前选择字段                 
                ListFieldItem tFieldItem = this.listFields.SelectedItem as ListFieldItem;

                if (tFieldItem != null)
                {
                    if (tFieldItem.IsNew == true)
                        UpdateNewField(tFieldItem);
                    else
                        UpdateOldField(tFieldItem);
                }    
      
          
                //添加字段
                for (int i = 0; i < this.m_AddFields.Count; i++)
                {
                    tFieldItem = this.m_AddFields[i];
                    if (tFieldItem.IsNew == true)
                    {
                        this.m_ObjectClass.AddField(tFieldItem.Field);
                        tFieldItem.IsNew = false;
                    }
                }

                //删除字段
                for (int i = 0; i < this.m_DelFields.Count; i++)
                {
                    tFieldItem = this.m_DelFields[i];
                    if (tFieldItem.IsNew == false)
                    {                   
                        this.m_ObjectClass.DeleteField(tFieldItem.Field);
                    }
                }

                //清空所有项
                this.m_DelFields.Clear();              
                this.m_AddFields.Clear();

                //查询IClassSchemaEdit接口引用
                IClassSchemaEdit tClassSchemaEdit = this.m_ObjectClass as IClassSchemaEdit;
                if (tClassSchemaEdit != null && string.Equals(this.m_ObjectClass.AliasName, this.txtTableAlias.Text) == false)
                {
                    tClassSchemaEdit.AlterAliasName(this.txtTableAlias.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.btnApply.Enabled = false;
            }
        }

        #region 私有定义方法
        /// <summary>
        /// 设置要显示的控件文件内容
        /// </summary>
        /// <param name="pListFieldItem">ListFieldItem项</param>
        private void SetControlText(ListFieldItem pListFieldItem)
        {
            IField tField = pListFieldItem.Field;

            this.txtFieldName.Text = tField.Name;
            this.txtFieldAlias.Text = tField.AliasName;
            this.combFieldType.SelectedItem = ((EnumFieldItems)((int)tField.Type)).ToString();
            this.txtDefault.Text =Convert.ToString( tField.DefaultValue);
            this.txtDefault.Tag = tField.DefaultValue;
            this.chkIsNull.Checked = tField.IsNullable;

            #region 设置属性域
            if (tField.Domain != null)
            {
                //设置属性域
                for (int index = 0; index < this.combDomains.Items.Count; index++)
                {
                    if (string.Compare(this.combDomains.Items[index].ToString(), tField.Domain.Description) == 0)
                    {
                        this.combDomains.SelectedIndex = index;
                        break;
                    }
                }
            }
            else
            {
                this.combDomains.SelectedItem = null;
            }
            #endregion


            if (tField.Type == esriFieldType.esriFieldTypeGeometry)
            {
                #region 设置Grid大小
                //如果字段类型为几何对象，则需要设置其Grid大小
                for (int i = 0; i < 3; i++)
                {
                    //实例化标签对象
                    Label tLabel = new Label();
                    tLabel.AutoSize = true;
                    tLabel.Text = string.Format("Grid {0}", i + 1);
                    tLabel.Left = this.lblGeoType.Left;
                    tLabel.Top = this.lblGeoType.Top + (i + 1) * 28;

                    //实例化文本框对象
                    TextBox txtBox = new TextBox();
                    txtBox.Enabled = false;
                    txtBox.Size = this.txtGeometryType.Size;
                    txtBox.Left = this.txtGeometryType.Left;
                    txtBox.Top = this.txtGeometryType.Top + (i + 1) * 27;
                    if (i < tField.GeometryDef.GridCount)
                        txtBox.Text = Convert.ToString(tField.GeometryDef.get_GridSize(i));
                    else
                        txtBox.Text = "0";

                    //添加Label标签
                    this.pnlGeoAttr.Controls.Add(tLabel);
                    //添加TextBox控件
                    this.pnlGeoAttr.Controls.Add(txtBox);
                }
                #endregion

                this.txtGeometryType.Text = GetGeoTypeString(tField.GeometryDef.GeometryType);
            }
            else
            {
                #region 设置字段长度及小数位
                if (tField.Type == esriFieldType.esriFieldTypeDouble || tField.Type == esriFieldType.esriFieldTypeSingle)
                {
                    this.lblLength.Text = "字段精度:";
                    this.txtLength.Text = Convert.ToString(tField.Precision);
                    this.txtScale.Text = Convert.ToString(tField.Scale);
                }
                else if (tField.Type == esriFieldType.esriFieldTypeInteger || tField.Type == esriFieldType.esriFieldTypeSmallInteger)
                {
                    this.lblLength.Text = "字段精度:";
                    this.txtLength.Text = Convert.ToString(tField.Length);
                    this.txtScale.Text = "";                    
                }
                else
                {
                    this.lblLength.Text = "字段长度:";
                    this.txtLength.Text = Convert.ToString(tField.Length);
                    this.txtScale.Text = "";
                }
                #endregion
            }
        }

        /// <summary>
        /// 设置控件状态的可用性
        /// </summary>
        /// <param name="bIsNew">是否为新增字段</param>
        private void SetControlEnable(ListFieldItem pListFieldItem)
        {
            if (pListFieldItem.Field.Type == esriFieldType.esriFieldTypeOID ||
                pListFieldItem.Field.Type == esriFieldType.esriFieldTypeRaster ||
                pListFieldItem.Field.Type == esriFieldType.esriFieldTypeGeometry ||
                pListFieldItem.Field.Type == esriFieldType.esriFieldTypeGUID ||
                pListFieldItem.Field.Type == esriFieldType.esriFieldTypeGlobalID)
            {
                //删除按钮不可用
                this.btnDelField.Enabled = false;
                this.txtDefault.Enabled = false;
                this.txtFieldAlias.Enabled = false;
            }
            else
            {
                this.btnDelField.Enabled = true;
                this.txtDefault.Enabled = true;
                this.txtFieldAlias.Enabled = true;
            }

            if (pListFieldItem.IsNew ==true && (pListFieldItem.Field.Type == esriFieldType.esriFieldTypeDouble 
                || pListFieldItem.Field.Type == esriFieldType.esriFieldTypeSingle))
            {
                this.txtScale.Enabled = true;
            }
            else
            {
                this.txtScale.Enabled = false;
            }

            //是否为新增字段
            bool bIsNew = pListFieldItem.IsNew;
            this.txtFieldName.Enabled = bIsNew;
            this.combFieldType.Enabled = bIsNew;
            this.chkIsNull.Enabled = bIsNew;
            this.txtLength.Enabled = bIsNew;
          
        }

        /// <summary>
        /// 设置面板的可见性
        /// </summary>
        /// <param name="enumFieldType">字段类型</param>
        private void SetPanelVisible(esriFieldType enumFieldType)
        {
            //置为底层
            this.pnlCommon.SendToBack();

            if (enumFieldType == esriFieldType.esriFieldTypeGeometry)
            {
                this.pnlGeoAttr.Visible = true;
                this.pnlAtrr.Visible = false;
            }
            else if(enumFieldType==esriFieldType.esriFieldTypeOID)
            {
                this.pnlAtrr.Visible=false;
                this.pnlGeoAttr.Visible=false;
            }
            else 
            {
                this.pnlGeoAttr.Visible = false;
                this.pnlAtrr.Visible = true;
            } 
        }

        /// <summary>
        /// 更新字段值
        /// </summary>
        /// <param name="pListFieldItem">下拉列表字段项</param>
        private void UpdateCurrentField(ListFieldItem pListFieldItem)
        {
            if (pListFieldItem != this.m_CurrentFieldItem && this.m_CurrentFieldItem != null)
            {
                if (this.m_CurrentFieldItem.IsNew == true)
                {
                    //更新新建的字段对象
                    UpdateNewField(this.m_CurrentFieldItem);
                }
                else
                {
                    //更新原有字段对象
                    UpdateOldField(this.m_CurrentFieldItem);
                }
            }

            //改变当前字段项
            this.m_CurrentFieldItem = pListFieldItem;
        }

        /// <summary>
        /// 更新新添加的字段
        /// </summary>
        /// <param name="pListFieldItem">下拉列表字段项</param>
        public void UpdateNewField(ListFieldItem pListFieldItem)
        {
            //查询引用接口
            IFieldEdit2 tFieldEdit = pListFieldItem.Field as IFieldEdit2;
            tFieldEdit.Name_2 = this.txtFieldName.Text;
            tFieldEdit.AliasName_2 = this.txtFieldAlias.Text;
            tFieldEdit.Editable_2 = true;
            tFieldEdit.IsNullable_2 = this.chkIsNull.Checked;
            tFieldEdit.Type_2 = (esriFieldType)this.combFieldType.Tag;
        
            if (this.txtDefault.Tag != null)
            {
                //设置默认值
                tFieldEdit.DefaultValue_2 = this.txtDefault.Tag;
            }
                        
            if (this.combDomains.SelectedItem != null)
            {
                //设置属性域
                tFieldEdit.Domain_2 = (this.combDomains.SelectedItem as ListDomainItem).Domain;
            }

            try
            {
                if (tFieldEdit.Type == esriFieldType.esriFieldTypeString)
                {
                    //设置字段长度
                    tFieldEdit.Length_2 = Convert.ToInt32(this.txtLength.Text);
                }
                else if (tFieldEdit.Type == esriFieldType.esriFieldTypeSingle ||
                    tFieldEdit.Type == esriFieldType.esriFieldTypeDouble ||
                    tFieldEdit.Type == esriFieldType.esriFieldTypeInteger ||
                    tFieldEdit.Type == esriFieldType.esriFieldTypeSmallInteger)
                {
                    //设置精度值勤
                    tFieldEdit.Precision_2 = Convert.ToInt32(this.txtLength.Text);

                    if (tFieldEdit.Type == esriFieldType.esriFieldTypeDouble ||
                        tFieldEdit.Type == esriFieldType.esriFieldTypeSingle)
                    {             
                        //设置小数位数
                        tFieldEdit.Scale_2 = Convert.ToInt32(this.txtScale.Text);
                    }
                }
            }
            catch
            {
                MessageBox.Show("请输入有效的字段长度或小数位数值");
            }  
        } 

        /// <summary>
        /// 更新表中的原有字段
        /// </summary>
        /// <param name="pListFieldItem">下拉列表字段项</param>
        public void UpdateOldField(ListFieldItem pListFieldItem)
        {
            //如果当前表不能修改则返回
            if (CanEdit() == false) return;

            //修改字段别名
            IClassSchemaEdit tClassSchemaEdit = this.m_ObjectClass as IClassSchemaEdit;
            if (tClassSchemaEdit != null)
            {
                IField tField = pListFieldItem.Field;        

                if (string.Compare(this.txtFieldAlias.Text, tField.AliasName) != 0)
                {        
                    //修改字段别名
                    tClassSchemaEdit.AlterFieldAliasName(tField.Name, this.txtFieldAlias.Text);
                }

                if (tField.DefaultValue.ToString() != Convert.ToString(this.txtDefault.Tag))
                {      
                    //修改默认值
                    tClassSchemaEdit.AlterDefaultValue(tField.Name, this.txtDefault.Tag);                   
                }

                if (tField.DomainFixed == false)
                {
                    //获取当前选择的属性域列表项
                    ListDomainItem tListDomainItem = this.combDomains.SelectedItem as ListDomainItem;
                    if (tListDomainItem != null)
                    {
                        //获取字段属性域名称
                        string strDomainName = "";
                        if (tField.Domain != null)
                            strDomainName = tField.Domain.Name;

                        if (string.Compare(strDomainName, tListDomainItem.Domain.Name) != 0)
                        {
                            //修改属性域
                            tClassSchemaEdit.AlterDomain(tField.Name, tListDomainItem.Domain);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取几何对象类型的名称
        /// </summary>
        /// <param name="pGeometryType">空间对象类型</param>
        /// <returns>返回对应的名称</returns>
        private string GetGeoTypeString(esriGeometryType pGeometryType)
        {
            if (pGeometryType == esriGeometryType.esriGeometryMultipoint || pGeometryType == esriGeometryType.esriGeometryPoint)
            {
                return "点";
            }
            else if(pGeometryType==esriGeometryType.esriGeometryPolyline || pGeometryType==esriGeometryType.esriGeometryLine)
            {             
                return "线";
            }
            else if (pGeometryType == esriGeometryType.esriGeometryPolygon)
            {
                return "面";
            }
            else
            {
                return pGeometryType.ToString();
            }
        }

        /// <summary>
        /// 返回当前表是否能够进行编辑操作
        /// </summary>
        /// <returns>如果能够编辑则为 true,否则为 false</returns>
        private bool CanEdit()
        {
            //查询引用接口
            ISchemaLock tSchemaLock = this.m_ObjectClass as ISchemaLock;
            if (tSchemaLock != null)
            {
                IEnumSchemaLockInfo tEnumSchemaLockInfo;
                //得到当前表的锁定信息（枚举）
                tSchemaLock.GetCurrentSchemaLocks(out tEnumSchemaLockInfo);
                ISchemaLockInfo tSchemaLockInfo = tEnumSchemaLockInfo.Next();
                //如果锁定信息为独占锁则返回 false
                if (tSchemaLockInfo != null && tSchemaLockInfo.SchemaLockType == esriSchemaLock.esriExclusiveSchemaLock) return false;
                
            }
  
            //如果m_ObjectClass源对象为文件类型
            //或者被锁定,则返回true
            return true;
        }

        #endregion
    }
}