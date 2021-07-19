using Prism;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlProject.CoreLibrary.Mvvm
{
    public class ActiveAwareViewModelBase : RegionViewModelBase, IActiveAware
    {
        #region IActiveAware
        public event EventHandler? IsActiveChanged;

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (!SetProperty(ref _isActive, value)) return;
                OnIsActiveChanged();
            }
        } 
        #endregion
        public ActiveAwareViewModelBase(IRegionManager regionManager) : base(regionManager)
        {
        }

        public virtual void OnIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, new EventArgs());
        }
    }
}
