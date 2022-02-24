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

        DispatcherTimer timer;

        public event EventHandler Elapsed;

        private void Timer_Tick(object sender, EventArgs e)
        {
            Elapsed?.Invoke(this, e);
        }

        public void Start(int intervals = 1000)
        {
            if (timer == null)
            {
                timer = new DispatcherTimer();
            }
            timer.Interval = TimeSpan.FromMilliseconds(intervals);
            timer.Tick -= Timer_Tick;
            timer.Tick += Timer_Tick;
            timer.Start();
        }


        public void Stop()
        {
            if (timer != null)
            {
                timer.Tick -= Timer_Tick;
                timer.Stop();
                timer = null;
            }
        }

    }
}
