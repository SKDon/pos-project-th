﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.BL.DTO.SU;
using POS.BL.Utilities;
using POS.BL.Service.SU;
using POS.BL;
using Core.Standards.Converters;
using POS.BL.Entities.Entity;
using POS.SU.SetupEmployee;
using POS.SO.MappingMenu;

namespace POS.SU
{
    public partial class SetupMaster : POS.Control.FormBase
    {
        List<ListMenuDTO> listMenuDTO = new List<ListMenuDTO>();

        public SetupMaster()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;

            var toolStrip = new ToolStrip();
            toolStrip.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(toolStrip);
            tableLayoutPanel1.SetCellPosition(toolStrip, new TableLayoutPanelCellPosition(0, 0));
            tableLayoutPanel1.SetColumnSpan(toolStrip, 3);

            //---Add list Menu
            //---Setting
            listMenuDTO.Add(new ListMenuDTO() { Module = ModuleName.Setting, MenuName = ProgramName.SetupEmployee });
            listMenuDTO.Add(new ListMenuDTO() { Module = ModuleName.Setting, MenuName = ProgramName.SetupRole });
            listMenuDTO.Add(new ListMenuDTO() { Module = ModuleName.Setting, MenuName = ProgramName.AssignEmployeeRole });
            listMenuDTO.Add(new ListMenuDTO() { Module = ModuleName.Setting, MenuName = ProgramName.SystemConfiguration });

            //---SO
            listMenuDTO.Add(new ListMenuDTO() { Module = ModuleName.Sold, MenuName = ProgramName.MappingMenu });

            //---IN
            listMenuDTO.Add(new ListMenuDTO() { Module = ModuleName.Inventory, MenuName = ProgramName.SetupBillOfMaterial });

            btnManage_Click(btnSetting, null);
            btnSetting.Focus();
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == "btnSetting")
            {
                listMenu.DataSource = listMenuDTO.Where(item => item.Module == ModuleName.Setting).Select(item => item.MenuName).ToList();
            }
            else if (btn.Name == "btnInventory")
            {
                listMenu.DataSource = listMenuDTO.Where(item => item.Module == ModuleName.Inventory).Select(item => item.MenuName).ToList();
            }
            else if (btn.Name == "btnSold")
            {
                listMenu.DataSource = listMenuDTO.Where(item => item.Module == ModuleName.Sold).Select(item => item.MenuName).ToList();
            }

            listMenu.ClearSelected();
            // toolStripGridview.Visible = false;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listMenu = sender as ListBox;

            string selected = listMenu.SelectedItem == null ? null : listMenu.SelectedItem.ToString();

            pnlContent.Controls.Clear();

            switch (selected)
            {
                case ProgramName.SetupEmployee: pnlContent.Controls.Add(new ucEmployee()); break;
                case ProgramName.SetupRole: break;
                case ProgramName.SystemConfiguration: break;
                case ProgramName.MappingMenu: pnlContent.Controls.Add(new ucMappingMenu()); break;
                case ProgramName.SetupBillOfMaterial: pnlContent.Controls.Add(new IN.BillOfMaterial.ucBillOfMaterial()); break;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
