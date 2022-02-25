using AutoMapper;
using DryIoc;
using MaterialDesignThemes.Wpf;
using Prism.DryIoc;
using Prism.Ioc;
using System.Windows;
using ToDoReminder.Client.Common;
using ToDoReminder.Client.Common.DialogServices;
using ToDoReminder.Client.Common.DialogServices.Proxy;
using ToDoReminder.Client.Common.Extensions;
using ToDoReminder.Client.Common.Helpers;
using ToDoReminder.Client.Common.Helpers.Proxy;
using ToDoReminder.Client.Extensions;
using ToDoReminder.Client.Services;
using ToDoReminder.Client.Services.Http;
using ToDoReminder.Client.Services.Proxy;
using ToDoReminder.Client.Views;
using ToDoReminder.Client.Views.Dialogs;
using ToDoReminder.Client.Views.Settings;
using ToDoReminder.Client.Views.Statistics;

namespace ToDoReminder.Client
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<ShellView>();
        }

        protected override void OnInitialized()
        {
            Global.Initial(Container.Resolve<PaletteHelper>(), Container.Resolve<ITimerHelper>());
            base.OnInitialized();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<IndexView>();
            containerRegistry.RegisterForNavigation<MemoView>();
            containerRegistry.RegisterForNavigation<ToDoReminderView>();

            containerRegistry.RegisterForNavigation<StatisticView>();
            containerRegistry.RegisterForNavigation<CompletionRateView>();
            containerRegistry.RegisterForNavigation<MonthlyToDoReminderColumnView>();

            containerRegistry.RegisterForNavigation<SettingView>();
            containerRegistry.RegisterForNavigation<SkinSettingView>();
            containerRegistry.RegisterForNavigation<SystemSettingView>();

            containerRegistry.RegisterForNavigation<AddDialogView>();
            containerRegistry.RegisterDialog<ReminderView>();



            containerRegistry.Register<IUserService, UserService>();
            containerRegistry.Register<IMemoService, MemoService>();
            containerRegistry.Register<IToDoReminderService, ToDoReminderService>();
            containerRegistry.Register<IDialogHostService, DialogHostService>();
            containerRegistry.Register<IStatisticService, StatisticService>();

            containerRegistry.RegisterSingleton<ILog, LogHelper>();
            containerRegistry.RegisterSingleton<ITimerHelper, TimerHelper>();
            containerRegistry.RegisterSingleton<PaletteHelper>();
            const string apiUrl = "ApiUrl";
            containerRegistry.GetContainer().Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: apiUrl));
            containerRegistry.GetContainer().RegisterInstance(apiUrl.GetConnectionStringsConfig(), serviceKey: apiUrl);
            containerRegistry.RegisterInstance(new MapperConfiguration(new AutoMapperProFile()).CreateMapper());


        }
    }
}
