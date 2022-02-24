using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ToDoReminder.Client.Common.Helpers.Proxy
{
    public interface ITimerHelper
    {
        event EventHandler Elapsed;

        void Start(int intervals = 1000);

        void Stop();

    }
}
