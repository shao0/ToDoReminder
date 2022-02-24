using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDoReminder.Client.Common.DialogServices.Proxy
{
    public interface IDialogHostService : IDialogService
    {
        Task<IDialogResult> ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "RootDialog");

        void ShowEdgeWindow(string name, IDialogParameters parameters, Action<IDialogResult> callback, Location location = Location.RightBottom, string windowName = null);
    }
    public enum Location
    {
        RightBottom,
        RightTop,
        LeftBottom,
        LeftTop,
    }
}
