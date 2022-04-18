using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlarmMonitorSystem.SubForm;

namespace AlarmMonitorSystem.Controls
{
    public partial class SwitchMenu : UserControl
    {
        public SwitchMenu()
        {
            InitializeComponent();
        }

        private void sumaryBtn_Click(object sender, EventArgs e)
        {
            Form parentForm = (Form)this.Parent;
            if(parentForm != RouteFormManager.Instance.SumaryFrm)
            {
                RouteFormManager.Instance.ChangeFrom(RouteFormManager.Instance.SumaryFrm);
            }
        }

        private void HistoryBtn_Click(object sender, EventArgs e)
        {
            Form parentForm = (Form)this.Parent;
            if (parentForm != RouteFormManager.Instance.HistoryFrm)
            {
                RouteFormManager.Instance.ChangeFrom(RouteFormManager.Instance.HistoryFrm);
            }
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            Form parentForm = (Form)this.Parent;
            if (parentForm != RouteFormManager.Instance.SettingsFrm)
            {
                RouteFormManager.Instance.ChangeFrom(RouteFormManager.Instance.SettingsFrm);
            }
        }
    }
}
