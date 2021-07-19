using Prism.Regions;
using System;

namespace SqlProject.CoreLibrary.Mvvm
{
    public class RegionViewModelBase : DestructibleBase, INavigationAware, IConfirmNavigationRequest
    {
        public IRegionManager RegionManager { get; }

        public RegionViewModelBase(IRegionManager regionManager)
        {
            RegionManager = regionManager;
        }

        #region Methods
        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback.Invoke(true);
        }

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
        #endregion
    }
}
