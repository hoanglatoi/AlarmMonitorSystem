using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmMonitorSystem.SubForm
{
    public class RouteFormManager
    {
        private static RouteFormManager instance = null!;
        private static object _lock = new object();

        private Form currentForm = null!;

        public MainFrm MainFrm { get; set; }

        private SumaryFrm sumaryFrm = new SumaryFrm();
        private HistoriesFrm historyFrm = new HistoriesFrm();
        private SettingsFrm settingsFrm = new SettingsFrm();

        private RouteFormManager()
        {
        }
        public static RouteFormManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new RouteFormManager();
                        }
                    }
                }
                return instance;
            }
        }

        public Form CurrentForm { get => currentForm; set => currentForm = value; }
        public SumaryFrm SumaryFrm { get => sumaryFrm; set => sumaryFrm = value; }
        public HistoriesFrm HistoryFrm { get => historyFrm; set => historyFrm = value; }
        public SettingsFrm SettingsFrm { get => settingsFrm; set => settingsFrm = value; }

        public void ChangeFrom(Form desFrom)
        {
            if(desFrom != null && desFrom != currentForm)
            {                               
                desFrom.Show(MainFrm);
                if (currentForm != null) CurrentForm.Hide();
                currentForm = desFrom;

            }
        }

        public void CloseForm()
        {
            Application.Exit();
        }
    }
}
