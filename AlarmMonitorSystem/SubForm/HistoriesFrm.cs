﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmMonitorSystem.SubForm
{
    public partial class HistoriesFrm : Form
    {
        public HistoriesFrm()
        {
            InitializeComponent();
        }

        private void HistoriesFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RouteFormManager.Instance.CloseForm();
        }
    }
}
