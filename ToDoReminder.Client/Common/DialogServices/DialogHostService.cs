using MaterialDesignThemes.Wpf;
using Prism.Common;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using ToDoReminder.Client.Common.DialogServices.Proxy;

namespace ToDoReminder.Client.Common.DialogServices
{
    /// <summary>
    /// 对话主机服务(自定义)
    /// </summary>
    public class DialogHostService : DialogService, IDialogHostService
    {
        private readonly IContainerExtension containerExtension;

        public DialogHostService(IContainerExtension containerExtension) : base(containerExtension)
        {
            this.containerExtension = containerExtension;
        }

        public async Task<IDialogResult> ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "RootDialog")
        {
            if (parameters == null)
                parameters = new DialogParameters();

            //从容器当中去除弹出窗口的实例
            var content = containerExtension.Resolve<object>(name);

            //验证实例的有效性 
            if (!(content is FrameworkElement dialogContent))
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");

            if (dialogContent is FrameworkElement view && view.DataContext is null && ViewModelLocator.GetAutoWireViewModel(view) is null)
                ViewModelLocator.SetAutoWireViewModel(view, true);

            if (!(dialogContent.DataContext is IDialogHostAware viewModel))
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");

            viewModel.DialogHostName = dialogHostName;

            DialogOpenedEventHandler eventHandler = (sender, eventArgs) =>
            {
                if (viewModel is IDialogHostAware aware)
                {
                    aware.OnDialogOpend(parameters);
                }
                eventArgs.Session.UpdateContent(content);
            };

            return (IDialogResult)await DialogHost.Show(dialogContent, viewModel.DialogHostName, eventHandler);
        }

        public void ShowEdgeWindow(string dialogName, IDialogParameters parameters, Action<IDialogResult> callback, Location location = Location.RightBottom, string windowName = null)
        {
            if (parameters == null)
                parameters = new DialogParameters();

            var dialog = Application.Current.Windows.OfType<IDialogWindow>().FirstOrDefault();
            if (dialog?.DataContext is IDialogAware vm)
            {
                vm.OnDialogOpened(parameters);
                return;
            }

            dialog = CreateDialogWindow(windowName);

            ConfigureDialogWindowEvents(dialog, callback);

            var contetnt = ConfigureDialogContent(dialogName, dialog, parameters);
            SetAnimaction(dialog, contetnt, location);

            dialog.Show();

        }


        protected FrameworkElement ConfigureDialogContent(string dialogName, IDialogWindow window, IDialogParameters parameters)
        {
            var content = containerExtension.Resolve<object>(dialogName);
            if (!(content is FrameworkElement dialogContent))
            {
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");
            }

            if (dialogContent is FrameworkElement view && view.DataContext is null && ViewModelLocator.GetAutoWireViewModel(view) is null)
                ViewModelLocator.SetAutoWireViewModel(view, true);

            if (!(dialogContent.DataContext is IDialogAware viewModel))
            {
                throw new NullReferenceException($"A dialog's ViewModel must implement the IDialogAware interface ({dialogContent.DataContext})");
            }

            var windowStyle = Dialog.GetWindowStyle(dialogContent);
            if (windowStyle != null)
                window.Style = windowStyle;

            window.Content = dialogContent;
            window.DataContext = viewModel; //we want the host window and the dialog to share the same data context

            MvvmHelpers.ViewAndViewModelAction<IDialogAware>(viewModel, d => d.OnDialogOpened(parameters));
            return dialogContent;
        }

        void SetAnimaction(IDialogWindow window, FrameworkElement element, Location location = Location.RightBottom)
        {
            if (window is Window w)
            {
                var i = 20;
                var loadedAnimation = new DoubleAnimationUsingKeyFrames();
                var timeSpan = TimeSpan.FromMilliseconds(1000);
                loadedAnimation.KeyFrames = new DoubleKeyFrameCollection { new LinearDoubleKeyFrame(0, timeSpan) };
                var translateTransform = new TranslateTransform();
                element.RenderTransform = translateTransform;
                var closingAnimation = new DoubleAnimation();
                closingAnimation.Duration = timeSpan;
                switch (location)
                {
                    case Location.LeftTop:
                        w.Left = i;
                        w.Top = 0;
                        closingAnimation.To= translateTransform.Y = -element.Height;
                        break;
                    case Location.LeftBottom:
                        w.Left = i;
                        w.Top = SystemParameters.WorkArea.Height - element.Height;
                        closingAnimation.To = translateTransform.Y = element.Height;
                        break;
                    case Location.RightTop:
                        w.Left = SystemParameters.WorkArea.Width - element.Width - i;
                        w.Top = 0;
                        closingAnimation.To = translateTransform.Y = -element.Height;
                        break;
                    case Location.RightBottom:
                    default:
                        w.Top = SystemParameters.WorkArea.Height - element.Height;
                        w.Left = SystemParameters.WorkArea.Width - element.Width - i;
                        closingAnimation.To = translateTransform.Y = element.Height;
                        break;
                }
                translateTransform.BeginAnimation(TranslateTransform.YProperty, loadedAnimation);
                w.Closing += closing;
                void closing(object sender, CancelEventArgs cancel)
                {
                    w.Closing -= closing;
                    cancel.Cancel = true;
                    closingAnimation.Completed += ClosingAnimation_Completed;
                    translateTransform.BeginAnimation(TranslateTransform.YProperty, closingAnimation);
                }
                void ClosingAnimation_Completed(object s, EventArgs e)
                {
                    closingAnimation.Completed -= ClosingAnimation_Completed;
                    w.Close();
                }
            }
        }

    }
}
