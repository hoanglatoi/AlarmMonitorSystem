using Timer = System.Threading.Timer;

namespace AlarmMonitorSystem.Background
{
    public class TimedHostedService : IDisposable
    {
        private int executionCount = 0;
        private static readonly log4net.ILog _logger
               = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);
        private Timer _timer = null!;

        protected int _interval = 100;
        protected int _delay = 0;
        protected TIMER_UNIT _timerUnit = TIMER_UNIT.MILISECOND;

        public TimedHostedService(int interval, int delay, TIMER_UNIT timerUnit = TIMER_UNIT.MILISECOND)
        {
            _interval = interval;
            _delay = delay;
            _timerUnit = timerUnit;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.Info("Timed Hosted Service running.");

            //_timer = new Timer(DoWork, null, TimeSpan.Zero,
            //    TimeSpan.FromSeconds(5));
            switch (_timerUnit)
            {
                case TIMER_UNIT.MILISECOND:
                    _timer = new Timer(DoWork, null, new TimeSpan(0, 0, 0, 0, _delay), TimeSpan.FromMilliseconds(_interval));
                    break;
                case TIMER_UNIT.SECOND:
                    _timer = new Timer(DoWork, null, new TimeSpan(0, 0, 0, _delay, 0), TimeSpan.FromSeconds(_interval));
                    break;
                case TIMER_UNIT.MINUTE:
                    _timer = new Timer(DoWork, null, new TimeSpan(0, 0, _delay, 0, 0), TimeSpan.FromMinutes(_interval));
                    break;
                case TIMER_UNIT.HOUR:
                    _timer = new Timer(DoWork, null, new TimeSpan(0, _delay, 0, 0, 0), TimeSpan.FromHours(_interval));
                    break;
                case TIMER_UNIT.DAY:
                    _timer = new Timer(DoWork, null, new TimeSpan(_delay, 0, 0, 0, 0), TimeSpan.FromDays(_interval));
                    break;
            }

            return Task.CompletedTask;
        }

        public virtual void DoWork(object? state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.Info(
                "Timed Hosted Service is working. Count: " + count);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.Info("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
