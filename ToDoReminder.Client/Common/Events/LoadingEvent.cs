using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoReminder.Client.Common.Events
{
    public class LoadingModel
    {
        public bool IsOpen { get; set; }
    }

    public class LoadingEvent:PubSubEvent<LoadingModel>
    {
    }
}
