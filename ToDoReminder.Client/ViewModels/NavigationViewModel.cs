using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;

namespace ToDoReminder.Client.ViewModels
{
    public class NavigationViewModel : BindableBase, INavigationAware
    {

        #region LoadedCommand 加载命令
        /// <summary>
        /// 加载命令
        /// </summary>
        public ICommand LoadedCommand => new DelegateCommand(Loaded);

        protected virtual void Loaded() { }
        #endregion

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}
