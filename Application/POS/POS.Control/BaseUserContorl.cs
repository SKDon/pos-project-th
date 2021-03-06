﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POS.Control
{
    public partial class BaseUserControl : UserControl
    {
        public delegate void NotifyReturnHandler(object param);
        public event NotifyReturnHandler NotifyReturnEvent;
        public FormBase formBase { get { return this.FindForm() as FormBase; } }
        public BaseUserControl()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
        protected void onNotifyReturnEvent(object param)
        {
            if (NotifyReturnEvent != null)
            {
                NotifyReturnEvent(param);
            }
        }
    }
}
