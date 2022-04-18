using System;
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
    public partial class SumaryFrm : Form
    {
        public SumaryFrm()
        {
            InitializeComponent();
            
        }

        private void SumaryFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RouteFormManager.Instance.CloseForm();
        }

        public void ChangeView(int alarmID)
        {
            if(IsHandleCreated == false) return;
            label1.Invoke(new Action(() =>
            {
                label1.Text = "Alarm: " + alarmID.ToString();
                label1.BackColor = Color.Red;
            }));
        }
    }
}
