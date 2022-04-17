namespace AlarmMonitorSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Startup startup = new Startup();
            startup.TestScheduleTimer();
        }
    }
}