﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.Control;
using POS.BL.Utilities;
using Core.Standards.Exceptions;
using POS.BL;
using POS.BL.Entities.Entity;
using Core.Standards.Converters;
using Core.Standards.Validations;

namespace POS.IN.SetupMaterial
{
    public partial class ucSetupMaterial : BaseUserControl
    {
        TabPage tabPageAddEdit = new TabPage();
        string DataKeyName = "ID";
        AddEditSetupMaterial addEditSetupMaterial = new AddEditSetupMaterial();
        string programName = ProgramName.SetupINMaterial;
        string tabName = "List Material";

        public ucSetupMaterial()
        {
            InitializeComponent();

            this.Dock = DockStyle.Fill;
            tabPage1.Text = tabName;

            grdBase.onAddNewRow += new EventHandler(grdBase_onAddNewRow);
            grdBase.onSelectedDataRow += new EventHandler<Control.GridView.RowEventArgs>(grdBase_onSelectedDataRow);
            grdBase.onDeleteDataRows += new EventHandler<Control.GridView.RowsEventArgs>(grdBase_onDeleteDataRows);
            grdBase.onLoadDataGrid += new EventHandler<Control.GridView.DataBindArgs>(grdBase_onLoadDataGrid);
            grdBase.onCellFormatting += new EventHandler<DataGridViewCellFormattingEventArgs>(grdBase_onCellFormatting);

            grdBase.LoadData();
        }

        #region :: Event Gridview ::
        public void grdBase_onLoadDataGrid(object sender, POS.Control.GridView.DataBindArgs e)
        {
            Material entity = new Material();
            entity.material_code = txtMaterialCode.Text;
            entity.material_name = txtMaterialName.Text;
            grdBase.DataSourceDataSet = ServiceProvider.MaterialService.GetGridMaterial(entity);
            grdBase.DataKeyName = new string[] { DataKeyName };
        }
        public void grdBase_onAddNewRow(object sender, EventArgs e)
        {
            addEditSetupMaterial = new AddEditSetupMaterial();
            this.AddEditTab(string.Format(TabName.Add, programName), addEditSetupMaterial);
        }
        public void grdBase_onSelectedDataRow(object sender, Control.GridView.RowEventArgs e)
        {
            Dictionary<string, object> dataKey = (Dictionary<string, object>)sender;
            addEditSetupMaterial = new AddEditSetupMaterial(dataKey[DataKeyName].ToString());
            this.AddEditTab(string.Format(TabName.Edit, programName), addEditSetupMaterial);
        }
        public void grdBase_onDeleteDataRows(object sender, Control.GridView.RowsEventArgs e)
        {
            try
            {
                List<Dictionary<string, object>> list = (List<Dictionary<string, object>>)sender;
                List<Material> listMat = new List<Material>();
                foreach (Dictionary<string, object> item in list)
                {
                    listMat.Add(new Material() { material_id = Converts.ParseLong(item[DataKeyName].ToString()) });
                }
                ServiceProvider.MaterialService.Delete(listMat, new string[] { ValidationRuleset.Delete });
            }
            catch (ValidationException ex)
            {
                base.formBase.ShowErrorMessage(ex);
            }
        }
        public void grdBase_onCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //object grd = sender;
            //object val = e.Value;
            ////if (e.ColumnIndex == 4 || e.ColumnIndex == 6 || e.ColumnIndex == 7)
            ////{
            ////    e.CellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            ////}
            ////else
            ////{
            ////    e.CellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            ////}
            ////if (e.ColumnIndex == 2)
            ////{
            ////    e.CellStyle.WrapMode = DataGridViewTriState.True;
            ////}
            ////if (e.ColumnIndex == 4 || e.ColumnIndex == 6 || e.ColumnIndex == 7)
            ////{
            ////    e.CellStyle.Format = FormatString.FormatDate;
            ////}
            //e.CellStyle.WrapMode = DataGridViewTriState.True;

            //Type dataType = val.GetType();

            //if (e.ColumnIndex == 2)
            //{
            //    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //}
            //else
            //{
            //    if (typeof(int) == dataType || typeof(decimal) == dataType || typeof(float) == dataType || typeof(long) == dataType || typeof(double) == dataType)
            //    {
            //        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //    }
            //    else if (typeof(DateTime) == dataType)
            //    {
            //        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //        e.CellStyle.Format = FormatString.FormatDate;
            //    }
            //    else
            //    {
            //        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //    }
            //}
        }

        #endregion

        protected void NotifyReturnEvent(object param)
        {
            if (param != null)
            {
                ControlMode controlMode = (ControlMode)param;
                if (tabControl1.TabPages.Count > 1)
                {
                    tabControl1.TabPages.RemoveAt(1);
                }
                if (controlMode == ControlMode.Save)
                {
                    grdBase.LoadData();
                }
                tabControl1.SelectedTab = tabPage1;
            }
        }

        #region :: Private Function ::
        private void AddEditTab(string TabTitle, AddEditSetupMaterial controlAddEdit)
        {
            if (tabControl1.TabPages.Count == 1 || (tabControl1.TabPages.Count > 1 && base.formBase.ShowConfirmMessage(GeneralMessage.ConfirmNewTab, "Confirm")))
            {
                controlAddEdit.NotifyReturnEvent += new Control.BaseUserControl.NotifyReturnHandler(NotifyReturnEvent);
                tabPageAddEdit = new TabPage();
                tabPageAddEdit.Controls.Clear();
                tabPageAddEdit.Controls.Add(controlAddEdit);
                tabPageAddEdit.Text = TabTitle;
                controlAddEdit.Dock = DockStyle.Fill;

                if (tabControl1.TabPages.Count > 1)
                {
                    tabControl1.TabPages.RemoveAt(1);
                }
                tabControl1.TabPages.Remove(tabPageAddEdit);
                tabControl1.TabPages.Add(tabPageAddEdit);
            }
            tabControl1.SelectedTab = tabPageAddEdit;
        }
        #endregion
    }
}
