﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlarmMonitorSystem.SubForm;

namespace AlarmMonitorSystem
{
    public partial class MainFrm : Form
    {
        Startup startup;
        public MainFrm()
        {
            InitializeComponent();          
            RouteFormManager.Instance.MainFrm = this;
            RouteFormManager.Instance.ChangeFrom(RouteFormManager.Instance.SumaryFrm);
            startup = new Startup();
            startup.TestScheduleTimer();
        }

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            startup.Dispose();
        }
    }
}
