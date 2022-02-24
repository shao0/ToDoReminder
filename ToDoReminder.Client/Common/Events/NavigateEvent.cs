using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoReminder.Client.Common.Events
{
    public class NavigateEvent: PubSubEvent<NavigationParameters>
    {
    }
}
