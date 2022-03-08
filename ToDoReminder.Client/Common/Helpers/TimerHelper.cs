using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;
using ToDoReminder.Client.Common.Helpers.Proxy;

namespace ToDoReminder.Client.Common.Helpers
{
    public class TimerHelper : ITimerHelper
    {

        private DispatcherTimer _timer;

        public event EventHandler Elapsed;

        private void Timer_Tick(object sender, EventArgs e)
        {
            Elapsed?.Invoke(this, e);
        }

        public void Start(int intervals = 1000)
        {
            if (_timer == null)
            {
                _timer = new DispatcherTimer();
            }
            _timer.Interval = TimeSpan.FromMilliseconds(intervals);
            _timer.Tick -= Timer_Tick;
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }


        public void Stop()
        {
            if (_timer == null) return;
            _timer.Tick -= Timer_Tick;
            _timer.Stop();
            _timer = null;
        }

    }
}
