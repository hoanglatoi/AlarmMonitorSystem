using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Threading.Timer;

namespace AlarmMonitorSystem.Background
{
    internal class ScheduleTimer2
    {
        private static readonly log4net.ILog _logger
               = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);
        private Timer _timer = null!;

        private int _interval = 100;
        private TimeSpan _delay;
        private TIMER_UNIT _timerUnit = TIMER_UNIT.MILISECOND;
        Action _task = null!;

        public ScheduleTimer2(Action task, int interval, TimeSpan delay, TIMER_UNIT timerUnit = TIMER_UNIT.MILISECOND)
        {
            _interval = interval;
            _delay = delay;
            _timerUnit = timerUnit;
            _task = task;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.Info("Timed Hosted Service running.");

            switch (_timerUnit)
            {
                case TIMER_UNIT.MILISECOND:
                    _timer = new Timer(x =>
                    {
                        _task.Invoke();
                    }, null, _delay, TimeSpan.FromMilliseconds(_interval));
                    break;
                case TIMER_UNIT.SECOND:
                    _timer = new Timer(x =>
                    {
                        _task.Invoke();
                    }, null, _delay, TimeSpan.FromMilliseconds(_interval));
                    break;
                case TIMER_UNIT.MINUTE:
                    _timer = new Timer(x =>
                    {
                        _task.Invoke();
                    }, null, _delay, TimeSpan.FromMilliseconds(_interval));
                    break;
                case TIMER_UNIT.HOUR:
                    _timer = new Timer(x =>
                    {
                        _task.Invoke();
                    }, null, _delay, TimeSpan.FromMilliseconds(_interval));
                    break;
                case TIMER_UNIT.DAY:
                    _timer = new Timer(x =>
                    {
                        _task.Invoke();
                    }, null, _delay, TimeSpan.FromMilliseconds(_interval));
                    break;
            }

            return Task.CompletedTask;
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
